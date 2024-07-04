package com.apitel_bank.main_banking_service.routers;

import com.apitel_bank.main_banking_service.models.*;
import com.apitel_bank.main_banking_service.repositories.*;
import com.apitel_bank.main_banking_service.singletons.GameState;
import org.apache.camel.Exchange;
import org.apache.camel.builder.RouteBuilder;
import org.apache.camel.model.dataformat.JsonLibrary;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class DebitOrderPaymentQueueingRouter extends RouteBuilder {

    @Autowired
    private DebitOrdersRepository debitOrdersRepository;

    @Autowired
    private ExternalAccountsRepository externalAccountsRepository;

    @Autowired
    private DebitOrderRecipientsRepository debitOrderRecipientsRepository;

    @Autowired
    private AccountsRespository accountsRespository;

    @Autowired
    private CustomersRepository customersRepository;

    @Autowired
    private GameState gameState;

    @Value( "${apitel.apitel-make-payment-url}" )
    private String apitelMakePaymentUrl;

    @Override
    public void configure() throws Exception {
        from("timer://cronDebitOrders?period=120000")
                .process(exchange -> {
                    int currentDay = -1;

                    try {
                        currentDay = gameState.getCurrentGameTime().getDayOfMonth();
                    } catch(Exception e) {
                        exchange.getIn().setBody(new ArrayList<ApitelPaymentRequest>());
                    }

                    final int finalCurrentDay = currentDay;
                    if(currentDay > 0) {
                        final List<ApitelPaymentRequest> requests = debitOrdersRepository.findAll()
                                .stream().filter(debitOrder -> debitOrder.getDayInTheMonth() == finalCurrentDay)

                                .map(dueDebitOrder -> {
                                    ExternalAccounts recipient = getDebitOrderRecipient(dueDebitOrder.getDebitOrderRecipientId());

                                    int customerIdNumber = getCustomerIdNumberByAccountId(dueDebitOrder.getAccountId());
                                    return new ApitelPaymentRequest(
                                            customerIdNumber,
                                            dueDebitOrder.getAmountInMibiBBDough(),
                                            String.format("retail-bank-do-%d-%d", customerIdNumber, dueDebitOrder.getDebitOrderId()),
                                            new ApitelPaymentRequest.Recipient(
                                                    recipient.getBankId(),
                                                    recipient.getExternalCustomerAccountId()
                                            )
                                    );
                                }).toList();

                        System.out.println(requests);
                        exchange.getIn().setBody(requests);
                    }
                })
                .split(body())
                .marshal().json(JsonLibrary.Gson)
                .to("aws2-sqs://sqs-payment-requests?amazonSQSClient=#amazonSQSClient");

        from("aws2-sqs://sqs-payment-requests?amazonSQSClient=#amazonSQSClient")
                .setHeader("X-PartnerId", constant("retail-bank"))
                .setHeader(Exchange.HTTP_METHOD, constant("POST"))
                .setHeader(Exchange.CONTENT_TYPE, constant("application/json"))
                .to(apitelMakePaymentUrl);
    }

    private ExternalAccounts getDebitOrderRecipient(int debitOrderRecipientId) {
        DebitOrderRecipients recipient = debitOrderRecipientsRepository.findById(debitOrderRecipientId).get();
        return externalAccountsRepository.findById(recipient.getExternalAccountId()).get();
    }

    private int getCustomerIdNumberByAccountId(int accountId) {
        Accounts account = accountsRespository.findById(accountId).get();
        return customersRepository.findById(account.getCustomerId()).get().getBBDoughId();
    }
}

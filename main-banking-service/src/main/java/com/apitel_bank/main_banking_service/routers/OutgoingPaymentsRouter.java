package com.apitel_bank.main_banking_service.routers;

import com.apitel_bank.main_banking_service.models.CommercialBankDepositRequest;
import com.apitel_bank.main_banking_service.models.OutgoingPayment;
import org.apache.camel.Exchange;
import org.apache.camel.builder.RouteBuilder;
import org.apache.camel.model.dataformat.JsonLibrary;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.util.ArrayList;
import java.util.List;

@Component
public class OutgoingPaymentsRouter extends RouteBuilder {
    @Value( "${apitel.commercial-bank-deposits-url}" )
    private String commercialBankDepositsUrl;

    @Value( "${apitel.apitel-bank-deposits-url}" )
    private String retailBankDepositsUrl;

    @Override
    public void configure() throws Exception {
        from("aws2-sqs://sqs-outgoing-payments?amazonSQSClient=#amazonSQSClient")
                .log("Received message from SQS: ${body}")
                .unmarshal().json(JsonLibrary.Gson, OutgoingPayment.class)
                .to("direct:processOutgoingPayment");

        from("direct:processOutgoingPayment")
                .process(exchange -> {
                    final OutgoingPayment outgoingPayment = exchange.getIn().getBody(OutgoingPayment.class);

                    final List<CommercialBankDepositRequest.Deposit> deposits = new ArrayList<>();
                    deposits.add(new CommercialBankDepositRequest.Deposit(
                            Integer.toString(outgoingPayment.getPersonaId()),
                            outgoingPayment.getReceivingAccountId(),
                            outgoingPayment.getAmountInMibiBBDough(),
                            outgoingPayment.getReference(),
                            outgoingPayment.getReference()
                        ));
                    exchange.getIn().setBody(new CommercialBankDepositRequest(deposits));
                })
                .marshal().json(JsonLibrary.Gson)
                .setHeader("X-Origin", constant("retail_bank"))
                .setHeader(Exchange.HTTP_METHOD, constant("POST"))
                .setHeader(Exchange.CONTENT_TYPE, constant("application/json"))
                .to(commercialBankDepositsUrl);
    }
}

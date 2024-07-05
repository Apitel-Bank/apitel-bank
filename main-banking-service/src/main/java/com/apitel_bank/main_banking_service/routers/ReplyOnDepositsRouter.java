package com.apitel_bank.main_banking_service.routers;

import com.apitel_bank.main_banking_service.models.CommercialBankDepositReplyRequest;
import org.apache.camel.Exchange;
import org.apache.camel.builder.RouteBuilder;
import org.apache.camel.model.dataformat.JsonLibrary;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

@Component
public class ReplyOnDepositsRouter extends RouteBuilder {

    @Value( "${apitel.commercial-bank-deposits-reply-url}" )
    private String commercialBankDepositsReplyUrl;


    @Override
    public void configure() throws Exception {
        from("aws2-sqs://sqs-deposits-pending-verification?amazonSQSClient=#amazonSQSClient")
                .process(exchange -> {
                    final String reference = exchange.getIn().getBody(String.class);
                    exchange.getIn().setBody(new CommercialBankDepositReplyRequest(reference, "success"));
                })
                .marshal().json(JsonLibrary.Gson)
                .setHeader("X-Origin", constant("retail_bank"))
                .setHeader(Exchange.HTTP_METHOD, constant("POST"))
                .setHeader(Exchange.CONTENT_TYPE, constant("application/json"))
                .to(commercialBankDepositsReplyUrl)
                .onException(Exception.class)
                .handled(true)
                .log(body().toString());
    }
}

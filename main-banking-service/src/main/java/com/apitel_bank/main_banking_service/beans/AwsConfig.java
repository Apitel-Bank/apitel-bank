package com.apitel_bank.main_banking_service.beans;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import software.amazon.awssdk.auth.credentials.AwsBasicCredentials;
import software.amazon.awssdk.auth.credentials.StaticCredentialsProvider;
import software.amazon.awssdk.services.sqs.SqsClient;
import software.amazon.awssdk.regions.Region;

import java.net.URI;

@Configuration
public class AwsConfig {
    @Value( "${camel.component.aws2-sqs.region}" )
    private String sqsRegion;

    @Value( "${apitel.localstack-url:null}" )
    private String localStackUrl;

    @Value( "${camel.component.aws2-sqs.access-key}" )
    private String sqsAccessKey;

    @Value( "${camel.component.aws2-sqs.secret-key}" )
    private String sqsSecretKey;

    @Bean
    public SqsClient amazonSQSClient() {
        try {
            return SqsClient.builder()
                    .endpointOverride(URI.create(localStackUrl))
                    .credentialsProvider(StaticCredentialsProvider.create(
                            AwsBasicCredentials.create(sqsAccessKey, sqsSecretKey)
                    ))
                    .region(Region.of(sqsRegion))
                    .build();
        } catch(Exception e) {
            return SqsClient.builder()
                    .credentialsProvider(StaticCredentialsProvider.create(
                            AwsBasicCredentials.create(sqsAccessKey, sqsSecretKey)
                    ))
                    .region(Region.of(sqsRegion))
                    .build();
        }
    }
}

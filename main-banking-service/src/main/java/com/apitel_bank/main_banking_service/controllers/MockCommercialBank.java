package com.apitel_bank.main_banking_service.controllers;

import com.apitel_bank.main_banking_service.models.CommercialBankDepositRequest;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping
public class MockCommercialBank {
    @PostMapping("/mock-commercial-deposit")
    public void onDeposit(@RequestBody CommercialBankDepositRequest request) {
        System.out.println("Mock Commercial Bank Deposit request received: " + request.toString());
    }
}

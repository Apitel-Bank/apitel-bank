package com.apitel_bank.reporting_service.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.reporting_service.models.AccountTransactions;
import com.apitel_bank.reporting_service.repositories.AccountsTransactionsRepository;

@RestController
@RequestMapping("/accountTransactions")
public class AccountTransactionsController {
    
    @Autowired
    private AccountsTransactionsRepository transactionsRepo;

    @GetMapping
    public ResponseEntity<List<AccountTransactions>> getAllTransactions() {
        List<AccountTransactions> transactions = transactionsRepo.findAll(); 

        return new ResponseEntity<List<AccountTransactions>>(transactions, HttpStatus.OK);
    }
}

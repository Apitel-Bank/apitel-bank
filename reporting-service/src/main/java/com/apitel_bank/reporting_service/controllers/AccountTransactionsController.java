package com.apitel_bank.reporting_service.controllers;

import java.time.LocalDate;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.reporting_service.models.AccountTransactions;
import com.apitel_bank.reporting_service.repositories.AccountsTransactionsRepository;

@RestController
@CrossOrigin(origins = "http://localhost:3000")
@RequestMapping("/accountTransactions")
public class AccountTransactionsController {

    @Autowired
    private AccountsTransactionsRepository transactionsRepo;

    @GetMapping
    public ResponseEntity<List<AccountTransactions>> getFilteredTransactions(
            @RequestParam(required = false) String accountId,
            @RequestParam(required = false) Integer dateRange) {

        List<AccountTransactions> transactions;

        transactions = transactionsRepo.findAll();

        return new ResponseEntity<>(transactions, HttpStatus.OK);
    }
}
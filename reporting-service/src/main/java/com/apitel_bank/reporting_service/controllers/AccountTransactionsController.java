package com.apitel_bank.reporting_service.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
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
@RequestMapping("/accountTransactions")
public class AccountTransactionsController {

    @Autowired
    private AccountsTransactionsRepository transactionsRepo;

    // Endpoint to fetch filtered transactions with pagination
    @GetMapping
    public ResponseEntity<Page<AccountTransactions>> getFilteredTransactions(
            @RequestParam(required = false) Integer accountId,
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "50") int size) {

        Pageable pageable = PageRequest.of(page, size);

        Page<AccountTransactions> accountTransactions;
        if (accountId != null) {
            accountTransactions = transactionsRepo.findByAccountId(accountId, pageable);
        } else {
            accountTransactions = transactionsRepo.findAll(pageable);
        }

        return new ResponseEntity<>(accountTransactions, HttpStatus.OK);
    }

    // Endpoint to get count of all account transactions
    @GetMapping("/count")
    public ResponseEntity<Long> getCountOfAllAccountTransactions() {
        long count = transactionsRepo.count();
        return ResponseEntity.ok(count);
    }
}

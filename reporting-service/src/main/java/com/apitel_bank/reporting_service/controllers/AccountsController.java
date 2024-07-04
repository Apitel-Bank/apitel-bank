package com.apitel_bank.reporting_service.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.PageRequest;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import com.apitel_bank.reporting_service.models.Accounts;
import com.apitel_bank.reporting_service.repositories.AccountsRespository;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
@RestController
@RequestMapping("/accounts")
public class AccountsController {

    @Autowired
    private AccountsRespository accountsRepo;

    // Endpoint to get all accounts with pagination
    @GetMapping
    public ResponseEntity<Page<Accounts>> getAllAccounts(
            @RequestParam(defaultValue = "0") int page,
            @RequestParam(defaultValue = "10") int size,
            @RequestParam(required = false) Long accountId,
            @RequestParam(required = false) Long customerId) {

        Pageable paging = PageRequest.of(page, size);
        Page<Accounts> accountsPage;

        if (accountId != null || customerId != null) {
            accountsPage = accountsRepo.findByAccountIdAndCustomerId(accountId, customerId, paging);
        } else {
            accountsPage = accountsRepo.findAll(paging);
        }

        return new ResponseEntity<>(accountsPage, HttpStatus.OK);
    }

    @GetMapping("/count")
    public ResponseEntity<Long> getCountOfAllAccounts() {
        long count = accountsRepo.count();
        return ResponseEntity.ok(count);
    }
}

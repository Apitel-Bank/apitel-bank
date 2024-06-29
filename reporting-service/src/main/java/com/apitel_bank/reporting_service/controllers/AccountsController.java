package com.apitel_bank.reporting_service.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.reporting_service.models.Accounts;
import com.apitel_bank.reporting_service.repositories.AccountsRespository;

@RestController
@RequestMapping("/accounts")
public class AccountsController {

    @Autowired
    private AccountsRespository accountsRepo;

    @GetMapping
    public ResponseEntity<List<Accounts>> getAllAccounts() {
        List<Accounts> accounts = accountsRepo.findAll();

        return new ResponseEntity<List<Accounts>>(accounts, HttpStatus.OK);
    }
   
}

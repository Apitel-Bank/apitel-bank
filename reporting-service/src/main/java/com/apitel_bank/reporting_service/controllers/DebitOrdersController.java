package com.apitel_bank.reporting_service.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.reporting_service.models.DebitOrders;
import com.apitel_bank.reporting_service.repositories.DebitOrdersRepository;

@RestController
@RequestMapping("/debitOrders")
public class DebitOrdersController {

    @Autowired
    private DebitOrdersRepository debitOrdersRepo;

    @GetMapping
    public ResponseEntity<List<DebitOrders>> getAllDebitOrders() {
        List<DebitOrders> debitOrders = debitOrdersRepo.findAll();

        return new ResponseEntity<List<DebitOrders>>(debitOrders, HttpStatus.OK);
    }

}

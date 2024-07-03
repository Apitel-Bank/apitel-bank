package com.apitel_bank.main_banking_service.controllers;

import java.util.Map;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("")
public class AppController {

    @GetMapping("")
    public ResponseEntity<Map<String, String>> isOnline() {
        return new ResponseEntity<>(Map.of("message", "apitel main backing service"), HttpStatus.OK);
    }

    @GetMapping("/test")
    public ResponseEntity<Map<String, String>> test() {
        return new ResponseEntity<>(Map.of("message", "Deployed"), HttpStatus.OK);
    }
}

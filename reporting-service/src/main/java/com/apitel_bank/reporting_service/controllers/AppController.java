package com.apitel_bank.reporting_service.controllers;

import java.util.Map;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/")
@EnableWebSecurity
public class AppController {

    @GetMapping("/isOnline")
    public ResponseEntity<Map<String, Boolean>> isOnline() {
        return new ResponseEntity<>(Map.of("isOnline", true), HttpStatus.OK);
    }

}

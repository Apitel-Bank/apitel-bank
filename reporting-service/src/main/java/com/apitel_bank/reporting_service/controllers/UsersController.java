package com.apitel_bank.reporting_service.controllers;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.reporting_service.models.Users;
import com.apitel_bank.reporting_service.repositories.UsersRepository;

@RestController
@RequestMapping("/users")
public class UsersController {

    @Autowired
    private UsersRepository userRepo;
    
    @GetMapping
    public ResponseEntity<List<Users>> getAllUsers() {
        List<Users> users = userRepo.findAll();

        return new ResponseEntity<List<Users>>(users, HttpStatus.OK);
    }
}

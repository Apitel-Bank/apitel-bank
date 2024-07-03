package com.apitel_bank.main_banking_service.controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.apitel_bank.main_banking_service.models.Users;
import com.apitel_bank.main_banking_service.repositories.AccountsRespository;
import com.apitel_bank.main_banking_service.repositories.AccountsTransactionsRepository;
import com.apitel_bank.main_banking_service.repositories.UsersRepository;
import com.apitel_bank.main_banking_service.singletons.GameState;

import org.springframework.web.bind.annotation.GetMapping;

import java.time.Instant;
import java.time.ZonedDateTime;

@RestController
@RequestMapping("/simulation")
public class GameController {
    @Autowired
    private GameState gameState;

    @PostMapping("/control")
    public String controlGame(@RequestBody GameControlRequest request) {
        if ("start".equalsIgnoreCase(request.getAction())) {
            gameState.startGame(Instant.parse(request.getStartTime()));
            return "Game started";
        } else if ("reset".equalsIgnoreCase(request.getAction())) {
            gameState.resetGame(Instant.parse(request.getStartTime()));
            //TODO: Clear everything in the database
            return "Game reset";
        } else {
            return "Invalid action";
        }
    }

    @GetMapping("/time")
    public ZonedDateTime getCurrentGameTime() {
        return gameState.getCurrentGameTime();
    }
}

class GameControlRequest {
    private String action;
    private String startTime;

    public String getAction() {
        return action;
    }

    public void setAction(String action) {
        this.action = action;
    }

    public String getStartTime() {
        return startTime;
    }

    public void setStartTime(String startTime) {
        this.startTime = startTime;
    }
}

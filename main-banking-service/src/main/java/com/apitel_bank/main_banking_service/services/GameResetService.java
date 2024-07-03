package com.apitel_bank.main_banking_service.services;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
public class GameResetService {

    @Autowired
    private JdbcTemplate jdbcTemplate;

    @Transactional
    public void resetGame() {
        jdbcTemplate.execute("DELETE FROM AccountTransactionRejectionReasons");
        jdbcTemplate.execute("DELETE FROM AccountTransactionStatusProgressions");
        jdbcTemplate.execute("DELETE FROM DebitOrderTransactions");
        jdbcTemplate.execute("DELETE FROM DebitOrders");
        jdbcTemplate.execute("DELETE FROM AccountTransactions");
        jdbcTemplate.execute("DELETE FROM Accounts");
        jdbcTemplate.execute("DELETE FROM DebitOrderRecipients");
        jdbcTemplate.execute("DELETE FROM Customers");
        jdbcTemplate.execute("DELETE FROM ExternalAccounts");
        jdbcTemplate.execute("DELETE FROM Users");
    }
}

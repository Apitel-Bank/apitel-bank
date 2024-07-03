package com.apitel_bank.reporting_service.repositories;

import java.time.LocalDate;
import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

import com.apitel_bank.reporting_service.models.AccountTransactions;

public interface AccountsTransactionsRepository extends JpaRepository<AccountTransactions, Integer> {
    List<AccountTransactions> findByAccountIdAndTransactionDateBetween(String accountId, LocalDate startDate,
            LocalDate endDate);
}

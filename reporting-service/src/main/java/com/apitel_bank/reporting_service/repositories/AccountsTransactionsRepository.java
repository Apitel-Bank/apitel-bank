package com.apitel_bank.reporting_service.repositories;

import org.springframework.data.jpa.repository.JpaRepository;

import com.apitel_bank.reporting_service.models.AccountTransactions;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

public interface AccountsTransactionsRepository extends JpaRepository<AccountTransactions, Integer> {
    Page<AccountTransactions> findByAccountId(Integer accountId, Pageable pageable);
}

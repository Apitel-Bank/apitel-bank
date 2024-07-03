package com.apitel_bank.reporting_service.repositories;


import org.springframework.data.jpa.repository.JpaRepository;

import com.apitel_bank.reporting_service.models.AccountTransactions;

public interface AccountsTransactionsRepository extends JpaRepository<AccountTransactions, Integer> {

}

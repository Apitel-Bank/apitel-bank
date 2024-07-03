package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.AccountTransactions;
import org.springframework.data.jpa.repository.JpaRepository;

public interface AccountsTransactionsRepository extends JpaRepository<AccountTransactions, Integer> {

}

package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.ExternalAccounts;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ExternalAccountsRepository extends JpaRepository<ExternalAccounts, Integer> {
}

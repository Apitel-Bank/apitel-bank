package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.Accounts;
import org.springframework.data.jpa.repository.JpaRepository;



public interface AccountsRespository extends JpaRepository<Accounts, Integer>{
    
}

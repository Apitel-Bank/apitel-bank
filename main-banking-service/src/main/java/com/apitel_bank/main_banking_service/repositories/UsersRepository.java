package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.Users;
import org.springframework.data.jpa.repository.JpaRepository;


public interface UsersRepository extends JpaRepository<Users, Integer>{
    
}

package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.Customers;
import org.springframework.data.jpa.repository.JpaRepository;

public interface CustomersRepository extends JpaRepository<Customers, Integer> {
}

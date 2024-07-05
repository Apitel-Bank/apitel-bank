package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.DebitOrders;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DebitOrdersRepository extends JpaRepository<DebitOrders, Integer> {
}

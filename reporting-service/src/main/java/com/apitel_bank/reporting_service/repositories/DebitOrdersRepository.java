package com.apitel_bank.reporting_service.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import com.apitel_bank.reporting_service.models.DebitOrders;

public interface DebitOrdersRepository extends JpaRepository<DebitOrders, Integer> {

}

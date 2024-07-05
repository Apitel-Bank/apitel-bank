package com.apitel_bank.main_banking_service.repositories;

import com.apitel_bank.main_banking_service.models.DebitOrderRecipients;
import org.springframework.data.jpa.repository.JpaRepository;

public interface DebitOrderRecipientsRepository extends JpaRepository<DebitOrderRecipients, Integer> {
}

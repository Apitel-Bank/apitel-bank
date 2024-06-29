package com.apitel_bank.reporting_service.models;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "AccountTransactionRejectionReasons")
public class AccountTransactionRejectionReasons {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int AccountTransactionRejectionReasonId;
    private int AccountTransactionStatusProgressionId;
    private int TransactionErrorCode;
}

package com.apitel_bank.main_banking_service.models;

import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "AccountTransactionStatusProgressions")
public class AccountTransactionStatusProgressions {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Accounttransactionstatusprogressionid")
    private int AccountTransactionStatusProgressionId;

    @Column(name = "Accounttransactionid")
    private int AccountTransactionId;

    @Column(name = "Accounttransactionstatusid")
    private int AccountTransactionStatusId;
}

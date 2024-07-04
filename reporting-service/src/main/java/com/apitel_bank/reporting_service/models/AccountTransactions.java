package com.apitel_bank.reporting_service.models;

import jakarta.persistence.Column;
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
@Table(name = "Accounttransactions") // Ensure this matches your actual database table name
public class AccountTransactions {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Accounttransactionid") // Column name in the database
    private int accountTransactionId; // Java naming convention for camelCase

    @Column(name = "Accountid")
    private int accountId;

    @Column(name = "Debitinmibibbdough")
    private int debitInMibiBBDough;

    @Column(name = "Creditinmibibbdough")
    private String creditInMibiBBDough;

    @Column(name = "Reference")
    private String reference;

    @Column(name = "Otherpartyid")
    private int otherPartyId;
}
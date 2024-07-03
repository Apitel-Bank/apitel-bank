package com.apitel_bank.main_banking_service.models;

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
@Table(name = "Accounttransactions")
public class AccountTransactions {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Accounttransactionid")
    private int AccountTransactionId;

    @Column(name = "Accountid")
    private int AccountId;

    @Column(name = "Debitinmibibbdough")
    private int DebitInMibiBBDough;

    @Column(name = "Creditinmibibbdough")
    private String CreditInMibiBBDough;

    @Column(name = "Otherpartyid")
    private int OtherPartyId;
}

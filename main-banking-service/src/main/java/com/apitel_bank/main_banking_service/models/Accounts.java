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
@Table(name = "Accounts")
public class Accounts {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Accountid")
    private Integer AccountId;

    @Column(name = "Customerid")
    private Integer CustomerId;

    @Column(name = "Name")
    private String Name;

    public Integer getAccountId() {
        return AccountId;
    }

    public Integer getCustomerId() {
        return CustomerId;
    }

    public String getName() {
        return Name;
    }
}

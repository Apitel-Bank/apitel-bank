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
@Table(name = "Externalaccounts")
public class ExternalAccounts {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Externalaccountid")
    private int ExternalAccountId;

    @Column(name = "Bankid")
    private int BankId;

    @Column(name = "Externalcustomeraccountid")
    private String ExternalCustomerAccountId;

    public int getExternalAccountId() {
        return ExternalAccountId;
    }

    public void setBankId(int bankId) {
        BankId = bankId;
    }

    public int getBankId() {
        return BankId;
    }

    public void setExternalAccountId(int externalAccountId) {
        ExternalAccountId = externalAccountId;
    }

    public String getExternalCustomerAccountId() {
        return ExternalCustomerAccountId;
    }

    public void setExternalCustomerAccountId(String externalCustomerAccountId) {
        ExternalCustomerAccountId = externalCustomerAccountId;
    }
}

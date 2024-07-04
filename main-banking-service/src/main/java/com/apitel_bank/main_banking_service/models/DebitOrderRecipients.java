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
@Table(name = "Debitorderrecipients")
public class DebitOrderRecipients {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Debitorderrecipientid")
    private int DebitOrderRecipientId;

    @Column(name = "Externalaccountid")
    private int ExternalAccountId;

    public int getDebitOrderRecipientId() {
        return DebitOrderRecipientId;
    }

    public int getExternalAccountId() {
        return ExternalAccountId;
    }
}

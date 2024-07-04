package com.apitel_bank.main_banking_service.models;

import java.util.Date;

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
@Table(name = "Debitorders")
public class DebitOrders {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Debitorderid")
    private int DebitOrderId;

    @Column(name = "Amountinmibibbdough")
    private int AmountInMibiBBDough;

    @Column(name = "Accountid")
    private int AccountId;

    @Column(name = "Dayinthemonth")
    private int DayInTheMonth;

    @Column(name = "Endsat")
    private Date EndsAt;

    @Column(name = "Debitorderrecipientid")
    private int DebitOrderRecipientId;

    public Date getEndsAt() {
        return EndsAt;
    }

    public int getAccountId() {
        return AccountId;
    }

    public int getAmountInMibiBBDough() {
        return AmountInMibiBBDough;
    }

    public int getDayInTheMonth() {
        return DayInTheMonth;
    }

    public int getDebitOrderId() {
        return DebitOrderId;
    }

    public int getDebitOrderRecipientId() {
        return DebitOrderRecipientId;
    }
}

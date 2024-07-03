package com.apitel_bank.main_banking_service.models;

import java.util.Date;

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
@Table(name = "DebitOrders")
public class DebitOrders {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int DebitOrderId;
    private int AmountInMibiBBDough;
    private int AccountId;
    private int DayInTheMonth;
    private Date EndsAt;
    private int DebitOrderRecipientId;
}

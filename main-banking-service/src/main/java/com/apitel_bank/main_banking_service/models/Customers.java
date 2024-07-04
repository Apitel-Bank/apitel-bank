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
@Table(name = "Customers")
public class Customers {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Customerid")
    private int CustomerId;

    @Column(name = "Userid")
    private int UserId;

    @Column(name = "BBDoughid")
    private int BBDoughId;

    @Column(name = "Deletedat")
    private Date DeletedAt;

    @Column(name = "Deletedby")
    private Integer DeletedBy;

    public Date getDeletedAt() {
        return DeletedAt;
    }

    public int getBBDoughId() {
        return BBDoughId;
    }

    public int getCustomerId() {
        return CustomerId;
    }

    public int getDeletedBy() {
        return DeletedBy;
    }

    public int getUserId() {
        return UserId;
    }
}

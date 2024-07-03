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
@Table(name = "users")
public class Users {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "Userid")
    private Integer UserId;
    
    @Column(name = "Displayname")
    private String DisplayName;

    @Column(name = "Deletedat")
    private Date DeletedAt;

    @Column(name = "Deletedby")
    private Integer DeletedBy;
}

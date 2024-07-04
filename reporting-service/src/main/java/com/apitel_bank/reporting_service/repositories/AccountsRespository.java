package com.apitel_bank.reporting_service.repositories;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import com.apitel_bank.reporting_service.models.Accounts;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;

public interface AccountsRespository extends JpaRepository<Accounts, Integer> {

    @Query(value = "EXEC GetBalance :customerId", nativeQuery = true)
    Integer getBalanceByCustomerId(@Param("customerId") Integer customerId);

        Page<Accounts> findAll(Pageable pageable);

    @Query("SELECT a FROM Accounts a WHERE " +
            "(:accountId IS NULL OR a.accountId = :accountId) AND " +
            "(:customerId IS NULL OR a.customerId = :customerId)")
    Page<Accounts> findByAccountIdAndCustomerId(
            @Param("accountId") Long accountId,
            @Param("customerId") Long customerId,
            Pageable pageable);

}

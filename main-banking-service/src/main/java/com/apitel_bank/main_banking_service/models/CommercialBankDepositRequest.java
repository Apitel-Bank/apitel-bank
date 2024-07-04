package com.apitel_bank.main_banking_service.models;

import java.util.ArrayList;
import java.util.List;

public class CommercialBankDepositRequest {
    private List<Deposit> deposits;

    public CommercialBankDepositRequest() {
        deposits = new ArrayList<>();
    }

    public CommercialBankDepositRequest(List<Deposit> deposits) {
        this.deposits = deposits;
    }

    public List<Deposit> getDeposits() {
        return deposits;
    }

    public void setDeposits(List<Deposit> deposits) {
        this.deposits = deposits;
    }

    @Override
    public String toString() {
        return "CommercialBankDepositRequest{" +
                "deposits=" + deposits +
                '}';
    }

    public static class Deposit {
        private String debitAccountName;
        private String creditAccountName;
        private long amount;
        private String debitRef;
        private String creditRef;

        public Deposit() {
            debitAccountName = null;
            creditAccountName = null;
            amount = 0;
            debitRef = null;
            creditRef = null;
        }

        public Deposit(String debitAccountName, String creditAccountName, long amount, String debitRef, String creditRef) {
            this.debitAccountName = debitAccountName;
            this.creditAccountName = creditAccountName;
            this.amount = amount;
            this.debitRef = debitRef;
            this.creditRef = creditRef;
        }

        public void setCreditAccountName(String creditAccountName) {
            this.creditAccountName = creditAccountName;
        }

        public void setAmount(long amount) {
            this.amount = amount;
        }

        public void setCreditRef(String creditRef) {
            this.creditRef = creditRef;
        }

        public void setDebitAccountName(String debitAccountName) {
            this.debitAccountName = debitAccountName;
        }

        public void setDebitRef(String debitRef) {
            this.debitRef = debitRef;
        }

        public long getAmount() {
            return amount;
        }

        public String getCreditAccountName() {
            return creditAccountName;
        }

        public String getCreditRef() {
            return creditRef;
        }

        public String getDebitAccountName() {
            return debitAccountName;
        }

        public String getDebitRef() {
            return debitRef;
        }

        @Override
        public String toString() {
            return "Deposit{" +
                    "debitAccountName='" + debitAccountName + '\'' +
                    ", creditAccountName='" + creditAccountName + '\'' +
                    ", amount=" + amount +
                    ", debitRef='" + debitRef + '\'' +
                    ", creditRef='" + creditRef + '\'' +
                    '}';
        }
    }
}

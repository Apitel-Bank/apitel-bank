package com.apitel_bank.main_banking_service.models;

public class ApitelPaymentRequest {
    private int senderId;
    private long amountInMibiBBDough;
    private String reference;
    private Recipient recepient;

    public ApitelPaymentRequest() {}

    public ApitelPaymentRequest(int senderId, long amountInMibiBBDough, String reference, Recipient recepient) {
        this.senderId = senderId;
        this.amountInMibiBBDough = amountInMibiBBDough;
        this.reference = reference;
        this.recepient = recepient;
    }

    @Override
    public String toString() {
        return "ApitelPaymentRequest{" +
                "senderId=" + senderId +
                ", amountInMibiBBDough=" + amountInMibiBBDough +
                ", reference='" + reference + '\'' +
                ", recepient=" + recepient +
                '}';
    }

    public String getReference() {
        return reference;
    }

    public long getAmountInMibiBBDough() {
        return amountInMibiBBDough;
    }

    public int getSenderId() {
        return senderId;
    }

    public void setAmountInMibiBBDough(long amountInMibiBBDough) {
        this.amountInMibiBBDough = amountInMibiBBDough;
    }

    public void setReference(String reference) {
        this.reference = reference;
    }

    public void setSenderId(int senderId) {
        this.senderId = senderId;
    }

    public void setRecepient(Recipient recepient) {
        this.recepient = recepient;
    }

    public Recipient getRecepient() {
        return recepient;
    }

    public static class Recipient {
        private int bankId;
        private String accountId;

        public Recipient() {}

        public Recipient(int bankId, String accountId) {
            this.bankId = bankId;
            this.accountId = accountId;
        }

        public int getBankId() {
            return bankId;
        }

        public String getAccountId() {
            return accountId;
        }

        public void setAccountId(String accountId) {
            this.accountId = accountId;
        }

        public void setBankId(int bankId) {
            this.bankId = bankId;
        }

        @Override
        public String toString() {
            return "Recipient{" +
                    "bankId=" + bankId +
                    ", accountId='" + accountId + '\'' +
                    '}';
        }
    }
}

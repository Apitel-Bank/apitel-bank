package com.apitel_bank.main_banking_service.models;

public class CommercialBankDepositReplyRequest {
    private String reference;
    private String status;

    public CommercialBankDepositReplyRequest() {}

    public CommercialBankDepositReplyRequest(String reference, String status) {
        this.reference = reference;
        this.status = status;
    }

    public String getReference() {
        return reference;
    }

    public String getStatus() {
        return status;
    }

    public void setReference(String reference) {
        this.reference = reference;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public String toString() {
        return "CommercialBankDepositReplyRequest{" +
                "reference='" + reference + '\'' +
                ", status='" + status + '\'' +
                '}';
    }
}

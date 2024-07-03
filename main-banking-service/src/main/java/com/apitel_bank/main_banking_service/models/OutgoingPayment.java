package com.apitel_bank.main_banking_service.models;

public class OutgoingPayment {
    private final String reference;
    private final long amountInMibiBBDough;
    private final int receivingBankId;
    private final String receivingAccountId;
    private final int personaId;

    public OutgoingPayment(String reference, long amountInMibiBBDough, int receivingBankId, String receivingAccountId, int personaId) {
        this.reference = reference;
        this.amountInMibiBBDough = amountInMibiBBDough;
        this.receivingBankId = receivingBankId;
        this.receivingAccountId = receivingAccountId;
        this.personaId = personaId;
    }

    public int getReceivingBankId() {
        return receivingBankId;
    }

    public long getAmountInMibiBBDough() {
        return amountInMibiBBDough;
    }

    public String getReceivingAccountId() {
        return receivingAccountId;
    }

    public String getReference() {
        return reference;
    }

    public int getPersonaId() {
        return personaId;
    }
}

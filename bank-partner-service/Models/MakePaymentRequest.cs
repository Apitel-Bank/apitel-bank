namespace BankPartnerService.Models
{
    public record MakePaymentRequest(long? SenderId, long AmountInMibiBBDough, PaymentRecepient Recepient, string Reference);
}

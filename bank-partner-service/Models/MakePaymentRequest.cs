namespace BankPartnerService.Models
{
    public record MakePaymentRequest(int SenderId, long AmountInMibiBBDough, PaymentRecepient Recepient, string Reference);
}

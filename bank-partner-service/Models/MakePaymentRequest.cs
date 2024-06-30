namespace BankPartnerService.Models
{
    public record MakePaymentRequest(int SenderId, int AmountInMibiBBDough, PaymentRecepient Recepient, string Reference);
}

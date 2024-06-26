namespace BankPartnerService.Models
{
    public record PaymentRecepient(string BankId, string AccountId);

    public record CreateDebitOrderRequest(long AmountInMibiBBDough, long PersonaId, short DayInMonth, string EndsAt, string Reference, PaymentRecepient Recepient);
}

namespace BankPartnerService.Models
{
    public record PaymentRecepient(string BankId, string AccountId);

    public record CreateDebitOrder(long AmountInMibiBBDough, long PersonaId, short DayInMonth, string EndsAt, string Reference, PaymentRecepient Recepient);
}

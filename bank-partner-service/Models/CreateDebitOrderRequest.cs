namespace BankPartnerService.Models
{
    public record PaymentRecepient(int BankId, string AccountId);

    public record CreateDebitOrderRequest(long AmountInMibiBBDough, long PersonaId, short DayInMonth, string EndsAt, PaymentRecepient Recepient);
}

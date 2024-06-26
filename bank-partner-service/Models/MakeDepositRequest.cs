namespace BankPartnerService.Models
{
    public record MakeDepositRequest(long ToPersonaId, long AmountInMibiBBDough, string Reference);
}

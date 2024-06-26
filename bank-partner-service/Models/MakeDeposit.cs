namespace BankPartnerService.Models
{
    public record MakeDeposit(long ToPersonaId, long AmountInMibiBBDough, string Reference);
}

namespace BankPartnerService.Models
{
    public record MakeDepositRequest(int ToPersonaId, int AmountInMibiBBDough, string Reference, string fromAccountId);
}

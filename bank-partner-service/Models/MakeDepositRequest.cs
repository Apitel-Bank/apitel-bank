namespace BankPartnerService.Models
{
    public record MakeDepositRequest(int ToPersonaId, long AmountInMibiBBDough, string Reference, string FromAccountId);
}

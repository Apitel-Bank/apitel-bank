namespace BankPartnerService.Models
{
    public record GetAcountResponse(long AccountId, string CardNumber, long BalanceInMibiBBDough);
}

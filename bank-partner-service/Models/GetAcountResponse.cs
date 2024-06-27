namespace BankPartnerService.Models
{
    public record GetAcountResponse(long AccountId, string AccountName, long BalanceInMibiBBDough);
}

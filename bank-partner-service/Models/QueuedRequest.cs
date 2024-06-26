namespace BankPartnerService.Models
{
    public record QueuedRequest<T>(string CallbackUri, T Request);
}

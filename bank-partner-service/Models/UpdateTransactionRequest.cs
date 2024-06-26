namespace BankPartnerService.Models
{
    public record UpdateTransactionRequest(bool ProcessedSuccessfully, int? RejectionCode, string? RejectionReason);
}

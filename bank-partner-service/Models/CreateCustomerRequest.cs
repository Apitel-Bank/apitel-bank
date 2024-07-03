namespace BankPartnerService.Models
{
    public record CreateCustomerRequest(IEnumerable<long> PersonaIds);
}

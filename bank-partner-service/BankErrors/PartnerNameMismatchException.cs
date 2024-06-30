namespace BankPartnerService.BankErrors
{
    public class PartnerNameMismatchException : BaseBankException
    {
        public PartnerNameMismatchException() : base(49, "The resolved partner name does not match that in transaction reference.") { }

    }
}

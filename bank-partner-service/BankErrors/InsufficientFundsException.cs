namespace BankPartnerService.BankErrors
{
    public class InsufficientFundsException: BaseBankException
    {
        public InsufficientFundsException(): base(51, "The source bank account does not have enough funds for this transaction.") { }
    }
}

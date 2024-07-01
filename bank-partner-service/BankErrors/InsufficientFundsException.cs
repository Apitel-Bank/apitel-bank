namespace BankPartnerService.BankErrors
{
    public class InsufficientFundsException: BaseBankException
    {
        public static readonly int ERROR_CODE = 51;
        public static readonly string ERROR_MESSAGE = "The source bank account does not have enough funds for this transaction.";

        public InsufficientFundsException(): base(ERROR_CODE, ERROR_MESSAGE) { }
    }
}

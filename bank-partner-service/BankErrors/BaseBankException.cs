namespace BankPartnerService.BankErrors
{
    public class BaseBankException(int errorCode, string message) : Exception(message)
    {
        public int ErrorCode { get; } = errorCode;
    }

}

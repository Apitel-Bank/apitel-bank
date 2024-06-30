namespace BankPartnerService.BankErrors
{
    public class UnknownBankException: BaseBankException
    {
        public UnknownBankException(): base(48, "The sending bank could not be matched with the list of known banks. Please contact us for a partnership.") { }
    }
}

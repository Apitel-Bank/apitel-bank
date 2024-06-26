namespace BankPartnerService.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SenderId">If not given, CardNumber must be given.</param>
    /// <param name="CardNumber">If not given, SenderId must be given</param>
    /// <param name="AmountInMibiBBDough"></param>
    /// <param name="Recepient"></param>
    /// <param name="Reference"></param>
    public record MakePayment(long? SenderId, string? CardNumber, long AmountInMibiBBDough, PaymentRecepient Recepient, string Reference);
}

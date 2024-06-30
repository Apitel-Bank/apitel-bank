using BankPartnerService.Models;
using BankPartnerService.Repositories;
using System.Data.SqlClient;

namespace BankPartnerService.Services
{
    public class DebitOrdersService(DebitOrdersRepository debitOrdersRepository, Db db)
    {
 
        public CreateDebitOrderResponse AddDebitOrder(CreateDebitOrderRequest debitOrderRequest)
        {
           //TODO: Ensure that endAt is a valid date in the future (or null), dayInTheMonth is between 1-30, limit on the amount?
           //TODO: Ensure that the persona has an account at our bank and the recipient exists too (at our bank or the other bank)
            return debitOrdersRepository.AddDebitOrder(debitOrderRequest);
        }

        public int CancelDebitOrder(int debitOrderId)
        {
            //TODO: Ensure the persona/business making the request to cancel is authorized
            return debitOrdersRepository.CancelDebitOrder(debitOrderId);
        }
    }
}

using BankPartnerService.Models;
using BankPartnerService.Repositories;
using System.Data.SqlClient;

namespace BankPartnerService.Services
{
    public class DebitOrdersService(DebitOrdersRepository debitOrdersRepository, Db db)
    {
 
        public CreateDebitOrderResponse AddDebitOrder(CreateDebitOrderRequest debitOrderRequest)
        {
           
            return debitOrdersRepository.AddDebitOrder(debitOrderRequest);
        }

        public int CancelDebitOrder(int debitOrderId)
        {
            //TODO: Ensure the persona/business making the request to cancel is authorized
            return debitOrdersRepository.CancelDebitOrder(debitOrderId);
        }
    }
}

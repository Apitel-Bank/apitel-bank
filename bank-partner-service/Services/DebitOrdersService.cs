using BankPartnerService.Models;
using BankPartnerService.Repositories;

namespace BankPartnerService.Services
{
    public class DebitOrdersService(DebitOrdersRespository debitOrdersRespository)
    {
        public CreateDebitOrderResponse AddDebitOrder(CreateDebitOrderRequest debitOrderRequest)
        {
            return debitOrdersRespository.AddDebitOrder(debitOrderRequest);
        }

        public int CancelDebitOrder(int debitOrderId)
        {
            return debitOrdersRespository.CancelDebitOrder(debitOrderId);
        }
    }
}

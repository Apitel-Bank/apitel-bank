using BankPartnerService.Models;
using BankPartnerService.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebitOrdersController : ControllerBase
    {
        private readonly DebitOrdersService _debitOrdersService;

        public DebitOrdersController(DebitOrdersService debitOrdersService)
        {
            _debitOrdersService = debitOrdersService;
        }

        /// <summary>
        /// Creates a new debit order.
        /// </summary>
        [HttpPost]
        public ActionResult<CreateDebitOrderResponse> CreateDebitOrder(CreateDebitOrderRequest request)
        {
            var response = _debitOrdersService.AddDebitOrder(request);
            return Ok(response);
        }

        /// <summary>
        /// Cancels a debit order.
        /// </summary>
        [HttpDelete("{debitOrderId}")]
        public IActionResult DeleteDebitOrder(int debitOrderId)
        {
            var response = _debitOrdersService.CancelDebitOrder(debitOrderId);
            return Ok(response);
        }
    }
}

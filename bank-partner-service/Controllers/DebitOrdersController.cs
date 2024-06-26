using BankPartnerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DebitOrdersController : ControllerBase
    {
        /// <summary>
        /// Creates a new debit order.
        /// </summary>
        [HttpPost()]
        public ActionResult<CreateDebitOrderResponse> CreateDebitOrder(IEnumerable<CreateDebitOrderRequest> request)
        {
            return new CreatedResult();
        }

        /// <summary>
        /// Cancels a debit order.
        /// </summary>
        [HttpDelete("{debitOrderId}")]
        public IActionResult DeleteDebitOrder()
        {
            return Ok();
        }
    }
}

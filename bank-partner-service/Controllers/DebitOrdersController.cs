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
        /// Queues a new debit order creation.
        /// </summary>
        /// <response code="202">Accepted</response>
        [HttpPost()]
        public IActionResult CreateDebitOrder(QueuedRequest<IEnumerable<CreateDebitOrder>> Request)
        {
            return new CreatedResult();
        }

        /// <summary>
        /// Queues the given debit order for cancellation.
        /// </summary>
        /// <response code="202">Accepted</response>
        [HttpDelete("{DebitOrderId}")]
        public IActionResult DeleteDebitOrder()
        {
            return Ok();
        }
    }
}

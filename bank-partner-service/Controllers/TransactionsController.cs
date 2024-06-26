using BankPartnerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// Queues a payment order into the given bank and recepients.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="202">Accepted</response>
        [HttpPost("Payments")]
        public IActionResult MakePayment(QueuedRequest<MakePayment> request)
        {
            return Ok();
        }

        /// <summary>
        /// Queues deposits into the accounts for the given persona ids and amounts.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="202">Accepted</response>
        [HttpPost("Deposits")]
        public IActionResult MakeDeposit(QueuedRequest<IEnumerable<MakeDeposit>> request)
        {
            return Ok();
        }

        /// <summary>
        /// Returns status of deposit/payment with given reference.
        /// </summary>
        /// <response code="200">New|Accepted|Verified|Rejected|Reversed values for status</response>
        [HttpGet("{Reference}/Status")]
        public ActionResult<GetTransactionStatusResponse> GetTransactionStatus()
        {
            return Ok();
        }
    }
}

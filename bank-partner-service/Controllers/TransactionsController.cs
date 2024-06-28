using BankPartnerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// Makes payment from given persona account into recepient account.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("payments")]
        public ActionResult<MakePaymentResponse> MakePayment(MakePaymentRequest request)
        {
            return Ok();
        }

        /// <summary>
        /// Makes a deposit into the accounts for the given persona ids and amounts.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="202">Accepted</response>
        [HttpPost("deposits")]
        public IActionResult MakeDeposit(IEnumerable<MakeDepositRequest> request)
        {
            return Created();
        }

        /// <summary>
        /// Returns status of deposit/payment with given reference.
        /// </summary>
        /// <response code="200">New|Accepted|Verified|Rejected|Reversed values for status</response>
        [HttpGet("{reference}/status")]
        public ActionResult<GetTransactionStatusResponse> GetTransactionStatus()
        {
            return Ok();
        }

        /// <summary>
        /// Marks the transaction as verified or reversed.
        /// 
        /// Other banks call this endpoint when they have successfully/unsuccessfully processed a deposit into their bank account from us.
        /// </summary>
        [HttpPut("{reference}/status")]
        public ActionResult<GetTransactionStatusResponse> UpdateTransactionStatus(UpdateTransactionRequest request)
        {
            return Ok();
        }
    }
}

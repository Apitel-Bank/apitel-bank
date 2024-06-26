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
        [HttpPost("deposits")]
        public ActionResult<MakeDepositResponse> MakeDeposit(IEnumerable<MakeDepositRequest> request)
        {
            return Ok();
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
    }
}

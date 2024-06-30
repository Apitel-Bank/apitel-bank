using BankPartnerService.BankErrors;
using BankPartnerService.Models;
using BankPartnerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace BankPartnerService.Controllers
{

    record BankErrorResponse(int ErrorCode, string Messages);

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController(TransactionsService transactionsService) : ControllerBase
    {
        /// <summary>
        /// Makes payment from given persona account into recepient account.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("payments")]
        public ActionResult<TransactionResponse> MakePayment([FromHeader(Name = "X-PartnerId")] string partnerId, MakePaymentRequest request)
        {
            try
            {
                var transactionId = transactionsService.AddPayment(request.SenderId, request.AmountInMibiBBDough, request.Reference, partnerId, request.Recepient.BankId, request.Recepient.AccountId);
                return Created(string.Format("transactions/{0}", transactionId), new TransactionResponse(transactionId));
            } catch (BaseBankException ex)
            {
                return Conflict(new BankErrorResponse(ex.ErrorCode, ex.Message));
            }
        }

        /// <summary>
        /// Makes a deposit into the account for the given persona id and amount.
        /// 
        /// This endpoint will not accept transactions whose references is not unique.
        /// </summary>
        /// <param name="request"></param>
        /// <response code="202">Accepted</response>
        [HttpPost("deposits")]
        public IActionResult MakeDeposit([FromHeader(Name = "X-PartnerId")] string partnerId, MakeDepositRequest request)
        {
            try
            {
                var transactionId = transactionsService.AddDeposit(request.ToPersonaId, request.AmountInMibiBBDough, request.Reference, partnerId, request.fromAccountId);
                return Accepted(string.Format("transactions/{0}", transactionId), new TransactionResponse(transactionId));
            } catch (BaseBankException ex)
            {
                return Conflict(new BankErrorResponse(ex.ErrorCode, ex.Message));
            }
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

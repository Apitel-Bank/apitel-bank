using BankPartnerService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Creates a new customers. The customers will also get new bank accounts. A customer can only have one bank account.
        /// The customer's bank account will also automatically get a debit card.
        /// </summary>
        [HttpPost()]
        public ActionResult<GetAcountResponse> CreateCustomers(IEnumerable<CreateCustomerRequest> request)
        {
            return new CreatedResult();
        }

        /// <summary>
        /// Gets the personas' active accounts and their linked debit cards.
        /// </summary>
        /// <param name="personaId">The id of the persona to get the accounts of.</param>
        [HttpGet("{personaId}/accounts")]
        public ActionResult<IEnumerable<GetAcountResponse>> GetAccounts(long personaId)
        {
            return Ok();
        }
    }
}

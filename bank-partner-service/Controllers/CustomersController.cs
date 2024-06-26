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
        /// Queues a command to create new customers. The customers will also get new bank accounts. A customer can only have one bank account.
        /// The customer's bank account will also automatically get a debit card.
        /// </summary>
        /// <response code="202">Accepted</response>
        [HttpPost()]
        public IActionResult CreateCustomers(QueuedRequest<IEnumerable<CreateCustomer>> Request)
        {
            return new CreatedResult();
        }

        /// <summary>
        /// Gets the personas' active accounts and their linked debit cards.
        /// </summary>
        /// <param name="PersonaId">The id of the persona to get the accounts of.</param>
        [HttpGet("{PersonaId}/Accounts")]
        public ActionResult<IEnumerable<GetAcountResponse>> GetAccounts(long PersonaId)
        {
            return Ok();
        }
    }
}

using BankPartnerService.Models;
using BankPartnerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController (CustomersService customersService) : ControllerBase
    {
        /// <summary>
        /// Creates a new customers. The customers will also get new bank accounts. A customer can only have one bank account.
        /// The customer's bank account will also automatically get a debit card.
        /// </summary>
        /// <response code="201">All Created</response>
        [HttpPost()]
        public ActionResult<IEnumerable<GetAcountResponse>> CreateCustomers(IEnumerable<CreateCustomerRequest> request)
        {
            var accounts = customersService.BulkCreateCustomerWithAccounts(request.Select(customer => (customer.PersonaId, customer.PersonaId.ToString())));
            return new CreatedResult("/customers", accounts.Select(account => new GetAcountResponse(account.accountId, account.accountName, 0)));
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

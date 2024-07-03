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
        public ActionResult<IEnumerable<GetAcountResponse>> CreateCustomers(CreateCustomerRequest request)
        {
            var accounts = customersService.BulkCreateCustomerWithAccounts(request.PersonaIds.Select(customerIdNumber => (customerIdNumber, customerIdNumber.ToString())));
            return new CreatedResult("/customers", accounts.Select(account => new GetAcountResponse(account.accountId, account.accountName, 0)));
        }

        /// <summary>
        /// Gets the personas' active accounts and their linked debit cards.
        /// </summary>
        /// <param name="personaId">The id of the persona to get the accounts of.</param>
        [HttpGet("{personaId}/accounts")]
        public ActionResult<IEnumerable<GetAcountResponse>> GetAccounts(int personaId)
        {
            var account = customersService.GetAccount(personaId);
            return Ok(new List<GetAcountResponse> { new(account.account.AccountId, account.account.Name, account.balance) });
        }
    }
}

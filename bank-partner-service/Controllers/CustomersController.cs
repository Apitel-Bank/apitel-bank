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
        private readonly DebitOrdersService _debitOrdersService;
        /// <summary>
        /// Creates a new customers. The customers will also get new bank account. A customer can only have one bank account.
        /// </summary>
        /// <response code="201">All Created</response>
        [HttpPost()]
        public ActionResult<IEnumerable<GetAcountResponse>> CreateCustomers(IEnumerable<CreateCustomerRequest> request)
        {
            var accounts = customersService.BulkCreateCustomerWithAccounts(request.Select(customer => (customer.PersonaId, customer.PersonaId.ToString())));
            return new CreatedResult("/customers", accounts.Select(account => new GetAcountResponse(account.accountId, account.accountName, 0)));
        }

        /// <summary>
        /// Gets the personas' active account and their balance.
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

using BankPartnerService.Models;
using BankPartnerService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankPartnerService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController(BanksService banksService) : ControllerBase
    {
        /// <summary>
        /// Returns all the banks payments can be made to.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult<IEnumerable<Bank>> GetAllBanks()
        {
            return Ok(banksService.ListAllBanks());
        }
    }
}

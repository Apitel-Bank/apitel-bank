using BankPartnerService.Models;
using BankPartnerService.Repositories;

namespace BankPartnerService.Services
{
    public class BanksService(BanksRepository banksRepository)
    {
        public IEnumerable<Bank> ListAllBanks()
        {
            return banksRepository.ListAll();
        }
    }
}

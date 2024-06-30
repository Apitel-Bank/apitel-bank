using BankPartnerService.BankErrors;
using BankPartnerService.Models;
using BankPartnerService.Repositories;
using System.Data.Common;
using System.Transactions;

namespace BankPartnerService.Services
{
    public class TransactionsService(Db db, AccountsRepository accountsRepository, TransactionsRepository transactionsRepository,
        ExternalAccountsRepository externalAccountsRepository, AccountTransactionStatusesRepository accountTransactionStatusesRepository, BanksRepository banksRepository)

    {
        readonly int newTransactionStatus = accountTransactionStatusesRepository.GetStatusId("New");
        readonly int acceptedTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Accepted");
        readonly int visibleTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Visible");

        readonly IEnumerable<Bank> knownBanks = banksRepository.ListAll();

        public int AddPayment(int customerIdNumber, int amount, string reference, string partnerId, int recepientBankId, string recepientAccountId)
        {
            if (!reference.StartsWith(partnerId))
            {
                throw new PartnerNameMismatchException();
            } else
            {
                return db.WithTransaction(transaction =>
                {
                    int externalAccountId = externalAccountsRepository.AddExternalAccountIfNotExist(transaction, recepientBankId, recepientAccountId);
                    int customerBalance = accountsRepository.GetBalance(transaction, customerIdNumber);
                    if(customerBalance - amount < 0)
                    {
                        throw new InsufficientFundsException();
                    } else
                    {
                        var transactionId = transactionsRepository.AddAccountTransaction(transaction, customerIdNumber, amount, 0, reference, externalAccountId, newTransactionStatus);
                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, acceptedTransactionStatus);
                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, visibleTransactionStatus);
                        return transactionId;
                    }
                });
            }
        }

        public int AddDeposit(int customerIdNumber, int amount, string reference, string partnerId, string fromAccountId)
        {
            if(!reference.StartsWith(partnerId))
            {
                throw new PartnerNameMismatchException();
            } else
            {
                var fromBankId = GetBankIdFromPartnerId(partnerId);
                if(fromBankId < 0)
                {
                    throw new UnknownBankException();
                } else
                {
                    return db.WithTransaction(transaction =>
                    {
                        int externalAccountId = externalAccountsRepository.AddExternalAccountIfNotExist(transaction, fromBankId, fromAccountId);
                        var transactionId = transactionsRepository.AddAccountTransaction(transaction, customerIdNumber, 0, amount, reference, externalAccountId, newTransactionStatus);

                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, acceptedTransactionStatus);
                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, visibleTransactionStatus);
                        return transactionId;
                    });
                }

            }
        }

        private int GetBankIdFromPartnerId(string partnerId)
        {
            // TODO: Maybe not hard code known bank partners like this.
            if(partnerId == "retail-bank")
            {
                return knownBanks.Where(bank => bank.BankName == "Apitel Retail Bank").Select(bank => bank.BankId).First();
            }
            else if(partnerId == "commercial-bank")
            {
                return knownBanks.Where(bank => bank.BankName == "Commercial Bank").Select(bank => bank.BankId).First();
            } else
            {
                return -1;
            }
        }
    }
}

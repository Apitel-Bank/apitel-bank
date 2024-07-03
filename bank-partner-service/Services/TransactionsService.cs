using BankPartnerService.BankErrors;
using BankPartnerService.Models;
using BankPartnerService.Repositories;
using System.Data.Common;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Transactions;

namespace BankPartnerService.Services
{
    public class TransactionsService(Db db, AccountsRepository accountsRepository, TransactionsRepository transactionsRepository,
        ExternalAccountsRepository externalAccountsRepository, AccountTransactionStatusesRepository accountTransactionStatusesRepository, BanksRepository banksRepository)

    {
        readonly int newTransactionStatus = accountTransactionStatusesRepository.GetStatusId("New");
        readonly int acceptedTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Accepted");
        readonly int visibleTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Visible");
        readonly int verifiedTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Verified");
        readonly int reversedTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Reversed");
        readonly int rejectedTransactionStatus = accountTransactionStatusesRepository.GetStatusId("Rejected");

        readonly IEnumerable<Bank> knownBanks = banksRepository.ListAll();

        public int AddPayment(int customerIdNumber, long amount, string reference, string partnerId, int recepientBankId, string recepientAccountId)
        {
            if (!reference.StartsWith(partnerId))
            {
                throw new PartnerNameMismatchException();
            } else
            {
                (int transactionId, long balanceBeforeTransaction) = db.WithTransaction(transaction =>
                {
                    var customerBalance = accountsRepository.GetBalance(transaction, customerIdNumber);
                    var externalAccountId = externalAccountsRepository.AddExternalAccountIfNotExist(transaction, recepientBankId, recepientAccountId);
                    var transactionId = transactionsRepository.AddAccountTransaction(transaction, customerIdNumber, amount, 0, reference, externalAccountId, newTransactionStatus);

                    return (transactionId, customerBalance);
                });

                if(balanceBeforeTransaction - amount < 0)
                {
                    db.WithTransaction(transaction =>
                    {
                        transactionsRepository.MarkAsReversedOrRejected(transaction, reference, InsufficientFundsException.ERROR_CODE, InsufficientFundsException.ERROR_MESSAGE, rejectedTransactionStatus);
                        return 0;
                    });

                    throw new InsufficientFundsException();
                } else
                {
                    return db.WithTransaction(transaction =>
                    {
                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, acceptedTransactionStatus);
                        transactionsRepository.ProgressTransactionStatus(transaction, transactionId, visibleTransactionStatus);
                        return transactionId;
                    });
                }
            }
        }

        public int AddDeposit(int customerIdNumber, long amount, string reference, string partnerId, string fromAccountId)
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
                        return transactionId;
                    });
                }

            }
        }

        public string GetStatus(string reference)
        {
            var statusId = transactionsRepository.GetStatus(reference);
            return accountTransactionStatusesRepository.GetStatusString(statusId);
        }

        public void MarkTransactionAsVerified(string reference)
        {
            db.WithTransaction(transaction =>
            {
                transactionsRepository.ProgressTransactionStatuByReference(transaction, reference, verifiedTransactionStatus);
                return 0;
            });
        }

        public void OnOtherBankResponseForTransaction(string transactionReference, bool processedSuccessfully, int rejectionCode, string rejectionReason)
        {
            db.WithTransaction(transaction =>
            {
                if(processedSuccessfully)
                {
                    transactionsRepository.ProgressTransactionStatuByReference(transaction, transactionReference, verifiedTransactionStatus);
                } else
                {
                    transactionsRepository.MarkAsReversedOrRejected(transaction, transactionReference, rejectionCode, rejectionReason, reversedTransactionStatus);
                }
                return 0;
            });
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

using BankPartnerService.Models;
using BankPartnerService.Models;
using BankPartnerService.Repositories;
using System.Data.SqlClient;

namespace BankPartnerService.Services
{
    public class CustomersService(Db db, CustomersRepository customersRepository, AccountsRepository accountsRepository)
    {
        private const string SAVINGS_ACCOUNT_NAME = "Savings Account";

        private (int customerId, int accountId, string accountName) CreateCustomerWithAccount(SqlTransaction transaction, long idNumber, string displayName)
        {
            //Check if this customer has an account, if they do then do not let them make another one
            string checkCustomerSql = @"
            SELECT c.CustomerId, a.AccountId, a.Name 
            FROM Customers c
            JOIN Accounts a ON c.CustomerId = a.CustomerId
            WHERE c.BBDoughId = @IdNumber";

            using var checkCustomerCommand = new SqlCommand(checkCustomerSql, db.Connection);
            checkCustomerCommand.Parameters.AddWithValue("@IdNumber", idNumber);
            var reader = checkCustomerCommand.ExecuteReader();
            if (reader.Read())
            {
                throw new Exception("Customer already has an account.");
            }
                
            int customerId = customersRepository.AddCustomer(transaction, idNumber, displayName);
            int accountId = accountsRepository.AddAccount(transaction, customerId, SAVINGS_ACCOUNT_NAME);
            return (customerId, accountId, SAVINGS_ACCOUNT_NAME);
        }

        public IEnumerable<(int customerId, int accountId, string accountName)> BulkCreateCustomerWithAccounts(IEnumerable<(long idNumber, string displayName)> customers)
        {
            return db.WithTransaction(transaction =>
            {
                return customers.Select(customer => CreateCustomerWithAccount(transaction, customer.idNumber, customer.displayName));
            });
        }

        public (Account account, int balance) GetAccount(long customerIdNumber)
        {
            return (accountsRepository.GetAccount(customerIdNumber), accountsRepository.GetBalanceOptionalTransaction(customerIdNumber, null));
        }
    }
}

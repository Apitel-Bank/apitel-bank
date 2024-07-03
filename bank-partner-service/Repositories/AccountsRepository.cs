using BankPartnerService.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BankPartnerService.Repositories
{
    public class AccountsRepository(Db db, AccountTransactionStatusesRepository accountTransactionStatusesRepository)
    {
        public int AddAccount(SqlTransaction transaction, int customerId, string accountName)
        {
            var sql = @"INSERT INTO Accounts(CustomerId, Name)
                        VALUES (@CustomerId, @AccountName);
                        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var command = new SqlCommand(sql, db.Connection, transaction);
            command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = customerId;
            command.Parameters.Add("@AccountName", System.Data.SqlDbType.NVarChar).Value = accountName;
            return (int)command.ExecuteScalar();
        }

        public Account GetAccount(long customerIdNumber)
        {
            var sql = @"SELECT AccountId, Name FROM Accounts
                        INNER JOIN Customers ON Customers.CustomerId = Accounts.CustomerId
                        WHERE Customers.BBDoughId = @CustomerIdNumber;";

            using var command = new SqlCommand(sql, db.Connection);
            command.Parameters.Add("@CustomerIdNumber", System.Data.SqlDbType.Int).Value = customerIdNumber;

            var reader = command.ExecuteReader();
            if(reader.Read())
            {
                var account = new Account(reader.GetInt32("AccountId"), reader.GetString("Name"));
                reader.Close();
                return account;
            } else
            {
                throw new KeyNotFoundException("Could not find customer with the given id");
            }
        }

        public int GetBalance(SqlTransaction transaction, long customerIdNumber)
        {
            return GetBalanceOptionalTransaction(customerIdNumber, transaction);
        }

        public int GetBalanceOptionalTransaction(long customerIdNumber, SqlTransaction transaction = null)
        {
            var sql = @"DECLARE @Balance INT;
                        EXEC @Balance = dbo.GetBalance @CustomerIdNumber;
                        SELECT @Balance;";

            using var command = transaction != null ? new SqlCommand(sql, transaction.Connection, transaction): new SqlCommand(sql, db.Connection);
            command.Parameters.Add("@CustomerIdNumber", System.Data.SqlDbType.Int).Value = customerIdNumber;
            var balance = command.ExecuteScalar();

            if (balance is DBNull)
            {
                return 0;
            }
            else
            {
                return (int)balance;
            }
        }
    }
}

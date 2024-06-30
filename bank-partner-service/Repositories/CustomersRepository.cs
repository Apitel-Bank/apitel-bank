using BankPartnerService.Models;
using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class CustomersRepository(Db db)
    {
        public int AddCustomer(SqlTransaction transaction, long idNumber, string displayName)
        {
            var sql = @"INSERT INTO Users(DisplayName) VALUES (@DisplayName);
                        INSERT INTO Customers(UserId, BBDoughId) VALUES (SCOPE_IDENTITY(), @IdNumber);
                        select CAST(SCOPE_IDENTITY() AS INT);";

            using var command = new SqlCommand(sql, db.Connection, transaction);
            command.Parameters.Add("@DisplayName", System.Data.SqlDbType.NVarChar).Value = displayName;
            command.Parameters.Add("@IdNumber", System.Data.SqlDbType.Int).Value = idNumber;
            return (int)command.ExecuteScalar();
        }

        public GetAcountResponse GetAccount(long personaId)
        {
            string customerSql = @"select customerId from customers where BBDoughId = @personaId";
            using var getCustomerIdCommand = new SqlCommand(customerSql, db.Connection);
            getCustomerIdCommand.Parameters.AddWithValue("@personaId", personaId);
            var customerId = getCustomerIdCommand.ExecuteScalar();
            if (customerId == null)
            {
                throw new Exception("Customer not found.");
            }


            string accountSql = @"SELECT AccountId, Name FROM Accounts WHERE CustomerId = @CustomerId";
            using var getAccountIdCommand = new SqlCommand(accountSql, db.Connection);
            getAccountIdCommand.Parameters.AddWithValue("@CustomerId", customerId);
            var accountReader = getAccountIdCommand.ExecuteReader();
            long accountId;
            string accountName;
            if (accountReader.Read())
            {
                accountId = accountReader.GetInt32(0);
                accountName = accountReader.GetString(1);
            } else
            {
                throw new Exception("Account not found.");
            }
            accountReader.Close();


            string balanceSql = @"
                SELECT 
                SUM(DebitInMibiBBDough) AS TotalDebits,
                SUM(CreditInMibiBBDough) AS TotalCredits
                FROM AccountTransactions
                WHERE AccountId = @AccountId";
            using var getBalanceCommand = new SqlCommand(balanceSql, db.Connection);
            getBalanceCommand.Parameters.AddWithValue("@AccountId", accountId);
            var balanceReader = getBalanceCommand.ExecuteReader();
            long totalDebits = 0;
            long totalCredits = 0;
            
            if (balanceReader.Read())
            {
                totalDebits = balanceReader.IsDBNull(0) ? 0 : balanceReader.GetInt32(0);
                totalCredits = balanceReader.IsDBNull(1) ? 0 : balanceReader.GetInt32(1);
            }
            balanceReader.Close();
            long balance = totalCredits - totalDebits;

            return new GetAcountResponse(accountId, accountName, balance);
        }
    }   
}

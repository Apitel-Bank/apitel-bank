using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class AccountsRepository(Db db)
    {
        public int AddAccount(SqlTransaction transaction, int customerId, string accountName)
        {
            var sql = @"INSERT INTO Accounts(CustomerId, Name)
                        VALUES (@CustomerId, @AccountName);
                        SELECT SCOPE_IDENTITY();";

            using var command = new SqlCommand(sql, db.Connection, transaction);
            command.Parameters.Add("@CustomerId", System.Data.SqlDbType.Int).Value = customerId;
            command.Parameters.Add("@AccountName", System.Data.SqlDbType.NVarChar).Value = accountName;
            return (int)(decimal)command.ExecuteScalar();
        }
    }
}

using System.Data.SqlClient;
using System.Transactions;

namespace BankPartnerService.Repositories
{
    public class CustomersRepository(Db db)
    {
        public int AddCustomer(SqlTransaction transaction, long idNumber, string displayName)
        {
            var sql = @"INSERT INTO Users(DisplayName) VALUES (@DisplayName);
                        INSERT INTO Customers(UserId, BBDoughId) VALUES (SCOPE_IDENTITY(), @IdNumber);
                        select SCOPE_IDENTITY();";

            using var command = new SqlCommand(sql, db.Connection, transaction);
            command.Parameters.Add("@DisplayName", System.Data.SqlDbType.NVarChar).Value = displayName;
            command.Parameters.Add("@IdNumber", System.Data.SqlDbType.Int).Value = idNumber;
            return (int)(decimal)command.ExecuteScalar();
        }
    }   
}

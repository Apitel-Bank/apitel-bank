using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class ExternalAccountsRepository
    {
        public int AddExternalAccountIfNotExist(SqlTransaction transaction, int bankId, string externalAccountId)
        {
            var sql = @"IF EXISTS (SELECT 1 FROM ExternalAccounts WHERE BankId=@BankId AND ExternalCustomerAccountId=@ExternalAccountId)
                            SELECT ExternalAccountId FROM ExternalAccounts WHERE BankId=@BankId AND ExternalCustomerAccountId=@ExternalAccountId;
                        ELSE
                            INSERT INTO ExternalAccounts(BankId, ExternalCustomerAccountId) VALUES(@BankId, @ExternalAccountId);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using var command = new SqlCommand(sql, transaction.Connection, transaction);
            command.Parameters.Add("@BankId", System.Data.SqlDbType.Int).Value = bankId;
            command.Parameters.Add("@ExternalAccountId", System.Data.SqlDbType.NVarChar).Value = externalAccountId;
            return (int)command.ExecuteScalar();
        }
    }
}

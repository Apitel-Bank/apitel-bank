using System.Data.SqlClient;

namespace BankPartnerService.Repositories
{
    public class TransactionsRepository
    {
        public int AddAccountTransaction(SqlTransaction transaction, int customerIdNumber, int debit, int credit, string reference, int otherPartyId, int initialStatus)
        {
            var sql = @"DECLARE @TransactionId INT;
                        DECLARE @AccountId INT;

                        SET @AccountId = (
                            SELECT AccountId FROM Accounts
                            INNER JOIN Customers on Customers.CustomerId = Accounts.CustomerId
                            WHERE Customers.BBDoughId = @CustomerIdNumber
                        );

                        INSERT INTO AccountTransactions(AccountId, DebitInMibiBBDough, CreditInMibiBBDough, Reference, OtherPartyId)
                           VALUES(@AccountId, @Debit, @Credit, @Reference, @OtherPartyId);
                        SET @TransactionId = SCOPE_IDENTITY();
                        INSERT INTO AccountTransactionStatusProgressions(AccountTransactionId, AccountTransactionStatusId)
                           VALUES(@TransactionId, @InitialStatusId);
                        SELECT @TransactionId;";

            using var command = new SqlCommand(sql, transaction.Connection, transaction);
            command.Parameters.Add("@CustomerIdNumber", System.Data.SqlDbType.Int).Value = customerIdNumber;
            command.Parameters.Add("@Debit", System.Data.SqlDbType.Int).Value = debit;
            command.Parameters.Add("@Credit", System.Data.SqlDbType.Int).Value = credit;
            command.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = reference;
            command.Parameters.Add("@OtherPartyId", System.Data.SqlDbType.Int).Value = otherPartyId;
            command.Parameters.Add("@InitialStatusId", System.Data.SqlDbType.Int).Value = initialStatus;

            return (int)command.ExecuteScalar();
        }

        public void ProgressTransactionStatus(SqlTransaction transaction, int transactionId, int newStatusId)
        {
            var sql = @"EXEC dbo.ProgressTransactionStatus @TransactionId, @NextStatusId;";
            using var command = new SqlCommand(sql, transaction.Connection, transaction);
            command.Parameters.Add("@TransactionId", System.Data.SqlDbType.Int).Value = transactionId;
            command.Parameters.Add("@NextStatusId", System.Data.SqlDbType.Int).Value = newStatusId;
            command.ExecuteNonQuery();
        }
    }
}

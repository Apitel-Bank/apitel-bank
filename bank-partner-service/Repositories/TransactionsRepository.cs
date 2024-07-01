using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace BankPartnerService.Repositories
{
    public class TransactionsRepository(Db db)
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

        public void ProgressTransactionStatuByReference(SqlTransaction transaction, string reference, int newStatusId)
        {
            var sql = @"DECLARE @TransactionId INT;
                        SET @TransactionId = (SELECT AccountTransactionId FROM AccountTransactions WHERE Reference=@Reference);
                        EXEC dbo.ProgressTransactionStatus @TransactionId, @NextStatusId;";
            using var command = new SqlCommand(sql, transaction.Connection, transaction);
            command.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = reference;
            command.Parameters.Add("@NextStatusId", System.Data.SqlDbType.Int).Value = newStatusId;
            command.ExecuteNonQuery();
        }

        public void MarkAsReversedOrRejected(SqlTransaction transaction, string reference, int rejectionErrorCode, string rejectionReason, int reversedOrRejectedStatusId)
        {
            var sql = @"DECLARE @TransactionId INT;
                        SET @TransactionId = (SELECT AccountTransactionId FROM AccountTransactions WHERE Reference=@Reference);
                        DECLARE @StatusProgressionId INT;
                        EXEC @StatusProgressionId = dbo.ProgressTransactionStatus @TransactionId, @NextStatusId;
                        INSERT INTO AccountTransactionRejectionReasons(TransactionErrorCode, RejectionReason, AccountTransactionStatusProgressionId)
                            VALUES(@TransactionErrorCode, @RejectionReason, @StatusProgressionId);";

            var command = new SqlCommand( sql, transaction.Connection, transaction);
            command.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = reference;
            command.Parameters.Add("@TransactionErrorCode", System.Data.SqlDbType.Int).Value = rejectionErrorCode;
            command.Parameters.Add("@RejectionReason", System.Data.SqlDbType.NVarChar).Value = rejectionReason;
            command.Parameters.Add("@NextStatusId", System.Data.SqlDbType.Int).Value = reversedOrRejectedStatusId;
            command.ExecuteNonQuery();
        }

        public int GetStatus(string reference)
        {
            var sql = @"SELECT AccountTransactionStatusId from AccountTransactionWithActiveStatus WHERE Reference=@Reference;";
            using var command = new SqlCommand(sql, db.Connection);
            command.Parameters.Add("@Reference", System.Data.SqlDbType.NVarChar).Value = reference;
            var status = command.ExecuteScalar();

            if(status is DBNull)
            {
                throw new KeyNotFoundException("Could not find transaction with given reference");
            } else
            {
                return (int)status;
            }
        }
    }
}

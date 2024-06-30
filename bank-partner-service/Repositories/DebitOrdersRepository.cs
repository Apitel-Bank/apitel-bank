using BankPartnerService.Models;
using System.Data.SqlClient;


namespace BankPartnerService.Repositories
{
    public class DebitOrdersRepository(Db db)
    {
       public CreateDebitOrderResponse AddDebitOrder(CreateDebitOrderRequest request)
       { 
           string findAccountQuery = @"
            SELECT a.AccountId
            FROM Customers c
            JOIN Accounts a ON c.CustomerId = a.CustomerId
            WHERE c.BBDoughId = @PersonaId";

           using var getAccountId = new SqlCommand(findAccountQuery, db.Connection);
           getAccountId.Parameters.AddWithValue("@PersonaId", request.PersonaId);
           var accountId = getAccountId.ExecuteScalar();

           string findExternalAccountIdQuery = @"
            SELECT ExternalAccountId
            FROM ExternalAccounts
            WHERE BankId = @BankId AND ExternalCustomerAccountId = @AccountId";

            using var getExternalAccountId = new SqlCommand(findExternalAccountIdQuery, db.Connection);
            getExternalAccountId.Parameters.AddWithValue("@BankId", request.Recepient.BankId);
            getExternalAccountId.Parameters.AddWithValue("@AccountId", request.Recepient.AccountId);
            var externalAccountId = getExternalAccountId.ExecuteScalar();

            //TODO: Validation that the BankId matches one we have
            //TODO: Validation that the externalAccountId exists at the external bank/our bank?
            if (externalAccountId == null)
            {
                // If we don't have the externalAccountId then add external account
                string insertExternalAccountQuery = @"
                 INSERT INTO ExternalAccounts (BankId, ExternalCustomerAccountId)
                 VALUES (@BankId, @AccountId);
                 SELECT SCOPE_IDENTITY();";

                using var insertExternalAccountCommand = new SqlCommand(insertExternalAccountQuery, db.Connection);
                insertExternalAccountCommand.Parameters.AddWithValue("@BankId", request.Recepient.BankId);
                insertExternalAccountCommand.Parameters.AddWithValue("@AccountId", request.Recepient.AccountId);
                externalAccountId = insertExternalAccountCommand.ExecuteScalar();

            }

            string findDebitOrderRecepientQuery = @"
             SELECT DebitOrderRecipientId
             FROM DebitOrderRecipients
             WHERE ExternalAccountId = @ExternalAccountId";

            using var getDebitOrderRecepientId = new SqlCommand(findDebitOrderRecepientQuery, db.Connection);
            getDebitOrderRecepientId.Parameters.AddWithValue("@ExternalAccountId", externalAccountId);
            var debitOrderRecepientId = getDebitOrderRecepientId.ExecuteScalar();

            if (debitOrderRecepientId == null)
            {
                // If no recipient exists, add the recipient
                string insertDebitOrderRecepientQuery = @"
                 INSERT INTO DebitOrderRecipients (ExternalAccountId)
                 VALUES (@ExternalAccountId);
                 SELECT SCOPE_IDENTITY();";

                using var insertDebitOrderRecepientCommand = new SqlCommand(insertDebitOrderRecepientQuery, db.Connection);
                insertDebitOrderRecepientCommand.Parameters.AddWithValue("@ExternalAccountId", externalAccountId);
                debitOrderRecepientId = insertDebitOrderRecepientCommand.ExecuteScalar();   
            }


            string insertDebitOrderQuery = @"
            INSERT INTO DebitOrders (AmountInMibiBBDough, AccountId, DayInTheMonth, EndsAt, DebitOrderRecipientId)
            VALUES (@AmountInMibiBBDough, @AccountId, @DayInTheMonth, @EndsAt, @DebitOrderRecipientId);
            SELECT SCOPE_IDENTITY();";

            using var insertDebitOrderCommand = new SqlCommand(insertDebitOrderQuery, db.Connection);
            insertDebitOrderCommand.Parameters.AddWithValue("@AmountInMibiBBDough", request.AmountInMibiBBDough);
            insertDebitOrderCommand.Parameters.AddWithValue("@AccountId", accountId);
            insertDebitOrderCommand.Parameters.AddWithValue("@DayInTheMonth", request.DayInMonth);
            insertDebitOrderCommand.Parameters.AddWithValue("@EndsAt", request.EndsAt);
            insertDebitOrderCommand.Parameters.AddWithValue("@DebitOrderRecipientId", debitOrderRecepientId);
            var debitOrderId = insertDebitOrderCommand.ExecuteScalar();

            CreateDebitOrderResponse response = new CreateDebitOrderResponse(Convert.ToInt32(debitOrderId));
            return response;
        }

        public int CancelDebitOrder(int debitOrderId) {

            //TODO: Only edit debit orders you created

            string editDebitOrderQuery = @"
            UPDATE DebitOrders
            SET CancelledAt = GETDATE()
            WHERE DebitOrderId = @debitOrderId;
            SELECT SCOPE_IDENTITY();";

            using var editDebitOrderCommand = new SqlCommand(editDebitOrderQuery, db.Connection);
            editDebitOrderCommand.Parameters.AddWithValue("@debitOrderId", debitOrderId);
            var test = editDebitOrderCommand.ExecuteScalar();

            //TODO: return the id?
            return 0;
        }
    }

   
}
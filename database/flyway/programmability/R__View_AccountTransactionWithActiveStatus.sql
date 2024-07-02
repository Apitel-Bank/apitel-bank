CREATE OR ALTER VIEW AccountTransactionWithActiveStatus
AS
  SELECT AccountTransactions.AccountTransactionId,
         AccountTransactions.AccountId,
         AccountTransactions.DebitInMibiBBDough,
         AccountTransactions.CreditInMibiBBDough,
         AccountTransactions.Reference,
         AccountTransactions.OtherPartyId,
         StatusHistory.AccountTransactionStatusId
  FROM AccountTransactions
  INNER JOIN (
    SELECT atsp.AccountTransactionStatusProgressionId, atsp.AccountTransactionStatusId, atsp.AccountTransactionId,
          ROW_NUMBER() OVER (PARTITION BY AccountTransactionId ORDER BY AccountTransactionStatusProgressionId DESC) AS RowNumber
          FROM AccountTransactionStatusProgressions atsp
  ) StatusHistory ON StatusHistory.AccountTransactionId=AccountTransactions.AccountTransactionId AND StatusHistory.RowNumber = 1


CREATE OR ALTER PROCEDURE ProgressTransactionStatus
@TransactionId INT,
@NextStatusId INT
AS
  DECLARE @CurrentStatus INT;
  SET @CurrentStatus = (SELECT AccountTransactionStatusId FROM AccountTransactionWithActiveStatus WHERE AccountTransactionId=@TransactionId);
  IF EXISTS (SELECT 1 FROM AllowedAccountTransactionStatusProgression WHERE FromStatusId=@CurrentStatus AND ToStatusId=@NextStatusId)
  BEGIN
    INSERT INTO AccountTransactionStatusProgressions(AccountTransactionId, AccountTransactionStatusId)
      VALUES(@TransactionId, @NextStatusId);

    RETURN SCOPE_IDENTITY();
  END
  ELSE
  BEGIN;
    THROW 51000, 'Cannot progress the account transaction to the give status', 1;
  END
GO

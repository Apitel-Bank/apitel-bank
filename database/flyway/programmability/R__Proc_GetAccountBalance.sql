CREATE OR ALTER PROCEDURE GetBalance
@CustomerIdNumber INT
AS
  DECLARE @Balance INT;
  WITH VisibleOrVerifiedTransactionStatusIds AS (
    SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description IN ('Visible', 'Verified')
  )
  SELECT SUM(atwas.CreditInMibiBBDough) - SUM(atwas.DebitInMibiBBDough) FROM AccountTransactionWithActiveStatus atwas
    INNER JOIN Accounts ON Accounts.AccountId = atwas.AccountId
    INNER JOIN Customers ON Customers.CustomerId = Accounts.CustomerId
    INNER JOIN VisibleOrVerifiedTransactionStatusIds vvtsi ON vvtsi.AccountTransactionStatusId = atwas.AccountTransactionStatusId
    WHERE Customers.BBDoughId = @CustomerIdNumber;

  RETURN;
GO

CREATE TABLE AccountTransactionStatuses (
  AccountTransactionStatusId INT PRIMARY KEY IDENTITY(1, 1),
  Description VARCHAR(31)
)
GO

ALTER TABLE AccountTransactionStatusProgressions
ADD CONSTRAINT FK_AccountTransactionStatusProgressions_AccountTransactionStatusId
  FOREIGN KEY (AccountTransactionStatusId) REFERENCES AccountTransactionStatuses(AccountTransactionStatusId)
GO

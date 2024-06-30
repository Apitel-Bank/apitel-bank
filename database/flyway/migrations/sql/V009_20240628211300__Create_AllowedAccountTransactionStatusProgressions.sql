CREATE TABLE AllowedAccountTransactionStatusProgression (
  AllowedAccountTransactionStatusProgressionId INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
  FromStatusId INT NOT NULL,
  ToStatusId INT NOT NULL
)
GO

ALTER TABLE AllowedAccountTransactionStatusProgression
ADD CONSTRAINT FK_AllowedAccountTransactionStatusProgression_FromStatusId
  FOREIGN KEY (FromStatusId) REFERENCES AccountTransactionStatuses(AccountTransactionStatusId)
GO

ALTER TABLE AllowedAccountTransactionStatusProgression
ADD CONSTRAINT FK_AllowedAccountTransactionStatusProgression_ToStatusId
  FOREIGN KEY (ToStatusId) REFERENCES AccountTransactionStatuses(AccountTransactionStatusId)
GO

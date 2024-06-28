CREATE TABLE DebitOrderTransactions (
  [DebitOrderTransactionId] INT PRIMARY KEY IDENTITY(1, 1),
   DebitOrderId INT NOT NULL,
   AccountTransactionId INT NOT NULL,
)
GO

ALTER TABLE [DebitOrderTransactions] ADD FOREIGN KEY ([DebitOrderId]) REFERENCES [DebitOrders] ([DebitOrderId])
GO

ALTER TABLE [DebitOrderTransactions] ADD FOREIGN KEY ([AccountTransactionId]) REFERENCES [AccountTransactions] ([AccountTransactionId])
GO


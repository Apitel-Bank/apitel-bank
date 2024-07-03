ALTER TABLE AccountTransactions
DROP CONSTRAINT CHK_AccountTransactions_DebitInMibiBBDough;
GO

ALTER TABLE AccountTransactions
DROP CONSTRAINT CHK_AccountTransactions_CreditInMibiBBDough;
GO

ALTER TABLE AccountTransactions
ALTER COLUMN DebitInMibiBBDough BIGINT NOT NULL
GO

ALTER TABLE AccountTransactions
ALTER COLUMN CreditInMibiBBDough BIGINT NOT NULL
GO

ALTER TABLE DebitOrders
ALTER COLUMN AmountInMibiBBDough BIGINT NOT NULL
GO

ALTER TABLE AccountTransactions
ADD CONSTRAINT CHK_AccountTransactions_DebitInMibiBBDough CHECK (DebitInMibiBBDough >= 0)
GO

ALTER TABLE AccountTransactions
ADD CONSTRAINT CHK_AccountTransactions_CreditInMibiBBDough CHECK (CreditInMibiBBDough >= 0)
GO

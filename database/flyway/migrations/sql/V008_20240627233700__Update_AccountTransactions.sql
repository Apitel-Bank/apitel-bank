ALTER TABLE AccountTransactions
ADD CONSTRAINT CHK_AccountTransactions_DebitInMibiBBDough CHECK (DebitInMibiBBDough >= 0)
GO

ALTER TABLE AccountTransactions
ADD CONSTRAINT CHK_AccountTransactions_CreditInMibiBBDough CHECK (CreditInMibiBBDough >= 0)
GO

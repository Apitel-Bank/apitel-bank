ALTER TABLE AccountTransactions
ADD CONSTRAINT UC_AccountTransactions_Reference UNIQUE (Reference)
GO

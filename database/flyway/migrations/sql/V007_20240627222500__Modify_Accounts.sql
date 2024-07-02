-- Adding this constraint because a customer can only have account
ALTER TABLE Accounts
ADD CONSTRAINT UC_Accounts_CustomerId UNIQUE (CustomerId)
GO

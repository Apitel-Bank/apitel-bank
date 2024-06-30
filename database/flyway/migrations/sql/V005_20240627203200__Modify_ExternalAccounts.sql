ALTER TABLE ExternalAccounts
ADD CONSTRAINT FK_ExternalAccounts_BankId FOREIGN KEY (BankId) REFERENCES Banks(BankId)
GO

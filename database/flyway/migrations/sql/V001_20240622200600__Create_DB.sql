CREATE TABLE [Users] (
  [UserId] INT PRIMARY KEY IDENTITY(1, 1),
  [DisplayName] NVARCHAR[256],
  [DeletedAt] DATETIME,
  [DeletedBy] INT
)
GO

CREATE TABLE [Customers] (
  [CustomerId] INT PRIMARY KEY IDENTITY(1, 1),
  [UserId] INT,
  [BBDoughId] INT,
  [DeletedAt] DATETIME,
  [DeletedBy] INT
)
GO

CREATE TABLE [Accounts] (
  [AccountId] INT PRIMARY KEY IDENTITY(1, 1),
  [CustomerId] INT,
  [Name] NVARCHAR[256]
)
GO

CREATE TABLE [AccountTransactions] (
  [AccountTransactionId] INT PRIMARY KEY IDENTITY(1, 1),
  [AccountId] INT,
  [DebitInMibiBBDough] INT,
  [CreditInMibiBBDough] INT,
  [Reference] NVARCHAR[512],
  [OtherPartyId] INT
)
GO

CREATE TABLE [ExternalAccounts] (
  [ExternalAccountId] INT PRIMARY KEY IDENTITY(1, 1),
  [BankId] INT,
  [ExternalCustomerAccountId] NVARCHAR[512]
)
GO

CREATE TABLE [AccountTransactionStatusProgressions] (
  [AccountTransactionStatusProgressionId] INT PRIMARY KEY IDENTITY(1, 1),
  [AccountTransactionId] INT,
  [AccountTransactionStatusId] INT
)
GO

CREATE TABLE [AccountTransactionRejectionReasons] (
  [AccountTransactionRejectionReasonId] INT PRIMARY KEY IDENTITY(1, 1),
  [AccountTransactionStatusProgressionId] INT,
  [TransactionErrorCode] INT
)
GO

CREATE TABLE [DebitOrders] (
  [DebitOrderId] INT PRIMARY KEY IDENTITY(1, 1),
  [AmountInMibiBBDough] INT,
  [AccountId] INT,
  [DayInTheMonth] INT,
  [EndsAt] DATETIME,
  [DebitOrderRecipientId] INT
)
GO

CREATE TABLE [DebitOrderRecipients] (
  [DebitOrderRecipientId] INT PRIMARY KEY IDENTITY(1, 1),
  [ExternalAccountId] INT
)
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([DeletedBy]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Customers] ADD FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Customers] ADD FOREIGN KEY ([DeletedBy]) REFERENCES [Users] ([UserId])
GO

ALTER TABLE [Accounts] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId])
GO

ALTER TABLE [AccountTransactions] ADD FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId])
GO

ALTER TABLE [AccountTransactions] ADD FOREIGN KEY ([OtherPartyId]) REFERENCES [ExternalAccounts] ([ExternalAccountId])
GO

ALTER TABLE [AccountTransactionStatusProgressions] ADD FOREIGN KEY ([AccountTransactionId]) REFERENCES [AccountTransactions] ([AccountTransactionId])
GO

ALTER TABLE [AccountTransactionRejectionReasons] ADD FOREIGN KEY ([AccountTransactionStatusProgressionId]) REFERENCES [AccountTransactionStatusProgressions] ([AccountTransactionStatusProgressionId])
GO

ALTER TABLE [DebitOrders] ADD FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId])
GO

ALTER TABLE [DebitOrders] ADD FOREIGN KEY ([DebitOrderRecipientId]) REFERENCES [DebitOrderRecipients] ([DebitOrderRecipientId])
GO

ALTER TABLE [DebitOrderRecipients] ADD FOREIGN KEY ([ExternalAccountId]) REFERENCES [ExternalAccounts] ([ExternalAccountId])
GO

ALTER TABLE DebitOrders
ADD CONSTRAINT chk_day_in_month CHECK (DayInTheMonth BETWEEN 1 AND 30);
GO
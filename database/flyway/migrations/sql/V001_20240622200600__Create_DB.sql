CREATE TABLE [Users] (
  [UserId] INT PRIMARY KEY IDENTITY(1, 1),
  [DisplayName] NVARCHAR(256) NOT NULL,
  [DeletedAt] DATETIME NULL,
  [DeletedBy] INT NULL
)
GO


CREATE TABLE [Customers] (
  [CustomerId] INT PRIMARY KEY IDENTITY(1, 1),
  [UserId] INT NOT NULL,
  [BBDoughId] INT NOT NULL,
  [DeletedAt] DATETIME NULL,
  [DeletedBy] INT NULL
)
GO

CREATE TABLE [Accounts] (
  [AccountId] INT PRIMARY KEY IDENTITY(1, 1),
  [CustomerId] INT NOT NULL,
  [Name] NVARCHAR(256)
)
GO

CREATE TABLE [AccountTransactions] (
  [AccountTransactionId] INT PRIMARY KEY IDENTITY(1, 1),
  [AccountId] INT NOT NULL,
  [DebitInMibiBBDough] INT,
  [CreditInMibiBBDough] INT,
  [Reference] NVARCHAR(512),
  [OtherPartyId] INT
)
GO

CREATE TABLE [ExternalAccounts] (
  [ExternalAccountId] INT PRIMARY KEY IDENTITY(1, 1),
  [BankId] INT NOT NULL,
  [ExternalCustomerAccountId] NVARCHAR(512) NOT NULL,
)
GO

CREATE TABLE [AccountTransactionStatusProgressions] (
  [AccountTransactionStatusProgressionId] INT PRIMARY KEY IDENTITY(1, 1),
  [AccountTransactionId] INT NOT NULL,
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
  [AmountInMibiBBDough] INT NOT NULL,
  [AccountId] INT NOT NULL,
  [DayInTheMonth] INT NOT NULL,
  [EndsAt] DATETIME NULL,
  [CancelledAt] DATETIME NULL,
  [DebitOrderRecipientId] INT NOT NULL
)
GO

CREATE TABLE [DebitOrderRecipients] (
  [DebitOrderRecipientId] INT PRIMARY KEY IDENTITY(1, 1),
  [ExternalAccountId] INT NOT NULL
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
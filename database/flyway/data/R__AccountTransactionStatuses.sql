WITH
  StatusesData
  AS
  (
    SELECT 'New' AS Description
    UNION
      SELECT 'Accepted'
    UNION
      SELECT 'Rejected'
    UNION
      SELECT 'Verified'
    UNION
      SELECT 'Reversed'
    UNION
      SELECT 'Visible'        
  )

MERGE INTO AccountTransactionStatuses AS Target
USING StatusesData AS Source
ON Target.Description = Source.Description
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Description) VALUES (Source.Description)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
OUTPUT $action, INSERTED.*, DELETED.*;
GO

With
  AllowedAccountTransactionStatusProgressionData
  AS (
    SELECT (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='New') AS FromStatusId,
            (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Accepted') AS ToStatusId
    UNION
    SELECT (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Accepted') AS FromStatusId,
            (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Visible') AS ToStatusId    
    UNION
    SELECT (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Visible') AS FromStatusId,
            (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Verified') AS ToStatusId 
    UNION
    SELECT (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Visible') AS FromStatusId,
            (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Reversed') AS ToStatusId 
    UNION
    SELECT (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='New') AS FromStatusId,
            (SELECT AccountTransactionStatusId FROM AccountTransactionStatuses WHERE Description='Rejected') AS ToStatusId                         
  )

MERGE INTO AllowedAccountTransactionStatusProgression AS Target
USING AllowedAccountTransactionStatusProgressionData AS Source
ON Target.FromStatusId = Source.FromStatusId AND Target.ToStatusId = Source.ToStatusId
WHEN NOT MATCHED BY TARGET THEN
  INSERT (FromStatusId, ToStatusId) VALUES (Source.FromStatusId, Source.ToStatusId)
WHEN NOT MATCHED BY Source THEN
  DELETE
OUTPUT $action, INSERTED.*, DELETED.*;
GO

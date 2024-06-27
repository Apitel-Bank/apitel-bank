WITH
  BanksData
  AS
  (
    SELECT N'Commercial Bank' AS BankName
    UNION
      SELECT N'Apitel Retail Bank'
  )

MERGE INTO Banks AS Target
USING BanksData AS Source
ON Target.BankName = Source.BankName
WHEN NOT MATCHED BY TARGET THEN
    INSERT (BankName) VALUES (Source.BankName)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
OUTPUT $action, INSERTED.*, DELETED.*;
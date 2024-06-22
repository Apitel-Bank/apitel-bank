WITH
  UsersData
  AS
  (
          SELECT N'Joe' AS DisplayName
    UNION
      SELECT N'Andy'
  )

MERGE INTO Users AS Target
USING UsersData AS Source
ON Target.DisplayName = Source.DisplayName
WHEN NOT MATCHED BY TARGET THEN
    INSERT (DisplayName) VALUES (Source.DisplayName)
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
OUTPUT $action, INSERTED.*, DELETED.*;
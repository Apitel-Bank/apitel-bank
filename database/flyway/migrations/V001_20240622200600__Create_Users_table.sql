CREATE TABLE Users
(
  UserId INT IDENTITY(1, 1) PRIMARY KEY,
  DisplayName NVARCHAR(256) NOT NULL,
  DeletedAt DATETIME,
  DeletedBy INT,

  CONSTRAINT fk_Users_DeletedBy FOREIGN KEY (DeletedBy) REFERENCES Users(UserId)
)

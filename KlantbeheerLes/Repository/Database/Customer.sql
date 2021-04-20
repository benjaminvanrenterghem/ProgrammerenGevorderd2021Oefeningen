CREATE TABLE [dbo].[Customer]
(
	[CustomerID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(500) NOT NULL, -- N staat voor unicode; varchar betekent string is net zo lang als de inhoud
    [Address] NVARCHAR(1500) NOT NULL
)
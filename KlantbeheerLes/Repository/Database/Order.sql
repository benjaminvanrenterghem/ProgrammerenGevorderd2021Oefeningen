CREATE TABLE [dbo].[Order] -- dbo is default db user
(
	[OrderID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Date] DATETIME2 NOT NULL, 
    [CustomerID] INT NULL, 
    [CostPrice] FLOAT NULL, 
    [IsPaid] BIT NOT NULL,
    CONSTRAINT [FK_Order_Customer] FOREIGN KEY ([CustomerID]) REFERENCES [Customer]([CustomerID])
)
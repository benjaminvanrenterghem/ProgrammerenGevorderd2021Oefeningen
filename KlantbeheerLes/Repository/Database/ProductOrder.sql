CREATE TABLE [dbo].[ProductOrder] -- n:m tabel: tussentabel
(
    [OrderID] INT NOT NULL, 
	[ProductID] INT NOT NULL,
	[AmountOfProduct] INT NOT NULL,
	PRIMARY KEY(ProductID, OrderID),
    CONSTRAINT [FK_ProductOrder_Product] FOREIGN KEY ([ProductID]) REFERENCES [Product]([ProductID]),
	CONSTRAINT [FK_ProductOrder_Order] FOREIGN KEY ([OrderID]) REFERENCES [Order]([OrderID])
)
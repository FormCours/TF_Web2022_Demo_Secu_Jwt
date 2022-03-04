CREATE TABLE [dbo].[Product]
(
	[Product_Id] INT NOT NULL IDENTITY,
 -- [Product_Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWSEQUENTIALID(),
	[Name] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(1000),
	[Price] MONEY NOT NULL,
	[Quantity] INT NOT NULL DEFAULT 0,
	[Category_Id] INT NOT NULL,
	[Create_Date] DATETIME2 NOT NULL DEFAULT GETDATE(),
	[Update_Date] DATETIME2 NULL,
	CONSTRAINT PK_Product 
		PRIMARY KEY ([Product_Id]),
	CONSTRAINT FK_Product_Category 
		FOREIGN KEY ([Category_Id]) 
		REFERENCES [Category]([Category_Id])
)

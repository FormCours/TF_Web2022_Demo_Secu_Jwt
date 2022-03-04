CREATE TABLE [dbo].[Category]
(
	[Category_Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(20) NOT NULL,
	CONSTRAINT PK_Category PRIMARY KEY([Category_Id])
);

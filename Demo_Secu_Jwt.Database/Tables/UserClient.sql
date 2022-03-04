CREATE TABLE [dbo].[UserClient]
(
	[UserClient_Id] INT NOT NULL IDENTITY,
	[Pseudo] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(250) NOT NULL,
	[Password_Hash] NCHAR(60) NOT NULL,
	CONSTRAINT PK_UserClient PRIMARY KEY ([UserClient_Id]),
	CONSTRAINT UK_UserClient_Email UNIQUE([Email])
)
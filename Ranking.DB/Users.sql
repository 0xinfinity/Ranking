CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nickname] NVARCHAR(MAX) NOT NULL, 
    [ClanId] INT NULL, 
    [UserType] INT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [IsBlocked] BIT NOT NULL, 
    [IsDeleted] BIT NULL, 
    [LastActivityDate] DATETIME NOT NULL
)

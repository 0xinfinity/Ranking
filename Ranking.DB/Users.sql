CREATE TABLE [dbo].[Users]
(
	[UserId] INT NOT NULL PRIMARY KEY IDENTITY,
	[Nickname] NVARCHAR(400) NOT NULL, 
    [ClanId] INT NULL, 
    [UserType] INT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [IsBlocked] BIT NOT NULL, 
    [IsDeleted] BIT NULL, 
    [LastActivityDate] DATETIME NOT NULL, 
    [OnlineTime] INT NOT NULL
)

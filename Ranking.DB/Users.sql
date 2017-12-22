CREATE TABLE [dbo].[Users]
(
	[Nickname] NVARCHAR(400) NOT NULL PRIMARY KEY, 
    [ClanId] INT NULL, 
    [UserType] INT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [IsBlocked] BIT NOT NULL, 
    [IsDeleted] BIT NULL, 
    [LastActivityDate] DATETIME NOT NULL, 
    [OnlineTime] INT NOT NULL
)

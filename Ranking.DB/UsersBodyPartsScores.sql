CREATE TABLE [dbo].[UsersBodyPartsScores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Nickname] NVARCHAR(400) NOT NULL, 
    [BodyPartId] INT NOT NULL, 
    [KillsCount] INT NOT NULL, 
    [DeathsCount] INT NOT NULL
)

CREATE TABLE [dbo].[UsersBodyPartsScores]
(
	[UserId] INT NOT NULL PRIMARY KEY, 
    [BodyPartId] INT NOT NULL, 
    [KillsCount] INT NOT NULL, 
    [DeathsCount] INT NOT NULL
)

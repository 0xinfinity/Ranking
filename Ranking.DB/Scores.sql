CREATE TABLE [dbo].Scores
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [WeaponId] INT NOT NULL, 
    [BodyPartId] INT NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [IsSuicide] BIT NOT NULL, 
    [IsTeamKill] BIT NOT NULL, 
    [IsSpawnKill] BIT NOT NULL, 
    [KilledUserId] INT NULL, 
    [IsDeleted] BIT NOT NULL
)

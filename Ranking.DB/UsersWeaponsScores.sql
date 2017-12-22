CREATE TABLE [dbo].[UsersWeaponsScores]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Nickname] NVARCHAR(400) NOT NULL , 
    [WeaponId] INT NOT NULL, 
    [KillsCount] INT NOT NULL, 
    [DeathsCount] INT NOT NULL
)

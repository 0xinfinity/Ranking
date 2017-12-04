CREATE TABLE [dbo].[UsersWeaponsScores]
(
	[UserId] INT NOT NULL PRIMARY KEY, 
    [WeaponId] INT NOT NULL, 
    [KillsCount] INT NOT NULL, 
    [DeathsCount] INT NOT NULL
)

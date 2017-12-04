CREATE TABLE [dbo].[Clans]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NULL, 
    [Tag] NVARCHAR(MAX) NULL, 
    [Color] NVARCHAR(MAX) NULL, 
    [IsBlocked] BIT NULL, 
    [IsDeleted] BIT NULL
)

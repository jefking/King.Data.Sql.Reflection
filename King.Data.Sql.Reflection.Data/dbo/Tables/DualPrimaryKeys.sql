CREATE TABLE [dbo].[DualPrimaryKeys]
(
	[FirstId] INT NOT NULL , 
    [SecondId] INT NOT NULL, 
    [SomeData] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(), 
    PRIMARY KEY ([SecondId], [FirstId])
)

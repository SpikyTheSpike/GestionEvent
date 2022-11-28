CREATE TABLE [dbo].[Event]
(
	[Event_Id] INT NOT NULL IDENTITY,
    [Nom] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(500) ,
    [DateDebut] DateTime2  NOT NULL,
    [DateFin] DateTime2  NOT NULL,
    [Photo] NVARCHAR(500) NOT NULL,
    [LimitePersonne] Int,
    [CreateAt] DATETIME2 DEFAULT GETDATE(),
    [ModifiedAt]DATETIME2 DEFAULT GETDATE(),

    CONSTRAINT PK_Event PRIMARY KEY ([Event_Id]),
    
)

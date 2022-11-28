CREATE TABLE [dbo].[Member]
(
    [Member_Id] INT NOT NULL IDENTITY,
    [Email] NVARCHAR(250) NOT NULL,
    [Pseudo] NVARCHAR(50) NOT NULL,
    [Pwd_Hash] CHAR(97) NOT NULL,
    [FirstName]  NVARCHAR(50) ,
    [LastName] NVARCHAR(50),
    [BirthDate] DateTime2,

    CONSTRAINT PK_Member PRIMARY KEY ([Member_Id]),
    CONSTRAINT Uk_Member_Email UNIQUE ([Email]),
    CONSTRAINT UK_Member_Pseudo UNIQUE ([Pseudo])
)

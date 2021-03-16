CREATE TABLE [dbo].[student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [naam] NVARCHAR(50) NULL, 
    [klasId] INT NOT NULL, 
    CONSTRAINT [FK_student_klas] FOREIGN KEY ([klasId]) REFERENCES [klas]([Id])
)

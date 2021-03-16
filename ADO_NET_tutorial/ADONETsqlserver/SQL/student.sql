CREATE TABLE [dbo].[studentSQL]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [naam] NVARCHAR(50) NULL, 
    [klasId] INT NOT NULL, 
    CONSTRAINT [FK_student_klasSQL] FOREIGN KEY ([klasId]) REFERENCES [klasSQL]([Id])
)

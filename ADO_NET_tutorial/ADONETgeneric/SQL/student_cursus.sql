CREATE TABLE [dbo].[student_cursus]
(
	[cursusId] INT NOT NULL , 
    [studentId] INT NOT NULL, 
    PRIMARY KEY ([cursusId], [studentId]), 
    CONSTRAINT [FK_student_cursus_cursus] FOREIGN KEY ([cursusId]) REFERENCES [cursus]([id]), 
    CONSTRAINT [FK_student_cursus_student] FOREIGN KEY ([studentId]) REFERENCES [student]([id])
)

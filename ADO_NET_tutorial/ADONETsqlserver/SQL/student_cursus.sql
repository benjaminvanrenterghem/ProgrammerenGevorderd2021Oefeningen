CREATE TABLE [dbo].[student_cursusSQL]
(
	[cursusId] INT NOT NULL , 
    [studentId] INT NOT NULL, 
    PRIMARY KEY ([cursusId], [studentId]), 
    CONSTRAINT [FK_student_cursus_cursusSQL] FOREIGN KEY ([cursusId]) REFERENCES [cursusSQL]([id]), 
    CONSTRAINT [FK_student_cursus_studentSQL] FOREIGN KEY ([studentId]) REFERENCES [studentSQL]([id])
)

USE SurveyModel
GO
INSERT 
INTO Questions (Text, SurveyId_Id, QuestionType)
VALUES ('New Question', 1, 0);

INSERT INTO Answers (Text, QuestionId_Id)
VALUES ('Good Response', @@IDENTITY), ('Bad Response', @@IDENTITY), ('Very Bad Response', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, SurveyId_Id, QuestionType)
VALUES ('New Second Question', 1, 1);

INSERT INTO Answers (Text, QuestionId_Id)
VALUES ('Ugly Response', @@IDENTITY), ('Ugly Response 2', @@IDENTITY), ('Ugly', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, SurveyId_Id, QuestionType)
VALUES ('New Third Question', 1, 2);

INSERT INTO Answers (Text, QuestionId_Id)
VALUES ('Good Response', @@IDENTITY);
GO
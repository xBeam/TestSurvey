USE SurveyModel
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('New Question', 2, 0);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Good Response', @@IDENTITY), ('Bad Response', @@IDENTITY), ('Very Bad Response', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('New Second Question', 2, 1);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Ugly Response', @@IDENTITY), ('Ugly Response 2', @@IDENTITY), ('Nice', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('New Third Question', 2, 2);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Type answer here', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('Is it very Important Question?', 2, 1);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Yes', @@IDENTITY), ('No', @@IDENTITY), ('I doubt', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('Choose your favorite color(s)', 2, 0);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Yellow', @@IDENTITY), ('Green', @@IDENTITY), ('Pink', @@IDENTITY), ('Black', @@IDENTITY);
GO

INSERT 
INTO Questions (Text, Survey_Id, QuestionType)
VALUES ('Write your biggest fear in work:', 2, 2);
INSERT INTO Answers (Text, Question_Id)
VALUES ('Type answer here', @@IDENTITY);
GO
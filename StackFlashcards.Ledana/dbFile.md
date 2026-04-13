CREATE TABLE Stack (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    [NAME] NVARCHAR (25) NOT NULL UNIQUE
);

CREATE TABLE Flashcard (
Id INT IDENTITY (1,1) PRIMARY KEY,
Front NVARCHAR(MAX) NOT NULL,
Back NVARCHAR(MAX) NOT NULL,
StackId INT NOT NULL,
CONSTRAINT FK_Flashcard_Stack FOREIGN KEY (StackId) REFERENCES Stack(Id) ON DELETE CASCADE
);

CREATE TABLE StudySessions (
Id INT PRIMARY KEY IDENTITY (1,1),
[Date] DATE NOT NULL,
Score NVARCHAR(25) NOT NULL,
StackId INT NOT NULL,
CONSTRAINT FK_StudySessions_Stack FOREIGN KEY (StackId) REFERENCES Stack(Id) ON DELETE CASCADE
);

 INSERT INTO Stack([Name])
VALUES ('Biology');

INSERT INTO Flashcard (Front, Back, StackId)
VALUES ('What does the heart do?', 'The heart pumps blood into all our body and brain', 1),
('What does liver do?', 'Liver stores sugar, fat and carbs for us', 1),
('what do lungs do?', 'Lungs take oxygen and tranforms it into monoxyde carbon', 1);

INSERT INTO StudySessions ([Date], Score, StackId)
VALUES ('2026-04-01', '2/3', 1),
('2026-04-04', '3/3', 1);


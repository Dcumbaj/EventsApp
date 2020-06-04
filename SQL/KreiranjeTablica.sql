IF OBJECT_ID('dbo.EventParticipatns', 'U') IS NOT NULL
DROP TABLE dbo.EventParticipatns

IF OBJECT_ID('dbo.Events', 'U') IS NOT NULL
DROP TABLE dbo.Events

IF OBJECT_ID('dbo.Participants', 'U') IS NOT NULL
DROP TABLE dbo.Participants

IF OBJECT_ID('dbo.Locations', 'U') IS NOT NULL
DROP TABLE dbo.Locations

IF OBJECT_ID('dbo.Cities', 'U') IS NOT NULL
DROP TABLE dbo.Cities
------------------------------------------------------------------------------
CREATE TABLE Cities(
CityID INT NOT NULL IDENTITY(1,1),
Name VARCHAR(255) NOT NULL 
PRIMARY KEY (CityID)
)
------------------------------------------------------------------------------
CREATE TABLE Participants(
ParticipantsID INT NOT NULL IDENTITY(1,1),
First_Name VARCHAR(50) NOT NULL,
Last_Name VARCHAR(50) NOT NULL,
Adress VARCHAR(255),
CityID INT NOT NULL
PRIMARY KEY (ParticipantsID),
FOREIGN KEY (CityID) REFERENCES Cities(CityID)
)
------------------------------------------------------------------------------
CREATE TABLE Locations(
LocationID INT NOT NULL IDENTITY(1,1),
Name VARCHAR(50) NOT NULL,
Adress VARCHAR(255),
CityID INT NOT NULL
PRIMARY KEY (LocationID),
FOREIGN KEY (CityID) REFERENCES Cities(CityID)
)
------------------------------------------------------------------------------
CREATE TABLE Events(
EventID INT NOT NULL IDENTITY(1,1),
Name VARCHAR(255) NOT NULL,
Ongoing BIT NOT NULL, -- 1 == ONGOING  0 == NOT ONGOING
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
LocationID INT NOT NULL
PRIMARY KEY (EventID),
FOREIGN KEY (LocationID) REFERENCES Locations(LocationID)
)
------------------------------------------------------------------------------
CREATE TABLE EventParticipatns(
EventParticipantsID INT NOT NULL IDENTITY(1,1),
CityID INT NOT NULL,
EventID INT NOT NULL
PRIMARY KEY (EventParticipantsID),
FOREIGN KEY (CityID) REFERENCES Cities(CityID),
FOREIGN KEY (EventID) REFERENCES Events(EventID) 
)

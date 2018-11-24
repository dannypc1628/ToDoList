Create Table DoList
(
	ID int PRIMARY KEY IDENTITY (1,1),
	Owner_ID int NOT NULL,
	Title nvarchar(50) NOT NULL,
	Detail nvarchar(255),
	Completed bit,
	Tag nvarchar(50),
	Create_Date datetime ,
	Completed_Date datetime 
)
Create Table Users
(
	ID  int PRIMARY KEY IDENTITY (10000000,1),
	UserText char(15) UNIQUE NOT NULL,
	Name nvarchar(10) NOT NULL,
	Password  char(20) NOT NULL,
	Phone char(20),
	Email char(30)
)
Create Table Team
(
	ID int PRIMARY KEY  IDENTITY (20000000,1),
	Name nvarchar(10) NOT NULL
)
Create Table Team_Users
(
	Team_ID  int REFERENCES Team(ID),
	User_ID  int REFERENCES Users(ID)
)
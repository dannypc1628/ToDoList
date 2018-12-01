Create Table ToDoList
(
	ID int PRIMARY KEY IDENTITY (1,1),
	Owner_ID int NOT NULL,
	Title nvarchar(50) NOT NULL,
	Detail nvarchar(255),
	Completed bit,
	Create_Date datetime ,
	Completed_Date datetime 
)
Create Table Users
(
	ID  int PRIMARY KEY IDENTITY (100000,1),
	Account char(15) UNIQUE NOT NULL,
	Name nvarchar(10) NOT NULL,
	Password  char(20) NOT NULL,
	Phone char(20),
	Email char(30)
)
Create Table Project
(
	ID int PRIMARY KEY  IDENTITY (500000,1),
	Name nvarchar(10) NOT NULL
)
Create Table Project_Users
(
	Team_ID  int REFERENCES Project(ID),
	User_ID  int REFERENCES Users(ID)
)
Create Table Color
(
	ID int PRIMARY KEY IDENTITY (1,1),
	Color_Name nvarchar(50) NOT NULL 
	
)
ALTER TABLE ToDoList  ADD Color_ID int REFERENCES Color(ID)
Insert into dbo.Color ('Color_Name') Values('all')
Insert into dbo.Color ('Color_Name') Values('primary')
Update dbo.ToDoList set Color_ID =1
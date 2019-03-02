CREATE VIEW ToDoList_view  
AS   
SELECT          dbo.ToDoList.ID, dbo.ToDoList.Owner_ID, dbo.ToDoList.Title, dbo.ToDoList.Detail, dbo.ToDoList.Completed, 
                            dbo.ToDoList.Create_Date, dbo.ToDoList.Completed_Date, dbo.ToDoList.Deleted, dbo.Color.Color_Name
FROM              dbo.ToDoList INNER JOIN
                            dbo.Color ON dbo.ToDoList.Color_ID = dbo.Color.ID 
GO 

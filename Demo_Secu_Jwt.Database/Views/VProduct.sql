CREATE VIEW [dbo].[VProduct]
	AS 
SELECT P.Product_Id, P.Name, P.Description, P.Price, P.Quantity, 
	   P.Create_Date, P.Update_Date, C.Name AS [Category]
FROM Product P 
	LEFT JOIN Category C ON P.Category_Id = C.Category_Id;

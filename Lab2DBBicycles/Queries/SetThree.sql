SELECT Bicycles.Name
FROM Bicycles
WHERE Bicycles.Year = Count1
AND NOT EXISTS
	(SELECT *
	FROM (SELECT * FROM Stores WHERE (SELECT COUNT(*) FROM Sales WHERE StoreId = Stores.Id) = Count2) AS Stores
	WHERE NOT EXISTS
		(SELECT *
		FROM Sales
		WHERE BicycleId = Bicycles.Id AND StoreId = Stores.Id))
	AND EXISTS (SELECT * FROM Stores WHERE (SELECT COUNT(*) FROM Sales WHERE StoreId = Stores.Id) = Count2)
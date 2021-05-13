SELECT Stores.Name
FROM Stores
WHERE NOT EXISTS
	(SELECT Bicycles.Id
	FROM Bicycles
	WHERE Bicycles.Price >= Price1 AND Price <= Price2
	AND NOT EXISTS
		(SELECT *
		FROM Sales
		WHERE BicycleId = Bicycles.Id AND StoreId = Stores.Id))
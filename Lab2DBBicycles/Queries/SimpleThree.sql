SELECT COUNT(*)
FROM Stores
WHERE (SELECT COUNT(*)
		FROM Brands
		WHERE Brands.Id IN
			(SELECT Bicycles.BrandId
			FROM Bicycles
			WHERE EXISTS
				(SELECT *
				FROM Sales 
				WHERE Sales.BicycleId = Bicycles.Id 
					AND Sales.StoreId = Stores.Id))) > StoreCount
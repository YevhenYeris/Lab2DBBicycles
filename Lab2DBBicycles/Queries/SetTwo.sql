SELECT Brands.Name
FROM Brands
WHERE NOT EXISTS
	(SELECT *
	FROM Bicycles
	WHERE Bicycles.BrandId = Brands.Id AND NOT EXISTS
		(SELECT *
		FROM Sales
		WHERE BicycleId = Bicycles.Id AND
			(SELECT Stores.Name
			FROM Stores
			WHERE Stores.Id = StoreId) = 'Store1'))
	AND NOT EXISTS
		(SELECT *
		FROM Bicycles
		WHERE Bicycles.BrandId = Brands.Id AND Price <= (SELECT AVG(Price)
		FROM Bicycles
		WHERE Bicycles.Id IN
			(SELECT BicycleId
			FROM Sales
			WHERE StoreId IN
				(SELECT Stores.Id
				FROM Stores
				WHERE Stores.Name = 'Store2'))))
SELECT Stores.Name
FROM Stores
WHERE Stores.Id IN
	(SELECT StoreId
	FROM Sales
	WHERE Sales.BicycleId IN
		(SELECT Bicycles.Id
		FROM Bicycles
		WHERE Bicycles.BrandId IN
			(SELECT Brands.Id
			FROM Brands
			WHERE Brands.CountryId IN
				(SELECT Countries.Id
				FROM Countries
				WHERE Countries.Name = 'CountryA'))))
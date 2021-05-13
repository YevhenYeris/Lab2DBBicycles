SELECT Brands.Name, AVG(Price)
FROM Brands INNER JOIN Bicycles ON BrandId = Brands.Id
	GROUP BY Brands.Name
	HAVING AVG(Price) >
		(SELECT AVG(Price)
		FROM (Bicycles INNER JOIN Sales
		ON Bicycles.Id = BicycleId) INNER JOIN
				Stores ON StoreId = Stores.Id
					AND Stores.Name = 'StoreA')
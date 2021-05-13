SELECT Bicycles.Name
FROM Bicycles
WHERE Bicycles.BrandId IN
	(SELECT Brands.Id
	FROM (Brands INNER JOIN Bicycles ON BrandId = Brands.Id)
	GROUP BY Brands.Id
	HAVING SUM(Price)  > PriceNumber)
AND Bicycles.BrandId NOT IN
	(SELECT BrandId
	FROM (Bicycles INNER JOIN Sales 
				ON BicycleId = Bicycles.Id)
				INNER JOIN Stores ON StoreId = Stores.Id
							AND Stores.Name = 'StoreA')
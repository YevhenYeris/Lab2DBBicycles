SELECT DISTINCT Bicycles.Name
FROM ((Bicycles INNER JOIN Sales ON
				Bicycles.Id = Sales.BicycleId)
				INNER JOIN Stores ON 
				(Stores.Id = StoreId AND Stores.Name = 'StoreA'))
WHERE Bicycles.Id NOT IN
			(SELECT Sales.BicycleId FROM
				(Sales INNER JOIN Stores ON
				(Sales.StoreId = Stores.Id AND Stores.Name = 'StoreB')))
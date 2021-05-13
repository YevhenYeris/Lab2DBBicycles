SELECT 'StoreA', AVG(Price)
		FROM (Bicycles INNER JOIN Sales
		ON Bicycles.Id = BicycleId) INNER JOIN
				Stores ON StoreId = Stores.Id
					AND Stores.Name = 'StoreA'
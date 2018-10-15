--Запрос, который выводит название товара,
--название типа и название производителя тех товаров,
--цена которых больше 5000
SELECT ITEM.Name, 
	(SELECT TYPE.Name 
	FROM TYPE
	WHERE TYPE.ID=ITEM.TypeId) AS Type,
	ITEM.Factory
FROM ITEM
WHERE ITEM.Price > 5000

--Запрос выводит минимальную цену всех товаров.(просто число)
SELECT MIN(PRICE) AS 'Min. price'
FROM ITEM

--Запрос выводит минимальную цену для каждого типа товара.
--(название типа, мин. цена)
SELECT TYPE.Name,
	(SELECT MIN(PRICE)
	FROM ITEM
	WHERE ITEM.TypeId=TYPE.ID) AS 'Min.price'
FROM TYPE

--Запрос выводит минимальную цену для каждого типа товара,
 --сумма цен которых больше 20000
SELECT TYPE.Name,
	(SELECT MIN(PRICE)
	FROM ITEM
	WHERE ITEM.TypeId=TYPE.ID) AS MinPrice
FROM TYPE 	
WHERE 20000<(SELECT SUM(PRICE)
			FROM ITEM 
			WHERE TYPE.ID=ITEM.TypeId)
			 
--Запрос выводит количество складов для каждого менеджера 
--(имя менеджера, количество)
SELECT 
	(SELECT MANAGER.Name
		FROM MANAGER 
		WHERE MANAGER.ID=WAREHOUSE.ManagerID) AS Name, 
	COUNT(MANAGER.Name) AS WarehousesHas
FROM WAREHOUSE
INNER JOIN MANAGER ON WAREHOUSE.ManagerID=MANAGER.ID
GROUP BY WAREHOUSE.ManagerID
  
--Запрос выводит менеджеров, чьи склады обслуживают более одного магазина.
--(имя менеджера)
SELECT 
	(SELECT SubMAN.Name 
	FROM MANAGER AS SubMAN 
	WHERE MAN.ID = SubMAN.ID) AS Name
FROM MANAGER AS MAN
JOIN WAREHOUSE ON WAREHOUSE.ManagerID=MAN.ID 
JOIN WarehousesShops ON WarehousesShops.WarehouseId=WAREHOUSE.ID
GROUP BY WAREHOUSE.ID, MAN.ID
HAVING COUNT(WAREHOUSE.ID)>1

--Вывести топ 3 менеджеров на чьих складах большее количество товаров.
SELECT TOP 3
	(SELECT SubMan.Name 
	FROM MANAGER AS SubMan 
	WHERE MAN.ID=SubMan.ID) AS Name
FROM MANAGER AS MAN
JOIN WAREHOUSE ON WAREHOUSE.ManagerID=MAN.ID
JOIN WarehousesProducts ON WarehousesProducts.WarehouseId=WAREHOUSE.ID
GROUP BY MAN.ID
ORDER BY SUM(WarehousesProducts.ItemsCount) DESC

--Увеличить на 1 количество всех товаров на складах
--с именем товара «FISTASHKA» и 
--которые обслуживают магазины с именем «Верный»

UPDATE WarehousesProducts
SET WarehousesProducts.ItemsCount=ItemsCount+1
FROM ITEM, WarehousesShops, SHOP
WHERE ITEM.Name='FISTASHKA' 
	AND ITEM.ID=WarehousesProducts.ItemId 
	AND SHOP.Name='Верный'
	and WarehousesShops.ShopId=SHOP.ID
	AND WarehousesProducts.WarehouseId=WarehousesShops.WarehouseId		

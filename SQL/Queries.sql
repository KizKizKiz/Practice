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
SELECT t.Name, i.minPrice
FROM TYPE as t	
join (
	select ITEM.TypeId, min(item.Price) MinPrice
	from ITEM
	group by item.TypeId
	having sum(item.price)>20000) as i 
	on i.TypeId = t.ID 

--Запрос выводит количество складов для каждого менеджера 
--(имя менеджера, количество)
SELECT m.*, ware.WarehousesHas
FROM MANAGER as m
join (
	select WAREHOUSE.ManagerID, count(WAREHOUSE.ManagerID) as WarehousesHas
	from WAREHOUSE
	group by WAREHOUSE.ManagerID) as ware
	on ware.ManagerID=m.ID

--Запрос выводит менеджеров, чьи склады обслуживают более одного магазина.
--(имя менеджера)
select man.*
from MANAGER as man
join (
	select ware.ManagerID
	from WAREHOUSE as ware
	join WarehousesShops on ware.ID=WarehousesShops.WarehouseId
	group by WarehousesShops.WarehouseId, ware.ManagerID
	having count(ware.ManagerID)>1) as w
	on w.ManagerID=man.ID	

--Вывести топ 3 менеджеров на чьих складах большее количество товаров.
select  man.*
from MANAGER as man
join (
	select top 3 sum(WarehousesProducts.ItemsCount) as Products, WAREHOUSE.ManagerID
	from WAREHOUSE 
	join WarehousesProducts on WAREHOUSE.ID=WarehousesProducts.WarehouseId
	group by WAREHOUSE.ManagerID) as ware
	on ware.ManagerID=man.ID

--Увеличить на 1 количество всех товаров на складах
--с именем товара «FISTASHKA» и 
--которые обслуживают магазины с именем «Верный»
UPDATE WarehousesProducts
SET WarehousesProducts.ItemsCount=ItemsCount+1
FROM WarehousesProducts
join ITEM on item.ID = WarehousesProducts.ItemId
join WarehousesShops on WarehousesShops.WarehouseId = WarehousesProducts.WarehouseId 
join SHOP on SHOP.ID = WarehousesShops.ShopId
where Item.Name='FISTASHKA' and SHOP.Name='Верный'
--������, ������� ������� �������� ������,
--�������� ���� � �������� ������������� ��� �������,
--���� ������� ������ 5000
SELECT ITEM.Name, 
	(SELECT TYPE.Name 
	FROM TYPE
	WHERE TYPE.ID=ITEM.TypeId) AS Type,
	ITEM.Factory
FROM ITEM
WHERE ITEM.Price > 5000

--������ ������� ����������� ���� ���� �������.(������ �����)
SELECT MIN(PRICE) AS 'Min. price'
FROM ITEM

--������ ������� ����������� ���� ��� ������� ���� ������.
--(�������� ����, ���. ����)
SELECT TYPE.Name,
	(SELECT MIN(PRICE)
	FROM ITEM
	WHERE ITEM.TypeId=TYPE.ID) AS 'Min.price'
FROM TYPE

--������ ������� ����������� ���� ��� ������� ���� ������,
 --����� ��� ������� ������ 20000
SELECT TYPE.Name,
	(SELECT MIN(PRICE)
	FROM ITEM
	WHERE ITEM.TypeId=TYPE.ID) AS MinPrice
FROM TYPE 	
WHERE 20000<(SELECT SUM(PRICE)
			FROM ITEM 
			WHERE TYPE.ID=ITEM.TypeId)

--������ ������� ���������� ������� ��� ������� ��������� 
--(��� ���������, ����������)
SELECT 
	(SELECT MANAGER.Name
		FROM MANAGER 
		WHERE MANAGER.ID=WAREHOUSE.ManagerID) AS Name, 
	COUNT(MANAGER.Name) AS WarehousesHas
FROM WAREHOUSE
INNER JOIN MANAGER ON WAREHOUSE.ManagerID=MANAGER.ID
GROUP BY WAREHOUSE.ManagerID

--������ ������� ����������, ��� ������ ����������� ����� ������ ��������.
--(��� ���������)
SELECT 
	(SELECT SubMAN.Name 
	FROM MANAGER AS SubMAN 
	WHERE MAN.ID = SubMAN.ID) AS Name
FROM MANAGER AS MAN
JOIN WAREHOUSE ON WAREHOUSE.ManagerID=MAN.ID 
JOIN WarehousesShops ON WarehousesShops.WarehouseId=WAREHOUSE.ID
GROUP BY WAREHOUSE.ID, MAN.ID
HAVING COUNT(WAREHOUSE.ID)>1

--������� ��� 3 ���������� �� ���� ������� ������� ���������� �������.
SELECT TOP 3
	(SELECT SubMan.Name 
	FROM MANAGER AS SubMan 
	WHERE MAN.ID=SubMan.ID) AS Name
FROM MANAGER AS MAN
JOIN WAREHOUSE ON WAREHOUSE.ManagerID=MAN.ID
JOIN WarehousesProducts ON WarehousesProducts.WarehouseId=WAREHOUSE.ID
GROUP BY MAN.ID
ORDER BY SUM(WarehousesProducts.ItemsCount) DESC

--��������� �� 1 ���������� ���� ������� �� �������
--� ������ ������ �FISTASHKA� � 
--������� ����������� �������� � ������ �������

UPDATE WarehousesProducts
SET WarehousesProducts.ItemsCount=ItemsCount+1
FROM ITEM, WarehousesShops, SHOP
WHERE ITEM.Name='FISTASHKA' 
	AND ITEM.ID=WarehousesProducts.ItemId 
	AND SHOP.Name='������'
	and WarehousesShops.ShopId=SHOP.ID
	AND WarehousesProducts.WarehouseId=WarehousesShops.WarehouseId		

IF NOT EXISTS
(
	SELECT name
	FROM sys.databases
	WHERE name = 'FurnitureFactoryDB'
)
BEGIN
	CREATE DATABASE FurnitureFactoryDB
END
GO
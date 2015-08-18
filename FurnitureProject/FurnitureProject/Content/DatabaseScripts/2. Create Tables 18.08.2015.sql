IF NOT EXISTS
(
	SELECT name
	FROM sys.tables
	WHERE name = 'Customers'
)
BEGIN
	CREATE TABLE Customers
	(
		ID int IDENTITY(0, 1) PRIMARY KEY,
		Bulstat int NOT NULL,
		MOL varchar(255) NOT NULL,
		Name varchar(255) NOT NULL,
		Address varchar(255) NOT NULL
	)
END
GO

IF NOT EXISTS
(
	SELECT name
	FROM sys.tables
	WHERE name = 'Products'
)
BEGIN
	CREATE TABLE Products
	(
		ID int IDENTITY(0, 1) PRIMARY KEY,
		Name varchar(255) NOT NULL,
		Price real NOT NULL,
		Weight int NOT NULL,
		Description varchar(255) NULL,
		Barcode int NULL
	)
END
GO

IF NOT EXISTS
(
	SELECT name
	FROM sys.tables
	WHERE name = 'Orders'
)
BEGIN
	CREATE TABLE Orders
	(
		ID int IDENTITY(0, 1) PRIMARY KEY,
		CustomerID int NOT NULL,
		Date date NULL,

		CONSTRAINT FK_ORD_CUST_ID FOREIGN KEY (CustomerID) REFERENCES Customers(ID)
	)
END
GO

IF NOT EXISTS
(
	SELECT name
	FROM sys.tables
	WHERE name = 'ProductOrders'
)
BEGIN
	CREATE TABLE ProductOrders
	(
		ID int IDENTITY(0, 1) PRIMARY KEY,
		ProductID int NOT NULL,
		OrderID int NOT NULL,
		Quantity int NOT NULL,

		CONSTRAINT FK_PO_PROD_ID FOREIGN KEY (ProductID) REFERENCES Products(ID),
		CONSTRAINT FK_PO_ORD_ID FOREIGN KEY (OrderID) REFERENCES Orders(ID)
	)
END
GO
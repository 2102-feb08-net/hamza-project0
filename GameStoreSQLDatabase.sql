
-- Creating all the tables

CREATE TABLE Customers(
	id INT IDENTITY PRIMARY KEY,
	first_name NVARCHAR(30) NOT NULL,
	last_name NVARCHAR(30) NOT NULL,
	user_name NVARCHAR(30) NOT NULL UNIQUE,
	city NVARCHAR(30) NOT NULL,
	state NVARCHAR(30) NOT NULL
);

CREATE TABLE Products(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(30) NOT NULL,
	price FLOAT NOT NULL
);

CREATE TABLE Locations(
	id INT IDENTITY PRIMARY KEY,
	city NVARCHAR(30) NOT NULL,
	state NVARCHAR(30) NOT NULL
);

CREATE TABLE Orders(
	id INT IDENTITY PRIMARY KEY,
	customer_id INT NOT NULL,
	time_placed DATETIME2 NOT NULL,
	total_price FLOAT NOT NULL,
	FOREIGN KEY (customer_id) REFERENCES Customers(id),
);

-- m-n Orders and Products
CREATE TABLE OrderLine(
	id INT IDENTITY PRIMARY KEY,
	order_id INT NOT NULL,
	product_id INT NOT NULL,
	FOREIGN KEY (order_id) REFERENCES Orders(id),
	FOREIGN KEY (product_id) REFERENCES Products(id)
);

-- m-n Locations and Products
CREATE TABLE LocationInventory(
	id INT IDENTITY PRIMARY KEY,
	location_id INT NOT NULL,
	product_id INT NOT NULL,
	FOREIGN KEY (location_id) REFERENCES Locations(id),
	FOREIGN KEY (product_id) REFERENCES Products(id)
);


-- Creating some data


INSERT INTO Customers(first_name, last_name, user_name, city, state) VALUES
	('Hamza', 'Butt', 'hamza012', 'Buffalo', 'New York'),
	('John', 'Doe', 'johnD213', 'Chicago', 'Illinois'),
	('Jane', 'Smith', 'jane999', 'Houston', 'Texas'),
	('Tina', 'White', 'tina657', 'Seattle', 'Washington');

INSERT INTO Products(name, price) VALUES
	('Call of Duty MW2', 20.00),
	('Minecraft', 26.95),
	('Super Smash Bros Ultimate', 60.00),
	('Dark Souls 3', 39.99);

INSERT INTO Locations(city, state) VALUES
	('Yonkers', 'New York'),
	('Philadelphia', 'Pennsylvania'),
	('Houston', 'Texas'),
	('Charlotte', 'North Carolina');

INSERT INTO Orders(customer_id, time_placed, total_price) VALUES
	(1, '2021-01-15T19:58:47', 86.95), -- Hamza bought MC and SSBU
	(1, '2021-02-07T13:37:21', 39.99), -- Hamza bought DS3
	(3, '2021-01-19T04:14:00', 46.95), -- Jane bought COD and MC
	(4, '2021-02-25T10:26:16', 86.94);  -- Tina bought COD, MC, DS3

INSERT INTO LocationInventory(location_id, product_id) VALUES
	(1, 2),
	(1, 3),  -- Yonkers has MC,SSBU,DS3
	(1, 4),
	(2, 1),
	(2, 2),	 -- Philadelphia has COD,MC,SSBU,DS3
	(2, 3),
	(2, 4),
	(3, 1),
	(3, 2),	 --	Houston has COD,MC,DS3
	(3, 4),
	(4, 1),
	(4, 3);  -- Charlotte has COD,SSBU

INSERT INTO OrderLine(order_id, product_id) VALUES
	(1, 2),		-- Hamza bought MC and SSBU
	(1, 3),
	(2, 4),		-- Hamza bought DS3
	(3, 1),
	(3, 2),		-- Jane bought COD and MC
	(4, 1),
	(4, 2),		-- Tina bought COD, MC, DS3
	(4, 4);


SELECT * FROM Customers;
SELECT * FROM Products;
SELECT * FROM Locations;
SELECT * FROM Orders;
SELECT * FROM LocationInventory;
SELECT * FROM OrderLine;









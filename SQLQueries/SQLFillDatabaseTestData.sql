INSERT INTO app.Product
VALUES
    ('Dice Set (Blue)', 7.00),
	('Dice Set (Red)', 7.00),
	('Dice Set (Metal Blue)', 10.00),
	('Dice Set (Metal Red)', 10.00),
    ('Pathfinder Core Rulebook', 49.99),
    ('Dungeon Masters Screen', 24.99),
	('Bard Figurine (Blank)', 15.00),
	('Bard Figurine (Painted)', 30.00),
	('Wizard Figurine (Blank)', 15.00),
	('Wizard Figurine (Painted)', 30.00),
	('Ranger Figurine (Blank)', 15.00),
	('Ranger Figurine (Painted)', 30.00),
	('Druid Figurine (Blank)', 15.00),
	('Druid Figurine (Painted)', 30.00),
	('Sorcerer Figurine (Blank)', 15.00),
	('Sorcerer Figurine (Painted)', 30.00),
	('Rogue Figurine (Blank)', 15.00),
	('Rogue Figurine (Painted)', 30.00),
	('Barbarian Figurine (Blank)', 15.00),
	('Barbarian Figurine (Painted)', 30.00),
	('Bestiary Figurine Pack 20 ct (Blank)', 90.00),
	('Bestiary Figurine Pack 20 ct (Painted)', 175.00);
INSERT INTO app.Customer
VALUES
    ('Kevin', 'McCallister', '671 Lincoln Ave', 'Winnetka', 'IL', '60093'),
    ('Long', 'Silvers', '700 W Abram St', 'Arlington', 'TX', '76013'),
    ('Nancy', 'Drew', '9909 Mystery Ave', 'Springfield', 'MO', '65619'),
	('Electra', 'Pleiades', '12 Scales St', 'Scaletip', 'AS', '77777');

-- More names for testing if needed
--('Phil','Young'),
--('Wendy','Lou'),
--('Susan','Barter'),
--('Gwenneth','Jones'),
--('Joe','Schmo');

INSERT INTO app.Store
VALUES
    ('99 Wayford Way', 'Arlington', 'TX', '76013'),
    ('420 Chill Place', 'Denver', 'CO', '80239'),
    ('99 Whammy Drive', 'Cena', 'WI', '84847'),
    ('2020 Warehouse Way', 'Loveland', 'CO', '80631');
INSERT INTO app.Orders
VALUES
    (1,1),
    (2,2),
    (3,3),
	(1,2),
	(2,3);
INSERT INTO app.OrderProduct
VALUES
    (1, 1, 1),
    (1, 2, 2),
    (1, 3, 3),
    (2, 1, 1),
    (2, 2, 2),
    (3, 3, 3),
    (4, 1, 1),
    (5, 2, 2);
INSERT INTO app.InventoryProduct
VALUES
    (1, 1, 20),
    (1, 2, 25),
    (1, 3, 15),
    (1, 4, 30),
    (1, 5, 20),
    (1, 9, 25),
    (1, 12, 15),
    (1, 15, 10);
INSERT INTO app.InventoryProduct
VALUES
    (2, 3, 20),
    (2, 6, 25),
    (2, 7, 15),
    (2, 8, 30),
    (2, 12, 20),
    (2, 15, 25),
    (2, 17, 15),
    (2, 22, 10);
INSERT INTO app.InventoryProduct
VALUES
    (3, 3, 20),
    (3, 4, 25),
    (3, 5, 15),
    (3, 8, 30),
    (3, 13, 20),
    (3, 16, 25),
    (3, 19, 15),
    (3, 20, 10);
INSERT INTO app.InventoryProduct
VALUES
    (4, 1, 30),
    (4, 2, 30),
    (4, 3, 30),
    (4, 4, 30),
    (4, 5, 30),
    (4, 6, 30),
    (4, 7, 30),
    (4, 8, 30),
    (4, 9, 30),
    (4, 10, 30),
    (4, 11, 30),
    (4, 12, 30),
    (4, 13, 30),
    (4, 14, 30),
    (4, 15, 30),
    (4, 16, 30),
    (4, 17, 30),
    (4, 18, 30),
    (4, 19, 30),
    (4, 20, 30),
    (4, 21, 30),
    (4, 22, 30);

INSERT INTO app.Manager
VALUES
    (1, 'John', 'Johnston'),
    (2, 'Man', 'Manston'),
    (3, 'Big', 'Largeston'),
    (4, 'Cave', 'Johnson');

SELECT *
FROM app.Customer;

SELECT *
FROM app.Product

SELECT *
FROM app.InventoryProduct;

SELECT *
FROM app.Orders;

SELECT *
FROM app.OrderProduct;

SELECT *
FROM app.Store;

SELECT *
FROM app.Manager;

SELECT *
FROM app.Store;

SELECT *
FROM app.InventoryProduct;

SELECT *
FROM app.Orders;

SELECT *
FROM app.OrderProduct;

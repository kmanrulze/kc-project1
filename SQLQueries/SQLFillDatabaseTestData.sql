INSERT INTO app.Product
VALUES
    ('Dice Set (Pathfinder)', 7.00),
    ('Pathfinder Core Rulebook', 49.99),
    ('Dungeon Masters Screen', 24.99);
INSERT INTO app.Customer
VALUES
    ('Kevin', 'McCallister', '671 Lincoln Ave', 'Winnetka', 'IL', 60093),
    ('Long', 'Silvers', '700 W Abram St', 'Arlington', 'TX', 76013),
    ('Nancy', 'Drew', '9909 Mystery Ave', 'Springfield', 'MO', '65619');

-- More names for testing if needed
--('Phil','Young'),
--('Wendy','Lou'),
--('Susan','Barter'),
--('Gwenneth','Jones'),
--('Joe','Schmo');

INSERT INTO app.Store
VALUES
    ('99 Wayford Way', 'Arlington', 'TX', 76013),
    ('420 Chill Place', 'Denver', 'CO', 80239),
    ('99 Whammy Drive', 'Cena', 'WI', 84847);
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
    (2, 1, 30),
    (2, 2, 20),
    (2, 3, 25),
    (3, 1, 15),
    (3, 2, 10);
INSERT INTO app.Manager
VALUES
    (1, 'John', 'Johnston'),
    (2, 'Man', 'Manston'),
    (3, 'Big', 'Largeston');

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

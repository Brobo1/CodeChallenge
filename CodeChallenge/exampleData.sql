﻿DELETE
FROM OrderProducts;
DELETE
FROM WishlistProducts;
DELETE
FROM Ratings;
DELETE
FROM Orders;
DELETE
FROM Wishlists;
DELETE
FROM Products;
DELETE
FROM Categories;
DELETE
FROM Customers;

DELETE
FROM sqlite_sequence
WHERE name = 'OrderProducts';
DELETE
FROM sqlite_sequence
WHERE name = 'WishlistProducts';
DELETE
FROM sqlite_sequence
WHERE name = 'Ratings';
DELETE
FROM sqlite_sequence
WHERE name = 'Orders';
DELETE
FROM sqlite_sequence
WHERE name = 'Wishlists';
DELETE
FROM sqlite_sequence
WHERE name = 'Products';
DELETE
FROM sqlite_sequence
WHERE name = 'Categories';
DELETE
FROM sqlite_sequence
WHERE name = 'Customers';

-- Insert into Customers
INSERT INTO Customers (Name, Email)
VALUES ('John Doe', 'john.doe@example.com'),
       ('Jane Doe', 'jane.doe@example.com'),
       ('Alice Smith', 'alice.smith@example.com'),
       ('Bob Johnson', 'bob.johnson@example.com'),
       ('Charlie Brown', 'charlie.brown@example.com'),
       ('David Williams', 'david.williams@example.com');

INSERT INTO Categories (Name)
VALUES ('Computers'),
       ('Phones'),
       ('TV & video'),
       ('Audio'),
       ('Camera & Photo'),
       ('Wearables'),
       ('Appliances'),
       ('Office Electronics'),
       ('Gaming'),
       ('Electronic Components');

-- Products for 'Computers & Accessories'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Laptop', 1000, 1, 'High performance laptop for work and play'),
       ('Desktop', 800, 1, 'Powerful desktop computer for home or office use'),
       ('Monitor', 200, 1, 'High resolution monitor for clear display'),
       ('Keyboard', 50, 1, 'Ergonomic keyboard for comfortable typing'),
       ('Mouse', 20, 1, 'Wireless mouse with high precision');

-- Products for 'Cell Phones & Accessories'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Smartphone', 700, 2, 'Latest model smartphone with advanced features'),
       ('Phone Case', 15, 2, 'Durable phone case for protection'),
       ('Charger', 20, 2, 'Fast charging adapter for quick power up'),
       ('Headphones', 100, 2, 'Noise cancelling headphones for immersive sound'),
       ('Screen Protector', 10, 2, 'Scratch resistant screen protector');

-- Products for 'TV & Video'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('LED TV', 500, 3, 'High definition LED TV for a clear viewing experience'),
       ('Smart TV', 600, 3, 'Smart TV with internet connectivity and apps'),
       ('Home Theater System', 800, 3, 'Complete home theater system for an immersive audio-visual experience'),
       ('TV Stand', 100, 3, 'Sturdy TV stand with storage space'),
       ('TV Remote', 20, 3, 'Universal TV remote with programmable buttons');

-- Products for 'Audio & Home Theater'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Speakers', 200, 4, 'High quality speakers for clear and loud audio'),
       ('Sound Bar', 150, 4, 'Compact sound bar with surround sound capability'),
       ('Subwoofer', 100, 4, 'Powerful subwoofer for deep and rich bass'),
       ('AV Receiver', 250, 4, 'AV receiver with multiple input and output options'),
       ('Projector', 500, 4, 'HD projector for big screen viewing at home');

-- Products for 'Camera & Photo'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('DSLR Camera', 1200, 5, 'High resolution DSLR camera for professional photography'),
       ('Camera Lens', 500, 5, 'Versatile camera lens for different shooting conditions'),
       ('Tripod', 100, 5, 'Sturdy tripod for stable shooting'),
       ('Memory Card', 30, 5, 'High capacity memory card for storing photos and videos'),
       ('Camera Bag', 50, 5, 'Durable camera bag for protecting your equipment');

-- Products for 'Wearable Technology'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Smart Watch', 300, 6, 'Smart watch with fitness tracking and notifications'),
       ('Fitness Tracker', 100, 6, 'Fitness tracker for monitoring your health and activity'),
       ('VR Headset', 400, 6, 'VR headset for immersive gaming and virtual experiences'),
       ('Wireless Earbuds', 150, 6, 'Wireless earbuds with high quality sound'),
       ('Smart Glasses', 500, 6, 'Smart glasses with augmented reality features');

-- Products for 'Appliances'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Refrigerator', 1200, 7, 'Refrigerator with large capacity and low energy consumption'),
       ('Microwave Oven', 250, 7, 'Microwave oven for quick and efficient cooking'),
       ('Washing Machine', 800, 7, 'Washing machine with efficient cleaning and durable performance'),
       ('Blender', 50, 7, 'Blender for smooth blending performance'),
       ('Coffee Maker', 40, 7, 'Coffee maker for perfect brew every time');

-- Products for 'Office Electronics'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Printer', 100, 8, 'Printer for printing documents and photos'),
       ('Scanner', 80, 8, 'Scanner for digitizing documents and photos'),
       ('Shredder', 60, 8, 'Shredder for disposing of sensitive documents'),
       ('Fax Machine', 150, 8, 'Fax machine for sending and receiving documents'),
       ('Projector', 400, 8, 'Projector for presentations and meetings');

-- Products for 'Video Game Consoles & Accessories'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Game Console', 400, 9, 'Game console for playing the latest video games'),
       ('Controller', 50, 9, 'Controller for precise game control'),
       ('Headset', 80, 9, 'Headset for in-game audio and communication'),
       ('Game', 60, 9, 'Video game for entertainment'),
       ('Console Stand', 30, 9, 'Console stand for organizing your game console and accessories');

-- Products for 'Electronic Components'
INSERT INTO Products (Name, Price, CategoryId, Description)
VALUES ('Motherboard', 200, 10, 'Motherboard for connecting and communicating between all computer components'),
       ('Processor', 300, 10, 'Processor for executing instructions and running software'),
       ('RAM', 100, 10, 'RAM for temporary storage and quick access to data'),
       ('Hard Drive', 80, 10, 'Hard drive for long-term storage of data'),
       ('Graphics Card', 400, 10, 'Graphics card for rendering images and video');

-- Insert into Orders
INSERT INTO Orders (CustomerId, Date)
VALUES (1, '2022-01-01');

-- Insert into OrderProducts
INSERT INTO OrderProducts (ProductId, OrderId)
VALUES (1, 1);

-- Insert into Ratings
INSERT INTO Ratings (CustomerId, ProductId, Stars)
VALUES (1, 1, 5);

-- Insert into Wishlists
INSERT INTO Wishlists (CustomerId)
VALUES (1);

-- Insert into WishlistProducts
INSERT INTO WishlistProducts (WishlistId, ProductId)
VALUES (1, 1);


with tree as
         (select *
          from Categories
          union all
          select c.id, c.categoryid, c.name
          from Categories c 
          where CategoryId =
                (select Id
                 from Categories
                 where Categories.Name = 'Laptops'))
select *
from tree;

with tree AS
         (select *
          from Categories
          where Name = 'Laptops'
          union all
          select c.id, c.categoryid, c.name
          from Categories c
                   inner join tree ct ON c.CategoryId = ct.Id)
select *
from tree;

with CategoryTree AS (select Id, CategoryId, Name
                      from Categories
                      where Name = 'Gaming'
                      union all
                      select c.Id, c.CategoryId, c.Name
                      from Categories c
                               inner join CategoryTree ct ON c.Id = ct.CategoryId)
select *
from CategoryTree;


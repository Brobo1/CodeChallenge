-- Insert into Customers
INSERT INTO Customers (Name, Email)
VALUES ('John Doe', 'john.doe@example.com'),
       ('Jane Doe', 'jane.doe@example.com'),
       ('Alice Smith', 'alice.smith@example.com'),
       ('Bob Johnson', 'bob.johnson@example.com'),
       ('Charlie Brown', 'charlie.brown@example.com'),
       ('David Williams', 'david.williams@example.com');

INSERT INTO Category (Name)
VALUES ('Computers & Accessories'),
       ('Cell Phones & Accessories'),
       ('TV & Video'),
       ('Audio & Home Theater'),
       ('Camera & Photo'),
       ('Wearable Technology'),
       ('Car Electronics & GPS'),
       ('Office Electronics'),
       ('Video Game Consoles & Accessories'),
       ('Electronic Components');

-- Products for 'Computers & Accessories'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Laptop', 1000, 1),
       ('Desktop', 800, 1),
       ('Monitor', 200, 1),
       ('Keyboard', 50, 1),
       ('Mouse', 20, 1);

-- Products for 'Cell Phones & Accessories'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Smartphone', 700, 2),
       ('Phone Case', 15, 2),
       ('Charger', 20, 2),
       ('Headphones', 100, 2),
       ('Screen Protector', 10, 2);

-- Products for 'TV & Video'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('LED TV', 500, 3),
       ('Smart TV', 600, 3),
       ('Home Theater System', 800, 3),
       ('TV Stand', 100, 3),
       ('TV Remote', 20, 3);

-- Products for 'Audio & Home Theater'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Speakers', 200, 4),
       ('Sound Bar', 150, 4),
       ('Subwoofer', 100, 4),
       ('AV Receiver', 250, 4),
       ('Projector', 500, 4);

-- Products for 'Camera & Photo'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('DSLR Camera', 1200, 5),
       ('Camera Lens', 500, 5),
       ('Tripod', 100, 5),
       ('Memory Card', 30, 5),
       ('Camera Bag', 50, 5);

-- Products for 'Wearable Technology'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Smart Watch', 300, 6),
       ('Fitness Tracker', 100, 6),
       ('VR Headset', 400, 6),
       ('Wireless Earbuds', 150, 6),
       ('Smart Glasses', 500, 6);

-- Products for 'Car Electronics & GPS'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Car Stereo', 200, 7),
       ('GPS Navigator', 150, 7),
       ('Dash Cam', 100, 7),
       ('Car Charger', 20, 7),
       ('Car Mount', 15, 7);

-- Products for 'Office Electronics'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Printer', 100, 8),
       ('Scanner', 80, 8),
       ('Shredder', 60, 8),
       ('Fax Machine', 150, 8),
       ('Projector', 400, 8);

-- Products for 'Video Game Consoles & Accessories'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Game Console', 400, 9),
       ('Controller', 50, 9),
       ('Headset', 80, 9),
       ('Game', 60, 9),
       ('Console Stand', 30, 9);

-- Products for 'Electronic Components'
INSERT INTO Products (Name, Price, CategoryId)
VALUES ('Motherboard', 200, 10),
       ('Processor', 300, 10),
       ('RAM', 100, 10),
       ('Hard Drive', 80, 10),
       ('Graphics Card', 400, 10);


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
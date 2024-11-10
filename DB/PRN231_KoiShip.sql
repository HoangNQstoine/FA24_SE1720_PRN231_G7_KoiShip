USE [master]
GO

IF DB_ID('KoiShip_DB') IS NOT NULL
BEGIN
    --DROP DATABASE [SWD392_DB]
    ALTER DATABASE [KoiShip_DB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [KoiShip_DB];
END
GO
CREATE DATABASE [KoiShip_DB]
GO

USE [KoiShip_DB]
GO

CREATE TABLE [Role] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(50) NULL,
    [Status] BIT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [User] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    UserName NVARCHAR(50) NULL,
    [Password] NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    DOB DATE NULL,
    [Address] NVARCHAR(100) NULL,
    Phone_Number NVARCHAR(50) NULL,
    [Role_Id] INT NULL,
    [Gender] NVARCHAR(50) NULL,
    [ImgURL] NVARCHAR(MAX) NULL,
    [Status] INT NULL,
FOREIGN KEY (Role_Id) REFERENCES [Role]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Pricing] 
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Quantity] INT NULL,
    [weight_range] NVARCHAR(MAX) NULL,
    [shipping_method] NVARCHAR(MAX) NULL,
    [Description] NVARCHAR(MAX) NULL,
    [price] FLOAT NULL, 
    [Currency] NVARCHAR(MAX) NULL,
    [EffectiveDate] DATETIME NULL,
    [ExpiryDate] DATETIME NULL,
    [Status] BIT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [Category] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(100) NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Url_IMG] NVARCHAR(MAX) NULL, 
    [Status] BIT NULL, 
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [koiFish] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [User_Id] INT NULL,
    [Category_Id] INT NULL,
    [Name] NVARCHAR(100) NULL,
    [Weight] FLOAT NULL,
    [Age] INT NULL,
    [ColorPattern] NVARCHAR(100) NULL,
    [Price] FLOAT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Url_IMG] NVARCHAR(MAX) NULL, 
    [Status] BIT NULL, 
FOREIGN KEY ([User_Id]) REFERENCES [User] ([Id]),
FOREIGN KEY ([Category_Id]) REFERENCES [Category]([Id]),
PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO

CREATE TABLE [ShipMent] 
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [User_Id] INT NULL,
    [vehicle] NVARCHAR(MAX) NULL,
    [EstimatedArrivalDate] DATETIME NULL,
    [Start_Date] DATETIME NULL,
    [End_Date] DATETIME NULL,
    [health_check] NVARCHAR(MAX) NULL,
    [Description] NVARCHAR(MAX) NULL,
    [Weight] INT NULL,
    [Status] BIT NULL,
    FOREIGN KEY ([User_Id]) REFERENCES [User]([Id])
) ON [PRIMARY];
GO

CREATE TABLE [ShippingOrders] 
(
    [Id] INT IDENTITY(1,1) PRIMARY KEY,
    [User_Id] INT NULL,
    [Pricing_Id] INT NULL,
    [ShipMent_Id] INT NULL,
    [Adress_To] NVARCHAR(MAX) NULL,
    [PhoneNumber] NVARCHAR(MAX) NULL,
    [Total_Price] INT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [OrderDate] DATETIME NULL,
    [ShippingDate] DATETIME NULL,
    [EstimatedDeliveryDate] DATETIME NULL,
    [Status] INT NULL,
    FOREIGN KEY ([User_Id]) REFERENCES [User]([Id]),
    FOREIGN KEY ([ShipMent_Id]) REFERENCES [ShipMent]([Id]),
    FOREIGN KEY ([Pricing_Id]) REFERENCES [Pricing]([Id])
) ON [PRIMARY];
GO

CREATE TABLE [ShippingOrderDetails] 
(
    Id INT IDENTITY(1,1) NOT NULL,
    [ShippingOrders_Id] INT NULL,
    [KoiFish_Id] INT UNIQUE NULL,
    [Quantity] INT NULL,
    [Status] BIT NULL,
    FOREIGN KEY ([KoiFish_Id]) REFERENCES [KoiFish] ([Id]),
    FOREIGN KEY ([ShippingOrders_Id]) REFERENCES [ShippingOrders] ([Id]),
    PRIMARY KEY CLUSTERED ([Id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
GO
INSERT INTO [Role] ([Name], [Status]) VALUES
('Admin', 1),
('User', 1),
('Manager', 1),
('Support', 1),
('Guest', 1);
INSERT INTO [User] ([UserName], [Password], [Email], [DOB], [Address], [Phone_Number], [Role_Id], [Gender], [ImgURL], [Status]) VALUES
('john_doe', 'password123', 'john.doe@example.com', '1985-03-15', '123 Main St', '555-1234', 1, 'Male', 'https://example.com/img/john.jpg', 1),
('jane_smith', 'securepass456', 'jane.smith@example.com', '1990-07-25', '456 Elm St', '555-5678', 2, 'Female', 'https://example.com/img/jane.jpg', 1),
('mark_taylor', 'pass7890', 'mark.taylor@example.com', '1982-11-10', '789 Oak St', '555-8765', 3, 'Male', 'https://example.com/img/mark.jpg', 1),
('lucy_brown', 'mypassword321', 'lucy.brown@example.com', '1995-05-30', '321 Pine St', '555-4321', 4, 'Female', 'https://example.com/img/lucy.jpg', 1),
('alice_white', 'strongpassword1', 'alice.white@example.com', '1987-02-20', '654 Maple St', '555-5679', 5, 'Female', 'https://example.com/img/alice.jpg', 1);
INSERT INTO [Category] ([Name], [Description], [Url_IMG], [Status]) VALUES
('Freshwater', 'Freshwater koi fish', 'https://example.com/img/freshwater.jpg', 1),
('Saltwater', 'Saltwater koi fish', 'https://example.com/img/saltwater.jpg', 1),
('Hybrid', 'Hybrid koi fish varieties', 'https://example.com/img/hybrid.jpg', 1),
('Exotic', 'Exotic koi fish species', 'https://example.com/img/exotic.jpg', 1),
('Miniature', 'Miniature koi fish', 'https://example.com/img/miniature.jpg', 1);
INSERT INTO [Pricing] ([Quantity], [weight_range], [shipping_method], [Description], [price], [Currency], [EffectiveDate], [ExpiryDate], [Status]) VALUES
(1, '0-1 kg', 'Standard Shipping', 'Basic pricing for small koi fish', 100.00, 'USD', '2024-01-01', '2024-12-31', 1),
(5, '1-5 kg', 'Express Shipping', 'Discounted pricing for bulk koi shipments', 450.00, 'USD', '2024-01-01', '2024-12-31', 1),
(10, '5-10 kg', 'Premium Shipping', 'Premium pricing for large koi shipments', 900.00, 'USD', '2024-01-01', '2024-12-31', 1),
(1, '0-1 kg', 'Overnight Shipping', 'Overnight delivery for small koi fish', 150.00, 'USD', '2024-01-01', '2024-12-31', 1),
(10, '10+ kg', 'Freight Shipping', 'Heavy-duty shipping for extra large koi', 2000.00, 'USD', '2024-01-01', '2024-12-31', 1);
INSERT INTO [koiFish] ([User_Id], [Category_Id], [Name], [Weight], [Age], [ColorPattern], [Price], [Description], [Url_IMG], [Status]) VALUES
(1, 1, 'Koi Fish A', 0.5, 2, 'Orange and White', 120.00, 'Beautiful koi with a vibrant orange and white pattern', 'https://example.com/img/koiA.jpg', 1),
(2, 2, 'Koi Fish B', 2.0, 3, 'Red and Black', 350.00, 'Bold red and black koi with a striking pattern', 'https://example.com/img/koiB.jpg', 1),
(3, 3, 'Koi Fish C', 4.0, 5, 'Yellow and Blue', 500.00, 'Unique yellow and blue koi with intricate patterns', 'https://example.com/img/koiC.jpg', 1),
(4, 4, 'Koi Fish D', 0.8, 1, 'White and Black', 150.00, 'Rare koi with a beautiful white and black color pattern', 'https://example.com/img/koiD.jpg', 1),
(5, 5, 'Koi Fish E', 0.2, 1, 'Orange', 100.00, 'Small and elegant koi fish in bright orange', 'https://example.com/img/koiE.jpg', 1);
INSERT INTO [ShipMent] ([User_Id], [vehicle], [EstimatedArrivalDate], [Start_Date], [End_Date], [health_check], [Description], [Weight], [Status]) VALUES
(1, 'Truck', '2024-03-01', '2024-02-15', '2024-03-05', 'Healthy', 'Standard shipment of koi fish to the destination', 50, 1),
(2, 'Van', '2024-03-03', '2024-02-20', '2024-03-06', 'Healthy', 'Expedited shipment for red and black koi', 35, 1),
(3, 'Freight', '2024-03-07', '2024-02-22', '2024-03-10', 'Healthy', 'Heavy koi fish shipment, priority delivery', 80, 1),
(4, 'Truck', '2024-03-10', '2024-02-28', '2024-03-12', 'Healthy', 'Exotic koi fish, handling with care', 40, 1),
(5, 'Cargo Ship', '2024-03-15', '2024-03-01', '2024-03-18', 'Healthy', 'Miniature koi shipment with delicate packaging', 25, 1);
INSERT INTO [ShippingOrders] ([User_Id], [Pricing_Id], [ShipMent_Id], [Adress_To], [PhoneNumber], [Total_Price], [Description], [OrderDate], [ShippingDate], [EstimatedDeliveryDate], [Status]) VALUES
(1, 1, 1, '123 Main St, City, Country', '555-1234', 120.00, 'Standard shipment of small koi fish', '2024-02-10', '2024-02-15', '2024-03-01', 1),
(2, 2, 2, '456 Elm St, City, Country', '555-5678', 350.00, 'Expedited shipment of red and black koi', '2024-02-12', '2024-02-20', '2024-03-03', 1),
(3, 3, 3, '789 Oak St, City, Country', '555-8765', 500.00, 'Premium koi shipment for large koi fish', '2024-02-15', '2024-02-22', '2024-03-07', 1),
(4, 4, 4, '321 Pine St, City, Country', '555-4321', 150.00, 'Exotic koi shipment with careful handling', '2024-02-18', '2024-02-28', '2024-03-10', 1),
(5, 5, 5, '654 Maple St, City, Country', '555-5679', 100.00, 'Miniature koi fish shipment', '2024-02-20', '2024-03-01', '2024-03-15', 1);
INSERT INTO [ShippingOrderDetails] ([ShippingOrders_Id], [KoiFish_Id], [Quantity], [Status]) VALUES
(1, 1, 1, 1),
(2, 2, 2, 1),
(3, 3, 3, 1),
(4, 4, 1, 1),
(5, 5, 1, 1);

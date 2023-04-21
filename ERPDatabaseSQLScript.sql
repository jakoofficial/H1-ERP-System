use H1PD021123_Gruppe2

CREATE TABLE Address(
  AddressId INT NOT NULL IDENTITY(1,1),
  Street TEXT NOT NULL,
  StreetNumber VARCHAR(50) NOT NULL,
  PostalCode VARCHAR(12) NOT NULL,
  City TEXT NOT NULL,
  Country TEXT NOT NULL,
  PRIMARY KEY(AddressId)
)

CREATE TABLE Products(
    ItemId INT NOT NULL IDENTITY(1,1),
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    SalesPrice FLOAT NOT NULL,
    PurchasePrice FLOAT NOT NULL,
    "Location" VARCHAR(4) NOT NULL,
    QuantityInStock FLOAT NOT NULL,
    Unit INT NOT NULL,
    PRIMARY KEY(ItemId),
    CONSTRAINT LocationLenght CHECK (LEN("Location") = 4)
)

CREATE TABLE Companies(
    CompanyId INT NOT NULL IDENTITY(1,1),
    CompanyName TEXT NOT NULL,
    AddressId INT,
    Currency INT NOT NULL,
    PRIMARY KEY(CompanyId),
    CONSTRAINT FK_AddressIdCompanies FOREIGN KEY (AddressId) REFERENCES Address(AddressId)
        ON DELETE SET NULL
)

CREATE TABLE Customers(
    CustomerId INT NOT NULL IDENTITY(1,1),
    LastPurchased TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    AddressId INT,
    PhoneNumber VARCHAR(20) NOT NULL,
    Email VARCHAR(320) NOT NULL,
    PRIMARY KEY(CustomerId),
    CONSTRAINT FK_AddressIdCustomers FOREIGN KEY (AddressId) REFERENCES Address(AddressId)
        ON DELETE SET NULL
)

CREATE TABLE SaleOrders(
    SaleOrderId INT IDENTITY(1,1),
    TimeCreated TEXT NOT NULL,
    ImplementationTime TEXT NOT NULL,
    CustomerId INT NOT NULL,
    Stage INT NOT NULL,
    PRIMARY KEY(SaleOrderId),
    CONSTRAINT FK_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
        ON DELETE NO ACTION
)

CREATE TABLE SaleOrderLine(
    SaleOrderLineId INT NOT NULL IDENTITY(1,1),
    ProductId INT,
    PurchasedDate TEXT NOT NULL,
    PurchasedAmount INT NOT NULL,
    SaleOrderId INT NOT NULL,
    PRIMARY KEY(SaleOrderLineId),
    CONSTRAINT FK_ProductId FOREIGN KEY (ProductId) REFERENCES Products(ItemId)
        ON DELETE SET NULL,
    CONSTRAINT FK_SaleOrderId FOREIGN KEY (SaleOrderId) REFERENCES SaleOrders(SaleOrderId)
        ON DELETE NO ACTION
)

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customers] (
    [id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Cpf] nvarchar(max) NULL,
    [phone] nvarchar(max) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([id])
);
GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [SaleValue] decimal(18,2) NOT NULL,
    [PriceValue] decimal(18,2) NOT NULL,
    [Type] nvarchar(max) NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AccountsPayables] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [Supplierid] int NULL,
    [IdSupplier] int NOT NULL,
    [TotalValue] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_AccountsPayables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountsPayables_Customers_Supplierid] FOREIGN KEY ([Supplierid]) REFERENCES [Customers] ([id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountsReceivables] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [Customerid] int NULL,
    [IdCustomer] int NOT NULL,
    [TotalValue] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_AccountsReceivables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountsReceivables_Customers_Customerid] FOREIGN KEY ([Customerid]) REFERENCES [Customers] ([id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemsAccountsPayables] (
    [Id] int NOT NULL IDENTITY,
    [accountsPayableId] int NULL,
    [IdAccountsPayable] int NOT NULL,
    [ProductId] int NULL,
    [IdProduct] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [UnitareValue] decimal(18,2) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ItemsAccountsPayables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemsAccountsPayables_AccountsPayables_accountsPayableId] FOREIGN KEY ([accountsPayableId]) REFERENCES [AccountsPayables] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemsAccountsPayables_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemsAccountsReceivables] (
    [Id] int NOT NULL IDENTITY,
    [accountsPayableId] int NULL,
    [IdAccountsPayable] int NOT NULL,
    [ProductId] int NULL,
    [IdProduct] int NOT NULL,
    [Description] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [UnitareValue] decimal(18,2) NOT NULL,
    [ValueDiscont] decimal(18,2) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ItemsAccountsReceivables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemsAccountsReceivables_AccountsPayables_accountsPayableId] FOREIGN KEY ([accountsPayableId]) REFERENCES [AccountsPayables] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemsAccountsReceivables_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AccountsPayables_Supplierid] ON [AccountsPayables] ([Supplierid]);
GO

CREATE INDEX [IX_AccountsReceivables_Customerid] ON [AccountsReceivables] ([Customerid]);
GO

CREATE INDEX [IX_ItemsAccountsPayables_accountsPayableId] ON [ItemsAccountsPayables] ([accountsPayableId]);
GO

CREATE INDEX [IX_ItemsAccountsPayables_ProductId] ON [ItemsAccountsPayables] ([ProductId]);
GO

CREATE INDEX [IX_ItemsAccountsReceivables_accountsPayableId] ON [ItemsAccountsReceivables] ([accountsPayableId]);
GO

CREATE INDEX [IX_ItemsAccountsReceivables_ProductId] ON [ItemsAccountsReceivables] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210520022451_initial', N'5.0.6');
GO

COMMIT;
GO


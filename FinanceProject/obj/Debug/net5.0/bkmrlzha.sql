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
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Phone] nvarchar(max) NULL,
    [Removed] int NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [SaleValue] decimal(18,2) NOT NULL,
    [PriceValue] decimal(18,2) NOT NULL,
    [Type] nvarchar(max) NULL,
    [removed] int NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AccountsPayables] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [SupplierId] int NULL,
    [TotalValue] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_AccountsPayables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountsPayables_Customers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [accountsReceivables] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [CustomerId] int NULL,
    [TotalValue] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_accountsReceivables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_accountsReceivables_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemsAccountsPayables] (
    [Id] int NOT NULL IDENTITY,
    [accountsPayableId] int NULL,
    [ProductId] int NULL,
    [Description] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [UnitareValue] decimal(18,2) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ItemsAccountsPayables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemsAccountsPayables_AccountsPayables_accountsPayableId] FOREIGN KEY ([accountsPayableId]) REFERENCES [AccountsPayables] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemsAccountsPayables_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ItemsAccountsReceivables] (
    [Id] int NOT NULL IDENTITY,
    [accountsPayableId] int NULL,
    [ProductId] int NULL,
    [Description] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [UnitareValue] decimal(18,2) NOT NULL,
    [ValueDiscont] decimal(18,2) NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ItemsAccountsReceivables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ItemsAccountsReceivables_AccountsPayables_accountsPayableId] FOREIGN KEY ([accountsPayableId]) REFERENCES [AccountsPayables] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ItemsAccountsReceivables_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountsMovimentations] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [accountsPayableId] int NULL,
    [accountsReceivableId] int NULL,
    [Description] nvarchar(max) NULL,
    [type] int NOT NULL,
    [Value] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_AccountsMovimentations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AccountsMovimentations_AccountsPayables_accountsPayableId] FOREIGN KEY ([accountsPayableId]) REFERENCES [AccountsPayables] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AccountsMovimentations_accountsReceivables_accountsReceivableId] FOREIGN KEY ([accountsReceivableId]) REFERENCES [accountsReceivables] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AccountsMovimentations_accountsPayableId] ON [AccountsMovimentations] ([accountsPayableId]);
GO

CREATE INDEX [IX_AccountsMovimentations_accountsReceivableId] ON [AccountsMovimentations] ([accountsReceivableId]);
GO

CREATE INDEX [IX_AccountsPayables_SupplierId] ON [AccountsPayables] ([SupplierId]);
GO

CREATE INDEX [IX_accountsReceivables_CustomerId] ON [accountsReceivables] ([CustomerId]);
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
VALUES (N'20210615064225_migracao', N'5.0.6');
GO

COMMIT;
GO


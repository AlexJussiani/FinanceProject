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
    [phone] nvarchar(max) NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [SaleValue] decimal(18,2) NOT NULL,
    [PriceValue] decimal(18,2) NOT NULL,
    [Type] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AccountsPayables] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [ExpirationDate] datetime2 NOT NULL,
    [SupplierId] int NULL,
    [IdSupplier] int NOT NULL,
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
    [IdCustomer] int NOT NULL,
    [TotalValue] decimal(18,2) NOT NULL,
    [Status] nvarchar(max) NULL,
    CONSTRAINT [PK_accountsReceivables] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_accountsReceivables_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE NO ACTION
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
    CONSTRAINT [FK_ItemsAccountsPayables_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
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
    CONSTRAINT [FK_ItemsAccountsReceivables_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AccountsMovimentations] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [accountsPayableId] int NULL,
    [IdAccountsPayable] int NOT NULL,
    [accountsReceivableId] int NULL,
    [IdAccountsReceivable] int NOT NULL,
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
VALUES (N'20210602010900_migrations', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Customers].[phone]', N'Phone', N'COLUMN';
GO

ALTER TABLE [Customers] ADD [Removed] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210609052746_RemovedCustomer', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Products] ALTER COLUMN [Name] nvarchar(max) NOT NULL;
ALTER TABLE [Products] ADD DEFAULT N'' FOR [Name];
GO

ALTER TABLE [Products] ADD [removed] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210610092536_Product', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AccountsPayables]') AND [c].[name] = N'IdSupplier');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AccountsPayables] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AccountsPayables] DROP COLUMN [IdSupplier];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210611061512_ajuste_Account_payable', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210611061532_Update-Database', N'5.0.6');
GO

COMMIT;
GO


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

CREATE TABLE [Municipality] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Municipality] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TaxRecord] (
    [Id] uniqueidentifier NOT NULL,
    [FromDate] datetimeoffset NOT NULL,
    [ToDate] datetimeoffset NOT NULL,
    [TaxRate] float NOT NULL,
    [Type] int NOT NULL,
    [MunicipalityId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_TaxRecord] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TaxRecord_Municipality_MunicipalityId] FOREIGN KEY ([MunicipalityId]) REFERENCES [Municipality] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_TaxRecord_MunicipalityId] ON [TaxRecord] ([MunicipalityId]);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Municipality]'))
    SET IDENTITY_INSERT [Municipality] ON;
INSERT INTO [Municipality] ([Id], [Name])
VALUES ('1bc65c4f-d901-4f7e-9b5a-478191ae7d4a', N'Copenhagen');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Municipality]'))
    SET IDENTITY_INSERT [Municipality] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] ON;
INSERT INTO [TaxRecord] ([Id], [MunicipalityId], [FromDate], [ToDate], [TaxRate], [Type])
VALUES ('a189e49a-35f6-4423-80c2-7cc7b1d73486', '1bc65c4f-d901-4f7e-9b5a-478191ae7d4a', '2024-01-01T00:00:00.0000000+01:00', '2024-12-31T23:59:59.0000000+01:00', 0.20000000000000001E0, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] ON;
INSERT INTO [TaxRecord] ([Id], [MunicipalityId], [FromDate], [ToDate], [TaxRate], [Type])
VALUES ('f52ee1c4-8907-4c17-b31d-0004b38b3853', '1bc65c4f-d901-4f7e-9b5a-478191ae7d4a', '2024-05-01T00:00:00.0000000+02:00', '2024-05-31T23:59:59.0000000+02:00', 0.40000000000000002E0, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] ON;
INSERT INTO [TaxRecord] ([Id], [MunicipalityId], [FromDate], [ToDate], [TaxRate], [Type])
VALUES ('0dca0120-dfe1-43a6-b7e3-bfd6fe1aa79c', '1bc65c4f-d901-4f7e-9b5a-478191ae7d4a', '2024-01-01T00:00:00.0000000+01:00', '2024-01-01T23:59:59.0000000+01:00', 0.10000000000000001E0, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] ON;
INSERT INTO [TaxRecord] ([Id], [MunicipalityId], [FromDate], [ToDate], [TaxRate], [Type])
VALUES ('4023b0c5-9a91-4ded-9fab-3b071270b096', '1bc65c4f-d901-4f7e-9b5a-478191ae7d4a', '2024-12-25T00:00:00.0000000+01:00', '2024-12-25T23:59:59.0000000+01:00', 0.10000000000000001E0, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MunicipalityId', N'FromDate', N'ToDate', N'TaxRate', N'Type') AND [object_id] = OBJECT_ID(N'[TaxRecord]'))
    SET IDENTITY_INSERT [TaxRecord] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240719231231_InitDb', N'8.0.7');
GO

COMMIT;
GO


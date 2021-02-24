IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Role] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleName] nvarchar(max) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([RoleId])
);

GO

CREATE TABLE [User] (
    [UserId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(50) NOT NULL,
    [LastName] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Password] char(64) NOT NULL,
    [PhoneNumber] nvarchar(50) NOT NULL,
    [IsAcceptEmails] bit NOT NULL,
    [DateRegistered] datetime2 NOT NULL DEFAULT (getdate()),
    [Country] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
);

GO

CREATE TABLE [UserInRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserInRoles] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserInRoles_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([RoleId]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserInRoles_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([UserId]) ON DELETE CASCADE
);

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([RoleId], [RoleName])
VALUES (1, N'Administrator');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] ON;
INSERT INTO [Role] ([RoleId], [RoleName])
VALUES (2, N'Registered');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'RoleName') AND [object_id] = OBJECT_ID(N'[Role]'))
    SET IDENTITY_INSERT [Role] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Country', N'DateRegistered', N'Email', N'FirstName', N'IsAcceptEmails', N'LastName', N'Password', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] ON;
INSERT INTO [User] ([UserId], [Country], [DateRegistered], [Email], [FirstName], [IsAcceptEmails], [LastName], [Password], [PhoneNumber])
VALUES (1, 0, '0001-01-01T00:00:00.0000000', N'eyal.ank@gmail.com', N'Eyal', 0, N'Ankri', '77aae185203edc6357676db95caa25d0f398d402c1723e6a7b42cfe8d2967f2e', N'054-6680240');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'Country', N'DateRegistered', N'Email', N'FirstName', N'IsAcceptEmails', N'LastName', N'Password', N'PhoneNumber') AND [object_id] = OBJECT_ID(N'[User]'))
    SET IDENTITY_INSERT [User] OFF;

GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[UserInRoles]'))
    SET IDENTITY_INSERT [UserInRoles] ON;
INSERT INTO [UserInRoles] ([RoleId], [UserId])
VALUES (1, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleId', N'UserId') AND [object_id] = OBJECT_ID(N'[UserInRoles]'))
    SET IDENTITY_INSERT [UserInRoles] OFF;

GO

CREATE INDEX [IX_UserInRoles_UserId] ON [UserInRoles] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20181018192305_InitializeDb', N'2.1.4-rtm-31024');

GO


﻿-- autogenerated with ErikEJ.SqlCeScripting version 3.5.2.74
SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Label] (
  [TableId] nvarchar(50)  NOT NULL
, [Id] int  NOT NULL
, [Language] nvarchar(3)  NOT NULL
, [Description] nvarchar(100)  NOT NULL
, CONSTRAINT [PK_Label] PRIMARY KEY ([TableId],[Id],[Language])
);
CREATE TABLE [ItemWithPropertyModifiedTracking] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [IntValue] int  NOT NULL
, CONSTRAINT [PK_ItemWithPropertyModifiedTracking] PRIMARY KEY ([Id])
);
CREATE TABLE [ItemWithIdentityIdAndDefaultValues] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [UniqueidentifierValue] uniqueidentifier NOT NULL
, [IntValue] int DEFAULT 42  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityIdAndDefaultValues] PRIMARY KEY ([Id])
);
CREATE TABLE [ItemWithIdentityId] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithIdentityId] PRIMARY KEY ([Id])
);
CREATE TABLE [ItemWithDefaultValueId] (
  [Id] uniqueidentifier NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_ItemWithDefaultValueId] PRIMARY KEY ([Id])
);
CREATE TABLE [ItemWithAuditFields] (
  [Id] INTEGER  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, [Created] datetime NOT NULL
, [CreatedUserId] int  NOT NULL
, [Modified] datetime NULL
, [ModifiedUserId] int  NULL
, [Deleted] datetime NULL
, [DeletedUserId] int  NULL
, CONSTRAINT [PK_ItemWithAuditFields] PRIMARY KEY ([Id])
);
CREATE TABLE [ItemWithAllSupportedValues] (
  [Id] uniqueidentifier NOT NULL
, [UniqueidentifierColumn] uniqueidentifier NOT NULL
, [UniqueidentifierColumnNull] uniqueidentifier NULL
, [Nvarchar50Column] nvarchar(50)  NOT NULL
, [Nvarchar50ColumnNull] nvarchar(50)  NULL
, [NvarcharMaxColumn] ntext NOT NULL
, [NvarcharMaxColumnNull] ntext NULL
, [BitColumn] bit NOT NULL
, [BitColumnNull] bit NULL
, [SmallintColumn] smallint NOT NULL
, [SmallintColumnNull] smallint NULL
, [IntColumn] int  NOT NULL
, [IntColumnNull] int  NULL
, [BigintColumn] bigint  NOT NULL
, [BigintColumnNull] bigint  NULL
, [RealColumn] real NOT NULL
, [RealColumnNull] real NULL
, [FloatColumn] float NOT NULL
, [FloatColumnNull] float NULL
, [Numeric10and3Column] numeric(10,3)  NOT NULL
, [Numeric10and3ColumnNull] numeric(10,3)  NULL
, [Numeric15and0Column] numeric(15,0)  NOT NULL
, [Numeric15and0ColumnNull] numeric(15,0)  NULL
, [DatetimeColumn] datetime NOT NULL
, [DatetimeColumnNull] datetime NULL
, [Varbinary50Column] varbinary(50)  NOT NULL
, [Varbinary50ColumnNull] varbinary(50)  NULL
, [VarbinaryMaxColumn] image NOT NULL
, [VarbinaryMaxColumnNull] image NULL
, CONSTRAINT [PK_ItemWithAllSupportedValues] PRIMARY KEY ([Id])
);
CREATE TABLE [Category] (
  [Id] int  NOT NULL
, CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
CREATE TABLE [ArticleSubstitution] (
  [OriginalArticleId] int  NOT NULL
, [SubstitutionArticleId] int  NOT NULL
, CONSTRAINT [PK_ArticleSubstitution] PRIMARY KEY ([OriginalArticleId],[SubstitutionArticleId])
);
CREATE TABLE [ArticlePart] (
  [Id] int  NOT NULL
, [ArticleId] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_ArticlePart] PRIMARY KEY ([Id])
);
CREATE TABLE [ArticleCategory] (
  [ArticleId] int  NOT NULL
, [CategoryId] int  NOT NULL
, CONSTRAINT [PK_ArticleCategory] PRIMARY KEY ([ArticleId],[CategoryId])
);
CREATE TABLE [Article] (
  [Id] int  NOT NULL
, [Price] numeric(10,2)  NOT NULL
, CONSTRAINT [PK_Article] PRIMARY KEY ([Id])
);
CREATE TABLE [LinkedItem] (
  [Id] int  NOT NULL
, [PreviousId] int  NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItem] PRIMARY KEY ([Id])
);
CREATE TABLE [LinkedItemChild] (
  [Id] int  NOT NULL
, [LinkedItemId] int  NOT NULL
, [Description] nvarchar(50)  NOT NULL
, CONSTRAINT [PK_LinkedItemChild] PRIMARY KEY ([Id])
);
COMMIT;


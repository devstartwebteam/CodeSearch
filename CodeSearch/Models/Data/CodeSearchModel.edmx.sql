
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/04/2018 07:28:18
-- Generated from EDMX file: C:\Users\njohnson\source\repos\CodeSearch\CodeSearch\Models\Data\CodeSearchModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CodeSearch];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CategorySnippetAssociationsCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategorySnippetAssociations] DROP CONSTRAINT [FK_CategorySnippetAssociationsCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_CategorySnippetAssociationsSnippet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategorySnippetAssociations] DROP CONSTRAINT [FK_CategorySnippetAssociationsSnippet];
GO
IF OBJECT_ID(N'[dbo].[FK_SnippetNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_SnippetNote];
GO
IF OBJECT_ID(N'[dbo].[FK_SnippetTag]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tags] DROP CONSTRAINT [FK_SnippetTag];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Snippets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Snippets];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notes];
GO
IF OBJECT_ID(N'[dbo].[Tags]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tags];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[CategorySnippetAssociations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategorySnippetAssociations];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Snippets'
CREATE TABLE [dbo].[Snippets] (
    [SnippetId] int IDENTITY(1,1) NOT NULL,
    [SnippetName] nvarchar(max)  NOT NULL,
    [SnippetContent] nvarchar(max)  NOT NULL,
    [SnippetDescription] nvarchar(max)  NULL,
    [Created] datetime  NULL,
    [Modified] datetime  NULL,
    [SnippetLanguage] nvarchar(max)  NULL,
    [ReferenceURL] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryName] nvarchar(max)  NOT NULL,
    [CategoryDescription] nvarchar(max)  NULL,
    [Created] datetime  NULL,
    [Modified] datetime  NULL,
    [ImageURL] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Notes'
CREATE TABLE [dbo].[Notes] (
    [NoteId] int IDENTITY(1,1) NOT NULL,
    [NoteTitle] datetime  NULL,
    [NoteContent] nvarchar(max)  NOT NULL,
    [NoteSnippetId] int  NOT NULL
);
GO

-- Creating table 'Tags'
CREATE TABLE [dbo].[Tags] (
    [TagId] int IDENTITY(1,1) NOT NULL,
    [TagName] nvarchar(max)  NOT NULL,
    [TagSnippetId] int  NOT NULL,
    [TagCategory] nvarchar(max)  NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int IDENTITY(1,1) NOT NULL,
    [ProjectTitle] nvarchar(max)  NOT NULL,
    [ProjectDescription] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategorySnippetAssociations'
CREATE TABLE [dbo].[CategorySnippetAssociations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryAssociationId] int  NOT NULL,
    [SnippetAssociationId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SnippetId] in table 'Snippets'
ALTER TABLE [dbo].[Snippets]
ADD CONSTRAINT [PK_Snippets]
    PRIMARY KEY CLUSTERED ([SnippetId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [NoteId] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [PK_Notes]
    PRIMARY KEY CLUSTERED ([NoteId] ASC);
GO

-- Creating primary key on [TagId] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [PK_Tags]
    PRIMARY KEY CLUSTERED ([TagId] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- Creating primary key on [Id] in table 'CategorySnippetAssociations'
ALTER TABLE [dbo].[CategorySnippetAssociations]
ADD CONSTRAINT [PK_CategorySnippetAssociations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryAssociationId] in table 'CategorySnippetAssociations'
ALTER TABLE [dbo].[CategorySnippetAssociations]
ADD CONSTRAINT [FK_CategorySnippetAssociationsCategory]
    FOREIGN KEY ([CategoryAssociationId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategorySnippetAssociationsCategory'
CREATE INDEX [IX_FK_CategorySnippetAssociationsCategory]
ON [dbo].[CategorySnippetAssociations]
    ([CategoryAssociationId]);
GO

-- Creating foreign key on [SnippetAssociationId] in table 'CategorySnippetAssociations'
ALTER TABLE [dbo].[CategorySnippetAssociations]
ADD CONSTRAINT [FK_CategorySnippetAssociationsSnippet]
    FOREIGN KEY ([SnippetAssociationId])
    REFERENCES [dbo].[Snippets]
        ([SnippetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategorySnippetAssociationsSnippet'
CREATE INDEX [IX_FK_CategorySnippetAssociationsSnippet]
ON [dbo].[CategorySnippetAssociations]
    ([SnippetAssociationId]);
GO

-- Creating foreign key on [NoteSnippetId] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_SnippetNote]
    FOREIGN KEY ([NoteSnippetId])
    REFERENCES [dbo].[Snippets]
        ([SnippetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SnippetNote'
CREATE INDEX [IX_FK_SnippetNote]
ON [dbo].[Notes]
    ([NoteSnippetId]);
GO

-- Creating foreign key on [TagSnippetId] in table 'Tags'
ALTER TABLE [dbo].[Tags]
ADD CONSTRAINT [FK_SnippetTag]
    FOREIGN KEY ([TagSnippetId])
    REFERENCES [dbo].[Snippets]
        ([SnippetId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SnippetTag'
CREATE INDEX [IX_FK_SnippetTag]
ON [dbo].[Tags]
    ([TagSnippetId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
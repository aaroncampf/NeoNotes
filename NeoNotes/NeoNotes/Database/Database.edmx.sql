
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/27/2016 18:03:22
-- Generated from EDMX file: C:\Users\aaron\Documents\GitHub\NeoNotes\NeoNotes\NeoNotes\Database\Database.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Database];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContactNote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notes] DROP CONSTRAINT [FK_ContactNote];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_CustomerContact];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerQuote]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Quotes] DROP CONSTRAINT [FK_CustomerQuote];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteQuoteLine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteLines] DROP CONSTRAINT [FK_QuoteQuoteLine];
GO
IF OBJECT_ID(N'[dbo].[FK_QuoteSectionQuoteSectionDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuoteSectionDetails] DROP CONSTRAINT [FK_QuoteSectionQuoteSectionDetail];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Companies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Companies];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Quotes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Quotes];
GO
IF OBJECT_ID(N'[dbo].[Notes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notes];
GO
IF OBJECT_ID(N'[dbo].[QuoteLines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteLines];
GO
IF OBJECT_ID(N'[dbo].[QuoteSections]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSections];
GO
IF OBJECT_ID(N'[dbo].[QuoteSectionDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuoteSectionDetails];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Companies'
CREATE TABLE [dbo].[Companies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL,
    [Phone] nvarchar(max)  NULL,
    [Zip] nvarchar(max)  NULL,
    [Misc] nvarchar(max)  NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NULL,
    [Position] nvarchar(max)  NULL,
    [Company_ID] int  NOT NULL
);
GO

-- Creating table 'Quotes'
CREATE TABLE [dbo].[Quotes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Company_ID] int  NOT NULL
);
GO

-- Creating table 'Notes'
CREATE TABLE [dbo].[Notes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Text] nvarchar(max)  NULL,
    [Contact_ID] int  NOT NULL
);
GO

-- Creating table 'QuoteLines'
CREATE TABLE [dbo].[QuoteLines] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Display] int  NOT NULL,
    [UNIT] nvarchar(max)  NOT NULL,
    [COST] decimal(18,0)  NOT NULL,
    [DESC] nvarchar(max)  NOT NULL,
    [IsCentered] bit  NOT NULL,
    [Quote_ID] int  NOT NULL
);
GO

-- Creating table 'QuoteSections'
CREATE TABLE [dbo].[QuoteSections] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'QuoteSectionDetails'
CREATE TABLE [dbo].[QuoteSectionDetails] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Display] int  NOT NULL,
    [UNIT] nvarchar(max)  NOT NULL,
    [COST] decimal(18,0)  NOT NULL,
    [DESC] nvarchar(max)  NOT NULL,
    [IsCentered] bit  NOT NULL,
    [QuoteSection_ID] int  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Gmail] nvarchar(max)  NOT NULL,
    [GmailPassword] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Companies'
ALTER TABLE [dbo].[Companies]
ADD CONSTRAINT [PK_Companies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Quotes'
ALTER TABLE [dbo].[Quotes]
ADD CONSTRAINT [PK_Quotes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [PK_Notes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'QuoteLines'
ALTER TABLE [dbo].[QuoteLines]
ADD CONSTRAINT [PK_QuoteLines]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'QuoteSections'
ALTER TABLE [dbo].[QuoteSections]
ADD CONSTRAINT [PK_QuoteSections]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'QuoteSectionDetails'
ALTER TABLE [dbo].[QuoteSectionDetails]
ADD CONSTRAINT [PK_QuoteSectionDetails]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Contact_ID] in table 'Notes'
ALTER TABLE [dbo].[Notes]
ADD CONSTRAINT [FK_ContactNote]
    FOREIGN KEY ([Contact_ID])
    REFERENCES [dbo].[Contacts]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ContactNote'
CREATE INDEX [IX_FK_ContactNote]
ON [dbo].[Notes]
    ([Contact_ID]);
GO

-- Creating foreign key on [Company_ID] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_CustomerContact]
    FOREIGN KEY ([Company_ID])
    REFERENCES [dbo].[Companies]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerContact'
CREATE INDEX [IX_FK_CustomerContact]
ON [dbo].[Contacts]
    ([Company_ID]);
GO

-- Creating foreign key on [Company_ID] in table 'Quotes'
ALTER TABLE [dbo].[Quotes]
ADD CONSTRAINT [FK_CustomerQuote]
    FOREIGN KEY ([Company_ID])
    REFERENCES [dbo].[Companies]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerQuote'
CREATE INDEX [IX_FK_CustomerQuote]
ON [dbo].[Quotes]
    ([Company_ID]);
GO

-- Creating foreign key on [Quote_ID] in table 'QuoteLines'
ALTER TABLE [dbo].[QuoteLines]
ADD CONSTRAINT [FK_QuoteQuoteLine]
    FOREIGN KEY ([Quote_ID])
    REFERENCES [dbo].[Quotes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteQuoteLine'
CREATE INDEX [IX_FK_QuoteQuoteLine]
ON [dbo].[QuoteLines]
    ([Quote_ID]);
GO

-- Creating foreign key on [QuoteSection_ID] in table 'QuoteSectionDetails'
ALTER TABLE [dbo].[QuoteSectionDetails]
ADD CONSTRAINT [FK_QuoteSectionQuoteSectionDetail]
    FOREIGN KEY ([QuoteSection_ID])
    REFERENCES [dbo].[QuoteSections]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuoteSectionQuoteSectionDetail'
CREATE INDEX [IX_FK_QuoteSectionQuoteSectionDetail]
ON [dbo].[QuoteSectionDetails]
    ([QuoteSection_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
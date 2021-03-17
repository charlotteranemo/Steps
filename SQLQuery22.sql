
USE [Steps_Db]
GO

/****** Object: Table [dbo].[Fitspos] Script Date: 2021-02-21 16:26:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fitspos] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [DateOfPost] DATETIME        NULL,
    [Title]      NVARCHAR (MAX)  NULL,
    [Blurb]      NVARCHAR (MAX)  NULL,
    [Post]       NVARCHAR (MAX)  NOT NULL,
    [Image]      VARBINARY (MAX) NULL
);


USE [Steps_Db]
GO

/****** Object: Table [dbo].[Login] Script Date: 2021-02-21 16:26:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Login] (
    [LoginId]  INT            IDENTITY (1, 1) NOT NULL,
    [Username] NVARCHAR (MAX) NOT NULL,
    [Password] NVARCHAR (MAX) NOT NULL
);


USE [Steps_Db]
GO

/****** Object: Table [dbo].[Emails] Script Date: 2021-02-21 16:27:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Emails] (
    [EmailId]  INT            IDENTITY (1, 1) NOT NULL,
    [EmailStr] NVARCHAR (MAX) NOT NULL
);



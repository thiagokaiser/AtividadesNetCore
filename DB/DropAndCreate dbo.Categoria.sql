USE [AtividadesDB]
GO

/****** Object: Table [dbo].[Categoria] Script Date: 08/11/2018 14:49:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Categoria];


GO
CREATE TABLE [dbo].[Categoria] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [Descricao] NCHAR (20) NULL,
    [Cor]       NCHAR (10) NULL
);



USE [AtividadesDB]
GO

/****** Object: Table [dbo].[Atividade] Script Date: 01/10/2018 08:12:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Atividade] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Descricao]   NCHAR (40) NULL,
    [Responsavel] NCHAR (40) NULL,
    [Setor]       NCHAR (40) NULL,
    [Categoria]   NCHAR (40) NULL
);



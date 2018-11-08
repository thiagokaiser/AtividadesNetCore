USE [AtividadesDB]
GO

/****** Object: Table [dbo].[Atividade] Script Date: 08/11/2018 14:48:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Atividade];


GO
CREATE TABLE [dbo].[Atividade] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [Descricao]   NCHAR (40) NULL,
    [Responsavel] NCHAR (40) NULL,
    [Setor]       NCHAR (40) NULL,
    [CategoriaId] INT        NULL,
    [Data]        DATE       NULL
);



USE [AtividadesDB]
GO

/****** Object: Table [dbo].[Atividade] Script Date: 25/02/2019 09:10:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Atividade];


GO
CREATE TABLE [dbo].[Atividade] (
    [Id]               INT         IDENTITY (1, 1) NOT NULL,
    [Descricao]        NCHAR (40)  NULL,
    [Responsavel]      NCHAR (40)  NULL,
    [Setor]            NCHAR (40)  NULL,
    [CategoriaId]      INT         NULL,
    [Data]             DATE        NULL,
    [Prioridade]       INT         NULL,
    [DataEncerramento] DATE        NULL,
    [Solicitante]      NCHAR (40)  NULL,
    [Narrativa]        NCHAR (200) NULL
);



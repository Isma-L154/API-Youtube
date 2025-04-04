CREATE TABLE [dbo].[ListasRecomendados] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Nombre]      NVARCHAR (255)   NOT NULL,
    [Descripcion] NVARCHAR (MAX)   NULL,
    [estado] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


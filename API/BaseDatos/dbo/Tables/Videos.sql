CREATE TABLE [dbo].[Videos] (
    [Id]          NVARCHAR (255) NOT NULL,
    [Titulo]      VARCHAR (255)  NOT NULL,
    [Descripcion] NVARCHAR (MAX) NULL,
    [Url]         VARCHAR (MAX)  NOT NULL,
    [Canal]       VARCHAR (MAX)  NOT NULL,
    [Fecha]       DATETIME       NULL,
    [Miniatura]   VARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


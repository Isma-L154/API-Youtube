CREATE TABLE [dbo].[ListaxVideo] (
    [Id]      UNIQUEIDENTIFIER NOT NULL,
    [IdLista] UNIQUEIDENTIFIER NOT NULL,
    [IdVideo] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([IdLista]) REFERENCES [dbo].[ListasRecomendados] ([Id]) ON DELETE CASCADE
);


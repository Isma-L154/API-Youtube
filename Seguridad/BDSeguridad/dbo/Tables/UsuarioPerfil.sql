CREATE TABLE [dbo].[UsuarioPerfil] (
    [UsuarioId] UNIQUEIDENTIFIER NOT NULL,
    [PerfilId]  INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([UsuarioId] ASC, [PerfilId] ASC),
    FOREIGN KEY ([PerfilId]) REFERENCES [dbo].[Perfil] ([Id]),
    FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuario] ([Id])
);


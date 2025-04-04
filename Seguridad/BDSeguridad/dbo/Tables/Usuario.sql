CREATE TABLE [dbo].[Usuario] (
    [Id]                UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NombreUsuario]     NVARCHAR (50)    NOT NULL,
    [PasswordHash]      NVARCHAR (256)   NOT NULL,
    [CorreoElectronico] NVARCHAR (100)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([CorreoElectronico] ASC)
);


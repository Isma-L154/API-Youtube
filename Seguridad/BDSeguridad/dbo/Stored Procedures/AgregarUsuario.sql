CREATE PROCEDURE AgregarUsuario
    @NombreUsuario NVARCHAR(MAX),
    @PasswordHash NVARCHAR(MAX),
    @CorreoElectronico NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @UsuarioId UNIQUEIDENTIFIER = NEWID();
	BEGIN TRAN 
	-- Insertar en la tabla Usuario
    INSERT INTO [dbo].[Usuario] (Id, NombreUsuario, PasswordHash, CorreoElectronico)
    VALUES (@UsuarioId, @NombreUsuario, @PasswordHash, @CorreoElectronico)
	SELECT @UsuarioId

    -- Insertar en la tabla UsuarioPerfil para asignar el perfil
    INSERT INTO [dbo].[UsuarioPerfil] (UsuarioId, PerfilId)
    VALUES (@UsuarioId, 1);
	COMMIT TRAN 
END
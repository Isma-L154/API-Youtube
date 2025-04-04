CREATE PROCEDURE ObtenerPerfilesxUsuario
    @CorreoElectronico NVARCHAR(MAX),
    @NombreUsuario NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.Id,
        P.Nombre
    FROM 
        Perfil P
    INNER JOIN 
        UsuarioPerfil UP ON P.Id = UP.PerfilId
    INNER JOIN 
        Usuario U ON U.Id = UP.UsuarioId
    WHERE 
        (U.CorreoElectronico = @CorreoElectronico) OR (U.NombreUsuario = @NombreUsuario);
END
CREATE PROCEDURE [dbo].[ObtenerUsuario]
    @CorreoElectronico NVARCHAR(MAX),
    @NombreUsuario NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM 
        Usuario
    WHERE 
        (CorreoElectronico = @CorreoElectronico OR NombreUsuario = @NombreUsuario);
END
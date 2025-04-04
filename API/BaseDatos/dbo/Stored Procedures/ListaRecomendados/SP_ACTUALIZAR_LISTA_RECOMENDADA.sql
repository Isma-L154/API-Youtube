CREATE PROCEDURE SP_ACTUALIZAR_LISTA_RECOMENDADA(
	@ID UNIQUEIDENTIFIER,
	@Nombre VARCHAR(100),
	@Descripcion VARCHAR(200)
)AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM ListasRecomendados WHERE Nombre = UPPER(@Nombre))
	BEGIN
		UPDATE ListasRecomendados
		SET Nombre = @Nombre,
		Descripcion = @Descripcion
		WHERE Id = @ID
	END
END
CREATE PROCEDURE SP_CREAR_LISTA_RECOMENDADA(
	@Nombre VARCHAR(100),
	@Descripcion VARCHAR(200)
)AS
BEGIN
	BEGIN
		INSERT INTO ListasRecomendados(Id,Nombre, Descripcion, estado) 
		VALUES (NEWID(),@Nombre, @Descripcion, 1);
	END
END
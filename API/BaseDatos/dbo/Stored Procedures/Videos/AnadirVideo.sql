CREATE PROCEDURE AnadirVideo(
	@IdLista UNIQUEIDENTIFIER,
	@IdVideo VARCHAR(200)
)AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM ListaxVideo WHERE IdLista =@IdLista AND IdVideo = @IdVideo)
	BEGIN
		INSERT INTO ListaxVideo(Id,IdLista, IdVideo) VALUES (NEWID(),@IdLista, @IdVideo);
	END
END
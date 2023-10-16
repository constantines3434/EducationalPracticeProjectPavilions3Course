CREATE PROCEDURE AddPavilionToMall
	@MallId INT,
	@PavilionName NVARCHAR(100),
	@Floor INT,
	@StatusId INT,
	@Square DOUBLE PRECISION,
	@Cost DOUBLE PRECISION,
	@PavilionCount INT,
	@ValueAddedFactor DOUBLE PRECISION
AS
BEGIN
	DECLARE @PavilionLimit INT;
	SELECT @PavilionLimit = PavilionCount
	FROM Malls
	WHERE IdShoppingMall = @MallId;

	IF @PavilionCount < @PavilionLimit  -- Проверка ограничения на количество Pavilions
	BEGIN
		INSERT INTO Pavilions (IdShoppingMall, NamePavilions, FloorPavilion, IdStatusPavilion, SquarePavilions, CostPerMeter, ValueAddedFactor)
		VALUES (@MallId, @PavilionName, @Floor, @StatusId, @Square, @Cost, @ValueAddedFactor);

		-- Увеличиваем счетчик PavilionCount
		UPDATE Malls
		SET PavilionCount = @PavilionCount + 1
		WHERE IdShoppingMall = @MallId;
	END
	ELSE
	BEGIN
    -- Здесь можно добавить обработку ошибки или вернуть сообщение о превышении лимита
    THROW 50001, 'Превышен лимит на количество Pavilions в данном Mall.', 1;
	END
END;

---- Пример вызова
--EXEC AddPavilionToMall
--    @MallId = 3, -- ID торгового центра
--    @PavilionName = 'ТестовыйПавильон',
--    @Floor = 1,
--    @StatusId = 2, -- ID статуса павильона
--    @Square = 120.5,
--    @Cost = 2500.0,
--    @PavilionCount = 0, -- Текущее количество павильонов (получим его из Malls)
--    @ValueAddedFactor = 1.0;
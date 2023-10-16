CREATE PROCEDURE DeletePavilion
    @PavilionId INT
AS
BEGIN
    -- Убеждаемся, что павильон с указанным идентификатором существует
    IF EXISTS (SELECT 1 FROM Pavilions WHERE IdPavilion = @PavilionId)
    BEGIN
        -- Удалить павильон
        DELETE FROM Pavilions WHERE IdPavilion = @PavilionId;
        
        -- Обновить PavilionCount в соответствующем торговом центре (Mall)
        DECLARE @MallId INT;
        SELECT @MallId = IdShoppingMall FROM Pavilions WHERE IdPavilion = @PavilionId;
        
        UPDATE Malls
        SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
        WHERE IdShoppingMall = @MallId;
    END
    ELSE
    BEGIN
        -- Обработать ситуацию, когда павильон с указанным идентификатором не найден
        -- Здесь можно добавить обработку ошибки или вернуть сообщение о том, что павильон не существует
        THROW 50000, 'Павильон с указанным идентификатором не найден.', 1;
    END
END;

----Пример удаления
--EXEC DeletePavilion @PavilionId = 3; -- Здесь 3 - это идентификатор павильона, который вы хотите удалить
CREATE TRIGGER PreventMallStatusChange
ON Malls
INSTEAD OF UPDATE
AS
BEGIN
    -- Проверяем изменения, которые предпринимаются
    IF UPDATE(IdStatusMall)
    BEGIN
        DECLARE @UpdatedId INT;
        SELECT @UpdatedId = IdShoppingMall FROM INSERTED;

        IF EXISTS (SELECT 1 FROM Pavilions WHERE IdShoppingMall = @UpdatedId AND IdStatusPavilion = (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Забронировано'))
        BEGIN
            -- Здесь используется THROW для бросания исключения с пользовательским сообщением и указанием уровня серьезности
            THROW 50000, 'Запрещено изменять статус ТЦ на "план", так как есть забронированные павильоны.', 1;
        END
    END

    -- Если изменений нет, выполняем стандартную операцию UPDATE
    UPDATE Malls
    SET IdStatusMall = (SELECT IdStatusMall FROM INSERTED)
    WHERE IdShoppingMall = (SELECT IdShoppingMall FROM INSERTED);
END

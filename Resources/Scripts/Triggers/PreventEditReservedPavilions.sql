-- Создаем триггер для предотвращения редактирования павильонов с определенными статусами
CREATE TRIGGER PreventEditReservedPavilions
ON Pavilions
INSTEAD OF UPDATE
AS
BEGIN
    -- Запрещенные статусы
    DECLARE @BlockedStatuses NVARCHAR(100);
    SET @BlockedStatuses = 'Забронировано, Арендован';

    -- Проверка статусов, которые предполагается обновлять
    IF (SELECT COUNT(*) FROM inserted WHERE IdStatusPavilion IN (
        SELECT IdStatusPavilion
        FROM StatusPavilions
        WHERE StatusName IN (SELECT value FROM STRING_SPLIT(@BlockedStatuses, ','))
    )) > 0
    BEGIN
        -- Если попытка обновления павильона на "забронирован" или "арендован" статус, бросаем ошибку
        THROW 50001, 'Невозможно отредактировать павильон на "забронирован" или "арендован" статус.', 1;
    END
    ELSE
    BEGIN
        -- Разрешенные обновления, обновляем Pavilion
        UPDATE p
        SET p.NamePavilions = i.NamePavilions,  -- Замените на необходимые поля
            p.FloorPavilion = i.FloorPavilion,
            p.SquarePavilions = i.SquarePavilions,
            p.CostPerMeter = i.CostPerMeter,
            p.ValueAddedFactor = i.ValueAddedFactor
        FROM Pavilions p
        JOIN inserted i ON p.IdPavilion = i.IdPavilion;
    END
END;

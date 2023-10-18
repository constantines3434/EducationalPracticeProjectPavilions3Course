-- Создаем триггер для предотвращения редактирования павильонов с определенными статусами
CREATE TRIGGER PreventEditReservedPavilions
ON Pavilions
FOR UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted AS d
        INNER JOIN inserted AS i ON d.IdPavilion = i.IdPavilion
        WHERE i.IdStatusPavilion IN (
            SELECT IdStatusPavilion
            FROM StatusPavilions
            WHERE StatusName IN ('Забронировано', 'Арендован')
        )
    )
    BEGIN
        -- Если попытка редактирования павильона на "забронирован" или "арендован" статус, бросаем ошибку
        THROW 50001, 'Невозможно отредактировать павильон на "забронирован" или "арендован" статус.', 1;
        ROLLBACK TRANSACTION;
    END
END;

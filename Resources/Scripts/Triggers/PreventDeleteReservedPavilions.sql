 CREATE TRIGGER PreventDeleteReservedPavilions
ON Pavilions
FOR DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted AS d
        WHERE d.IdStatusPavilion IN (
            SELECT IdStatusPavilion
            FROM StatusPavilions
            WHERE StatusName IN ('Забронировано', 'Арендован')
        )
    )
    BEGIN
        -- Если попытка удаления павильона с "забронирован" или "арендован" статусом, бросаем ошибку
        THROW 50001, 'Невозможно удалить павильон с "забронирован" или "арендован" статусом.', 1;
        ROLLBACK TRANSACTION;
    END
END;
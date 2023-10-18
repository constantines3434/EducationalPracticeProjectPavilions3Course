CREATE TRIGGER UpdatePavilionStatusOnRentChange
ON Rent
AFTER UPDATE
AS
BEGIN
    -- Изменение статуса павильона в зависимости от статуса аренды
    IF UPDATE(IdStatusRent)
    BEGIN
        UPDATE p
        SET p.IdStatusPavilion = 
            CASE
                WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Открыт') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Арендовано')
                WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Ожидает') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Забронировано')
                 WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Закрыт') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Свободен')
				ELSE (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Удален')
            END
        FROM Pavilions p
        JOIN inserted i ON p.IdPavilion = i.IdStatePavilion;
    END
END;

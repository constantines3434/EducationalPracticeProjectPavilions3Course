CREATE PROCEDURE RentOrReservePavilion
    @IdShoppingMall INT,
    @IdPavilion INT,
    @IdTenant INT,
    @StartOfLease DATE,
    @EndOfLease DATE,
    @IdStatusRent INT
AS
BEGIN
    BEGIN TRY
        -- Проверяем, доступен ли павильон для аренды или бронирования
        IF EXISTS (
            SELECT 1
            FROM Pavilions AS p
            WHERE p.IdPavilion = @IdPavilion
              AND p.IdStatusPavilion NOT IN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName IN ('Забронировано', 'Арендовано'))
        )
        BEGIN
            -- Вставить запись в таблицу аренды
            INSERT INTO Rent (IdTenant, IdStatePavilion, IdStatusRent, StartOfLease, EndOfLease)
            VALUES (@IdTenant, @IdPavilion, @IdStatusRent, @StartOfLease, @EndOfLease);

            -- Обновить статус павильона на "Забронировано" или "Арендовано", в зависимости от IdStatusRent
            UPDATE Pavilions
            SET IdStatusPavilion = CASE
                WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Открыт') THEN
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Арендовано')
                WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Ожидает') THEN
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Забронировано')
                WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = 'Закрыт') THEN
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Свободен')
					ELSE
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Удален')
            END
            WHERE IdPavilion = @IdPavilion;

            -- Успешное завершение процедуры
            RETURN 0;
        END
        ELSE
        BEGIN
            -- Попытка аренды/бронирования недоступного павильона
            THROW 50001, 'Павильон недоступен для аренды или бронирования.', 1;
        END
    END TRY
    BEGIN CATCH
        -- Обработка исключения и бросание исключения с сообщением
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        THROW @ErrorMessage, @ErrorSeverity, @ErrorState;
    END CATCH
END;

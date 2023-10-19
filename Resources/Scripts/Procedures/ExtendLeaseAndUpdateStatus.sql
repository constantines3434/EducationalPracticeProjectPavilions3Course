CREATE PROCEDURE ExtendLeaseAndUpdateStatus
AS
BEGIN
    BEGIN TRY
        -- Переносим срок аренды всех павильонов на год
        UPDATE Rent
        SET EndOfLease = DATEADD(YEAR, 1, EndOfLease);

        -- Обновляем статусы павильонов в зависимости от новой даты в таблице аренды
        UPDATE Pavilions
        SET IdStatusPavilion = 
            CASE
                WHEN Rent.EndOfLease >= GETDATE() THEN
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Арендован')
                ELSE
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = 'Свободен')
            END
        FROM Pavilions
        JOIN Rent ON Pavilions.IdPavilion = Rent.IdStatePavilion;

        -- Успешное завершение процедуры
        RETURN 0;
    END TRY
    BEGIN CATCH
        -- Обработка исключения и бросание исключения с сообщением
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        THROW @ErrorMessage, @ErrorSeverity, @ErrorState;
    END CATCH
END;

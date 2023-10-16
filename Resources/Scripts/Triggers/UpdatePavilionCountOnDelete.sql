CREATE TRIGGER UpdatePavilionCountOnDelete
ON Pavilions
AFTER DELETE
AS
BEGIN
    DECLARE @MallId INT;
    
    -- Получаем IdShoppingMall из удаленных записей
    SELECT @MallId = IdShoppingMall
    FROM deleted;
    
    -- Обновляем PavilionCount в Malls
    UPDATE Malls
    SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
    WHERE IdShoppingMall = @MallId;
END;
CREATE TRIGGER UpdatePavilionCountInMallOnInsert
ON Pavilions
AFTER INSERT
AS
BEGIN
    DECLARE @MallId INT;
    
    -- Получаем IdShoppingMall из вставленных записей
    SELECT @MallId = IdShoppingMall
    FROM inserted;
    
    -- Обновляем PavilionCount в Malls
    UPDATE Malls
    SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
    WHERE IdShoppingMall = @MallId;
END;

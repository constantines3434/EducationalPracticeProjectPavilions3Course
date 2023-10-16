CREATE TRIGGER UpdatePavilionCountOnDelete
ON Pavilions
AFTER DELETE
AS
BEGIN
    DECLARE @MallId INT;
    
    -- �������� IdShoppingMall �� ��������� �������
    SELECT @MallId = IdShoppingMall
    FROM deleted;
    
    -- ��������� PavilionCount � Malls
    UPDATE Malls
    SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
    WHERE IdShoppingMall = @MallId;
END;
CREATE TRIGGER UpdatePavilionCountInMallOnInsert
ON Pavilions
AFTER INSERT
AS
BEGIN
    DECLARE @MallId INT;
    
    -- �������� IdShoppingMall �� ����������� �������
    SELECT @MallId = IdShoppingMall
    FROM inserted;
    
    -- ��������� PavilionCount � Malls
    UPDATE Malls
    SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
    WHERE IdShoppingMall = @MallId;
END;

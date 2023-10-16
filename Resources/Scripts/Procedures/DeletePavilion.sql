CREATE PROCEDURE DeletePavilion
    @PavilionId INT
AS
BEGIN
    -- ����������, ��� �������� � ��������� ��������������� ����������
    IF EXISTS (SELECT 1 FROM Pavilions WHERE IdPavilion = @PavilionId)
    BEGIN
        -- ������� ��������
        DELETE FROM Pavilions WHERE IdPavilion = @PavilionId;
        
        -- �������� PavilionCount � ��������������� �������� ������ (Mall)
        DECLARE @MallId INT;
        SELECT @MallId = IdShoppingMall FROM Pavilions WHERE IdPavilion = @PavilionId;
        
        UPDATE Malls
        SET PavilionCount = (SELECT COUNT(*) FROM Pavilions WHERE IdShoppingMall = @MallId)
        WHERE IdShoppingMall = @MallId;
    END
    ELSE
    BEGIN
        -- ���������� ��������, ����� �������� � ��������� ��������������� �� ������
        -- ����� ����� �������� ��������� ������ ��� ������� ��������� � ���, ��� �������� �� ����������
        THROW 50000, '�������� � ��������� ��������������� �� ������.', 1;
    END
END;

----������ ��������
--EXEC DeletePavilion @PavilionId = 3; -- ����� 3 - ��� ������������� ���������, ������� �� ������ �������
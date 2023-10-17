CREATE TRIGGER PreventMallStatusChange
ON Malls
INSTEAD OF UPDATE
AS
BEGIN
    -- ��������� ���������, ������� ���������������
    IF UPDATE(IdStatusMall)
    BEGIN
        DECLARE @UpdatedId INT;
        SELECT @UpdatedId = IdShoppingMall FROM INSERTED;

        IF EXISTS (SELECT 1 FROM Pavilions WHERE IdShoppingMall = @UpdatedId AND IdStatusPavilion = (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '�������������'))
        BEGIN
            -- ����� ������������ THROW ��� �������� ���������� � ���������������� ���������� � ��������� ������ �����������
            THROW 50000, '��������� �������� ������ �� �� "����", ��� ��� ���� ��������������� ���������.', 1;
        END
    END

    -- ���� ��������� ���, ��������� ����������� �������� UPDATE
    UPDATE Malls
    SET IdStatusMall = (SELECT IdStatusMall FROM INSERTED)
    WHERE IdShoppingMall = (SELECT IdShoppingMall FROM INSERTED);
END

-- ������� ������� ��� �������������� �������������� ���������� � ������������� ���������
CREATE TRIGGER PreventEditReservedPavilions
ON Pavilions
INSTEAD OF UPDATE
AS
BEGIN
    -- ����������� �������
    DECLARE @BlockedStatuses NVARCHAR(100);
    SET @BlockedStatuses = '�������������, ���������';

    -- �������� ��������, ������� �������������� ���������
    IF (SELECT COUNT(*) FROM inserted WHERE IdStatusPavilion IN (
        SELECT IdStatusPavilion
        FROM StatusPavilions
        WHERE StatusName IN (SELECT value FROM STRING_SPLIT(@BlockedStatuses, ','))
    )) > 0
    BEGIN
        -- ���� ������� ���������� ��������� �� "������������" ��� "���������" ������, ������� ������
        THROW 50001, '���������� ��������������� �������� �� "������������" ��� "���������" ������.', 1;
    END
    ELSE
    BEGIN
        -- ����������� ����������, ��������� Pavilion
        UPDATE p
        SET p.NamePavilions = i.NamePavilions,  -- �������� �� ����������� ����
            p.FloorPavilion = i.FloorPavilion,
            p.SquarePavilions = i.SquarePavilions,
            p.CostPerMeter = i.CostPerMeter,
            p.ValueAddedFactor = i.ValueAddedFactor
        FROM Pavilions p
        JOIN inserted i ON p.IdPavilion = i.IdPavilion;
    END
END;

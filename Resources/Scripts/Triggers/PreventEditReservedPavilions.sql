-- ������� ������� ��� �������������� �������������� ���������� � ������������� ���������
CREATE TRIGGER PreventEditReservedPavilions
ON Pavilions
FOR UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted AS d
        INNER JOIN inserted AS i ON d.IdPavilion = i.IdPavilion
        WHERE i.IdStatusPavilion IN (
            SELECT IdStatusPavilion
            FROM StatusPavilions
            WHERE StatusName IN ('�������������', '���������')
        )
    )
    BEGIN
        -- ���� ������� �������������� ��������� �� "������������" ��� "���������" ������, ������� ������
        THROW 50001, '���������� ��������������� �������� �� "������������" ��� "���������" ������.', 1;
        ROLLBACK TRANSACTION;
    END
END;

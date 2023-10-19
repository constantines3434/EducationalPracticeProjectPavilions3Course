CREATE TRIGGER PreventDeleteReservedPavilions
ON Pavilions
FOR DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted AS d
        WHERE d.IdStatusPavilion IN (
            SELECT IdStatusPavilion
            FROM StatusPavilions
            WHERE StatusName IN ('�������������', '���������')
        )
    )
    BEGIN
        -- ���� ������� �������� ��������� � "������������" ��� "���������" ��������, ������� ������
        THROW 50001, '���������� ������� �������� � "������������" ��� "���������" ��������.', 1;
        ROLLBACK TRANSACTION;
    END
END;
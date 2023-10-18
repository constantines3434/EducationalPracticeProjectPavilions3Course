CREATE TRIGGER UpdatePavilionStatusOnRentChange
ON Rent
AFTER UPDATE
AS
BEGIN
    -- ��������� ������� ��������� � ����������� �� ������� ������
    IF UPDATE(IdStatusRent)
    BEGIN
        UPDATE p
        SET p.IdStatusPavilion = 
            CASE
                WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '������') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '����������')
                WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '�������') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '�������������')
                 WHEN i.IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '������') THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '��������')
				ELSE (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '������')
            END
        FROM Pavilions p
        JOIN inserted i ON p.IdPavilion = i.IdStatePavilion;
    END
END;

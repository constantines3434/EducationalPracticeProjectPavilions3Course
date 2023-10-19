CREATE PROCEDURE ExtendLeaseAndUpdateStatus
AS
BEGIN
    BEGIN TRY
        -- ��������� ���� ������ ���� ���������� �� ���
        UPDATE Rent
        SET EndOfLease = DATEADD(YEAR, 1, EndOfLease);

        -- ��������� ������� ���������� � ����������� �� ����� ���� � ������� ������
        UPDATE Pavilions
        SET IdStatusPavilion = 
            CASE
                WHEN Rent.EndOfLease >= GETDATE() THEN
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '���������')
                ELSE
                    (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '��������')
            END
        FROM Pavilions
        JOIN Rent ON Pavilions.IdPavilion = Rent.IdStatePavilion;

        -- �������� ���������� ���������
        RETURN 0;
    END TRY
    BEGIN CATCH
        -- ��������� ���������� � �������� ���������� � ����������
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        THROW @ErrorMessage, @ErrorSeverity, @ErrorState;
    END CATCH
END;

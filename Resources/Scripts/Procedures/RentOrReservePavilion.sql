CREATE PROCEDURE RentOrReservePavilion
    @IdShoppingMall INT,
    @IdPavilion INT,
    @IdTenant INT,
    @StartOfLease DATE,
    @EndOfLease DATE,
    @IdStatusRent INT
AS
BEGIN
    BEGIN TRY
        -- ���������, �������� �� �������� ��� ������ ��� ������������
        IF EXISTS (
            SELECT 1
            FROM Pavilions AS p
            WHERE p.IdPavilion = @IdPavilion
              AND p.IdStatusPavilion NOT IN (
                SELECT IdStatusPavilion
                FROM StatusPavilions
                WHERE StatusName IN ('�������������', '����������')
              )
        )
        BEGIN
            -- �������� ������ � ������� ������
            INSERT INTO Rent (IdTenant, IdStatePavilion, IdStatusRent, StartOfLease, EndOfLease)
            VALUES (@IdTenant, @IdPavilion, @IdStatusRent, @StartOfLease, @EndOfLease);

            -- �������� ������ ��������� �� "�������������" ��� "����������", � ����������� �� IdStatusRent
            UPDATE Pavilions
            SET IdStatusPavilion = (
                CASE
                    WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '������')
                    THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '����������')
                    WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '��������')
                    THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '�������������')
                    WHEN @IdStatusRent = (SELECT IdStatusRent FROM StatusRent WHERE StatusRentName = '������')
                    THEN (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '��������')
                    ELSE (SELECT IdStatusPavilion FROM StatusPavilions WHERE StatusName = '������')
                END
            )
            WHERE IdPavilion = @IdPavilion;

            -- �������� ���������� ���������
            RETURN 0;
        END
        ELSE
        BEGIN
            -- ������� ������/������������ ������������ ���������
            THROW 50001, '�������� ���������� ��� ������ ��� ������������.', 1;
        END
    END TRY
    BEGIN CATCH
        -- ��������� ���������� � �������� ���������� � ����������
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();

        THROW @ErrorMessage, @ErrorSeverity, @ErrorState;
    END CATCH
END;
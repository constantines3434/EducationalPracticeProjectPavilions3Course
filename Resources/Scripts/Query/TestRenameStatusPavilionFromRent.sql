	DECLARE @IdShoppingMall INT;
DECLARE @IdPavilion INT;
DECLARE @IdTenant INT;
DECLARE @StartOfLease DATE;
DECLARE @EndOfLease DATE;
DECLARE @IdStatusRent INT;

-- ���������� �������� ����������
SET @IdShoppingMall = 3; -- �������� �� �������� ��������
SET @IdPavilion = 1; -- �������� �� �������� ��������
SET @IdTenant = 3; -- �������� �� �������� ��������
SET @StartOfLease = '2023-10-01'; -- �������� �� �������� ��������
SET @EndOfLease = '2023-12-31'; -- �������� �� �������� ��������
SET @IdStatusRent = 2; -- �������� �� �������� ��������

-- ����� �������� ���������
EXEC RentOrReservePavilion
    @IdShoppingMall,
    @IdPavilion,
    @IdTenant,
    @StartOfLease,
    @EndOfLease,
    @IdStatusRent;
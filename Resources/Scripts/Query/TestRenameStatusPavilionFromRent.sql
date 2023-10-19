	DECLARE @IdShoppingMall INT;
DECLARE @IdPavilion INT;
DECLARE @IdTenant INT;
DECLARE @StartOfLease DATE;
DECLARE @EndOfLease DATE;
DECLARE @IdStatusRent INT;

-- Установите значения параметров
SET @IdShoppingMall = 3; -- Замените на реальное значение
SET @IdPavilion = 1; -- Замените на реальное значение
SET @IdTenant = 3; -- Замените на реальное значение
SET @StartOfLease = '2023-10-01'; -- Замените на реальное значение
SET @EndOfLease = '2023-12-31'; -- Замените на реальное значение
SET @IdStatusRent = 2; -- Замените на реальное значение

-- Вызов хранимой процедуры
EXEC RentOrReservePavilion
    @IdShoppingMall,
    @IdPavilion,
    @IdTenant,
    @StartOfLease,
    @EndOfLease,
    @IdStatusRent;
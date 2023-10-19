CREATE PROCEDURE GetTenantRents
    @TenantId INT
AS
BEGIN
    SELECT
        M.NameMalls AS 'MallName',
        C.NameCity AS 'CityName',
        P.NamePavilions AS 'PavilionsName',
        R.StartOfLease AS 'StartOfLease',
        R.EndOfLease AS 'EndOfLease',
        dbo.CalculateRentCost(P.SquarePavilions, P.CostPerMeter, P.ValueAddedFactor, M.Price)
		AS 'RentCost',
        SR.StatusRentName AS 'StatusRentName'
    FROM Rent R
    INNER JOIN Tenants T ON R.IdTenant = T.IdTenant
    INNER JOIN Pavilions P ON R.IdStatePavilion = P.IdPavilion
    INNER JOIN Malls M ON P.IdShoppingMall = M.IdShoppingMall
    INNER JOIN Cities C ON M.IdCity = C.IdCity
    INNER JOIN StatusRent SR ON R.IdStatusRent = SR.IdStatusRent
    WHERE T.IdTenant = @TenantId;
END;


--EXEC GetTenantRents @TenantId = YourTenantID;
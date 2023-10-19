CREATE FUNCTION CalculateRentCost(
    @Area DOUBLE PRECISION,
    @LevelCost DOUBLE PRECISION,
    @ComplexCost DOUBLE PRECISION,
    @Multiplier DOUBLE PRECISION
)
RETURNS DOUBLE PRECISION
AS
BEGIN
    DECLARE @Cost DOUBLE PRECISION;
    SET @Cost = (@Area * @Area + @LevelCost + @ComplexCost) * @Multiplier;
    RETURN @Cost;
END;

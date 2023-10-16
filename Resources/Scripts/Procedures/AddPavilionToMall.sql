CREATE PROCEDURE AddPavilionToMall
	@MallId INT,
	@PavilionName NVARCHAR(100),
	@Floor INT,
	@StatusId INT,
	@Square DOUBLE PRECISION,
	@Cost DOUBLE PRECISION,
	@PavilionCount INT,
	@ValueAddedFactor DOUBLE PRECISION
AS
BEGIN
	DECLARE @PavilionLimit INT;
	SELECT @PavilionLimit = PavilionCount
	FROM Malls
	WHERE IdShoppingMall = @MallId;

	IF @PavilionCount < @PavilionLimit  -- �������� ����������� �� ���������� Pavilions
	BEGIN
		INSERT INTO Pavilions (IdShoppingMall, NamePavilions, FloorPavilion, IdStatusPavilion, SquarePavilions, CostPerMeter, ValueAddedFactor)
		VALUES (@MallId, @PavilionName, @Floor, @StatusId, @Square, @Cost, @ValueAddedFactor);

		-- ����������� ������� PavilionCount
		UPDATE Malls
		SET PavilionCount = @PavilionCount + 1
		WHERE IdShoppingMall = @MallId;
	END
	ELSE
	BEGIN
    -- ����� ����� �������� ��������� ������ ��� ������� ��������� � ���������� ������
    THROW 50001, '�������� ����� �� ���������� Pavilions � ������ Mall.', 1;
	END
END;

---- ������ ������
--EXEC AddPavilionToMall
--    @MallId = 3, -- ID ��������� ������
--    @PavilionName = '����������������',
--    @Floor = 1,
--    @StatusId = 2, -- ID ������� ���������
--    @Square = 120.5,
--    @Cost = 2500.0,
--    @PavilionCount = 0, -- ������� ���������� ���������� (������� ��� �� Malls)
--    @ValueAddedFactor = 1.0;
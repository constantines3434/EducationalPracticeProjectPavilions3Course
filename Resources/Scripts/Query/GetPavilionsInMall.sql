SELECT
    P.IdPavilion AS 'ID ���������',
    P.NamePavilions AS '�������� ���������',
    P.FloorPavilion AS '����',
    PS.StatusName AS '������ ���������',
    P.SquarePavilions AS '�������',
    P.CostPerMeter AS '��������� ��. �',
    P.ValueAddedFactor AS '����������� ���������� ���������'
FROM
    Pavilions AS P
JOIN
    StatusPavilions AS PS ON P.IdStatusPavilion = PS.IdStatusPavilion
WHERE
    P.IdShoppingMall = 3;
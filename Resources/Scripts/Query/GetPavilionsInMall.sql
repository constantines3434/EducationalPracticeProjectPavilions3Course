SELECT
    P.IdPavilion AS 'ID павильона',
    P.NamePavilions AS 'Название павильона',
    P.FloorPavilion AS 'Этаж',
    PS.StatusName AS 'Статус павильона',
    P.SquarePavilions AS 'Площадь',
    P.CostPerMeter AS 'Стоимость кв. м',
    P.ValueAddedFactor AS 'Коэффициент добавочной стоимости'
FROM
    Pavilions AS P
JOIN
    StatusPavilions AS PS ON P.IdStatusPavilion = PS.IdStatusPavilion
WHERE
    P.IdShoppingMall = 3;
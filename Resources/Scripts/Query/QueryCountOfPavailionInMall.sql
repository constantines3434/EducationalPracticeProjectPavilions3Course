SELECT M.IdShoppingMall, M.NameMalls, COUNT(P.IdPavilion) AS PavilionCount
FROM Malls M
LEFT JOIN Pavilions P ON M.IdShoppingMall = P.IdShoppingMall
GROUP BY M.IdShoppingMall, M.NameMalls;
SELECT M.IdShoppingMall, M.NameMalls, COUNT(P.IdPavilion) AS PavilionCount
FROM Malls M
LEFT JOIN Pavilions P ON M.IdShoppingMall = P.IdShoppingMall
WHERE M.IdShoppingMall = 3
GROUP BY M.IdShoppingMall, M.NameMalls;
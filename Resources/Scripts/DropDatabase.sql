	USE master;
GO

-- Проверяем, существует ли база данных
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Base')
BEGIN
    -- Останавливаем все соединения с базой данных
    ALTER DATABASE Base
    SET SINGLE_USER
    WITH ROLLBACK IMMEDIATE;
    
    -- Удаляем базу данных
    DROP DATABASE Base;
END
GO


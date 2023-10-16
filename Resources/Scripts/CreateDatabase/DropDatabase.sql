	USE master;
GO

-- ���������, ���������� �� ���� ������
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Base')
BEGIN
    -- ������������� ��� ���������� � ����� ������
    ALTER DATABASE Base
    SET SINGLE_USER
    WITH ROLLBACK IMMEDIATE;
    
    -- ������� ���� ������
    DROP DATABASE Base;
END
GO


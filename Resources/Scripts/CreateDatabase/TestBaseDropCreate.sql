	USE master;
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'TestBase')
    DROP DATABASE TestBase;
GO
CREATE DATABASE TestBase
ON   
( NAME = 'TestBase_data',  
    FILENAME = 'C:\\VS Projects\\EducationalPracticeProjectPavilions3Course\\Resources\\Scripts\\TestBasedata.mdf',  
    SIZE = 8,  
    MAXSIZE = 100,  
    FILEGROWTH = 10 )  
LOG ON  
( NAME = 'TestBase_log',  
    FILENAME = 'C:\\VS Projects\\EducationalPracticeProjectPavilions3Course\\Resources\\Scripts\\TestBaselog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB );  
GO  

USE TestBase;
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Images')
	DROP TABLE Images
CREATE TABLE Images
(
	Id int IDENTITY PRIMARY KEY,
	ImageName varbinary(MAX),
);

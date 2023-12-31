	USE master;
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'PavilionsBase')
    DROP DATABASE PavilionsBase;
GO
CREATE DATABASE PavilionsBase
ON   
( NAME = 'PavilionsBase_data',  
    FILENAME = 'C:\\VS Projects\\EducationalPracticeProjectPavilions3Course\\Resources\\Scripts\\PavilionsBasedata.mdf',  
    SIZE = 8,  
    MAXSIZE = 100,  
    FILEGROWTH = 10 )  
LOG ON  
( NAME = 'PavilionsBase_log',  
    FILENAME = 'C:\\VS Projects\\EducationalPracticeProjectPavilions3Course\\Resources\\Scripts\\PavilionsBaselog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB );  
GO  

USE PavilionsBase;
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Cities')
	DROP TABLE Cities
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Employees')
	DROP TABLE Employees
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Pavilions')
	DROP TABLE Pavilions
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Rent')	
	DROP TABLE Rent
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Roles')
	DROP TABLE Roles
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Malls')
	DROP TABLE Malls
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StatePavilions')
	DROP TABLE StatePavilions
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StatusPavilions')
	DROP TABLE StatusPavilions
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StatusRent')
	DROP TABLE StatusRent
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StatusMalls')
	DROP TABLE StatusMalls
IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'Tenants')
	DROP TABLE Tenants

CREATE TABLE StatusMalls
(
    IdStatusMall INT IDENTITY PRIMARY KEY, 
	StatusMallName nvarchar(13)
);
INSERT INTO StatusMalls (StatusMallName)
VALUES 
('План'),
('Строительство'),
('Реализация'),
('Удалён');

CREATE TABLE Cities
(
	IdCity INT IDENTITY PRIMARY KEY,
    NameCity nvarchar(100)
);
INSERT INTO Cities (NameCity)
VALUES
('Москва'),
('Санкт-Петербург'),
('Новосибирск'),
('Екатеринбург'),
('Нижний Новгород'),
('Казань'),
('Челябинск'),
('Самара'),
('Ростов-на-Дону');
CREATE TABLE Malls
(
	IdShoppingMall int IDENTITY PRIMARY KEY,
    NameMalls nvarchar(100),
	IdStatusMall int,
	IdCity int,
	Price DOUBLE PRECISION,
	ValueAddedFactor DOUBLE PRECISION,
	NumberOfStoreys int,
	PavilionCount INT, --количество Pavilions
	ImageShoppingMall varbinary(MAX),
	FOREIGN KEY (IdStatusMall) REFERENCES StatusMalls (IdStatusMall) ON DELETE CASCADE
ON UPDATE NO ACTION,
	FOREIGN KEY (IdCity) REFERENCES Cities (IdCity) ON DELETE CASCADE
ON UPDATE NO ACTION
);


INSERT INTO Malls (NameMalls, PavilionCount, IdStatusMall, IdCity,
						   Price, ValueAddedFactor, NumberOfStoreys,
						   ImageShoppingMall)
SELECT 'Ривьера', 0, 1, 1, 8260042, 0.5, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Ривьера.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Авиапарк', 9, 2, 2, 3297976, 0.1, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Авиапарк.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Мега Белая Дача', 16, 4, 3, '9006645', 1.7, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Мега Белая Дача.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Рио', 5, 3, 4, 1888653, 0.5, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Рио.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Вегас', 0, 1, 5, 7567411, 0.4, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Вегас.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Лужайка', 10, 2, 6, 4603336, 0.8, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Лужайка.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Кунцево Плаза', 8, 2, 7, 6797653, 1.1, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Кунцево Плаза.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Мозаика', 20, 2, 8, 1429419, 0, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Мозаика.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Океания', 0, 1, 8, 5192089, 1.8, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Океания.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Гранд', 20, 2, 9, 2573981, 0.1, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Гранд.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Бутово Молл', 0, 1, 1, 5327641, 1.7, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Бутово Молл.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Рига Молл', 0, 1, 9, 9788316, 0.7, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Рига Молл.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Шоколад', 12, 2, 7, 2217279, 1.1, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Шоколад.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'АфиМолл Сити', 9, 3, 2, 8729160, 0.9, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\АфиМолл Сити.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Европейский', 9, 2, 1, 5690500, 0.7, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Европейский.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Гагаринский', 0, 1, 4, '1508807', 1.6, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Гагаринский.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Метрополис', 0, 1, 2, '8117913', 0.0, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Метрополис.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Мега Химки', 3, 3, 5, '3373234', 0.4, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Мега Химки.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Москва', 12, 3, 1, 4226505, 0.3, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Москва.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Вегас Кунцево', 12, 3, 1, 3878254, 0.2, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Вегас Кунцево.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Город Лефортово', 12, 4, 3, 1966214, 1.7, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Город Лефортово.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Золотой Вавилон Ростокино', 16,3, 4, 1632702, 1.8, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Золотой Вавилон Ростокино.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Шереметьевский', 16, 2, 3, 2941379, 1.0, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Шереметьевский.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Ханой-Москва', 0, 1, 1, 9580221, 0.3, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Ханой-Москва.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Ашан Сити Капитолий', 4, 2, 4, 5309117, 1.9, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Ашан Сити Капитолий.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Черемушки', 15, 3, 3, 7344604, 1.0, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Черемушки.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Филион',8 ,2, 1, 5343904, 0.3, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Филион.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Весна', 3, 4, 5, 4687701, 0.8, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Весна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Гудзон', 3, 3, 4, 7414460, 0.0, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Гудзон.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Калейдоскоп', 10, 3, 3, 7847659, 0.7, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Калейдоскоп.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Новомосковский', 0, 1, 6, 7161962.85, 0.4, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Новомосковский.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Хорошо', 20, 3, 9, 8306576, 1.9, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Хорошо.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Щука', 5, 2, 5, 5428485, 0.4, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Щука.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Атриум', 4, 3, 6, 5059779, 0.2, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Атриум.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Принц Плаза', 8, 3, 8, 1786688, 1.5, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Принц Плаза.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Облака', 0, 2, 2, 1688905, 0.6, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Облака.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Три Кита', 0, 2, 6, 3089700, 1.7, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Три Кита.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Реутов Парк', 4, 1, 9, 1995904, 1.5, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Реутов Парк.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'ЕвроПарк', 20, 4, 3, 9391338, 0.7, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\ЕвроПарк.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'ГУМ', 5, 3, 2, 6731491, 1.9, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\ГУМ.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Райкин Плаза', 12, 3, 2, 8498470, 1.8, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Райкин Плаза.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Новинский пассаж', 8, 3, 4, 3158957, 1.7, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Новинский пассаж.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Наш Гипермаркет', 0, 1, 9, 1088735, 1.2, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Наш Гипермаркет.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Красный Кит', 9, 2, 6, 1912149, 1.1, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Красный Кит.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Мегаполис', 0, 1, 7, 2175218.5, 0.5, 2, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Мегаполис.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Отрада', 4, 2, 2, 6760316, 0.9, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Отрада.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Твой дом', 0, 1, 7, 6810865, 1.7, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Твой дом.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Фестиваль', 16 , 4, 3, 1828013, 0.2, 4, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Фестиваль.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Времена Года', 15, 3, 4, 2650484, 1.7, 3, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Времена Года.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Армада', 0, 1, 9, 9172489, 0.9, 1, BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image ТЦ\Армада.jpg', SINGLE_BLOB) AS ImageData;
-----------------------------------------------------
CREATE TABLE Roles
(
	IdRole INT IDENTITY PRIMARY KEY,
    NameRole nvarchar(100)
);
INSERT INTO Roles(NameRole)
VALUES
('Администратор'),
('Менеджер А'),
('Менеджер С'),
('Удалён');
CREATE TABLE Employees
(
    IdEmployee INT IDENTITY PRIMARY KEY,
	Surname nvarchar(100),
	NameEmployee nvarchar(100),
	Patronimic nvarchar(100),
	LoginEmployee nvarchar(100),
	PasswordEmployee nvarchar(100),
	IdRole int,
	PhoneNumber nvarchar(100),
	Gender nvarchar(1),
	Photo varbinary(MAX),
	FOREIGN KEY (IdRole) REFERENCES Roles (IdRole) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO Employees (Surname, NameEmployee, Patronimic, LoginEmployee, PasswordEmployee, IdRole, PhoneNumber, Gender, Photo)
SELECT 'Чашин', 'Елизар', 'Михеевич', 'Elizor@gmai.com', 'yntiRS', 1, '8-107-628-29-16', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Чашин Елизар Михеевич.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Филенкова', 'Владлена', 'Олеговна', 'Vladlena@gmai.com', '07i7Lb', 2, '8-334-146-01-51', 'Ж', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Филенкова Владлена Олеговна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Ломовцев', 'Адам', 'Владимирович', 'Adam@gmai.com', '7SP9CV', 3,
'8-856-051-93-29', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Ломовцев Адам Владимирович.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Тепляков', 'Кир', 'Федосиевич', 'Kar@gmai.com', '6QF1WB', 4,
'8-824-893-24-03', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Тепляков Кир Федосиевич.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Рюриков', 'Юлий', 'Глебович', 'Yli@gmai.com', 'GwffgE', 1,
'8-640-270-13-10', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Рюриков Юлий Глебович.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Беломестина', 'Василиса', 'Тимофеевна', 'Vasilisa@gmai.com', 'd7iKKV,', 1,
'8-929-207-44-77', 'Ж', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Беломестина Василиса Тимофеевна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Панькива', 'Галина', 'Яковна', 'Galina@gmai.com', '8KC7wJ', 2,
'8-405-088-73-89', 'Ж', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Панькива Галина Якововна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Зарубин', 'Мирон', 'Мечиславович', 'Miron@gmai.com', 'x58OAN', 1,
'8-601-019-50-20', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Зарубин Мирон Мечиславович.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Веточкина', 'Вселава', 'Андрияновна', 'Vseslava@gmai.com', 'fhDSBf', 3,
'8-078-429-57-86', 'Ж', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Веточкина Всеслава Андрияновна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Рябова', 'Виктория', 'Елизаровна', 'Victoria@gmai.com', '9mlPQJ', 4,
'8-639-490-40-16', 'Ж', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Рябова Виктория Елизаровна.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Федотов', 'Леон', 'Фёдорович', 'Anisa@gmai.com', 'Wh4OYm', 1,
'8-223-264-95-99', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Федотов Леон Фёдорович.jpg', SINGLE_BLOB) AS ImageData
UNION ALL
SELECT 'Шарапов', 'Феофан', 'Кириллович', 'Feafan@gmai.com', 'Kc1PeS', 2,
'8-789-762-30-28', 'М', BulkColumn
FROM OPENROWSET(BULK 'C:\VS Projects\EducationalPracticeProjectPavilions3Course\Resources\Images\Image Сотрудники\Шарапов Феофан Кириллович.jpg', SINGLE_BLOB) AS ImageData

CREATE TABLE StatusPavilions
(
    IdStatusPavilion INT IDENTITY PRIMARY KEY,
    StatusName nvarchar(13)
);
INSERT INTO StatusPavilions (StatusName)
VALUES
('Арендован'),
('Забронировано'),
('Свободен'),
('Удален');

CREATE TABLE Pavilions
(
	IdPavilion INT IDENTITY PRIMARY KEY,
    IdShoppingMall int,
	NamePavilions nvarchar(100),
	FloorPavilion int,
	IdStatusPavilion int,
	SquarePavilions DOUBLE PRECISION,
	CostPerMeter DOUBLE PRECISION,
	ValueAddedFactor DOUBLE PRECISION,
	FOREIGN KEY (IdShoppingMall) REFERENCES Malls (IdShoppingMall) ON DELETE CASCADE
ON UPDATE NO ACTION,
	FOREIGN KEY (IdStatusPavilion) REFERENCES StatusPavilions (IdStatusPavilion) ON DELETE CASCADE
ON UPDATE NO ACTION
);
INSERT INTO Pavilions (NamePavilions, IdShoppingMall, FloorPavilion, IdStatusPavilion,
					   SquarePavilions, CostPerMeter, ValueAddedFactor)
VALUES
('88А', 3, 1, 3, 75, 3308, 0.1),('40А', 2, 3, 2, 96, 690, 1.1),
('76Б', 3, 2, 4, 199, 4938, 0.9),('61А', 4, 1, 1, 186, 2115, 0.9),
('58В', 10, 4, 1, 98, 1306, 1.9),('7А', 6, 2, 2, 187, 2046, 1),
('13Б',7, 1, 2, 141, 1131, 0.1),('60В', 8, 2, 2, 94, 361, 0.3),
('56А', 10, 1, 1, 148, 1163, 0.6),('63Г', 10, 2, 2, 153, 3486, 0.7),
('8Г', 11, 1, 1, 122, 2451, 1.5),('94В', 10, 3, 3, 132, 4825, 2),
('87Г', 13, 1, 2, 174, 793, 1.5),('93В', 14, 1, 1, 165, 4937, 0.8),
('10А', 15, 3, 2, 168, 1353, 1.7),('53Г', 23, 1, 1, 99, 3924, 1),
('73В', 23, 2, 3, 116, 3418, 0),('89Б', 18, 1 , 3, 92, 562, 0.4),
('44Г', 19, 2, 2, 66.6, 870, 1),('65А', 20, 2, 2, 95, 1381, 1.7),
('16Г', 21, 2, 4, 150, 747, 0.8),('61Б', 22, 1, 3, 58, 1032, 1.7),
('34Б', 23, 4, 2, 102, 4715, 0.3),('91Г', 23, 3, 1, 171, 1021, 1.2),
('70Г', 25, 1, 2, 83, 2246, 1.4),('10А', 26, 1, 2, 187, 2067, 0),
('80Г', 27, 1, 2, 117, 1049, 1.3),('2Б', 28, 1, 4, 176, 1673, 1.7),
('41А', 29, 1, 3, 108, 344, 0),('16Г', 30, 2, 1, 125, 1037, 1.3),
('13Б', 22, 2, 3, 161.5, 2776, 0.4),('83Г', 32, 2, 1, 85.5, 4584, 0.3),
('9Г', 33, 1 ,2, 131, 2477, 1.5),('49Г', 34, 1, 1, 164, 2728, 0.9),
('1Г', 35, 1, 4, 157, 1963, 1.6),('37А', 22, 3, 1, 187, 3173, 0.3),
('38Г', 22, 4, 1, 97, 1364, 0.5),('27В', 38, 1, 2, 96, 3134, 0.1),
('67Б', 39, 1, 4, 55, 4442, 0.8),('86Г', 40, 1, 3, 58, 3707, 0.5),
('98А', 41, 3, 2, 172.5, 4951, 1.1),('11Г',	42, 2, 1, 194, 827, 0.6),
('42В', 48, 1, 3, 166, 4216, 0.7),('55А', 44, 2, 2, 127, 703, 1),
('6Б', 48, 2, 3, 119, 3262, 1.9),('15А', 46, 1, 2, 90, 1569, 1.3),
('27Б', 48, 3, 1, 132, 627, 0.4),('65Б', 48, 4, 4, 60, 3052, 0.6),
('26А', 49, 1, 3, 95, 670, 1.7),('53В', 49, 3, 1, 132, 510, 1.2);
CREATE TABLE StatePavilions
(
	IdStatePavilion INT IDENTITY PRIMARY KEY,
	IdPavilion int,
    IdEmployee int NULL,
	FOREIGN KEY (IdPavilion) REFERENCES Pavilions (IdPavilion) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (IdEmployee) REFERENCES Employees (IdEmployee) ON DELETE CASCADE ON UPDATE CASCADE,
);
INSERT INTO StatePavilions (IdPavilion, IdEmployee)
VALUES
(1, 2),(2, 7),(3, NULL),(4, 11),(5, 2),(6, 7),(7, 11),(8, 2),(9, 11),(10, 2),(11, 7),(12, 2),(13, 2),(14, 11),(15, 7),(16, 7),(17, 11),(18, 2),
(19, NULL),(20, NULL),(21, NULL),(22, 7),(23, 2),(24, 11),(25, 2),(26, NULL),(27, 11),(28, NULL),(29, 7),(30, NULL),(31, 11),(32, 7),
(33, 11),(34, 2),(35, NULL),(36, 2),(37, 2),(38, 11),(39, NULL),(40, 7),(41, 11),(42, 11),(43, 2),(44, 7),(45, 2),(46, 2),(47, 11),(48, NULL),
(49, 11),(50,7);
CREATE TABLE Tenants
(
    IdTenant INT IDENTITY PRIMARY KEY, 
	CompanyName nvarchar(100),
	PhoneNumber nvarchar(15),
	Adress nvarchar(100)
);
INSERT INTO Tenants (CompanyName, PhoneNumber, Adress)
VALUES 
('AG Marine', '8-495-526-14-53', 'Москва, Бабаевская улица д.16'),
('Art-elle', '8-495-250-58-94', 'Санкт-Петербург, Амбарная улица д.25 корп.1'),
('Atlantis', '8-495-424-11-65', 'Новосибирск, Улица Каменская д.16'),
('Corporate Travel', '8-495-245-39-05', 'Екатеринбург, Улица Антона Валека д.56'),
('GallaDance', '8-495-316-77-94', 'Нижний Новгород, Улица Ларина д.34');
CREATE TABLE StatusRent
(
	IdStatusRent INT IDENTITY PRIMARY KEY,
    StatusRentName nvarchar(8)	
);
INSERT INTO StatusRent (StatusRentName)
VALUES 
('Открыт'),
('Ожидание'),
('Закрыт');
CREATE TABLE Rent
(
	IdRent INT IDENTITY PRIMARY KEY,
    IdTenant int,
	IdStatePavilion int,
	IdStatusRent int,
	StartOfLease date,
	EndOfLease date,
	FOREIGN KEY (IdTenant) REFERENCES Tenants (IdTenant) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (IdStatusRent) REFERENCES StatusRent (IdStatusRent) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (IdStatePavilion) REFERENCES StatePavilions (IdStatePavilion) ON DELETE CASCADE ON UPDATE CASCADE
);
INSERT INTO Rent (IdTenant, IdStatePavilion, IdStatusRent, StartOfLease, EndOfLease)
VALUES
(2, 1, 1, '2019-01-24', '2020-11-17'),(2, 2, 2, '2019-11-21', '2020-04-18'),
(5, 4, 3, '2018-11-12', '2019-06-28'),(1, 5, 3, '2018-10-18', '2019-09-16'),
(5, 6, 2, '2019-12-19', '2020-06-26'),(2, 7, 2, '2019-12-16','2020-05-12'),
(4, 8, 2, '2019-12-06', '2020-10-16'),(2, 9, 3, '2018-09-03', '2019-02-10'),
(2, 10, 2, '2019-11-04', '2020-06-27'),(4, 11, 3, '2018-11-08', '2019-01-16'),
(1, 12, 1, '2019-06-07', '2020-03-18'),(1, 13, 2, '2019-11-15', '2020-06-20'),
(5, 14, 3, '2018-10-09', '2019-09-22'),(5, 15, 2, '2019-11-24', '2020-07-17'),
(1, 16, 3, '2018-07-20', '2019-06-07'),(3, 17, 1, '2019-02-04', '2020-07-08'),
(2, 18, 1, '2019-08-06', '2020-08-20'),(1, 22, 1, '2019-05-23', '2020-05-13'),
(3, 23, 2, '2019-12-16', '2020-03-16'),(3, 24, 3, '2018-08-24', '2019-08-04'),
(5, 25, 2, '2019-11-09', '2020-08-17'),(4, 15, 2, '2019-12-02', '2020-07-24'),
(2, 27, 2, '2019-11-23', '2020-08-06'),(4, 29, 1, '2019-05-02', '2020-06-24'),
(3, 7, 2, '2019-12-08', '2020-08-17'),(3, 32, 3, '2018-11-14', '2019-04-19'),
(4, 33, 2, '2019-12-26', '2020-09-13'),(1, 34, 3, '2018-09-16', '2019-02-12'),
(3, 36, 3, '2018-10-18', '2019-06-22'),(4, 37, 3, '2018-06-23', '2019-08-26'),
(3, 38, 2, '2019-12-18', '2020-05-23'),(5, 40, 1, '2019-04-01', '2020-12-19'),
(4, 41, 2, '2019-11-22', '2020-08-15'),(3, 42, 3, '2018-10-08', '2019-07-21'),
(2, 43, 1, '2019-04-07', '2020-03-05'),(1, 44, 2, '2019-11-07', '2020-03-08'),
(3, 45, 1, '2019-07-15', '2020-04-25'),(1, 46, 2, '2019-12-09', '2020-12-02'),
(4, 47, 3, '2018-08-05', '2019-06-14'),(2, 49, 1, '2019-08-19', '2020-09-02'),
(4, 50, 2, '2018-09-20', '2019-02-12');

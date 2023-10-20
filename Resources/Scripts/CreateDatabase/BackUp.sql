-- Создайте резервную копию базы данных
DECLARE @BackupFileName NVARCHAR(1000)
SET @BackupFileName = 'C:\\VS Projects\\EducationalPracticeProjectPavilions3Course\\Resources\\Scripts\\CreateDatabase\\backup__PavilionsBase_' + 
    REPLACE(REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 120), ':', ''), '-', '_') + '.bak';

BACKUP DATABASE PavilionsBase
TO DISK = @BackupFileName;

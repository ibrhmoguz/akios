USE ACKAppDB
GO
DECLARE @tname VARCHAR(150)
DECLARE @strsql VARCHAR(300)
 
SELECT @tname = (SELECT TOP 1 [name] FROM sys.objects WHERE [type] = 'U'
ORDER BY [name])
 
WHILE @tname IS NOT NULL
BEGIN
    SELECT @strsql = 'DROP TABLE [dbo].[' + RTRIM(@tname) +']'
    EXEC (@strsql)
    PRINT 'Dropped Table : ' + @tname
    SELECT @tname = (SELECT TOP 1 [name] FROM sys.objects WHERE [type] = 'U' AND [name] > @tname 
    ORDER BY [name])
END
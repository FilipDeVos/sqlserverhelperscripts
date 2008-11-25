IF OBJECT_ID('dbo.Alone') is not null
   DROP PROCEDURE dbo.Alone
GO
/*
   Sometimes you want to be alone on a db, all alone. Especially on SQL Server 2008. Where Management Studio 
   wants to make connections for intellisense and what not.  This makes for example restoring databases really 
   a pain because there is always additional connections. 
   
   Meet "Alone". This procedure will kill all connections to a db except yourself. 
   
*/
CREATE PROCEDURE dbo.Alone
	@database sysname = NULL
AS

    SET @database = COALESCE(@database, db_name());

    DECLARE @spidstr VARCHAR(8000); 

    SELECT @spidstr = coalesce(@spidstr, '') + 'kill ' + cast(spid as varchar)+ '; ' 
    FROM master..sysprocesses 
    WHERE dbid = db_id(@database) 
      and spid >= 50
      and spid <> @@SPID; 
      
    EXEC (@spidstr)

GO


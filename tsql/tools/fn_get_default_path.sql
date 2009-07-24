IF object_id('dbo.fn_get_default_path') is not null
    DROP FUNCTION dbo.fn_get_default_path
GO
/*
In all it's wisdom the SQL Server Development team decided to store the location 
of the Default database path in a different registry location for about every possible 
release of SQL Server. And they did not provide a function/procedure to get the 
locations from inside SQL Server. This function allows you to query the default path 
for the DATA and LOG devices which is very handy to restore databases.

A common usage of this function:
declare @sql nvarchar(2000), 
        @data varchar(260), 
        @log varchar(260); 

select @data = dbo.fn_get_default_path(0),
       @log = dbo.fn_get_default_path(1)

select @sql= 'RESTORE DATABASE DefaultLocationDB 
FROM  DISK = N''c:\backups\DemoDB.bak'' WITH  FILE = 1,  
MOVE N''demo_data_device'' TO N''' + @data + '\DemoDb.ldf'',  
MOVE N''demo_log_device'' TO N''' +  @log + '\DemoDb.ldf'',  
NOUNLOAD, REPLACE'

exec (@sql)

  
   
*/
CREATE FUNCTION dbo.fn_get_default_path(@log bit)
RETURNS nvarchar(260)
AS
BEGIN
    DECLARE @instance_name nvarchar(200), @system_instance_name nvarchar(200), @registry_key nvarchar(512), @path nvarchar(260), @value_name nvarchar(20);

    SET @instance_name = COALESCE(convert(nvarchar(20), serverproperty('InstanceName')), 'MSSQLSERVER');

    -- sql 2005/2008 with instance
    EXEC master.dbo.xp_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\Microsoft SQL Server\Instance Names\SQL', @instance_name, @system_instance_name output;
    SET @registry_key = N'Software\Microsoft\Microsoft SQL Server\' + @system_instance_name + '\MSSQLServer';

    SET @value_name = N'DefaultData'
    IF @log = 1
      BEGIN
        SET @value_name = N'DefaultLog';
      END

    EXEC master.dbo.xp_regread N'HKEY_LOCAL_MACHINE', @registry_key, @value_name, @path output;

    IF @log = 0 AND @path is null -- sql 2005/2008 default instance
      BEGIN
        SET @registry_key = N'Software\Microsoft\Microsoft SQL Server\' + @system_instance_name + '\Setup';
        EXEC master.dbo.xp_regread N'HKEY_LOCAL_MACHINE', @registry_key, N'SQLDataRoot', @path output;
        SET @path = @path + '\Data';
      END

    IF @path IS NULL -- sql 2000 with instance
      BEGIN
        SET @registry_key = N'Software\Microsoft\Microsoft SQL Server\' + @instance_name + '\MSSQLServer';
        EXEC master.dbo.xp_regread N'HKEY_LOCAL_MACHINE', @registry_key, @value_name, @path output;
      END

    IF @path IS NULL -- sql 2000 default instance
      BEGIN
        SET @registry_key = N'Software\Microsoft\MSSQLServer\MSSQLServer';
        EXEC master.dbo.xp_regread N'HKEY_LOCAL_MACHINE', @registry_key, @value_name, @path output;
      END

    IF @log = 1 AND @path is null -- fetch the default data path instead.
      BEGIN
        SELECT @path = dbo.fn_get_default_path(0)
      END

    RETURN @path;
END
GO
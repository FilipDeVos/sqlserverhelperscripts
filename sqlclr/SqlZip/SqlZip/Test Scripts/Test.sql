-- Examples for queries that exercise different SQL objects implemented by this assembly

-----------------------------------------------------------------------------------------
-- Stored procedure
-----------------------------------------------------------------------------------------
-- exec StoredProcedureName


-----------------------------------------------------------------------------------------
-- User defined function
-----------------------------------------------------------------------------------------
-- select dbo.FunctionName()


-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- CREATE TABLE test_table (col1 UserType)
-- go
--
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 1'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 2'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 3'))
--
-- select col1::method1() from test_table



-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- select dbo.AggregateName(Column1) from Table1
declare @a nvarchar(max)
declare @b nvarchar(max)
declare @c nvarchar(max)

select @b = N'-- //////////////////////////////////////////////////////////////////////////////
-- // Purpose: Financial Studio Database Release 3.0.0 Feature Pack 1                //
-- //                                                                          //
-- // Feature List: Basel II Incremental update March 2006 //
-- //////////////////////////////////////////////////////////////////////////////
'
select len(@b)

select @a = dbo.fn_zip_string(@b)
select len(@a)

select @c = dbo.fn_unzip_string(@a)
select len(@c)

if (@b = @c)
  print 'string is the same'
else
  print 'string is different'


declare @x varbinary(max)
declare @y varbinary(max)
declare @z varbinary(max)

select @x = cast(replicate('qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[qwertyuiop[', 10) as varbinary(max))
select @x = @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x + @x
select @y = dbo.fn_zip_binary(@x)
select @z = dbo.fn_unzip_binary(@y)

select len(@x), len(@y), len(@z)

if (@x = @z)
  print 'varbinary is the same'
else
  print 'varbinary is different'

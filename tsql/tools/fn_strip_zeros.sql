IF object_id('dbo.fn_strip_zeros') is not null
    DROP FUNCTION dbo.fn_strip_zeros
GO
/*
Strip trailing zeroes from a numeric value. It's not something recommended to 
do in stored procedures, but the request does appear.

This function is regional settings aware, so it works with , and .  as decimal 
separator.

Example input/output:
---------------------
1.0501	needs to show	1.0501
1.0500	needs to show	1.05
1.0000	needs to show	1

*/
CREATE FUNCTION dbo.fn_strip_zeros(@number numeric(38,10))
RETURNS varchar(38)
WITH ENCRYPTION
AS
  BEGIN
    DECLARE @result varchar(38)
    DECLARE @decimal varchar(3)

    SET @decimal = substring(convert(varchar(38), convert(numeric(38,10),1)/5 ), 2,1)

    set @result = rtrim(replace(replace(rtrim(replace(@number,'0',' ')),' ','0') + ' ', @decimal + ' ', ''))

    RETURN @result
  END
GO
Use Rt3

--Create Table ReData
--(
--ContractDate DateTime,
--Price nvarchar(max),
--StreetNo nvarchar(max),
--Street nvarchar(max),
--Suburb nvarchar(max),
--PostCode nvarchar(max),
--Area nvarchar(max),
--AreaType nvarchar(max),
--[Type] nvarchar(max),
--[Type2] nvarchar(max),
--)

Insert Into ReData
--Select Distinct * From(
--select 
--	convert(datetime, [Column 10], 103) [ContractDate],
--	[Column 11] Price, 
--	[Column 6] StreetNo, 
--	[Column 7] Street, 
--	[Column 8] Suburb, 
--	[Column 9] PostCode,
--	[Column 13] Area,
--	[Column 14] AreaType,
--	''			[Type],
--	''			Type2
--from Rt1.dbo.results
--where len([Column 10]) = 10 And len([Column 7]) > 1
--UNION ALL
--select
--	convert(datetime, [Column 13], 112) [ContractDate],
--	[Column 15] Price,
--	[Column 7] StreetNo, 
--	[Column 8] Street, 
--	[Column 9] Suburb, 
--	[Column 10] PostCode,
--	[Column 11] Area,
--	[Column 12] AreaType,
--	[Column 17] [Type],
--	[Column 18] Type2

--from Rt1.dbo.results
--where len([Column 10]) != 10 And len([Column 8]) > 1
--UNION ALL

	
	Select
		CASE WHEN TRY_CONVERT(datetime, [Column 13], 112) IS NULL   
    THEN 
		Case when TRY_CONVERT(datetime, [Column 14], 112) IS NULL   
			THEN cast('1753-1-1' as datetime)
			ELSE convert(datetime, [Column 14], 112) 
		End
	ELSE convert(datetime, [Column 13], 112) 
	END [ContractDate],
	[Column 15] Price,
	[Column 7] StreetNo, 
	[Column 8] Street, 
	[Column 9] Suburb, 
	[Column 10] PostCode,
	[Column 11] Area,
	[Column 12] AreaType,
	[Column 17] [Type],
	[Column 18] Type2

from Rt21.dbo.results
where len([Column 10]) != 10 And len([Column 8]) > 1


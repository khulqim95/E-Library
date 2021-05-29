USE [db2TierTemplate]
GO
/****** Object:  User [local]    Script Date: 29/05/2021 19:32:54 ******/
CREATE USER [local] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  UserDefinedFunction [dbo].[fn_Test]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[fn_Test] 
(
@StartDate as date,
@EndDate as date
)
returns int
as
BEGIN
Declare @Output int
Declare @Current as date = DATEADD(DD, 1, @StartDate);

Declare @v Table (displayDate date,
 nama nvarchar(max))


WHILE @Current < @EndDate
BEGIN
insert into @v
Select @Current, DATENAME(DW,@Current)
set @Current = DATEADD(DD, 1, @Current) -- add 1 to current day
END

select @Output = count(*) from (
Select displayDate,D.nama
	 ,(CASE WHEN H.Nama is null and D.nama NOT IN ('Saturday','Sunday') THEN 0  
			WHEN D.nama IN ('Saturday','Sunday') THEN 1
			ELSE 1
	 END) as FlagHoliday
from @v D
LEFT JOIN dbo.Tbl_Holiday H ON D.displayDate = H.Tanggal and H.IsActive = 1 and H.IsDeleted = 0 
) tbl
where FlagHoliday = 0


RETURN @Output
END
GO
/****** Object:  UserDefinedFunction [dbo].[Only_Numbers]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Only_Numbers] (     @string VARCHAR(8000) ) RETURNS VARCHAR(8000) AS BEGIN DECLARE @IncorrectCharLoc SMALLINT SET @IncorrectCharLoc = PATINDEX('%[^0-9]%', @string) WHILE @IncorrectCharLoc > 0 BEGIN     SET @string = STUFF(@string, @IncorrectCharLoc, 1, '')     SET @IncorrectCharLoc = PATINDEX('%[^0-9]%', @string)     END     SET @string = @string Return  @string END 

GO
/****** Object:  UserDefinedFunction [dbo].[TRIM]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[TRIM](@string VARCHAR(MAX)) 	RETURNS VARCHAR(MAX) 	BEGIN 	RETURN LTRIM(RTRIM(@string)) 	END 

GO
/****** Object:  UserDefinedFunction [dbo].[uf_AddThousandSeparators]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_AddThousandSeparators](@NumStr numeric(30,2)) 
RETURNS Varchar(50) 
AS 
BEGIN 
return REPLACE(FORMAT(@NumStr, N'N', 'el-GR'),',00','')

END




GO
/****** Object:  UserDefinedFunction [dbo].[uf_DateRangeDynamicQueryGenerator]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_DateRangeDynamicQueryGenerator] 
(
	@DateFrom VARCHAR(50), --dd-MM-yyyy hh:mm tt
	@DateTo VARCHAR(50), --dd-MM-yyyy hh:mm tt
	@ColumnName VARCHAR(50)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @Query VARCHAR(MAX)

	DECLARE @QueryIncludeNullValue VARCHAR(MAX) = ''

	IF @DateFrom LIKE '%01-01-1900%' AND @DateTo LIKE '%31-12-9999%'
	BEGIN
		SET @QueryIncludeNullValue = ' OR ' + @ColumnName + ' IS NULL'
	END
	SET @Query = 
		' AND (' + @ColumnName + ' BETWEEN '''+ 
		CONVERT(VARCHAR,CONVERT(DATETIME, @DateFrom, 103),120)  +
		''' AND '''+ 
		CONVERT(VARCHAR,CONVERT(DATETIME, @DateTo, 103),120) + '''' 
		+ @QueryIncludeNullValue +')'

	RETURN @Query

END











GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetAllRoleJabatan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[uf_GetAllRoleJabatan](@JabatanId int, @StatusJabatan int, @UnitId int) 
RETURNS Varchar(max) 
AS 
BEGIN 
declare @AllRoleName nvarchar(max)

SELECT @AllRoleName = STUFF((
            SELECT '|||' + CONCAT('- ', MR.Nama)
			  FROM Tbl_Mapping_Kewenangan_Jabatan MKJ
			  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = MKJ.RoleId
			   WHERE MKJ.JabatanId = @JabatanId and MKJ.StatusJabatan = @StatusJabatan
			   and MKJ.UnitId = @UnitId and MKJ.IsDeleted = 0
            FOR XML PATH('')
            ), 1, 1, '')

	SET @AllRoleName = REPLACE(@AllRoleName,'|||','<br />')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&gt;','>')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&<br/>;','<br/>')

		RETURN SUBSTRING(@AllRoleName,3,50000)

END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetAllRoleMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_GetAllRoleMenu](@MenuId int) 
RETURNS Varchar(max) 
AS 
BEGIN 
declare @AllRoleName nvarchar(max)

SELECT @AllRoleName = STUFF((
            SELECT '|||' + CONCAT('- ', MR.Nama)
			  FROM NavigationAssignment R
			  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = R.Role_Id
			   WHERE R.Navigation_Id = @MenuId and MR.IsActive = 1 and MR.IsDeleted = 0
            FOR XML PATH('')
            ), 1, 1, '')

	SET @AllRoleName = REPLACE(@AllRoleName,'|||','<br />')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&gt;','>')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&<br/>;','<br/>')

		RETURN SUBSTRING(@AllRoleName,3,50000)

END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetAllRolePegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_GetAllRolePegawai](@Pegawai_Id int) 
RETURNS Varchar(max) 
AS 
BEGIN 
declare @AllRoleName nvarchar(max)

SELECT @AllRoleName = STUFF((
            SELECT '|||' + CONCAT('- ', MR.Nama,' Unit ',U.Name)
			  FROM Tbl_Role_Pegawai R
			  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = R.Role_Id 
			  LEFT JOIN Tbl_Unit U ON U.Id = R.Unit_Id
			   WHERE R.Pegawai_Id = @Pegawai_Id and R.IsDeleted = 0
			   AND  (R.StatusRole=1 OR (R.StatusRole = 2 AND R.DateStart <= convert(date,getdate())  and R.DateEnd >= convert(date,getdate())))
			   AND MR.IsDeleted = 0 AND MR.IsActive = 1 
            FOR XML PATH('')
            ), 1, 1, '')

	SET @AllRoleName = REPLACE(@AllRoleName,'|||','<br />')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&gt;','>')
	--SET @AllRoleName = REPLACE(@AllRoleName,'&<br/>;','<br/>')

		RETURN SUBSTRING(@AllRoleName,3,50000)

END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetAllTotalProjectByUnit]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_GetAllTotalProjectByUnit](@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '1900-01-01',
@TanggalAkhir nvarchar(max) = '2200-12-31') 
RETURNS int
AS 
BEGIN 
declare @Jumlah int
SELECT @Jumlah = Count(*) 
				 FROM dbo.vw_Dashboard_AllProjectByUnit
				  WHERE UnitId IN (select * from dbo.uf_SplitString(@UnitIdSearch,','))
				  AND TanggalEstimasiMulai >= @TanggalAwal AND TanggalEstimasiSelesai <= @TanggalAkhir

RETURN @Jumlah
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetAllTotalSelisihHari]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_GetAllTotalSelisihHari](
@TanggalAwal nvarchar(max) = '1900-01-01',
@TanggalAkhir nvarchar(max) = '1900-01-01') 
RETURNS INT
AS 
BEGIN 
DECLARE @Jumlah int
EXEC dbo.sp_SELECTALLDATES @StartDate = @TanggalAwal,@EndDate = @TanggalAkhir, @Jumlah = @Jumlah OUTPUT
RETURN @Jumlah
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_getExpandMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[uf_getExpandMenu](@ParentNavigation_Id int, @Url nvarchar(max))
returns int
begin
	return(
		select count(*) from Navigation where ParentNavigation_Id = @ParentNavigation_Id and [Route] = @Url
	)
end
GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetFormatNota]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE function [dbo].[uf_GetFormatNota](@TotalAngka nvarchar(10),@Nominal nvarchar(100))
returns nvarchar(max)
as
begin
return (SELECT REPLICATE('0',@TotalAngka-LEN(@Nominal)) + @Nominal)
end
GO
/****** Object:  UserDefinedFunction [dbo].[uf_getSelisihHari]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[uf_getSelisihHari] 
(
@StartDate as date,
@EndDate as date
)
returns int
as
BEGIN

declare @StartDateView date,@EndDateView date,@Pengali int = 1

IF(@EndDate < @StartDate)
BEGIN
	SET @StartDateView = @EndDate
	SET @EndDateView = @StartDate
	SET @Pengali = -1
END
ELSE
BEGIN
	SET @StartDateView = @StartDate
	SET @EndDateView = @EndDate
	SET @Pengali = 1
END
Declare @Output int
Declare @Current as date = DATEADD(DD, 1, @StartDateView);

Declare @v Table (displayDate date,
 nama nvarchar(max))


WHILE @Current < @EndDateView
BEGIN
insert into @v
Select @Current, DATENAME(DW,@Current)
set @Current = DATEADD(DD, 1, @Current) -- add 1 to current day
END

select @Output = count(*) from (
Select displayDate,D.nama
	 ,(CASE WHEN H.Nama is null and D.nama NOT IN ('Saturday','Sunday') THEN 0  
			WHEN D.nama IN ('Saturday','Sunday') THEN 1
			ELSE 1
	 END) as FlagHoliday
from @v D
LEFT JOIN dbo.Tbl_Holiday H ON D.displayDate = H.Tanggal and H.IsActive = 1 and H.IsDeleted = 0 
) tbl
where FlagHoliday = 0


RETURN @Output * @Pengali
END
GO
/****** Object:  UserDefinedFunction [dbo].[uf_GetUnitHirarki]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[uf_GetUnitHirarki]
(
@unitid int
)
returns @TabelUnit Table (UnitID int, Code nvarchar(200),NamaUnit nvarchar(200), ParentID int, TypeUnit int, LevelID int)
as

begin
WITH UnitHierarchy (Id,Code,Name, Parent_Id,Type, Level)
AS
(
	SELECT a.Id,a.Code, a.Name, a.Parent_Id,a.Type, 0 AS Level
	FROM Tbl_Unit a 
	WHERE a.Id IN(@unitid) and a.IsActive=1 and a.IsDelete = 0
	
	UNION ALL
	
	SELECT a.Id, a.Code,a.Name,  a.Parent_Id,a.Type, Level + 1
	FROM Tbl_Unit a INNER JOIN UnitHierarchy c ON a.Parent_Id = c.Id
	where a.IsActive=1 and a.IsDelete = 0
)
 
insert into @TabelUnit
SELECT * FROM UnitHierarchy 

return

end
GO
/****** Object:  UserDefinedFunction [dbo].[uf_IndonesianDate]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_IndonesianDate](@Tgl datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatTanggal nvarchar(100)

SELECT @FormatTanggal = CONCAT(DAY(@Tgl), ' ' ,dbo.uf_SingkatanNamaBulan(MONTH(@Tgl)) ,' ', YEAR(@Tgl))
	RETURN @FormatTanggal
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_IndonesianDateDDMMYYYY]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_IndonesianDateDDMMYYYY](@Tgl datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatTanggal nvarchar(100)

SELECT @FormatTanggal = FORMAT(@Tgl,'dd/MM/yyyy')
	RETURN @FormatTanggal
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_IndonesianDateTime]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_IndonesianDateTime](@Tanggal datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatDate nvarchar(100)

SELECT @FormatDate = FORMAT(@Tanggal,'dd') + ' ' + dbo.uf_NamaBulan(FORMAT(@Tanggal,'MM')) + ' ' + FORMAT(@Tanggal,'yyyy') + ' ' + FORMAT(@Tanggal,'HH:mm:ss') + ' WIB'
	RETURN @FormatDate
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_IndonesianFullDate]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_IndonesianFullDate](@Tgl datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatTanggal nvarchar(100)

SELECT @FormatTanggal = CONCAT(DAY(@Tgl), ' ' ,dbo.uf_NamaBulan(MONTH(@Tgl)) ,' ', YEAR(@Tgl))
	RETURN @FormatTanggal
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_IndonesianTime]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_IndonesianTime](@Tgl datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatTanggal nvarchar(100)

SELECT @FormatTanggal = FORMAT(@Tgl,'HH:mm:ss')
	RETURN @FormatTanggal
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_LookupDynamicQueryGenerator]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_LookupDynamicQueryGenerator] 
(
	@LookupValue VARCHAR(MAX) = '', --if given NULL, Default as empty
	@ColumnName VARCHAR(50)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @Query VARCHAR(MAX)

	IF @LookupValue != ''
		BEGIN
			SELECT
				@Query = 
				COALESCE(
					@Query + ' OR ',''
				) + ' ' + @ColumnName +' LIKE ''%' + ColumnData + '%''' 
			FROM [dbo].[uf_SplitString](@LookupValue, ';') --use function uf_SplitString with semi colon as separator
	
			SELECT @Query = ' AND (' + @Query + ')'
	
		END
	ELSE
		BEGIN
			SET @Query = ''
		END
	
	-- Return the result of the function
	RETURN @Query

END











GO
/****** Object:  UserDefinedFunction [dbo].[uf_LookupDynamicQueryGeneratorAllSearch]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_LookupDynamicQueryGeneratorAllSearch] 
(
	@LookupValue VARCHAR(MAX) = '', --if given NULL, Default as empty
	@ColumnName VARCHAR(50)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @Query VARCHAR(MAX)

	IF @LookupValue != ''
		BEGIN
			SELECT
				@Query = 
				COALESCE(
					@Query + ' OR ',''
				) + ' ' + @ColumnName +' LIKE ''%' + ColumnData + '%''' 
			FROM [dbo].[uf_SplitString](@LookupValue, ';') --use function uf_SplitString with semi colon as separator
	
			SELECT @Query = ' OR (' + @Query + ')'
	
		END
	ELSE
		BEGIN
			SET @Query = ''
		END
	
	-- Return the result of the function
	RETURN @Query

END











GO
/****** Object:  UserDefinedFunction [dbo].[uf_LookupDynamicQueryGeneratorEqual]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_LookupDynamicQueryGeneratorEqual] 
(
	@LookupValue VARCHAR(MAX) = '', --if given NULL, Default as empty
	@ColumnName VARCHAR(50)
)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @Query VARCHAR(MAX)

	IF @LookupValue != ''
		BEGIN
			SELECT
				@Query = 
				COALESCE(
					@Query + ' OR ',''
				) + ' ' + @ColumnName +' = ' + ColumnData + '' 
			FROM [dbo].[uf_SplitString](@LookupValue, ';') --use function uf_SplitString with semi colon as separator
	
			SELECT @Query = ' AND (' + @Query + ')'
	
		END
	ELSE
		BEGIN
			SET @Query = ''
		END
	
	-- Return the result of the function
	RETURN @Query

END











GO
/****** Object:  UserDefinedFunction [dbo].[uf_LookupDynamicQueryGeneratorWithSeparator]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_LookupDynamicQueryGeneratorWithSeparator] 
(
	@LookupValue VARCHAR(MAX) = '', --if given NULL, Default as empty
	@ColumnName VARCHAR(50),
	@Separator VARCHAR(50)

)
RETURNS VARCHAR(MAX)
AS
BEGIN
	DECLARE @Query VARCHAR(MAX)

	IF @LookupValue != ''
		BEGIN
			SELECT
				@Query = 
				COALESCE(
					@Query + ' OR ',''
				) + ' ' + @ColumnName +' LIKE ''%' + ColumnData + '%''' 
			FROM [dbo].[uf_SplitString](@LookupValue, @Separator) --use function uf_SplitString with semi colon as separator
	
			SELECT @Query = ' AND (' + @Query + ')'
	
		END
	ELSE
		BEGIN
			SET @Query = ''
		END
	
	-- Return the result of the function
	RETURN @Query

END











GO
/****** Object:  UserDefinedFunction [dbo].[uf_NamaBulan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_NamaBulan](@NumMonth int) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @NamaBulan nvarchar(100)

SELECT @NamaBulan = (CASE WHEN @NumMonth = 1 THEN 'Januari'
			 WHEN @NumMonth = 2 THEN 'Februari'
			 WHEN @NumMonth = 3 THEN 'Maret'
			 WHEN @NumMonth = 4 THEN 'April'
			 WHEN @NumMonth = 5 THEN 'Mei'
			 WHEN @NumMonth = 6 THEN 'Juni'
			 WHEN @NumMonth = 7 THEN 'Juli'
			 WHEN @NumMonth = 8 THEN 'Agustus'
			 WHEN @NumMonth = 9 THEN 'September'
			 WHEN @NumMonth = 10 THEN 'Oktober'
			 WHEN @NumMonth = 11 THEN 'November'
			 WHEN @NumMonth = 12 THEN 'Desember'
			  END)
	RETURN @NamaBulan
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_NamaHari]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_NamaHari](@NumDay int) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @NamaHari nvarchar(100)

SELECT @NamaHari = (CASE 
			 WHEN @NumDay = 1 THEN 'Minggu'
			 WHEN @NumDay = 2 THEN 'Senin'
			 WHEN @NumDay = 3 THEN 'Selasa'
			 WHEN @NumDay = 4 THEN 'Rabu'
			 WHEN @NumDay = 5 THEN 'Kamis'
			 WHEN @NumDay = 6 THEN 'Jumat'
			 WHEN @NumDay = 7 THEN 'Sabtu'
			  END)
	RETURN @NamaHari
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_ShortIndonesianDateTime]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_ShortIndonesianDateTime](@Tanggal datetime) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @FormatDate nvarchar(100)

SELECT @FormatDate = FORMAT(@Tanggal,'dd') + ' ' + dbo.uf_SingkatanNamaBulan(FORMAT(@Tanggal,'MM')) + ' ' + FORMAT(@Tanggal,'yyyy')+ ' ' + FORMAT(@Tanggal,'HH:mm:ss')
	RETURN @FormatDate
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_SingkatanNamaBulan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_SingkatanNamaBulan](@NumMonth int) 
RETURNS Varchar(50) 
AS 
BEGIN 
declare @NamaBulan nvarchar(100)

SELECT @NamaBulan = (CASE WHEN @NumMonth = 1 THEN 'Jan'
			 WHEN @NumMonth = 2 THEN 'Feb'
			 WHEN @NumMonth = 3 THEN 'Mar'
			 WHEN @NumMonth = 4 THEN 'Apr'
			 WHEN @NumMonth = 5 THEN 'Mei'
			 WHEN @NumMonth = 6 THEN 'Jun'
			 WHEN @NumMonth = 7 THEN 'Jul'
			 WHEN @NumMonth = 8 THEN 'Agus'
			 WHEN @NumMonth = 9 THEN 'Sept'
			 WHEN @NumMonth = 10 THEN 'Okt'
			 WHEN @NumMonth = 11 THEN 'Nov'
			 WHEN @NumMonth = 12 THEN 'Des'
			  END)
	RETURN @NamaBulan
END

GO
/****** Object:  UserDefinedFunction [dbo].[uf_SplitString]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Function [dbo].[uf_SplitString] (     @Value Varchar(max), 	@Separator varchar(1) = ';' ) RETURNS @Table TABLE (ColumnData VARCHAR(100)) AS BEGIN     IF RIGHT(@Value, 1) <> @Separator     SELECT @Value = @Value + @Separator     DECLARE @Pos    BIGINT,             @OldPos BIGINT     SELECT  @Pos    = 1,             @OldPos = 1     WHILE   @Pos < LEN(@Value)         BEGIN             SELECT  @Pos = CHARINDEX(@Separator, @Value, @OldPos)             INSERT INTO @Table             SELECT  LTRIM(RTRIM(SUBSTRING(@Value, @OldPos, @Pos - @OldPos))) Col001             SELECT  @OldPos = @Pos + 1         END     RETURN END 

GO
/****** Object:  UserDefinedFunction [dbo].[uf_TrimAll]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[uf_TrimAll](@string VARCHAR(MAX)) 	
RETURNS VARCHAR(MAX) 	
BEGIN 	
RETURN LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(@string,CHAR(32),'()'),')(',''),'()',CHAR(32))))
END 




GO
/****** Object:  Table [dbo].[Tbl_Pegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Pegawai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Unit_Id] [int] NULL,
	[Role_Id] [int] NULL,
	[Id_JenisKelamin] [int] NULL,
	[Npp] [nvarchar](80) NULL,
	[Nama] [nvarchar](300) NULL,
	[Tempat_Lahir] [nvarchar](150) NULL,
	[Tanggal_Lahir] [date] NULL,
	[Alamat] [nvarchar](150) NULL,
	[Email] [nvarchar](50) NULL,
	[Lastlogin] [datetime] NULL,
	[Images] [nvarchar](max) NULL,
	[ImagesFullPath] [nvarchar](max) NULL,
	[NameImages] [nvarchar](max) NULL,
	[No_HP] [nvarchar](25) NULL,
	[IsActive] [bit] NULL,
	[Created_Date] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[Updated_Date] [datetime] NULL,
	[UpdatedBy_Id] [int] NULL,
	[Delete_Date] [datetime] NULL,
	[DeleteBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[LDAPLogin] [bit] NULL,
	[Jabatan_Id] [int] NULL,
 CONSTRAINT [PK_Pegawai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KategoriProjectId] [int] NULL,
	[SubKategoriProjectId] [int] NULL,
	[SkorProjectId] [int] NULL,
	[KompleksitasProjectId] [int] NULL,
	[KlasifikasiProjectId] [int] NULL,
	[MandatoryId] [int] NULL,
	[PeriodeProjectId] [int] NULL,
	[NotifikasiId] [int] NULL,
	[IsPIR] [int] NULL,
	[AreaWakilPemimpinId] [int] NULL,
	[ProjectStatusId] [int] NULL,
	[CloseOpenId] [int] NULL,
	[StatusProjectDalamPantauan] [int] NULL,
	[TopProjectId] [int] NULL,
	[JenisProjectId] [int] NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[ProjectNo] [nvarchar](150) NULL,
	[NoMemo] [nvarchar](150) NULL,
	[TanggalMemo] [date] NULL,
	[NoDRF] [nvarchar](150) NULL,
	[TanggalDRF] [date] NULL,
	[TanggalDisposisi] [date] NULL,
	[TanggalKlarifikasi] [date] NULL,
	[DetailRequirment] [text] NULL,
	[TanggalEstimasiMulai] [date] NULL,
	[TanggalEstimasiSelesai] [date] NULL,
	[TanggalEstimasiProduction] [date] NULL,
	[TanggalEstimasiDevelopmentAwal] [date] NULL,
	[TanggalEstimasiDevelopmentAkhir] [date] NULL,
	[TanggalEstimasiTestingAwal] [date] NULL,
	[TanggalEstimasiTestingAkhir] [date] NULL,
	[TanggalEstimasiPilotingAwal] [date] NULL,
	[TanggalEstimasiPilotingAkhir] [date] NULL,
	[TanggalEstimasiPIRAwal] [date] NULL,
	[TanggalEstimasiPIRAkhir] [date] NULL,
	[IsDone] [bit] NULL,
	[TanggalSelesaiProject] [datetime] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Unit]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Unit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Parent_Id] [int] NULL,
	[Wilayah_Id] [int] NULL,
	[Divisi_Id] [int] NULL,
	[Type] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](150) NULL,
	[Telepon] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[KodeRubrikDiv] [nvarchar](50) NULL,
	[KodeRubrikMemo] [nvarchar](50) NULL,
	[KodeRubrikNotin] [nvarchar](50) NULL,
	[DeletedTime] [datetime] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_dbo.Unit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Member]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Member](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[JobPositionId] [int] NULL,
	[PegawaiId] [int] NULL,
	[UnitPegawaiId] [int] NULL,
	[Keterangan] [text] NULL,
	[StatusProgressId] [int] NULL,
	[CatatanPegawai] [text] NULL,
	[SendAsTask] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[IsDone] [bit] NULL,
	[TanggalPenyelesaian] [date] NULL,
	[KeteranganPenyelesaian] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Member] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Job_Position]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Job_Position](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Job_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_data_project_member]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO











/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view [dbo].[v_data_project_member]
as
SELECT PM.[Id]
      ,[ProjectId]
	  ,P.Nama as NamaProject
	  ,P.ProjectNo as ProjectNo
	   ,P.StatusProjectDalamPantauan
      ,[JobPositionId]
	  ,JP.Nama as JobPosition
      ,[PegawaiId]
	  ,PG.Nama as NamaPegawai
	  ,PG.Npp as NppPegawai
      ,[UnitPegawaiId]
	  ,UN.Name as NamaUnit
      ,PM.[Keterangan]
      ,[StatusProgressId]
      ,[CatatanPegawai]
      ,[SendAsTask]
      ,CONCAT(dbo.uf_IndonesianDate([StartDate]),' - ',dbo.uf_IndonesianDate([EndDate])) as Periode
	  ,dbo.uf_IndonesianDate(PM.TanggalPenyelesaian) as TanggalPenyelesaian
      ,PM.[IsDone]
	  ,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and PM.EndDate is not null THEN CONCAT(dbo.[uf_getSelisihHari](getdate(),PM.EndDate),' hari') ELSE ' - ' END) as Selisih
	  ,(CASE WHEN (PM.IsDone = 1 OR PM.IsDone is null) and PM.TanggalPenyelesaian is not null THEN CONCAT(dbo.[uf_getSelisihHari](PM.StartDate,PM.TanggalPenyelesaian),' hari') ELSE ' - ' END) as JumlahHariPengerjaan
	  	  ,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and PM.EndDate is not null THEN dbo.[uf_getSelisihHari](getdate(),PM.EndDate) ELSE null END) as SelisihAngka
	  ,(CASE WHEN (PM.IsDone = 1 OR PM.IsDone is null) and PM.TanggalPenyelesaian is not null THEN dbo.[uf_getSelisihHari](PM.StartDate,PM.TanggalPenyelesaian) ELSE null END) as JumlahHariPengerjaanAngka
	  ,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 20 and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) >= 0 THEN 'Kuning'
			WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 0 THEN 'Merah'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) >= 1 THEN 'Biru'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) = 0 THEN 'Hijau'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) < 0 THEN 'Merah'
			 ELSE '' END) as Warna
	  ,PM.CreatedBy_Id 
	  ,PM.CreatedTime
	  ,PM.UpdatedBy_Id
	  ,PM.UpdatedTime
	  ,PM.IsActive
	  ,PM.IsDeleted
	  ,PM.DeletedBy_Id
	  ,PM.DeletedTime
	  ,(CASE WHEN PM.IsDone = 0 THEN 'Dalam Proses' ELSE 'Selesai' END) as StatusProject
  FROM [dbo].[Tbl_Project_Member] PM
  LEFT JOIN dbo.Tbl_Project P ON PM.ProjectId = P.Id
  LEFT JOIN dbo.Tbl_Master_Job_Position JP ON JP.Id = PM.JobPositionId
  LEFT JOIN dbo.Tbl_Pegawai PG ON PG.Id = PM.PegawaiId
  LEFT JOIN dbo.Tbl_Unit UN ON UN.Id = PM.UnitPegawaiId
  Where PM.IsActive = 1 and PM.IsDeleted = 0 and PM.SendAsTask = 1
GO
/****** Object:  Table [dbo].[Tbl_Master_Status_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Status_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AreaWakilPemimpinId] [int] NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Presentase] [int] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Status_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Sub_Kategori_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Sub_Kategori_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Sub_Kategori_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Klasifikasi_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Klasifikasi_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Klasifikasi_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Kategori_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Kategori_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Kategori_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Kompleksitas_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Kompleksitas_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KategoriKompleksitasId] [int] NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Kompleksitas_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Skor_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Skor_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Skor] [int] NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Skor_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Lookup]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Lookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Value] [int] NULL,
	[Order_By] [int] NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_data_project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view [dbo].[v_data_project]
as
SELECT P.[Id]
      ,[KategoriProjectId]
      ,KP.Nama as KategoriProject
	  ,[SubKategoriProjectId]
	  ,SKP.Nama as SubKategoriProject
      ,[SkorProjectId]
	  ,MSK.Nama as SkorProject
      ,[KompleksitasProjectId]
	  ,MKP.Nama as KompleksitasProject
      ,[KlasifikasiProjectId]
	  ,KSP.Nama as KlasifikasiProject
      ,[MandatoryId]
	  ,L.Name as Mandatory
      ,[PeriodeProjectId]
	  ,L3.Name as PeriodeProject
      ,[NotifikasiId]
      ,[IsPIR]
	  ,L2.Name as PIR
      ,[ProjectStatusId]
	  ,MSP.Nama as ProjectStatus
      ,P.[Kode]
      ,P.[Nama]
	   ,P.StatusProjectDalamPantauan
      ,[ProjectNo]
      ,[NoMemo]
      ,[TanggalMemo]
      ,[NoDRF]
      ,[TanggalDRF]
      ,[TanggalDisposisi]
      ,[TanggalKlarifikasi]
      ,[DetailRequirment]
      ,[TanggalEstimasiMulai]
      ,[TanggalEstimasiSelesai]
      ,[TanggalEstimasiProduction]
      ,[TanggalEstimasiDevelopmentAwal]
      ,[TanggalEstimasiDevelopmentAkhir]
      ,[TanggalEstimasiTestingAwal]
      ,[TanggalEstimasiTestingAkhir]
      ,[TanggalEstimasiPilotingAwal]
      ,[TanggalEstimasiPilotingAkhir]
      ,[TanggalEstimasiPIRAwal]
      ,[TanggalEstimasiPIRAkhir]
      ,[IsDone]
      ,P.[IsDeleted]
      ,P.[IsActive]
	  ,MSP.Presentase
	  ,P.TanggalSelesaiProject
	  ,L4.Name as CloseOpenStatus
	   ,(CASE WHEN [CloseOpenId] = 1 THEN dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) ELSE null END) as SelisihDeadlineProject
	   ,(CASE WHEN [CloseOpenId] = 2 THEN dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) ELSE null END) as SLA
	  	,(CASE WHEN (P.[CloseOpenId] = 1 OR P.[CloseOpenId] is null) and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) < 20 and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) >= 0 THEN 'Kuning'
			WHEN (P.[CloseOpenId] = 1 OR P.[CloseOpenId] is null) and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) < 0 THEN 'Merah'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) >= 1 THEN 'Biru'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) = 0 THEN 'Hijau'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) < 0 THEN 'Merah'
			 ELSE '' END) as Warna
  FROM [dbo].[Tbl_Project] P
  LEFT JOIN dbo.Tbl_Master_Kategori_Project KP ON P.KategoriProjectId = KP.Id
  LEFT JOIN dbo.Tbl_Master_Status_Project MSP ON P.ProjectStatusId = MSP.Id
  LEFT JOIN dbo.Tbl_Master_Klasifikasi_Project KSP ON P.KlasifikasiProjectId = KSP.Id
  LEFT JOIN dbo.Tbl_Master_Kompleksitas_Project MKP ON P.KompleksitasProjectId = MKP.Id
  LEFT JOIN dbo.Tbl_Master_Sub_Kategori_Project SKP ON P.SubKategoriProjectId = SKP.Id
  LEFT JOIN dbo.Tbl_Master_Skor_Project MSK ON P.SkorProjectId = MSK.Id
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = P.MandatoryId and L.Type = 'MandatoryKategori' and L.IsActive = 1 and L.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Lookup L2 ON L2.Value = P.IsPIR and L2.Type = 'IsPIR' and L2.IsActive = 1 and L2.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Lookup L3 ON L3.Value = P.PeriodeProjectId and L3.Type = 'PeriodeProject' and L3.IsActive = 1 and L3.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Lookup L4 ON L4.Value = P.CloseOpenId and L4.Type = 'ProjectStatus' and L4.IsActive = 1 and L4.IsDeleted = 0

GO
/****** Object:  View [dbo].[vw_workload_pegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view [dbo].[vw_workload_pegawai]
as
SELECT P.Nama
		,P.Npp
		,P.Unit_Id
		,U.Name as NamaUnit
		,P.Id as PegawaiId
		,JumlahSemuaProject = (SELECT count(*) 
                       FROM Tbl_Project_Member PM
                       Where P.Id = PM.PegawaiId and PM.IsActive = 1 and PM.IsDeleted = 0 and PM.SendAsTask = 1)
		,JumlahSelesai = (SELECT count(*) 
                       FROM Tbl_Project_Member PM
                       Where P.Id = PM.PegawaiId and PM.IsActive = 1 and PM.IsDeleted = 0 and PM.SendAsTask = 1 and PM.IsDone = 1)
		,JumlahOnProgress = (SELECT count(*) 
                       FROM Tbl_Project_Member PM
                       Where P.Id = PM.PegawaiId and PM.IsActive = 1 and PM.IsDeleted = 0 and PM.SendAsTask = 1 and PM.IsDone = 0)
  FROM [dbo].[Tbl_Pegawai] P
  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
  Where P.IsActive = 1 and P.IsDeleted = 0 
 
GO
/****** Object:  View [dbo].[vw_Dashboard_AllProjectByUnit]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO










CREATE view [dbo].[vw_Dashboard_AllProjectByUnit]
as
SELECT distinct 
		P.Id
		,P.Id as ProjectId
	   ,P.ProjectNo
	   ,P.Nama
	   ,P.StatusProjectDalamPantauan
	   ,P.TanggalEstimasiMulai
	   ,P.TanggalEstimasiSelesai
	   ,PM.UnitPegawaiId as UnitId
	   ,U.Code as KodeUnit
	   ,U.Name as NamaUnit
	   ,P.ProjectStatusId
	   ,P.CloseOpenId
	   ,P.TanggalSelesaiProject
	  ,MSP.Presentase
	   ,(CASE WHEN [CloseOpenId] = 1 THEN dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) ELSE null END) as Selisih
	   ,(CASE WHEN [CloseOpenId] = 2 THEN dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) ELSE null END) as SLA
	  ,(CASE WHEN (P.[CloseOpenId] = 1 OR P.[CloseOpenId] is null) and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) < 20 and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) >= 0 THEN 'Kuning'
			WHEN (P.[CloseOpenId] = 1 OR P.[CloseOpenId] is null) and ((dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) * 100) / NULLIF(dbo.[uf_getSelisihHari](P.TanggalEstimasiMulai,P.TanggalEstimasiSelesai),0)) < 0 THEN 'Merah'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) >= 1 THEN 'Biru'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) = 0 THEN 'Hijau'
		   WHEN (P.[CloseOpenId] = 2) and dbo.[uf_getSelisihHari](P.TanggalSelesaiProject,P.TanggalEstimasiSelesai) < 0 THEN 'Merah'
			 ELSE '' END) as Warna
	FROM [dbo].[Tbl_Project] P
	LEFT JOIN dbo.Tbl_Project_Member PM ON P.Id = PM.ProjectId 
  LEFT JOIN dbo.Tbl_Master_Status_Project MSP ON P.ProjectStatusId = MSP.Id
	LEFT JOIN dbo.Tbl_Unit U ON PM.UnitPegawaiId = U.Id
	WHERE P.IsActive = 1 and P.IsDeleted = 0 and PM.IsDeleted = 0 and PM.IsActive = 1
	AND PM.SendAsTask = 1			 
GO
/****** Object:  View [dbo].[vw_Dashboard_AllProjectByPegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE view [dbo].[vw_Dashboard_AllProjectByPegawai]
as
SELECT P.Id
		,P.Id as ProjectId
	   ,P.ProjectNo
	   ,P.Nama
	   ,P.StatusProjectDalamPantauan
	   ,P.TanggalEstimasiMulai
	   ,P.TanggalEstimasiSelesai
	   ,PM.UnitPegawaiId as UnitId
	   ,U.Code as KodeUnit
	   ,U.Name as NamaUnit
	   ,P.ProjectStatusId
	   ,P.CloseOpenId
	   ,PM.PegawaiId
	   ,PG.Nama as NamaPegawai
	   ,(CASE WHEN [CloseOpenId] = 1 THEN dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) ELSE null END) as Selisih
	FROM [dbo].[Tbl_Project] P
	LEFT JOIN dbo.Tbl_Project_Member PM ON P.Id = PM.ProjectId 
	LEFT JOIN dbo.Tbl_Pegawai PG ON PG.Id = PM.PegawaiId 
	LEFT JOIN dbo.Tbl_Unit U ON PM.UnitPegawaiId = U.Id
	WHERE P.IsActive = 1 and P.IsDeleted = 0 and PM.IsDeleted = 0 and PM.IsActive = 1
    AND PM.SendAsTask = 1				 
GO
/****** Object:  Table [dbo].[Tbl_Project_User]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[ClientId] [int] NULL,
	[NppPIC] [nvarchar](50) NULL,
	[NamaPIC] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[NoHp] [nvarchar](50) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[TanggalMulai] [date] NULL,
	[TanggalSelesai] [date] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Type_Client]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Type_Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Type_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Client]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeClientId] [int] NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_Dashboard_AllProjectByUser]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE view [dbo].[vw_Dashboard_AllProjectByUser]
as
SELECT distinct 
		P.Id
		,P.Id as ProjectId
	   ,P.ProjectNo
	   ,P.StatusProjectDalamPantauan
	   ,P.Nama
	   ,P.TanggalEstimasiMulai
	   ,P.TanggalEstimasiSelesai
	   ,PU.ClientId as ClientId
	   ,MC.Kode as NamaClient
	   ,MTC.Nama as TypeClient
	   ,MTC.Id as TypeClientId
	   ,P.ProjectStatusId
	   ,P.CloseOpenId
	   ,(CASE WHEN [CloseOpenId] = 1 THEN dbo.[uf_getSelisihHari](getdate(),P.TanggalEstimasiSelesai) ELSE null END) as Selisih
	FROM [dbo].[Tbl_Project] P
	LEFT JOIN dbo.[Tbl_Project_User] PU ON P.Id = PU.ProjectId 
	LEFT JOIN dbo.[Tbl_Master_Client] MC ON MC.Id = PU.ClientId
	LEFT JOIN dbo.[Tbl_Master_Type_Client] MTC ON MTC.Id = MC.TypeClientId 
	WHERE P.IsActive = 1 and P.IsDeleted = 0 and PU.IsDeleted = 0 and PU.IsActive = 1		 
GO
/****** Object:  View [dbo].[vw_allproject_pegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




/****** Script for SelectTopNRows command from SSMS  ******/
CREATE view [dbo].[vw_allproject_pegawai]
as
SELECT PM.Id as Id
		,P.Nama as NamaProject
	   ,P.Id as ProjectId
	   ,P.DetailRequirment
	   ,P.ProjectNo as NoProject
	   ,P.TanggalEstimasiMulai
	   ,P.TanggalEstimasiSelesai
	   ,PM.StartDate
	   ,PM.EndDate
	   ,PM.PegawaiId
	   ,PG.Nama as NamaPegawai
	   ,PG.Npp as NppPegawai
	   ,MJ.Nama as JobPosition
	   ,PM.IsDone
	   ,P.ProjectStatusId
	   ,PM.UnitPegawaiId 
	   ,PM.TanggalPenyelesaian
	   ,P.StatusProjectDalamPantauan
	  ,(CASE WHEN PM.IsDone = 0 THEN 'Dalam Proses' ELSE 'Selesai' END) as StatusProject
	     ,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and PM.EndDate is not null and [CloseOpenId] = 1 THEN dbo.[uf_getSelisihHari](getdate(),PM.EndDate) ELSE null END) as Selisih
	  ,(CASE WHEN (PM.IsDone = 1) and PM.TanggalPenyelesaian is not null  THEN dbo.[uf_getSelisihHari](PM.StartDate,PM.TanggalPenyelesaian) ELSE null END) as JumlahHariPengerjaan
	  	  ,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 20 and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) >= 0 THEN 'Kuning'
			WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 0 THEN 'Merah'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) >= 1 THEN 'Biru'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) = 0 THEN 'Hijau'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) < 0 THEN 'Merah'
			 ELSE '' END) as Warna
  FROM [dbo].[Tbl_Project_Member] PM
  LEFT JOIN dbo.Tbl_Project P ON PM.ProjectId = P.Id
  LEFT JOIN dbo.Tbl_Pegawai PG ON PG.Id = PM.PegawaiId
  LEFT JOIN dbo.Tbl_Master_Job_Position MJ ON MJ.Id = PM.JobPositionId
  Where PM.IsActive = 1 and PM.IsDeleted = 0 and PM.SendAsTask = 1
GO
/****** Object:  Table [dbo].[Navigation]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Navigation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Route] [nvarchar](255) NULL,
	[Order] [int] NULL,
	[Visible] [int] NOT NULL,
	[ParentNavigation_Id] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[IconClass] [nvarchar](100) NULL,
	[IsDeleted] [bit] NULL,
	[DeletedById] [int] NULL,
	[DeletedTime] [datetime] NULL,
 CONSTRAINT [PK_dbo.Navigation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NavigationAssignment]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NavigationAssignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Navigation_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_dbo.NavigationAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Borrowed_Book]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Borrowed_Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_User] [int] NULL,
	[Id_Book] [int] NULL,
	[Borrow_Date] [datetime] NULL,
	[Finish_Date] [datetime] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[IsLate] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Borrrow_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Config_Apps]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Config_Apps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PathFolderFile] [nvarchar](max) NULL,
	[MaxFileSize] [decimal](18, 0) NULL,
	[TypeFileUpload] [nvarchar](max) NULL,
	[VirtualPath] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
 CONSTRAINT [PK_Tbl_Config_Apps] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_FAQ]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_FAQ](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Judul] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_FAQ] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_File_Repository]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_File_Repository](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LookupJenisFileId] [int] NULL,
	[NamaFile] [nvarchar](max) NULL,
	[FileExt] [nvarchar](100) NULL,
	[FileType] [nvarchar](max) NULL,
	[Size] [decimal](18, 0) NULL,
	[Path] [nvarchar](max) NULL,
	[FullPath] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[UploadTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[UploadBy_Id] [int] NULL,
	[PegawaiUploadUnitId] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Tbl_File_Repository] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Holiday]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Holiday](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tanggal] [date] NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Holiday] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Log_Book]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Log_Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NIK] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Npp] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
 CONSTRAINT [PK_Tbl_Log_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_LogActivity]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_LogActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Npp] [nvarchar](150) NULL,
	[Url] [nvarchar](max) NULL,
	[DataLama] [nvarchar](max) NULL,
	[DataBaru] [nvarchar](max) NULL,
	[ActionTime] [datetime] NULL,
	[Browser] [nvarchar](max) NULL,
	[IP] [nvarchar](150) NULL,
	[OS] [nvarchar](max) NULL,
	[ClientInfo] [nvarchar](max) NULL,
	[Keterangan] [nvarchar](max) NULL,
 CONSTRAINT [PK_Tbl_LogActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Mapping_Kewenangan_Jabatan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Mapping_Kewenangan_Jabatan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JabatanId] [int] NULL,
	[RoleId] [int] NULL,
	[UnitId] [int] NULL,
	[StatusJabatan] [int] NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Mapping_Kewenangan_Jabatan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Mapping_Operator]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Mapping_Operator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitId] [int] NULL,
	[Npp] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedById] [int] NULL,
	[UpdatedById] [int] NULL,
	[DeletedById] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Tbl_Mapping_Operator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Book]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Author] [nvarchar](100) NULL,
	[RealeaseDate] [datetime] NULL,
	[IsBestSeller] [bit] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[IsActive] [bit] NULL,
	[Picture] [nvarchar](max) NULL,
	[IsBorrowed] [bit] NULL,
	[Description] [text] NULL,
 CONSTRAINT [PK_Tbl_MasterBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Jabatan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Jabatan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](250) NULL,
	[Nama] [nvarchar](max) NULL,
	[GradeAwal] [int] NULL,
	[GradeAkhir] [int] NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Jabatan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Jenis_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Jenis_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Jenis_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_KategoriKompleksitas]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_KategoriKompleksitas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_KategoriKompleksitas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Periode_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Periode_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Periode_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Role]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [nvarchar](150) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Sistem]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Sistem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Sistem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Sub_Sistem]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Sub_Sistem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MasterSistemId] [int] NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[Order_By] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Sub_Sistem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Master_Type_Dokumen]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Master_Type_Dokumen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Master_Type_Dokumen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_MasterIconMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_MasterIconMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Icon] [nvarchar](150) NULL,
	[Images] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[CeratedBy_Id] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[UpdatedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Tbl_MasterIconMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Pegawai_Kelolaan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Pegawai_Kelolaan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AtasanId] [int] NULL,
	[PegawaiId] [int] NULL,
	[UnitAtasanId] [int] NULL,
	[UnitPegawaiId] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Pegawai_Kelolaan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Detail_Unit_Request]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Detail_Unit_Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[UnitId] [int] NULL,
	[Npp_PIC] [nvarchar](50) NULL,
	[No_HP_PIC] [nvarchar](50) NULL,
	[Keterangan] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Detail_Unit_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_File]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[TypeDokumenId] [int] NULL,
	[NamaFile] [nvarchar](max) NULL,
	[FileExt] [nvarchar](100) NULL,
	[FileType] [nvarchar](max) NULL,
	[Size] [decimal](18, 0) NULL,
	[Path] [nvarchar](max) NULL,
	[FullPath] [nvarchar](max) NULL,
	[Keterangan] [text] NULL,
	[UploadTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[UploadBy_Id] [int] NULL,
	[PegawaiUploadUnitId] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Log]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[PegawaiIdFrom] [int] NULL,
	[PegawaiIdTo] [int] NULL,
	[UnitIdPegawaiIdFrom] [int] NULL,
	[UnitIdPegawaiIdTo] [int] NULL,
	[ProjectStatusForm] [int] NULL,
	[ProjectStatusFormValue] [nvarchar](max) NULL,
	[ProjectStatusTo] [int] NULL,
	[ProjectStatusToValue] [nvarchar](max) NULL,
	[LogActivityId] [int] NULL,
	[LogActivityName] [nvarchar](max) NULL,
	[Komentar] [text] NULL,
	[Tanggal] [date] NULL,
	[Keterangan] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Log_Status]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Log_Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Projectid] [int] NULL,
	[ProjectStatusForm] [int] NULL,
	[ProjectStatusFormValue] [nvarchar](max) NULL,
	[ProjectStatusTo] [int] NULL,
	[ProjectStatusToValue] [nvarchar](max) NULL,
	[Tanggal] [date] NULL,
	[CreatedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Log_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Member_ProgressKerja]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Member_ProgressKerja](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectMemberId] [int] NULL,
	[Judul] [nvarchar](max) NULL,
	[Deskripsi] [text] NULL,
	[TanggalAwal] [date] NULL,
	[TanggalAkhir] [date] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Member_ProgressKerja] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Member_Task]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Member_Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectMemberId] [int] NULL,
	[Deskripsi] [text] NULL,
	[StatusTask] [int] NULL,
	[KeteranganStatus] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Member_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Member_Task_LogStatus]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Member_Task_LogStatus](
	[Id] [int] NOT NULL,
	[ProjectMemberTaskId] [int] NULL,
	[Deskripsi] [text] NULL,
	[StatusTask] [int] NULL,
	[KeteranganStatus] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Member_Task_LogStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Notes]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Notes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[PegawaiId] [int] NULL,
	[UnitId] [int] NULL,
	[Judul] [nvarchar](max) NULL,
	[Notes] [text] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Project_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Project_Relasi]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Project_Relasi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[RelasiProjectId] [int] NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Table_2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Role_Pegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Role_Pegawai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Pegawai_Id] [int] NULL,
	[Role_Id] [int] NULL,
	[Unit_Id] [int] NULL,
	[CreatedBy_Id] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[StatusRole] [int] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[UpdatedBy_Id] [int] NULL,
	[UpdatedTime] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DeletedTime] [datetime] NULL,
	[DeletedBy_Id] [int] NULL,
 CONSTRAINT [PK_Role_Pegawai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_SystemParameter]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_SystemParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_dbo.SystemParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Task_Pegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Task_Pegawai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NULL,
	[CatatanFrom] [nvarchar](max) NULL,
	[PegawaiIdFrom] [int] NULL,
	[PegawaiIdTo] [int] NULL,
	[FromStatusProject] [int] NULL,
	[ToStatusProject] [int] NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Task_Pegawai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_Tes]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Tes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kode] [nvarchar](50) NULL,
	[Nama] [nvarchar](max) NULL,
	[Keterangan] [nvarchar](max) NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[DeletedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[DeletedBy_Id] [int] NULL,
	[IsDeleted] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Tbl_Tes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_UploadFile]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_UploadFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NamaFile] [nvarchar](max) NULL,
	[DownloadPath] [nvarchar](max) NULL,
	[CreatedById] [int] NULL,
	[CreatedTime] [datetime] NULL,
 CONSTRAINT [PK_Tbl_UploadFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_User]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[Pegawai_Id] [int] NOT NULL,
	[CreatedTime] [datetime] NULL,
	[UpdatedTime] [datetime] NULL,
	[CreatedBy_Id] [int] NULL,
	[UpdatedBy_Id] [int] NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_UserLog]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_UserLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](10) NULL,
	[Name] [nvarchar](255) NULL,
	[Url] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](20) NULL,
	[Browser] [nvarchar](255) NULL,
	[CreatedTime] [datetime] NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tbl_UserSession]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_UserSession](
	[User_Id] [int] NOT NULL,
	[SessionID] [nvarchar](50) NOT NULL,
	[LastActive] [datetime] NOT NULL,
	[Info] [nvarchar](255) NULL,
	[Role_Id] [int] NULL,
	[Unit_Id] [int] NULL,
 CONSTRAINT [PK_Tbl_UserSession] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Navigation] ADD  CONSTRAINT [DF_Navigation_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[NavigationAssignment] ADD  CONSTRAINT [DF_NavigationAssignment_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[NavigationAssignment] ADD  CONSTRAINT [DF_NavigationAssignment_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Tbl_FAQ] ADD  CONSTRAINT [DF_Tbl_FAQ_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_FAQ] ADD  CONSTRAINT [DF_Tbl_FAQ_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_File_Repository] ADD  CONSTRAINT [DF_Tbl_File_Repository_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Holiday] ADD  CONSTRAINT [DF_Tbl_Holiday_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Holiday] ADD  CONSTRAINT [DF_Tbl_Holiday_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Lookup] ADD  CONSTRAINT [DF_Tbl_Lookup_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Lookup] ADD  CONSTRAINT [DF_Tbl_Lookup_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Mapping_Kewenangan_Jabatan] ADD  CONSTRAINT [DF_Tbl_Mapping_Kewenangan_Jabatan_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Mapping_Kewenangan_Jabatan] ADD  CONSTRAINT [DF_Tbl_Mapping_Kewenangan_Jabatan_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Client] ADD  CONSTRAINT [DF_Tbl_Master_Client_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Client] ADD  CONSTRAINT [DF_Tbl_Master_Client_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Jenis_Project] ADD  CONSTRAINT [DF_Tbl_Master_Jenis_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Jenis_Project] ADD  CONSTRAINT [DF_Tbl_Master_Jenis_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Job_Position] ADD  CONSTRAINT [DF_Tbl_Master_Job_Position_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Job_Position] ADD  CONSTRAINT [DF_Tbl_Master_Job_Position_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Kategori_Project] ADD  CONSTRAINT [DF_Tbl_Master_Kategori_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Kategori_Project] ADD  CONSTRAINT [DF_Tbl_Master_Kategori_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_KategoriKompleksitas] ADD  CONSTRAINT [DF_Tbl_Master_KategoriKompleksitas_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_KategoriKompleksitas] ADD  CONSTRAINT [DF_Tbl_Master_KategoriKompleksitas_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Klasifikasi_Project] ADD  CONSTRAINT [DF_Tbl_Master_Klasifikasi_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Klasifikasi_Project] ADD  CONSTRAINT [DF_Tbl_Master_Klasifikasi_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Kompleksitas_Project] ADD  CONSTRAINT [DF_Tbl_Master_Kompleksitas_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Kompleksitas_Project] ADD  CONSTRAINT [DF_Tbl_Master_Kompleksitas_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Periode_Project] ADD  CONSTRAINT [DF_Tbl_Master_Periode_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Periode_Project] ADD  CONSTRAINT [DF_Tbl_Master_Periode_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Role] ADD  CONSTRAINT [DF_Tbl_Master_Role_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Role] ADD  CONSTRAINT [DF_Tbl_Master_Role_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Sistem] ADD  CONSTRAINT [DF_Tbl_Master_Sistem_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Sistem] ADD  CONSTRAINT [DF_Tbl_Master_Sistem_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Skor_Project] ADD  CONSTRAINT [DF_Tbl_Master_Skor_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Skor_Project] ADD  CONSTRAINT [DF_Tbl_Master_Skor_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Status_Project] ADD  CONSTRAINT [DF_Tbl_Master_Status_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Status_Project] ADD  CONSTRAINT [DF_Tbl_Master_Status_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Kategori_Project] ADD  CONSTRAINT [DF_Tbl_Sub_Kategori_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Kategori_Project] ADD  CONSTRAINT [DF_Tbl_Sub_Kategori_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Sistem] ADD  CONSTRAINT [DF_Tbl_Master_Sub_Sistem_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Sistem] ADD  CONSTRAINT [DF_Tbl_Master_Sub_Sistem_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Type_Client] ADD  CONSTRAINT [DF_Tbl_Master_Type_Client_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Type_Client] ADD  CONSTRAINT [DF_Tbl_Master_Type_Client_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Master_Type_Dokumen] ADD  CONSTRAINT [DF_Tbl_Master_Type_Dokumen_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Master_Type_Dokumen] ADD  CONSTRAINT [DF_Tbl_Master_Type_Dokumen_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_MasterIconMenu] ADD  CONSTRAINT [DF_Table_1_IsActive]  DEFAULT ((1)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Pegawai] ADD  CONSTRAINT [DF_Tbl_Pegawai_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Pegawai] ADD  CONSTRAINT [DF_Tbl_Pegawai_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] ADD  CONSTRAINT [DF_Tbl_Pegawai_Kelolaan_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] ADD  CONSTRAINT [DF_Tbl_Pegawai_Kelolaan_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project] ADD  CONSTRAINT [DF_Tbl_Project_CloseOpenId]  DEFAULT ((1)) FOR [CloseOpenId]
GO
ALTER TABLE [dbo].[Tbl_Project] ADD  CONSTRAINT [DF_Tbl_Master_Project_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project] ADD  CONSTRAINT [DF_Tbl_Master_Project_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Detail_Unit_Request] ADD  CONSTRAINT [DF_Tbl_Project_Detail_Unit_Request_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Detail_Unit_Request] ADD  CONSTRAINT [DF_Tbl_Project_Detail_Unit_Request_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_File] ADD  CONSTRAINT [DF_Tbl_Project_File_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Log] ADD  CONSTRAINT [DF_Tbl_Project_Log_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Tbl_Project_Log] ADD  CONSTRAINT [DF_Tbl_Project_Log_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Log] ADD  CONSTRAINT [DF_Tbl_Project_Log_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Log_Status] ADD  CONSTRAINT [DF_Tbl_Project_Log_Status_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Tbl_Project_Log_Status] ADD  CONSTRAINT [DF_Tbl_Project_Log_Status_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Log_Status] ADD  CONSTRAINT [DF_Tbl_Project_Log_Status_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Member] ADD  CONSTRAINT [DF_Tbl_Project_Member_StatusProgressId]  DEFAULT ((0)) FOR [StatusProgressId]
GO
ALTER TABLE [dbo].[Tbl_Project_Member] ADD  CONSTRAINT [DF_Tbl_Project_Member_IsDone]  DEFAULT ((0)) FOR [IsDone]
GO
ALTER TABLE [dbo].[Tbl_Project_Member] ADD  CONSTRAINT [DF_Tbl_Project_Member_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Member] ADD  CONSTRAINT [DF_Tbl_Project_Member_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_ProgressKerja] ADD  CONSTRAINT [DF_Tbl_Project_Member_ProgressKerja_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_ProgressKerja] ADD  CONSTRAINT [DF_Tbl_Project_Member_ProgressKerja_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task] ADD  CONSTRAINT [DF_Tbl_Project_Member_Task_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task] ADD  CONSTRAINT [DF_Tbl_Project_Member_Task_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task_LogStatus] ADD  CONSTRAINT [DF_Tbl_Project_Member_Task_LogStatus_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task_LogStatus] ADD  CONSTRAINT [DF_Tbl_Project_Member_Task_LogStatus_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Notes] ADD  CONSTRAINT [DF_Tbl_Project_Notes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Notes] ADD  CONSTRAINT [DF_Tbl_Project_Notes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_Relasi] ADD  CONSTRAINT [DF_Table_2_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_Relasi] ADD  CONSTRAINT [DF_Table_2_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Project_User] ADD  CONSTRAINT [DF_Tbl_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Project_User] ADD  CONSTRAINT [DF_Tbl_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai] ADD  CONSTRAINT [DF_Tbl_Role_Pegawai_CreatedTime_1]  DEFAULT (getdate()) FOR [CreatedTime]
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai] ADD  CONSTRAINT [DF_Tbl_Role_Pegawai_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] ADD  CONSTRAINT [DF_Tbl_Task_Pegawai_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] ADD  CONSTRAINT [DF_Tbl_Task_Pegawai_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Tes] ADD  CONSTRAINT [DF_Tbl_Tes_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tbl_Tes] ADD  CONSTRAINT [DF_Tbl_Tes_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Unit] ADD  CONSTRAINT [DF_Tbl_Unit_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Tbl_Unit] ADD  CONSTRAINT [DF_Tbl_Unit_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Navigation]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Navigation_dbo.Navigation_ParentNavigation_Id] FOREIGN KEY([ParentNavigation_Id])
REFERENCES [dbo].[Navigation] ([Id])
GO
ALTER TABLE [dbo].[Navigation] CHECK CONSTRAINT [FK_dbo.Navigation_dbo.Navigation_ParentNavigation_Id]
GO
ALTER TABLE [dbo].[NavigationAssignment]  WITH CHECK ADD  CONSTRAINT [FK_NavigationAssignment_Navigation] FOREIGN KEY([Navigation_Id])
REFERENCES [dbo].[Navigation] ([Id])
GO
ALTER TABLE [dbo].[NavigationAssignment] CHECK CONSTRAINT [FK_NavigationAssignment_Navigation]
GO
ALTER TABLE [dbo].[Tbl_Master_Client]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Master_Client_Tbl_Master_Type_Client] FOREIGN KEY([TypeClientId])
REFERENCES [dbo].[Tbl_Master_Type_Client] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Master_Client] CHECK CONSTRAINT [FK_Tbl_Master_Client_Tbl_Master_Type_Client]
GO
ALTER TABLE [dbo].[Tbl_Master_Kompleksitas_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Master_Kompleksitas_Project_Tbl_Master_KategoriKompleksitas] FOREIGN KEY([KategoriKompleksitasId])
REFERENCES [dbo].[Tbl_Master_KategoriKompleksitas] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Master_Kompleksitas_Project] CHECK CONSTRAINT [FK_Tbl_Master_Kompleksitas_Project_Tbl_Master_KategoriKompleksitas]
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Sistem]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Master_Sub_Sistem_Tbl_Master_Sistem] FOREIGN KEY([MasterSistemId])
REFERENCES [dbo].[Tbl_Master_Sistem] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Master_Sub_Sistem] CHECK CONSTRAINT [FK_Tbl_Master_Sub_Sistem_Tbl_Master_Sistem]
GO
ALTER TABLE [dbo].[Tbl_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Tbl_Master_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Tbl_Master_Role] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai] CHECK CONSTRAINT [FK_Tbl_Pegawai_Tbl_Master_Role]
GO
ALTER TABLE [dbo].[Tbl_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Tbl_Unit] FOREIGN KEY([Unit_Id])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai] CHECK CONSTRAINT [FK_Tbl_Pegawai_Tbl_Unit]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Pegawai] FOREIGN KEY([PegawaiId])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] CHECK CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Pegawai1] FOREIGN KEY([AtasanId])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] CHECK CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Pegawai1]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Unit] FOREIGN KEY([UnitAtasanId])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] CHECK CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Unit]
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Unit1] FOREIGN KEY([UnitPegawaiId])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Pegawai_Kelolaan] CHECK CONSTRAINT [FK_Tbl_Pegawai_Kelolaan_Tbl_Unit1]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Master_Project_Tbl_Master_Kategori_Project] FOREIGN KEY([KategoriProjectId])
REFERENCES [dbo].[Tbl_Master_Kategori_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Master_Project_Tbl_Master_Kategori_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Master_Project_Tbl_Master_Skor_Project] FOREIGN KEY([SkorProjectId])
REFERENCES [dbo].[Tbl_Master_Skor_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Master_Project_Tbl_Master_Skor_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Tbl_Master_Jenis_Project] FOREIGN KEY([JenisProjectId])
REFERENCES [dbo].[Tbl_Master_Jenis_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Project_Tbl_Master_Jenis_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Tbl_Master_Klasifikasi_Project] FOREIGN KEY([KlasifikasiProjectId])
REFERENCES [dbo].[Tbl_Master_Klasifikasi_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Project_Tbl_Master_Klasifikasi_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Tbl_Master_Kompleksitas_Project] FOREIGN KEY([KompleksitasProjectId])
REFERENCES [dbo].[Tbl_Master_Kompleksitas_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Project_Tbl_Master_Kompleksitas_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Tbl_Master_Status_Project] FOREIGN KEY([ProjectStatusId])
REFERENCES [dbo].[Tbl_Master_Status_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Project_Tbl_Master_Status_Project]
GO
ALTER TABLE [dbo].[Tbl_Project]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Tbl_Master_Sub_Kategori_Project] FOREIGN KEY([SubKategoriProjectId])
REFERENCES [dbo].[Tbl_Master_Sub_Kategori_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project] CHECK CONSTRAINT [FK_Tbl_Project_Tbl_Master_Sub_Kategori_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_File]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_File_Tbl_Master_Type_Dokumen] FOREIGN KEY([TypeDokumenId])
REFERENCES [dbo].[Tbl_Master_Type_Dokumen] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_File] CHECK CONSTRAINT [FK_Tbl_Project_File_Tbl_Master_Type_Dokumen]
GO
ALTER TABLE [dbo].[Tbl_Project_File]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_File_Tbl_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_File] CHECK CONSTRAINT [FK_Tbl_Project_File_Tbl_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_Log]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Log_Tbl_Master_Status_Project] FOREIGN KEY([ProjectStatusForm])
REFERENCES [dbo].[Tbl_Master_Status_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Log] CHECK CONSTRAINT [FK_Tbl_Project_Log_Tbl_Master_Status_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_Log]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Log_Tbl_Master_Status_Project1] FOREIGN KEY([ProjectStatusTo])
REFERENCES [dbo].[Tbl_Master_Status_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Log] CHECK CONSTRAINT [FK_Tbl_Project_Log_Tbl_Master_Status_Project1]
GO
ALTER TABLE [dbo].[Tbl_Project_Log]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Log_Tbl_Pegawai] FOREIGN KEY([PegawaiIdFrom])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Log] CHECK CONSTRAINT [FK_Tbl_Project_Log_Tbl_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_Project_Log]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Log_Tbl_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Log] CHECK CONSTRAINT [FK_Tbl_Project_Log_Tbl_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_Member]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_Tbl_Master_Job_Position] FOREIGN KEY([JobPositionId])
REFERENCES [dbo].[Tbl_Master_Job_Position] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member] CHECK CONSTRAINT [FK_Tbl_Project_Member_Tbl_Master_Job_Position]
GO
ALTER TABLE [dbo].[Tbl_Project_Member]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_Tbl_Master_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member] CHECK CONSTRAINT [FK_Tbl_Project_Member_Tbl_Master_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_Member]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_Tbl_Pegawai] FOREIGN KEY([PegawaiId])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member] CHECK CONSTRAINT [FK_Tbl_Project_Member_Tbl_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_ProgressKerja]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_ProgressKerja_Tbl_Project_Member] FOREIGN KEY([ProjectMemberId])
REFERENCES [dbo].[Tbl_Project_Member] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member_ProgressKerja] CHECK CONSTRAINT [FK_Tbl_Project_Member_ProgressKerja_Tbl_Project_Member]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_Task_Tbl_Project_Member] FOREIGN KEY([ProjectMemberId])
REFERENCES [dbo].[Tbl_Project_Member] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task] CHECK CONSTRAINT [FK_Tbl_Project_Member_Task_Tbl_Project_Member]
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task_LogStatus]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Member_Task_LogStatus_Tbl_Project_Member_Task] FOREIGN KEY([ProjectMemberTaskId])
REFERENCES [dbo].[Tbl_Project_Member_Task] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Member_Task_LogStatus] CHECK CONSTRAINT [FK_Tbl_Project_Member_Task_LogStatus_Tbl_Project_Member_Task]
GO
ALTER TABLE [dbo].[Tbl_Project_Notes]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_Notes_Tbl_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_Notes] CHECK CONSTRAINT [FK_Tbl_Project_Notes_Tbl_Project]
GO
ALTER TABLE [dbo].[Tbl_Project_User]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_User_Tbl_Master_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Tbl_Master_Client] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_User] CHECK CONSTRAINT [FK_Tbl_Project_User_Tbl_Master_Client]
GO
ALTER TABLE [dbo].[Tbl_Project_User]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Project_User_Tbl_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Project_User] CHECK CONSTRAINT [FK_Tbl_Project_User_Tbl_Project]
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Master_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Tbl_Master_Role] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai] CHECK CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Master_Role]
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Pegawai] FOREIGN KEY([Pegawai_Id])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai] CHECK CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Unit] FOREIGN KEY([Unit_Id])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Role_Pegawai] CHECK CONSTRAINT [FK_Tbl_Role_Pegawai_Tbl_Unit]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project] FOREIGN KEY([FromStatusProject])
REFERENCES [dbo].[Tbl_Master_Status_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project1] FOREIGN KEY([ToStatusProject])
REFERENCES [dbo].[Tbl_Master_Status_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Master_Status_Project1]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Project] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Tbl_Project] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Project]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai] FOREIGN KEY([PegawaiIdFrom])
REFERENCES [dbo].[Tbl_Task_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai1] FOREIGN KEY([Id])
REFERENCES [dbo].[Tbl_Task_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai1]
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai2] FOREIGN KEY([Id])
REFERENCES [dbo].[Tbl_Task_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Task_Pegawai] CHECK CONSTRAINT [FK_Tbl_Task_Pegawai_Tbl_Task_Pegawai2]
GO
ALTER TABLE [dbo].[Tbl_Unit]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Unit_dbo.Unit_Parent_Id] FOREIGN KEY([Parent_Id])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_Unit] CHECK CONSTRAINT [FK_dbo.Unit_dbo.Unit_Parent_Id]
GO
ALTER TABLE [dbo].[Tbl_User]  WITH CHECK ADD  CONSTRAINT [FK_User_Tbl_Pegawai] FOREIGN KEY([Pegawai_Id])
REFERENCES [dbo].[Tbl_Pegawai] ([Id])
GO
ALTER TABLE [dbo].[Tbl_User] CHECK CONSTRAINT [FK_User_Tbl_Pegawai]
GO
ALTER TABLE [dbo].[Tbl_UserSession]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_UserSession_Tbl_Master_Role] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[Tbl_Master_Role] ([Id])
GO
ALTER TABLE [dbo].[Tbl_UserSession] CHECK CONSTRAINT [FK_Tbl_UserSession_Tbl_Master_Role]
GO
ALTER TABLE [dbo].[Tbl_UserSession]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_UserSession_Tbl_Unit] FOREIGN KEY([Unit_Id])
REFERENCES [dbo].[Tbl_Unit] ([Id])
GO
ALTER TABLE [dbo].[Tbl_UserSession] CHECK CONSTRAINT [FK_Tbl_UserSession_Tbl_Unit]
GO
ALTER TABLE [dbo].[Tbl_UserSession]  WITH CHECK ADD  CONSTRAINT [FK_Tbl_UserSession_Tbl_User] FOREIGN KEY([User_Id])
REFERENCES [dbo].[Tbl_User] ([Id])
GO
ALTER TABLE [dbo].[Tbl_UserSession] CHECK CONSTRAINT [FK_Tbl_UserSession_Tbl_User]
GO
/****** Object:  StoredProcedure [dbo].[sp_Beranda_GetTotalProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Beranda_GetTotalProject]
@PegawaiId nvarchar(100),
@TanggalAwal nvarchar(100)='',
@TanggalAkhir nvarchar(100)='',
@PegawaiIdSearchParam nvarchar(100)= '',
@RoleId nvarchar(100),
@UnitId nvarchar(100)=''

as
begin

declare @Query nvarchar(max) = '
Select Unit_Id,
		SUM(ISNULL([Dalam Proses],0)) as DalamProses,
		SUM(ISNULL(Selesai,0)) as Selesai,
		(SUM(ISNULL([Dalam Proses],0)) + SUM(ISNULL(Selesai,0))) as Total,
		convert(int,(SUM(ISNULL(Selesai,0)) * 100) / (SUM(ISNULL([Dalam Proses],0)) + SUM(ISNULL(Selesai,0)))) as Presentase
		 from 
(SELECT PegawaiId,PG.Unit_Id,(CASE WHEN (IsDone = 0 OR IsDone IS NULL) THEN ''Dalam Proses'' ELSE ''Selesai'' END) as Status, count(*) as Jumlah
  FROM [dbo].[Tbl_Project_Member] PM
  LEFT JOIN dbo.Tbl_Pegawai PG ON PM.PegawaiId= PG.Id
  WHERE PM.SendAsTask = 1 and PM.IsActive = 1 and PM.IsDeleted = 0 '

IF(@PegawaiIdSearchParam !='')
	begin
		SET @Query = @Query + '  AND PM.PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiIdSearchParam+ ''','',''))'
	end
	else
	begin
		SET @Query = @Query + '  AND PM.PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiId+ ''','',''))'
	end
IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND PM.StartDate >= '''+@TanggalAwal+''' and PM.StartDate <= '''+@TanggalAkhir+''''
END

SET @Query = @Query + ' 
  GROUP BY PegawaiId, IsDone,PG.Unit_Id)
  t 
PIVOT(
    SUM(Jumlah) 
    FOR Status IN (
        [Dalam Proses], 
        [Selesai])
) AS pivot_table GROUP BY Unit_Id ;'

exec (@Query)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Beranda_LoadDataProject_AllPendingTask]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Beranda_LoadDataProject_AllPendingTask] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @PegawaiLoginId varchar(MAX) ='',
  @PegawaiIdSearchParam varchar(MAX) ='',
  @UnitLoginId varchar(MAX) ='',
  @RoleId varchar(MAX) ='',
  @TypeTable varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Id
									  ,[NamaProject]
									  ,[ProjectId]
									  ,[DetailRequirment]
									  ,[NoProject] as ProjectNo
									  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiMulai]),'' - '',dbo.uf_IndonesianDate(TanggalEstimasiSelesai)) as TanggalProject
									  ,CONCAT(dbo.uf_IndonesianDate([StartDate]),'' - '',dbo.uf_IndonesianDate(EndDate)) as TimelinePengerjaan
									  ,[PegawaiId]
									  ,[NamaPegawai]
									  ,[NppPegawai]
									  ,[JobPosition]
									  ,[IsDone]
									  ,dbo.uf_IndonesianDate([TanggalPenyelesaian]) as TanggalPenyelesaian
									  ,[Selisih]
									  ,[JumlahHariPengerjaan]
									  ,[ProjectStatusId]
								from [dbo].[vw_allproject_pegawai] 
								WHERE (IsDone = 0 OR IsDone is null) ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''


IF(@PegawaiIdSearchParam !='')
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiIdSearchParam+ ''','',''))'
	end
	else
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiLoginId+ ''','',''))'
	end

IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND StartDate >= '''+@TanggalAwal+''' and StartDate <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'MSP.NoProject')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'MSP.NamaProject') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL '
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Beranda_LoadDataProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Beranda_LoadDataProject_Count] 	
  @ProjectNo varchar(MAX) ='',
    @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @NamaProject varchar(MAX) ='',
  @PegawaiLoginId varchar(MAX) ='',
  @PegawaiIdSearchParam varchar(MAX) ='',
  @UnitLoginId varchar(MAX) ='',
  @RoleId varchar(MAX) ='',
  @TypeTable varchar(MAX) ='' --1 Upcoming, 2 Overdue
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[vw_allproject_pegawai] 
								WHERE (IsDone = 0 OR IsDone is null) ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@TypeTable != '')
BEGIN
	IF(@TypeTable = '1')
	BEGIN
		SET @Query = @Query + ' AND Selisih > 0'
	END
	ELSE IF(@TypeTable = '2')
	BEGIN
		SET @Query = @Query + ' AND Selisih < 0'
	END
END

IF(@PegawaiIdSearchParam !='')
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiIdSearchParam+ ''','',''))'
	end
	else
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiLoginId+ ''','',''))'
	end
IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND StartDate >= '''+@TanggalAwal+''' and StartDate <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'NoProject')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'NamaProject') 

SET @Query =	@Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Beranda_LoadDataProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Beranda_LoadDataProject_View] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @PegawaiLoginId varchar(MAX) ='',
  @PegawaiIdSearchParam varchar(MAX) ='',
  @UnitLoginId varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @RoleId varchar(MAX) ='',
  @TypeTable varchar(MAX) ='', --1 Upcoming, 2 Overdue
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'Id'
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									  Id
									  ,[NamaProject]
									  ,[ProjectId]
									  ,[DetailRequirment]
									  ,[NoProject] as ProjectNo
									  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiMulai]),'' - '',dbo.uf_IndonesianDate(TanggalEstimasiSelesai)) as TanggalProject
									  ,CONCAT(dbo.uf_IndonesianDate([StartDate]),'' - '',dbo.uf_IndonesianDate(EndDate)) as TimelinePengerjaan
									  ,[PegawaiId]
									  ,[NamaPegawai]
									  ,[NppPegawai]
									  ,[JobPosition]
									  ,[IsDone]
									  ,dbo.uf_IndonesianDate([TanggalPenyelesaian]) as TanggalPenyelesaian
									  ,[Selisih]
									  ,[JumlahHariPengerjaan]
									  ,[ProjectStatusId]
									  ,Warna
									  ,StatusProject
									  ,StatusProjectDalamPantauan
								from [dbo].[vw_allproject_pegawai] 
								WHERE (IsDone = 0 OR IsDone is null) ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@TypeTable != '')
BEGIN
	IF(@TypeTable = '1')
	BEGIN
		SET @Query = @Query + ' AND Selisih > 0'
	END
	ELSE IF(@TypeTable = '2')
	BEGIN
		SET @Query = @Query + ' AND Selisih < 0'
	END
END

IF(@PegawaiIdSearchParam !='')
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiIdSearchParam+ ''','',''))'
	end
	else
	begin
		SET @Query = @Query + '  AND PegawaiId IN (select * from dbo.uf_SplitString('''+ @PegawaiLoginId+ ''','',''))'
	end

IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND StartDate >= '''+@TanggalAwal+''' and StartDate <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'NoProject')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'NamaProject') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_BookShelf_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_BookShelf_Count] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
							FROM [dbo].[Tbl_Master_Book] MB
							WHERE MB.[IsActive] = 1 and (MB.[IsBorrowed] = 0 OR MB.[IsBorrowed] is null)',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]')  

SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_BookShelf_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_BookShelf_View] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) ='',
  @sortColumn varchar(100)='MJ.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'MB.Id'
				WHEN 'Name' THEN 'MB.Name'
				WHEN 'Author' THEN 'MB.Author'
				WHEN 'RealeaseDate' THEN 'MB.RealeaseDate'
				WHEN 'IsBestSeller' THEN 'MB.IsBestSeller'
				WHEN 'Picture' THEN 'MB.Picture'			
				ELSE @sortColumn end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
							  ,MB.[Id]
							  ,MB.[Name]
							  ,MB.[Author]
							  ,[dbo].[uf_IndonesianDate](MB.[RealeaseDate]) as RealeaseDate
							  ,MB.[IsBestSeller]
							  ,MB.[CreatedTime]
							  ,MB.[UpdatedTime]
							  ,MB.[IsActive]
							  ,MB.[Picture]
							  ,MB.[IsBorrowed]
							  ,MB.[Description]
						  FROM [dbo].[Tbl_Master_Book] MB
						  WHERE MB.[IsActive] = 1 and (MB.[IsBorrowed] = 0 OR MB.[IsBorrowed] is null)',

	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_BorrowedBook_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_BorrowedBook_Count] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) ='',
  @UserId nvarchar(max)
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
						  FROM [dbo].[Tbl_Borrowed_Book] BB
						  LEFT JOIN [dbo].[Tbl_Master_Book] MB ON MB.Id = BB.Id_Book
						  WHERE MB.[IsActive] = 1 and BB.[Id_User] = CAST('+@UserId+' AS INT) and MB.[IsBorrowed] = 1',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]')  

SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_BorrowedBook_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_BorrowedBook_View] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) ='',
  @sortColumn varchar(100)='MJ.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT,
  @UserId nvarchar(max)
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'MB.Id'
				WHEN 'Name' THEN 'MB.Name'
				WHEN 'Author' THEN 'MB.Author'
				WHEN 'RealeaseDate' THEN 'MB.RealeaseDate'
				WHEN 'IsBestSeller' THEN 'MB.IsBestSeller'
				WHEN 'Picture' THEN 'MB.Picture'			
				ELSE @sortColumn end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
							  ,MB.[Id]
							  ,MB.[Name]
							  ,MB.[Author]
							  ,[dbo].[uf_IndonesianDate](MB.[RealeaseDate]) as RealeaseDate
							  ,MB.[IsBestSeller]
							  ,MB.[CreatedTime]
							  ,MB.[UpdatedTime]
							  ,MB.[IsActive]
							  ,BB.[IsLate]
							  ,MB.[Picture]
							  ,MB.[IsBorrowed]
							  ,MB.[Description]
							  ,[dbo].[uf_IndonesianDate](BB.[Borrow_Date]) as BorrowDate
							  ,[dbo].[uf_IndonesianDate](BB.[Finish_Date]) as FinishDate
						  FROM [dbo].[Tbl_Borrowed_Book] BB
						  LEFT JOIN [dbo].[Tbl_Master_Book] MB ON MB.Id = BB.Id_Book
						  WHERE MB.[IsActive] = 1 and BB.[Id_User] = CAST('+@UserId+' AS INT) and MB.[IsBorrowed] = 1',

	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Change_Roles]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Change_Roles]
@Id int
as
begin
SELECT RP.[Id]
	  ,convert(nvarchar(10),P.[Id]) as Pegawai_Id
      ,convert(nvarchar(10),RP.Unit_Id) as Unit_Id
	  ,[Id_JenisKelamin]
      ,Npp as Npp
      ,P.Nama as Nama_Pegawai
      ,[Alamat]
      ,P.[Email] as Email
      ,P.[Lastlogin]
      ,[Images]
      ,[Tanggal_Lahir]
      ,[No_HP]
	  ,P.IsActive
	  ,P.IsDeleted
	  ,U.Name as Nama_Unit
	  ,L.Name as Jenis_Kelamin
	  ,P.LDAPLogin
	  ,convert(nvarchar(10),RP.Role_Id) as Role_Id
	  ,convert(nvarchar(10),RP.Unit_Id) as Role_Unit_Id
	  ,MR.Nama as Nama_Role
	  ,U.Name as Role_Nama_Unit
	  ,L3.Name as Status_Role
	  ,ISNULL(P.Images,'../plugin/architectui/content/assets/images/avatars/1.jpg') as Images_User
	  ,convert(nvarchar(10),RP.Id) as User_Role_Id
  FROM [dbo].[Tbl_Role_Pegawai] RP
  LEFT JOIN dbo.Tbl_Pegawai P ON RP.Pegawai_Id = P.Id
  LEFT JOIN dbo.Tbl_Unit U ON RP.Unit_Id = U.Id
  LEFT JOIN dbo.Tbl_Lookup L3 ON L3.Value = RP.StatusRole and L3.Type = 'StatusRole'
  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = RP.Role_Id 
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = P.Id_JenisKelamin and L.Type = 'JenisKelamin'
  Where RP.Id= @Id
 end
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckAccessMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_CheckAccessMenu]
@Role_Id int,
@Url nvarchar(max)
as
begin

declare @Jumlah int,
		@Res bit

select @Jumlah = Count(*) FROM [dbo].[NavigationAssignment] NA
  LEFT JOIN [dbo].[Navigation] N ON NA.Navigation_Id = N.Id 
  where N.Route = @Url and Role_Id = @Role_Id

if(@Jumlah > 0)
begin
	Set @Res = 1
end
else
begin
	Set @Res = 0
end
end

select @Res
GO
/****** Object:  StoredProcedure [dbo].[sp_cleansing_all_project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
  CREATE proc [dbo].[sp_cleansing_all_project]
  as
  DELETE FROM [dbo].[Tbl_Project_Detail_Unit_Request]
  DELETE FROM [dbo].[Tbl_Project_File]
  DELETE FROM [dbo].[Tbl_Project_Log]
  DELETE FROM [dbo].[Tbl_Project_Log_Status]

  DELETE FROM [dbo].[Tbl_Project_Member_ProgressKerja]
  DELETE FROM [dbo].[Tbl_Project_Member]
  DELETE FROM [dbo].[Tbl_Project_Notes]
  DELETE FROM [dbo].[Tbl_Project_Relasi]
  DELETE FROM [dbo].[Tbl_Project_User]
  DELETE FROM [dbo].[Tbl_Project]

  DBCC CHECKIDENT ([Tbl_Project_Detail_Unit_Request], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_File], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_Log], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_Log_Status], RESEED, 1)

  DBCC CHECKIDENT ([Tbl_Project_Member_ProgressKerja], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_Member], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_Notes], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_Relasi], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project_User], RESEED, 1)
  DBCC CHECKIDENT ([Tbl_Project], RESEED, 1)



GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_GetSumaryProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Dashboard_GetSumaryProject]
@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '',
@TanggalAkhir nvarchar(max) = ''
as
declare @Query nvarchar(max),
		@TanggalAwalTotal nvarchar(max)= '1900-01-01',
		@TanggalAkhirTotal nvarchar(max)='2200-12-31',
		@PresentaseProjectClose int,
		@PresentaseOverdueProject int,
		@PresentaseUpcomingProject int

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @TanggalAwalTotal = @TanggalAwal
	SET @TanggalAkhirTotal = @TanggalAkhir
END


SELECT @PresentaseProjectClose = ISNULL((count(*) * 100) / NULLIF([dbo].[uf_GetAllTotalProjectByUnit](@UnitIdSearch,@TanggalAwalTotal,@TanggalAkhirTotal),0),0)
  FROM [dbo].[vw_Dashboard_AllProjectByUnit]
  Where UnitId IN (Select * from dbo.uf_SplitString(@UnitIdSearch,',')) 
  and TanggalEstimasiMulai >= @TanggalAwalTotal and TanggalEstimasiSelesai <= @TanggalAkhirTotal
  and CloseOpenId = 2

SELECT @PresentaseOverdueProject = ISNULL((count(*) * 100) / NULLIF([dbo].[uf_GetAllTotalProjectByUnit](@UnitIdSearch,@TanggalAwalTotal,@TanggalAkhirTotal),0),0)
  FROM [dbo].[vw_Dashboard_AllProjectByUnit]
  Where UnitId IN (Select * from dbo.uf_SplitString(@UnitIdSearch,',')) 
  and TanggalEstimasiMulai >= @TanggalAwalTotal and TanggalEstimasiSelesai <= @TanggalAkhirTotal
  and Selisih < 0

SELECT @PresentaseUpcomingProject = ISNULL((count(*) * 100) / NULLIF([dbo].[uf_GetAllTotalProjectByUnit](@UnitIdSearch,@TanggalAwalTotal,@TanggalAkhirTotal),0),0)
  FROM [dbo].[vw_Dashboard_AllProjectByUnit]
  Where UnitId IN (Select * from dbo.uf_SplitString(@UnitIdSearch,',')) 
  and TanggalEstimasiMulai >= @TanggalAwalTotal and TanggalEstimasiSelesai <= @TanggalAkhirTotal
  and Selisih >= 0 

SELECT @PresentaseProjectClose as PresentaseProjectClose
		,@PresentaseOverdueProject as PresentaseOverdueProject
		,@PresentaseUpcomingProject as PresentaseUpcomingProject
GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_GetTotalProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Dashboard_GetTotalProject]
@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '',
@TanggalAkhir nvarchar(max) = ''
as
declare @Query nvarchar(max),
		@TanggalAwalTotal nvarchar(max)= '1900-01-01',
		@TanggalAkhirTotal nvarchar(max)='2200-12-31'

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @TanggalAwalTotal = @TanggalAwal
	SET @TanggalAkhirTotal = @TanggalAkhir
END


SET @Query = '  select 0 as Id
				,''Total'' as [Status]
				,[dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+''') as Jumlah
				,''Total keseluruhan project'' as Keterangan
				UNION
				select Id
						,Name as [Status]
						,ISNULL(Tbl.Jumlah,0) as Jumlah
						,Keterangan
						from Tbl_Lookup L
				LEFT JOIN (
				SELECT CloseOpenId,
					   Count(*) as Jumlah
				 FROM vw_Dashboard_AllProjectByUnit
				  WHERE UnitId IN (select * from dbo.uf_SplitString('''+@UnitIdSearch+''','','')) '
IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @Query = @Query + ' AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' AND TanggalEstimasiSelesai <= '''+@TanggalAkhir+''' '
END				   

SET @Query = @Query + '  
				  GROUP BY CloseOpenId
				) Tbl ON Tbl.CloseOpenId = L.Value
				Where Type = ''ProjectStatus'' and l.IsActive = 1 and L.IsDeleted = 0 '
EXEC(@Query)
GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_GetTotalProject_ByStatuProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Dashboard_GetTotalProject_ByStatuProject]
@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '',
@TanggalAkhir nvarchar(max) = ''
as
declare @Query nvarchar(max),
		@TanggalAwalTotal nvarchar(max)= '1900-01-01',
		@TanggalAkhirTotal nvarchar(max)='2200-12-31'

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @TanggalAwalTotal = @TanggalAwal
	SET @TanggalAkhirTotal = @TanggalAkhir
END


SET @Query = 'select SP.Id
					   ,SP.Nama
						,ISNULL(Tbl.Jumlah,0) as Jumlah
						,[dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+''') as Total
						,ISNULL((ISNULL(Tbl.Jumlah,0) * 100) / NULLIF([dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+'''),0),0) as Presentase
						from Tbl_Master_Status_Project SP
				LEFT JOIN (
				SELECT [ProjectStatusId]
						,Count(*) as Jumlah
						  FROM [dbo].[vw_Dashboard_AllProjectByUnit]
				  WHERE UnitId IN (select * from dbo.uf_SplitString('''+@UnitIdSearch+''','','')) '
IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @Query = @Query + ' AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' AND TanggalEstimasiSelesai <= '''+@TanggalAkhir+''' '
END				   

SET @Query = @Query + '  
				  GROUP BY [ProjectStatusId]
				) Tbl ON tbl.[ProjectStatusId] = SP.Id
				Where SP.IsActive = 1 and SP.IsDeleted = 0 '
EXEC(@Query)
GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_LoadDataProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dashboard_LoadDataProject_Count] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @RoleId varchar(MAX) ='',
  @TypeTable varchar(MAX) ='' --1 Upcoming, 2 Overdue
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[vw_Dashboard_AllProjectByUnit]
								WHERE [CloseOpenId] = 1 ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@TypeTable != '')
BEGIN
	IF(@TypeTable = '1')
	BEGIN
		SET @Query = @Query + ' AND Selisih > 0'
	END
	ELSE IF(@TypeTable = '2')
	BEGIN
		SET @Query = @Query + ' AND Selisih < 0'
	END
END

IF(@UnitId !='')
begin
	SET @Query = @Query + '  AND UnitId IN (select * from dbo.uf_SplitString('''+ @UnitId+ ''','',''))'
end

IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' and TanggalEstimasiSelesai <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'Nama') 

SET @Query =	 @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_LoadDataProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dashboard_LoadDataProject_View] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @RoleId varchar(MAX) ='',
  @TypeTable varchar(MAX) ='', --1 Upcoming, 2 Overdue
  @sortColumn varchar(100)='ProjectId',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'AL.Id'
				WHEN 'ProjectId' THEN 'ProjectId'
				WHEN 'NamaProject' THEN 'AL.Nama'

				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									  [ProjectId]
									  ,[ProjectNo] as ProjectNo
									  ,AL.[Nama] as NamaProject
									  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiMulai]),'' - '',dbo.uf_IndonesianDate([TanggalEstimasiSelesai])) TanggalProject
									  ,[UnitId]
									  ,[KodeUnit]
									  ,[NamaUnit]
									  ,[ProjectStatusId]
									  ,[CloseOpenId]
									  ,[Selisih]
									  ,SP.Nama as ProjectStatus
										,L.Name as CloseOpen
										,dbo.uf_IndonesianDate(AL.TanggalSelesaiProject) as TanggalPenyelesaian
										,SLA as JumlahHariPengerjaan
										,Warna
										,StatusProjectDalamPantauan
										,AL.Presentase
								from [dbo].[vw_Dashboard_AllProjectByUnit] AL
								LEFT JOIN dbo.Tbl_Master_Status_Project SP ON AL.ProjectStatusId = SP.Id
								LEFT JOIn dbo.Tbl_Lookup L ON L.Value = AL.CloseOpenId and L.Type = ''ProjectStatus'' and L.IsActive = 1 and L.IsDeleted = 0 
								WHERE [CloseOpenId] = 1 ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@TypeTable != '')
BEGIN
	IF(@TypeTable = '1')
	BEGIN
		SET @Query = @Query + ' AND Selisih > 0'
	END
	ELSE IF(@TypeTable = '2')
	BEGIN
		SET @Query = @Query + ' AND Selisih < 0'
	END
END

IF(@UnitId !='')
begin
	SET @Query = @Query + '  AND UnitId IN (select * from dbo.uf_SplitString('''+ @UnitId+ ''','',''))'
end
IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' and TanggalEstimasiSelesai <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'Nama') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_LoadDataTopProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dashboard_LoadDataTopProject_Count] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @RoleId varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
									from [dbo].[vw_Dashboard_AllProjectByUnit] AL
									LEFT JOIN dbo.Tbl_Master_Status_Project SP ON AL.ProjectStatusId = SP.Id
									LEFT JOIn dbo.Tbl_Lookup L ON L.Value = AL.CloseOpenId and L.Type = ''ProjectStatus'' and L.IsActive = 1 and L.IsDeleted = 0 
									WHERE AL.ProjectId is not null ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''


IF(@UnitId !='')
begin
	SET @Query = @Query + '  AND UnitId IN (select * from dbo.uf_SplitString('''+ @UnitId+ ''','',''))'
end
IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' and TanggalEstimasiSelesai <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'AL.Nama') 

SET @Query =	@Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_LoadDataTopProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Dashboard_LoadDataTopProject_View] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @TanggalAwal nvarchar(100)='',
  @TanggalAkhir nvarchar(100)='',
  @RoleId varchar(MAX) ='',
  @sortColumn varchar(100)='ProjectId',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'AL.Id'
				WHEN 'NamaProject' THEN 'AL.Nama'
				WHEN 'ProjectStatus' THEN 'SP.Nama'
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    AL.Id
										,[ProjectId]
										,[ProjectNo] as ProjectNo
										,AL.[Nama] as NamaProject
										,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiMulai]),'' - '',dbo.uf_IndonesianDate([TanggalEstimasiSelesai])) TanggalProject
										,[UnitId]
										,[KodeUnit]
										,[NamaUnit]
										,[ProjectStatusId]
										,[CloseOpenId]
										,[Selisih]
										,SP.Nama as ProjectStatus
										,L.Name as CloseOpen
										,dbo.uf_IndonesianDate(AL.TanggalSelesaiProject) as TanggalPenyelesaian
										,SLA as JumlahHariPengerjaan
										,Warna
										,StatusProjectDalamPantauan
										,AL.Presentase
									from [dbo].[vw_Dashboard_AllProjectByUnit] AL
									LEFT JOIN dbo.Tbl_Master_Status_Project SP ON AL.ProjectStatusId = SP.Id
									LEFT JOIn dbo.Tbl_Lookup L ON L.Value = AL.CloseOpenId and L.Type = ''ProjectStatus'' and L.IsActive = 1 and L.IsDeleted = 0 
									WHERE AL.ProjectId is not null ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''


IF(@UnitId !='')
begin
	SET @Query = @Query + '  AND UnitId IN (select * from dbo.uf_SplitString('''+ @UnitId+ ''','',''))'
end
IF(@TanggalAwal != '' AND @TanggalAkhir !='')
BEGIN
		SET @Query = @Query + '  AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' and TanggalEstimasiSelesai <= '''+@TanggalAkhir+''''
END

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'AL.Nama') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_TopUser]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Dashboard_TopUser]
@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '',
@TanggalAkhir nvarchar(max) = ''
as
declare @Query nvarchar(max),
		@TanggalAwalTotal nvarchar(max)= '1900-01-01',
		@TanggalAkhirTotal nvarchar(max)='2200-12-31'

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @TanggalAwalTotal = @TanggalAwal
	SET @TanggalAkhirTotal = @TanggalAkhir
END

SET @Query = ' SELECT TOP 5 NamaClient as label
					  ,Count(*) as jumlah
					  ,[dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+''') as total
					  ,ISNULL((Count(*) * 100) /  NULLIF([dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+'''),0),0) as y
				  from [dbo].[vw_Dashboard_AllProjectByUser] PU
				LEFT JOIN dbo.vw_Dashboard_AllProjectByUnit UN ON PU.ProjectId = UN.ProjectId
				  WHERE UN.UnitId IN (select * from dbo.uf_SplitString('''+@UnitIdSearch+''','','')) '

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @Query = @Query + ' AND PU.TanggalEstimasiMulai >= '''+@TanggalAwal+''' AND PU.TanggalEstimasiSelesai <= '''+@TanggalAkhir+''' '
END				   

SET @Query = @Query + 'GROUP BY NamaClient
				  ORDER BY Jumlah desc '

EXEC(@Query)
GO
/****** Object:  StoredProcedure [dbo].[sp_Dashboard_TopWorkLoad]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Dashboard_TopWorkLoad]
@UnitIdSearch nvarchar(max) = '',
@TanggalAwal nvarchar(max) = '',
@TanggalAkhir nvarchar(max) = ''
as
declare @Query nvarchar(max),
		@TanggalAwalTotal nvarchar(max)= '1900-01-01',
		@TanggalAkhirTotal nvarchar(max)='2200-12-31'

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @TanggalAwalTotal = @TanggalAwal
	SET @TanggalAkhirTotal = @TanggalAkhir
END

SET @Query = ' SELECT TOP 5 [PegawaiId]
					  ,[NamaPegawai] as label
					  ,Count(*) as jumlah
					  ,[dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+''') as total
					  ,ISNULL((Count(*) * 100) /  NULLIF([dbo].[uf_GetAllTotalProjectByUnit]('''+@UnitIdSearch+''','''+@TanggalAwalTotal+''','''+@TanggalAkhirTotal+'''),0),0) as y
				  FROM [dbo].[vw_Dashboard_AllProjectByPegawai] 
				  WHERE UnitId IN (select * from dbo.uf_SplitString('''+@UnitIdSearch+''','','')) '

IF(@TanggalAwal != '' AND @TanggalAkhir != '')
BEGIN
	SET @Query = @Query + ' AND TanggalEstimasiMulai >= '''+@TanggalAwal+''' AND TanggalEstimasiSelesai <= '''+@TanggalAkhir+''' '
END				   

SET @Query = @Query + 'GROUP BY [PegawaiId]
					  ,[NamaPegawai]
				  ORDER BY Jumlah desc '

EXEC(@Query)
GO
/****** Object:  StoredProcedure [dbo].[sp_DataProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DataProject_Count] 	
  @NoProject varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @Tanggal varchar(MAX) ='',
  @StatusProjectId varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								  FROM [dbo].[v_data_project]
								   Where IsDeleted = 0 and IsActive = 1 ',
	@QueryNoProject varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = '',
	@QueryStatusProject varchar(MAX) = ''

SELECT @QueryNoProject = dbo.uf_LookupDynamicQueryGenerator(@NoProject, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'Nama') 
SELECT @QueryStatusProject = dbo.uf_LookupDynamicQueryGeneratorEqual(@StatusProjectId, 'ProjectStatusId') 

SET @Query =	@Query 
				+ @QueryNoProject
				+ @QueryNamaProject
				+ @QueryStatusProject
			
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_DataProject_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_DataProject_GetDataById]
@Id int
as
begin
SELECT P.[Id]
      ,[KategoriProjectId] as KategoriProjectId
      ,[SubKategoriProjectId] as SubKategoriProjectId
      ,[SkorProjectId] as SkorProjectId
      ,[KompleksitasProjectId] as KompleksitasProjectId
      ,[KlasifikasiProjectId] as KlasifikasiProjectId
      ,[MandatoryId] as MandatoryId
      ,[PeriodeProjectId] as PeriodeProjectId
      ,[NotifikasiId] as NotifikasiId
      ,[IsPIR] as isPIR
      ,[ProjectStatusId] as ProjectStatusId
      ,[Kode]
      ,[Nama]
      ,[ProjectNo] as NoProject
      ,[NoMemo]
      ,FORMAT([TanggalMemo],'dd/MM/yyyy') as [TanggalMemo]
      ,[NoDRF] as NoDrf
      ,FORMAT([TanggalDRF],'dd/MM/yyyy') as TanggalDrf
      ,FORMAT([TanggalDisposisi],'dd/MM/yyyy') as TanggalDisposisi
      ,FORMAT([TanggalKlarifikasi],'dd/MM/yyyy') as TanggalKlarifikasi 
      ,[DetailRequirment]
      ,(CASE WHEN [TanggalEstimasiMulai] is not null and [TanggalEstimasiSelesai] is not null THEN CONCAT(FORMAT([TanggalEstimasiMulai],'dd/MM/yyyy'),' - ',FORMAT([TanggalEstimasiSelesai],'dd/MM/yyyy')) ELSE NULL END) as TanggalEstimasiDone
      ,FORMAT([TanggalEstimasiProduction],'dd/MM/yyyy') as TanggalEstimasiProduction
      ,(CASE WHEN [TanggalEstimasiDevelopmentAwal] is not null and [TanggalEstimasiDevelopmentAkhir] is not null THEN CONCAT(FORMAT([TanggalEstimasiDevelopmentAwal],'dd/MM/yyyy'),' - ',FORMAT([TanggalEstimasiDevelopmentAkhir],'dd/MM/yyyy')) ELSE NULL END) as TanggalEstimasiDevelopment
      ,(CASE WHEN [TanggalEstimasiTestingAwal] is not null and [TanggalEstimasiTestingAkhir] is not null THEN CONCAT(FORMAT([TanggalEstimasiTestingAwal],'dd/MM/yyyy'),' - ',FORMAT([TanggalEstimasiTestingAkhir],'dd/MM/yyyy')) ELSE NULL END) as TanggalEstimasiTesting
      ,(CASE WHEN [TanggalEstimasiPilotingAwal] is not null and [TanggalEstimasiPilotingAkhir] is not null THEN CONCAT(FORMAT([TanggalEstimasiPilotingAwal],'dd/MM/yyyy'),' - ',FORMAT([TanggalEstimasiPilotingAkhir],'dd/MM/yyyy')) ELSE NULL END) as TanggalEstimasiPiloting
      ,(CASE WHEN [TanggalEstimasiPIRAwal] is not null and [TanggalEstimasiPIRAkhir] is not null THEN CONCAT(FORMAT([TanggalEstimasiPIRAwal],'dd/MM/yyyy'),' - ',FORMAT([TanggalEstimasiPIRAkhir],'dd/MM/yyyy')) ELSE NULL END) as TanggalEstimasiPir
      ,[IsDone]
	  ,CloseOpenId
	  ,L.Name as CloseOpenStatus
      ,P.[Order_By]
      ,P.[CreatedTime]
      ,P.[UpdatedTime]
      ,P.[DeletedTime]
      ,P.[CreatedBy_Id]
      ,P.[UpdatedBy_Id]
      ,P.[DeletedBy_Id]
      ,P.[IsDeleted]
      ,P.[IsActive]
	  ,P.StatusProjectDalamPantauan
	  ,P.[AreaWakilPemimpinId] as AreaWakilPemimpinId
      ,P.JenisProjectId as JenisProjectId
  FROM [dbo].[Tbl_Project] P
  LEFT JOIN dbo.Tbl_Lookup L ON P.CloseOpenId = L.Value and Type = 'ProjectStatus' and L.IsActive = 1 and L.IsDeleted = 0
  Where P.Id = @Id
end

--select * from Tbl_Lookup
GO
/****** Object:  StoredProcedure [dbo].[sp_DataProject_PrintDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_DataProject_PrintDataById]
@Id int
as
begin
SELECT P.[Id]
      ,[KategoriProjectId] as KategoriProjectId
      ,[SubKategoriProjectId] as SubKategoriProjectId
      ,[SkorProjectId] as SkorProjectId
	  ,MKP.Nama as KompleksitasProject
      ,[KompleksitasProjectId] as KompleksitasProjectId
	  ,MKS.Nama as KlasifikasiProject
      ,[KlasifikasiProjectId] as KlasifikasiProjectId
	  ,L.Name as Mandatory
      ,[MandatoryId] as MandatoryId
	  ,L2.Name as PeriodeProject
      ,[PeriodeProjectId] as PeriodeProjectId
      ,[NotifikasiId] as NotifikasiId
      ,[IsPIR] as isPIR
	  ,MSP.Nama as ProjectStatus
      ,[ProjectStatusId] as ProjectStatusId
	  ,MKKP.Nama as KategoriProject
	  ,MKSP.Nama as SubKategoriProject
      ,P.[Kode]
      ,P.[Nama]
      ,[ProjectNo] as NoProject
      ,[NoMemo]
      ,dbo.uf_IndonesianDate([TanggalMemo]) as [TanggalMemo]
      ,[NoDRF] as NoDrf
      ,dbo.uf_IndonesianDate([TanggalDRF]) as TanggalDrf
      ,dbo.uf_IndonesianDate([TanggalDisposisi]) as TanggalDisposisi
      ,dbo.uf_IndonesianDate([TanggalKlarifikasi]) as TanggalKlarifikasi 
      ,[DetailRequirment]
      ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiMulai]),' - ',dbo.uf_IndonesianDate([TanggalEstimasiSelesai])) as TanggalEstimasiDone
      ,dbo.uf_IndonesianDate([TanggalEstimasiProduction]) as TanggalEstimasiProduction 
      ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiDevelopmentAwal]),' - ',dbo.uf_IndonesianDate([TanggalEstimasiDevelopmentAkhir])) as TanggalEstimasiDevelopment
	  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiTestingAwal]),' - ',dbo.uf_IndonesianDate([TanggalEstimasiTestingAkhir])) as TanggalEstimasiTesting
	  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiPilotingAwal]),' - ',dbo.uf_IndonesianDate([TanggalEstimasiPilotingAkhir])) as TanggalEstimasiPiloting
	  ,CONCAT(dbo.uf_IndonesianDate([TanggalEstimasiPIRAwal]),' - ',dbo.uf_IndonesianDate([TanggalEstimasiPIRAkhir])) as TanggalEstimasiPir
      ,[IsDone]
	  ,(CASE WHEN P.IsPIR = 1 THEN 'Ya' ELSE 'Tidak'  END) as PIR
  FROM [dbo].[Tbl_Project] P
  LEFT JOIN dbo.Tbl_Master_Kompleksitas_Project MKP ON P.[KompleksitasProjectId] = MKP.Id
  LEFT JOIN dbo.Tbl_Master_Klasifikasi_Project MKS ON P.KlasifikasiProjectId = MKS.Id
  LEFT JOIN dbo.Tbl_Lookup L ON P.MandatoryId = L.Value and L.Type = 'MandatoryKategori' and L.IsActive = 1 and L.IsDeleted = 0
    LEFT JOIN dbo.Tbl_Lookup L2 ON P.PeriodeProjectId = L2.Value and L2.Type = 'PeriodeProject' and L2.IsActive = 1 and L2.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Master_Status_Project MSP ON P.ProjectStatusId = MSP.Id
  LEFT JOIN dbo.Tbl_Master_Kategori_Project MKKP ON P.KategoriProjectId = MKKP.Id
  LEFT JOIN dbo.Tbl_Master_Sub_Kategori_Project MKSP ON P.SubKategoriProjectId = MKSP.Id

  Where P.Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_DataProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DataProject_View] 	
  @NoProject varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @Tanggal varchar(MAX) ='',
  @StatusProjectId varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'NoProject' THEN 'ProjectNo'
				WHEN 'NamaProject' THEN 'Nama'
				WHEN 'TanggalMemo' THEN 'TanggalMemo'
				WHEN 'NoDRF' THEN 'NoDRF'
				WHEN 'TanggalEstimasiProject' THEN 'TanggalEstimasiMulai'
				WHEN 'TanggalEstimasiDevelopment' THEN 'TanggalEstimasiDevelopmentAwal'
				WHEN 'TanggalEstimasiTesting' THEN 'TanggalEstimasiTestingAwal'
				WHEN 'TanggalEstimasiPilotingAwal' THEN 'TanggalEstimasiPilotingAwal'
				WHEN 'TanggalEstimasiPIR' THEN 'TanggalEstimasiPIRAwal'
				WHEN 'StatusProjectDalamPantauan' THEN 'StatusProjectDalamPantauan'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'Id' end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    [ProjectNo] as NoProject
										,Id
									  ,Nama as NamaProject
									  ,[KategoriProjectId]
									  ,[KategoriProject]
									  ,[SubKategoriProjectId]
									  ,[SubKategoriProject]
									  ,[SkorProjectId]
									  ,[SkorProject]
									  ,[KompleksitasProjectId]
									  ,[KompleksitasProject]
									  ,[KlasifikasiProjectId]
									  ,[KlasifikasiProject]
									  ,[MandatoryId]
									  ,[Mandatory]
									  ,[PeriodeProjectId]
									  ,[PeriodeProject]
									  ,[NotifikasiId]
									  ,[IsPIR]
									  ,[PIR]
									  ,[ProjectStatusId]
									  ,[ProjectStatus]
									  ,[Kode]
									  ,[Nama]
									  ,[ProjectNo]
									  ,[NoMemo]
									  ,Presentase
									  ,CloseOpenStatus
									  ,StatusProjectDalamPantauan
									  ,dbo.[uf_IndonesianDateDDMMYYYY]([TanggalMemo]) as [TanggalMemo]
									  ,[NoDRF] as NoDrf
									  ,dbo.[uf_IndonesianDateDDMMYYYY]([TanggalDRF]) as TanggalDrf
									  ,dbo.[uf_IndonesianDateDDMMYYYY]([TanggalSelesaiProject]) as TanggalSelesaiProject
									  ,CONCAT(dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiMulai]),'' - '',dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiSelesai])) as [TanggalEstimasiProject]
									  ,CONCAT(dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiDevelopmentAwal]),'' - '',dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiDevelopmentAkhir])) as [TanggalEstimasiDevelopment]
									  ,CONCAT(dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiTestingAwal]),'' - '',dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiTestingAkhir])) as [TanggalEstimasiTesting]
									  ,CONCAT(dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiPilotingAwal]),'' - '',dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiPilotingAkhir])) as [TanggalEstimasiPiloting]
									  ,CONCAT(dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiPIRAwal]),'' - '',dbo.[uf_IndonesianDateDDMMYYYY]([TanggalEstimasiPIRAkhir])) as [TanggalEstimasiPir]
									  --,DATEDIFF(DAY,getdate(),[TanggalEstimasiSelesai]) as Selisih
									  ,[SelisihDeadlineProject] as Selisih
									  ,[SLA]
									  ,[Warna]
								  FROM [dbo].[v_data_project]
								  Where IsDeleted = 0 and IsActive = 1 ',
	@QueryNoProject varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = '',
	@QueryStatusProject varchar(MAX) = ''

SELECT @QueryNoProject = dbo.uf_LookupDynamicQueryGenerator(@NoProject, 'ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'Nama') 
SELECT @QueryStatusProject = dbo.uf_LookupDynamicQueryGeneratorEqual(@StatusProjectId, 'ProjectStatusId') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryNoProject
				+ @QueryNamaProject
				+ @QueryStatusProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataClient]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataClient]
@ProjectId int
as
begin
SELECT ROW_NUMBER() OVER(ORDER BY MC.Nama asc) as Nomor
	   ,PU.[Id]
	  ,MC.Nama as Client
	  ,PU.ClientId
      ,[NppPIC] as NppPic
      ,[NamaPIC] as NamaPic
      ,[Email]
      ,[NoHp] as NoHp
      ,PU.[Keterangan]
  FROM [dbo].[Tbl_Project_User] PU
  LEFT JOIN dbo.Tbl_Master_Client MC ON PU.ClientId = MC.Id
  WHERE PU.ProjectId = @ProjectId and PU.IsDeleted = 0
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataClientById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataClientById]
@Id int
as
begin
SELECT PU.[Id]
	  ,MC.Nama as Client
	  ,PU.ClientId
      ,[NppPIC] as NppPic
      ,[NamaPIC] as NamaPic
      ,[Email]
      ,[NoHp] as NoHp
      ,PU.[Keterangan]
  FROM [dbo].[Tbl_Project_User] PU
  LEFT JOIN dbo.Tbl_Master_Client MC ON PU.ClientId = MC.Id
  WHERE PU.Id = @Id 
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataNotes]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataNotes]
@ProjectId int
as
begin
SELECT PN.[Id]
      ,[ProjectId]
      ,[PegawaiId]
	  ,P.Nama as NamaPegawai
      ,[Notes]
	  ,P.Images
	  ,P.NameImages
	  ,P.Npp
	  ,PN.Judul
	  ,dbo.uf_IndonesianDateTime(PN.CreatedTime) as CreatedDate
  FROM [dbo].[Tbl_Project_Notes] PN 
  LEFT JOIN dbo.Tbl_Pegawai P On PN.PegawaiId = P.Id
  WHERE PN.ProjectId = @ProjectId and PN.IsActive = 1 and PN.IsDeleted = 0
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataNotesById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataNotesById]
@Id int
as
begin
SELECT PN.[Id]
      ,[Notes]
	  ,PN.Judul
  FROM [dbo].[Tbl_Project_Notes] PN 
  WHERE PN.Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataProjectFile]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataProjectFile]
@ProjectId int
as
begin
SELECT PF.[Id]
      ,[ProjectId]
      ,MT.Nama as TypeDokumen
      ,[NamaFile]
      ,[FileExt]
      ,[FileType]
      ,[Size]
      ,[Path]
      ,[FullPath]
      ,PF.[Keterangan]
      ,dbo.uf_ShortIndonesianDateTime([UploadTime]) as UploadTime
      ,CONCAT(P.Npp,' - ', P.Nama) as UploadBy
  FROM [dbo].[Tbl_Project_File] PF
  LEFT JOIN dbo.Tbl_Master_Type_Dokumen MT ON PF.TypeDokumenId = MT.Id
  LEFT JOIN dbo.Tbl_Pegawai P ON PF.UploadBy_Id = P.Id
  WHERE PF.ProjectId = @ProjectId and PF.IsDeleted = 0 
  order by PF.Id desc

end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataProjectFileById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataProjectFileById]
@Id int
as
begin
SELECT PF.[Id]
      ,[ProjectId]
      ,MT.Nama as TypeDokumen
      ,[NamaFile]
      ,[FileExt]
      ,[FileType]
      ,[Size]
      ,[Path]
      ,[FullPath]
      ,PF.[Keterangan]
      ,dbo.uf_ShortIndonesianDateTime([UploadTime]) as UploadTime
      ,CONCAT(P.Npp,' - ', P.Nama) as UploadBy
  FROM [dbo].[Tbl_Project_File] PF
  LEFT JOIN dbo.Tbl_Master_Type_Dokumen MT ON PF.TypeDokumenId = MT.Id
  LEFT JOIN dbo.Tbl_Pegawai P ON PF.UploadBy_Id = P.Id
  WHERE PF.Id = @Id 
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataProjectMember]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataProjectMember]
@ProjectId int
as
begin
declare @VirtualPathImages nvarchar(max)='',
		@ImagesDefault nvarchar(max)=''

select @VirtualPathImages = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathProfile'
select @ImagesDefault = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathImagesDefault'

SELECT ROW_NUMBER() OVER(ORDER BY U.Name asc) as Nomor
      ,PM.[Id]
	  ,MJ.Nama as JobPosisi
	  ,P.Npp
	  ,P.Nama
	  ,P.Email
	  ,P.No_HP as Telepon
	  ,U.Name as Unit
      ,PM.[Keterangan]
	  ,L.Name as StatusProgress
      ,[CatatanPegawai]
      ,[SendAsTask]
	  ,PM.IsDone
	  --,Images
	  ,(CASE WHEN P.Images is null THEN @ImagesDefault ELSE CONCAT(@VirtualPathImages,Images) END) as Images
	  ,NameImages
	  ,L2.Name as Status
	  ,dbo.uf_IndonesianDate(PM.TanggalPenyelesaian) as TanggalPenyelesaian
      ,CONCAT(dbo.uf_IndonesianDate(PM.[StartDate]),' - ',dbo.uf_IndonesianDate(PM.[EndDate])) as TanggalTargetPenyelesaianAnggotaTim
      --,ISNULL(dbo.uf_AddThousandSeparators(DATEDIFF(DAY,getdate(),EndDate)),'-') as Sisa
	,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and PM.EndDate is not null THEN CONCAT(dbo.[uf_getSelisihHari](getdate(),PM.EndDate),' hari') ELSE ' - ' END) as Sisa
	,(CASE WHEN (PM.IsDone = 1 OR PM.IsDone is null) and PM.TanggalPenyelesaian is not null THEN CONCAT(dbo.[uf_getSelisihHari](PM.StartDate,PM.TanggalPenyelesaian),' hari') ELSE ' - ' END) as JumlahHariPengerjaan
	,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and PM.EndDate is not null THEN dbo.[uf_getSelisihHari](getdate(),PM.EndDate) ELSE null END) as SelisihAngka
	,(CASE WHEN (PM.IsDone = 1 OR PM.IsDone is null) and PM.TanggalPenyelesaian is not null THEN dbo.[uf_getSelisihHari](PM.StartDate,PM.TanggalPenyelesaian) ELSE null END) as JumlahHariPengerjaanAngka
	,(CASE WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 20 and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) >= 0 THEN 'Kuning'
			WHEN (PM.IsDone = 0 OR PM.IsDone is null) and ((dbo.[uf_getSelisihHari](getdate(),PM.EndDate) * 100) / NULLIF(dbo.[uf_getSelisihHari](PM.StartDate,PM.EndDate),0)) < 0 THEN 'Merah'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) >= 1 THEN 'Biru'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) = 0 THEN 'Hijau'
		   WHEN (PM.IsDone = 1) and dbo.[uf_getSelisihHari](PM.TanggalPenyelesaian,PM.EndDate) < 0 THEN 'Merah'
			 ELSE '' END) as Warna
	,(CASE WHEN PM.IsDone = 0 THEN 'Dalam Proses' ELSE 'Selesai' END) as StatusDoneProject
  FROM [dbo].[Tbl_Project_Member] PM 
  LEFT JOIN dbo.Tbl_Pegawai P ON PM.PegawaiId = P.Id
  LEFT JOIN dbo.Tbl_Unit U ON U.Id = P.Unit_Id
  LEFT JOIN dbo.Tbl_Master_Job_Position MJ ON PM.JobPositionId = MJ.Id
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = PM.StatusProgressId AND L.Type = 'StatusProgressPekerjaan' and L.IsActive = 1 and L.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Lookup L2 ON L2.Value = PM.IsActive AND L2.Type = 'IsActive' and L2.IsActive = 1 and L2.IsDeleted = 0
  
  WHERE PM.ProjectId = @ProjectId and PM.IsActive = 1 and PM.IsDeleted = 0
  order by PM.Id desc

end

GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataProjectMemberById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataProjectMemberById]
@Id int
as
begin
SELECT PM.[Id]
	  ,PegawaiId
	  ,PM.JobPositionId
	  ,MJ.Nama as JobPosisi
	  ,P.Npp
	  ,P.Nama
	  ,P.Email
	  ,P.No_HP as Telepon
	  ,U.Name as Unit
      ,PM.[Keterangan]
	  ,L.Name as StatusProgress
      ,[CatatanPegawai]
      ,[SendAsTask]
	  ,Images
	  ,NameImages
	  ,L2.Name as Status
      --,CONCAT(dbo.uf_IndonesianDate([StartDate]),' - ',dbo.uf_IndonesianDate([EndDate])) as TanggalTargetPenyelesaianAnggotaTim
      ,CONCAT(FORMAT(StartDate,'dd/MM/yyyy'),' - ',FORMAT(EndDate,'dd/MM/yyyy')) as TanggalTargetPenyelesaianAnggotaTim
	  ,ISNULL(dbo.uf_AddThousandSeparators(DATEDIFF(DAY,getdate(),EndDate)),'-') as Sisa
  FROM [dbo].[Tbl_Project_Member] PM 
  LEFT JOIN dbo.Tbl_Pegawai P ON PM.PegawaiId = P.Id
  LEFT JOIN dbo.Tbl_Unit U ON U.Id = P.Unit_Id
  LEFT JOIN dbo.Tbl_Master_Job_Position MJ ON PM.JobPositionId = MJ.Id
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = PM.StatusProgressId AND L.Type = 'StatusProgressPekerjaan' and L.IsActive = 1 and L.IsDeleted = 0
  LEFT JOIN dbo.Tbl_Lookup L2 ON L2.Value = PM.IsActive AND L2.Type = 'IsActive' and L2.IsActive = 1 and L2.IsDeleted = 0
  
  WHERE PM.Id = @Id 
end

GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataRelasiProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataRelasiProject]
@ProjectId int
as
begin
SELECT ROW_NUMBER() OVER(ORDER BY P.ProjectNo asc) as Nomor
		,PR.[Id]
	  ,P.ProjectNo
	  ,P.Nama as NamaProject
      ,[Keterangan]
  FROM [dbo].[Tbl_Project_Relasi] PR
  LEFT JOIN dbo.Tbl_Project P ON PR.RelasiProjectId = P.Id
  WHERE PR.ProjectId = @ProjectId and PR.IsActive = 1 and PR.IsDeleted = 0
  order by PR.Id desc
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetailProject_GetDataRelasiProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_DetailProject_GetDataRelasiProjectById]
@Id int
as
begin
SELECT PR.[Id]
	  ,ProjectId
	  ,RelasiProjectId
	  ,P.ProjectNo
	  ,P.Nama
      ,[Keterangan]
  FROM [dbo].[Tbl_Project_Relasi] PR
  LEFT JOIN dbo.Tbl_Project P ON PR.RelasiProjectId = P.Id
  WHERE PR.Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY U.Name Asc,P.Npp asc) AS Number
								 ,P.Id as id 
							   ,CONCAT(U.Name,'' - '', [Nama],'' ('',[Npp],'')'') as text
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''



SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByParam]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByParam]
   @UnitId nvarchar(max) = '',
   @RoleId nvarchar(max) = '',
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY U.Name Asc,P.Npp asc) AS Number
								 ,P.Id as id 
							   ,CONCAT(U.Name,'' - '', [Nama],'' ('',[Npp],'')'') as text
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

IF(@UnitId != '')
BEGIN
	SET @Query = @Query + ' AND P.Unit_Id in (select * from dbo.uf_SplitString('''+@UnitId+''','','')) '
END

SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByParam_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByParam_Count]
   @UnitId nvarchar(max) = '',
   @RoleId nvarchar(max) = '',
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

IF(@UnitId != '')
BEGIN
	SET @Query = @Query + ' AND P.Unit_Id in (select * from dbo.uf_SplitString('''+@UnitId+''','','')) '
END

SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByPegawaiKelolaan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByPegawaiKelolaan]
   @UnitId nvarchar(max) = '',
   @RoleId nvarchar(max) = '',
   @PegawaiLoginId nvarchar(max) = '',
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY U.Name Asc,P.Npp asc) AS Number
								 ,P.Id as id 
							   ,CONCAT(U.Name,'' - '', [Nama],'' ('',[Npp],'')'') as text
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

IF(@PegawaiLoginId != '')
BEGIN
	SET @Query = @Query + ' AND (P.Id in (select [PegawaiId] from [dbo].[Tbl_Pegawai_Kelolaan] Where [AtasanId] = '''+@PegawaiLoginId+''') OR P.Id = '''+@PegawaiLoginId+''') '
END

SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByPegawaiKelolaan_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByPegawaiKelolaan_Count]
   @UnitId nvarchar(max) = '',
   @RoleId nvarchar(max) = '',
   @PegawaiLoginId nvarchar(max) = '',
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

IF(@PegawaiLoginId != '')
BEGIN
	SET @Query = @Query + ' AND (P.Id in (select [PegawaiId] from [dbo].[Tbl_Pegawai_Kelolaan] Where [AtasanId] = '''+@PegawaiLoginId+''') OR P.Id = '''+@PegawaiLoginId+''') '

	--SET @Query = @Query + ' AND P.Id in (select [PegawaiId] from [dbo].[Tbl_Pegawai_Kelolaan] Where [AtasanId] = '''+@PegawaiLoginId+''') '
END

SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =	 @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByRoleAndUnit]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByRoleAndUnit]
   @Parameter varchar(MAX) ='',
   @RoleId varchar(MAX) ='',
   @UnitIdLogin varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY U.Name Asc,P.Npp asc) AS Number
								 ,P.Id as id 
							   ,CONCAT(U.Name,'' - '', [Nama],'' ('',[Npp],'')'') as text
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
						  and P.Role_Id = '''+@RoleId+''' and P.Unit_Id = '''+@UnitIdLogin+''' 
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''



SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_ByRoleAndUnit_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_ByRoleAndUnit_Count]
   @Parameter varchar(MAX) ='',
   @RoleId varchar(MAX) ='',
   @UnitIdLogin varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
						  and P.Role_Id = '''+@RoleId+''' and P.Unit_Id = '''+@UnitIdLogin+''' 
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''



SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawai_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_AllPegawai_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
						  FROM [dbo].[Tbl_Pegawai] P
						  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
						  Where P.IsActive = 1 and U.IsActive = 1 and P.IsDeleted = 0 and U.IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''



SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Nama') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'U.Name') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNpp ELSE REPLACE(@QueryNpp,'OR','AND (') END)
				+ @QueryUnit
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_AllPegawaibyId]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Dropdown_AllPegawaibyId]
@Id int
as
begin
SELECT P.Id as id
	,CONCAT(U.Name,' - ', [Nama],' (',[Npp],')') as text
	 FROM [dbo].[Tbl_Pegawai] P
	LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
	Where P.Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Client]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Client]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Client]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Client_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Client_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Master_Client]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_ClientById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_ClientById]
@Id int
as
begin
SELECT N.Id as id
		,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Client N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_Jabatan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Dropdown_Jabatan]
as
begin
--SELECT N.Id as id
--		,N.Nama as text
--	FROM [dbo].Tbl_Master_Jabatan N
--	Where IsActive = 1 and N.IsDeleted =
SELECT N.Id as id
		,N.Nama as text
	FROM [dbo].Tbl_Master_Role N
	Where IsActive = 1 and N.IsDeleted = 00
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Jabatan_ById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Dropdown_Jabatan_ById]
  @Id nvarchar(max)
AS 
BEGIN
	SELECT Id as id
		   ,Nama as text
	FROM [dbo].Tbl_Master_Jabatan
	WHERE Id IN (select * from dbo.uf_SplitString(@Id,','))
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_JobPosition]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_JobPosition]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Job_Position]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_JobPosition_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_JobPosition_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								 FROM [dbo].[Tbl_Master_Job_Position]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	 @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_JobPositionById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_JobPositionById]
@Id int
as
begin
SELECT N.Id as id
		--,N.Nama as text
		,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Job_Position N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KategoriProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_KategoriProject]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Kategori_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KategoriProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_KategoriProject_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								 FROM [dbo].[Tbl_Master_Kategori_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	 @Query
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_KategoriProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_KategoriProjectById]
@Id int
as
begin
SELECT N.Id as id
		--,N.Nama as text
		,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Kategori_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KlasifikasiProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Dropdown_KlasifikasiProject]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Klasifikasi_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KlasifikasiProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Dropdown_KlasifikasiProject_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Master_Klasifikasi_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	 @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_KlasifikasiProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_KlasifikasiProjectById]
@Id int
as
begin
SELECT N.Id as id
		,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Klasifikasi_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KompleksitasProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_KompleksitasProject]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Kompleksitas_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_KompleksitasProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_KompleksitasProject_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Master_Kompleksitas_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
			
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_KompleksitasProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_KompleksitasProjectById]
@Id int
as
begin
SELECT N.Id as id
				,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]

	FROM [dbo].Tbl_Master_Kompleksitas_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_MasterRole]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_MasterRole]
as
begin
SELECT N.Id as id
		,N.Nama as text
	FROM [dbo].Tbl_Master_Role N
	Where IsActive = 1 and N.IsDeleted = 0
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_MasterRoleById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_MasterRoleById]
@Id int
as
begin
SELECT N.Id as id
		,N.Nama as text
	FROM [dbo].Tbl_Master_Role N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_MasterSistem]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_MasterSistem]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Sistem]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_MasterSistem_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_MasterSistem_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								 FROM [dbo].[Tbl_Master_Sistem]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_MasterSistemById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_MasterSistemById]
@Id int
as
begin
SELECT N.Id as id
				,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]

	FROM [dbo].Tbl_Master_Sistem N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_MasterTypeClient]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_MasterTypeClient]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Type_Client]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_MasterTypeClient_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_MasterTypeClient_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Master_Type_Client]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_MasterTypeClientById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_MasterTypeClientById]
@Id int
as
begin
SELECT N.Id as id
		,N.Nama as text
	FROM [dbo].Tbl_Master_Type_Client N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_dropdown_menu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_dropdown_menu]
as
begin
SELECT N.Id as id
		,CONCAT(N.Name,' - ',L.Name) as text
	FROM [dbo].[Navigation] N
	LEFT JOIN dbo.Tbl_Lookup L ON N.Type = L.Value and L.Type = 'TypeMenu'
	Where Visible = 1 and N.IsDeleted = 0
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Menu_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Dropdown_Menu_GetDataById]
@Id int
as
begin
select Id as id
	   ,Name as text
 from Navigation
Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Menu_GetDataRolesById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[SP_Dropdown_Menu_GetDataRolesById]
@Id nvarchar(max)
as
begin
select Id as id
	   ,Nama as text
 from Tbl_Master_Role
Where Id IN (select * from dbo.uf_SplitString(@Id,',')) 
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_Operator_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Dropdown_Operator_Count] 
	-- Add the parameters for the stored procedure here
	@Parameter varchar(MAX) ='',
	@PageNumber INT, 
	@RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Role_Pegawai] RP
								 left join Tbl_Pegawai P on RP.Pegawai_Id = P.Id 
								 Where RP.IsDeleted = 0 and (RP.Role_Id = 12 OR RP.Role_Id = 13 OR RP.Role_Id = 1)
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'P.Npp') 

SET @Query =   @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)

				EXEC(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_Operator_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_Dropdown_Operator_View] 
	-- Add the parameters for the stored procedure here
	@Parameter varchar(MAX) ='',
	@PageNumber INT, 
	@RowsPage INT
AS 
BEGIN

SELECT 
	ROW_NUMBER() OVER(ORDER BY Npp Asc) AS Number
	,RP.Pegawai_Id as id
	,P.Nama as [text]
	FROM [dbo].[Tbl_Role_Pegawai] RP
	left join Tbl_Pegawai P on RP.Pegawai_Id = P.Id 
	Where RP.IsDeleted = 0 and (RP.Role_Id = 12 or RP.Role_Id = 13 OR RP.Role_Id = 1)
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Project]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Project]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY [ProjectNo] Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN [ProjectNo] is not null THEN CONCAT([ProjectNo],'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNo varchar(MAX) = ''

SELECT @QueryNo = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'ProjectNo') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 


SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNo ELSE REPLACE(@QueryNo,'OR','AND (') END)
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Project_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Project_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								FROM [dbo].[Tbl_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = '',
	@QueryNo varchar(MAX) = ''

SELECT @QueryNo = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'ProjectNo') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 


SET @Query =	 @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNo ELSE REPLACE(@QueryNo,'OR','AND (') END)
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE @QueryNama + ')' END)
			
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_ProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_ProjectById]
@Id int
as
begin
SELECT N.Id as id
		,(CASE WHEN [ProjectNo] is not null THEN CONCAT([ProjectNo],' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_ProjectStatus]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_ProjectStatus]
   @Parameter varchar(MAX) ='',
   @AreaWakilPemimpinId nvarchar(max) = '',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Status_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''
IF(@AreaWakilPemimpinId != '')
BEGIN
	SET @Query = @Query + ' AND [AreaWakilPemimpinId] = '''+@AreaWakilPemimpinId+''' '
END

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_ProjectStatus_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_ProjectStatus_Count]
   @Parameter varchar(MAX) ='',
   @AreaWakilPemimpinId nvarchar(max) = ''

AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								FROM [dbo].[Tbl_Master_Status_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

IF(@AreaWakilPemimpinId != '')
BEGIN
	SET @Query = @Query + ' AND [AreaWakilPemimpinId] = '''+@AreaWakilPemimpinId+''' '
END

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_ProjectStatusById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_ProjectStatusById]
@Id int
as
begin
SELECT N.Id as id
		,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Status_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_SkorProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_SkorProject]
   @Parameter varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Skor Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Skor is not null THEN CONCAT(Skor,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Skor_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_SkorProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_SkorProject_Count]
   @Parameter varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								Count(*)
								 FROM [dbo].[Tbl_Master_Skor_Project]
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =  @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_SkorProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_SkorProjectById]
@Id int
as
begin
SELECT N.Id as id
		,N.Nama as text
	FROM [dbo].Tbl_Master_Skor_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_SubKategoriProject]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_SubKategoriProject]
   @Parameter varchar(MAX) ='',
   @KategoriProjectId varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Order_By Asc) AS Number
								 ,[Id] as id
								 ,(CASE WHEN Kode is not null THEN CONCAT(Kode,'' - '',Nama) ELSE Nama END) as [text]
								 FROM [dbo].[Tbl_Master_Sub_Kategori_Project] SK
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_SubKategoriProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_SubKategoriProject_Count]
   @Parameter varchar(MAX) ='',
   @KategoriProjectId varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Master_Sub_Kategori_Project] SK
								 Where IsActive = 1 and IsDeleted = 0
							  ',
	@QueryNama varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Nama') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_Dropdown_SubKategoriProjectById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_Dropdown_SubKategoriProjectById]
@Id int
as
begin
SELECT N.Id as id
				,(CASE WHEN Kode is not null THEN CONCAT(Kode,' - ',Nama) ELSE Nama END) as [text]
	FROM [dbo].Tbl_Master_Sub_Kategori_Project N
	Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_TypeDokumen]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_Dropdown_TypeDokumen]
as
SELECT [Id] as id
      ,[Nama] as text
  FROM [dbo].[Tbl_Master_Type_Dokumen]
  Where IsActive = 1 and IsDeleted = 0
GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Unit]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Unit]
   @Parameter varchar(MAX) ='',
   @TypeUnit varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Name Asc) AS Number
								 ,[Id] as id
								 ,Name as [text]
								 FROM [dbo].[Tbl_Unit]
								 Where IsActive = 1 and IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = ''

IF(@TypeUnit != '')
begin
	Set @Query = @Query + ' AND Type IN (select * from dbo.uf_SplitString('''+@Parameter+''','';''))'
end

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Unit_ByHirarki]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Unit_ByHirarki]
   @Parameter varchar(MAX) ='',
   @TypeUnit varchar(MAX) ='',
   @UnitIdLogin varchar(MAX) ='',
   @PageNumber INT, 
   @RowsPage INT
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 ROW_NUMBER() OVER(ORDER BY Name Asc) AS Number
								 ,[Id] as id
								 ,Name as [text]
								 FROM [dbo].[Tbl_Unit]
								 Where IsActive = 1 and IsDelete = 0 and Id in (select UnitID from dbo.uf_GetUnitHirarki('''+@UnitIdLogin+''')) 
							  ',
	@QueryNama varchar(MAX) = ''

IF(@TypeUnit != '')
begin
	Set @Query = @Query + ' AND Type IN (select * from dbo.uf_SplitString('''+@Parameter+''','';''))'
end

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Name') 

SET @Query =   'SELECT * FROM (' 
				+ @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				+ ') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Unit_ByHirarki_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Unit_ByHirarki_Count]
   @Parameter varchar(MAX) ='',
   @TypeUnit varchar(MAX) ='',
   @UnitIdLogin varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Unit]
								 Where IsActive = 1 and IsDelete = 0 and Id in (select UnitID from dbo.uf_GetUnitHirarki('''+@UnitIdLogin+''')) 
							  ',
	@QueryNama varchar(MAX) = ''

IF(@TypeUnit != '')
begin
	Set @Query = @Query + ' AND Type IN (select * from dbo.uf_SplitString('''+@Parameter+''','';''))'
end

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Name') 

SET @Query =	@Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Unit_ById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Unit_ById]
  @Id nvarchar(max)
AS 
BEGIN
	SELECT Id as id
		   ,Name as text
	FROM [dbo].Tbl_Unit
	WHERE Id IN (select * from dbo.uf_SplitString(@Id,','))
END 










GO
/****** Object:  StoredProcedure [dbo].[SP_Dropdown_Unit_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_Dropdown_Unit_Count]
   @Parameter varchar(MAX) ='',
   @TypeUnit varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
								 Count(*)
								 FROM [dbo].[Tbl_Unit]
								 Where IsActive = 1 and IsDelete = 0
							  ',
	@QueryNama varchar(MAX) = ''

IF(@TypeUnit != '')
begin
	Set @Query = @Query + ' AND Type IN (select * from dbo.uf_SplitString('''+@Parameter+''','';''))'
end

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGeneratorAllSearch(@Parameter, 'Name') 

SET @Query =   @Query
				+ (CASE WHEN @Parameter = '' THEN @QueryNama ELSE REPLACE(@QueryNama,'OR','AND (') + ')' END)
				
				EXEC(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_FAQ_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_FAQ_Count] 	
  @Judul varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select count(*)
								from [dbo].[Tbl_FAQ] FAQ
								LEFT JOIN dbo.Tbl_Pegawai PC ON FAQ.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON FAQ.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON FAQ.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on FAQ.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on FAQ.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (FAQ.IsDeleted = 0 OR FAQ.IsDeleted is null) ',
	@QueryJudul varchar(MAX) = ''
	
SELECT @QueryJudul = dbo.uf_LookupDynamicQueryGenerator(@Judul, 'FAQ.Judul')

SET @Query =	@Query 
				+ @QueryJudul
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_FAQ_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_FAQ_View] 	
  @Judul varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'FAQ.Id'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' FAQ.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'FAQ.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    FAQ.Id as Id,
									    FAQ.Judul,
										FAQ.Keterangan, 
									    FAQ.Order_By,	  
									    L2.Name as Status,
										FAQ.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](FAQ.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](FAQ.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [dbo].[Tbl_FAQ] FAQ
								LEFT JOIN dbo.Tbl_Pegawai PC ON FAQ.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON FAQ.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON FAQ.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on FAQ.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on FAQ.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (FAQ.IsDeleted = 0 OR FAQ.IsDeleted is null) ',
	@QueryJudul varchar(MAX) = ''
	
SELECT @QueryJudul = dbo.uf_LookupDynamicQueryGenerator(@Judul, 'FAQ.Judul')

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryJudul
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_getChildMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CReate proc [dbo].[sp_getChildMenu]
@Role_Id int,
@Url nvarchar(max)
as
begin
SELECT N.[Id]
      ,[Type]
      ,[Name]
      ,[Route]
      ,[Order]
      ,[Visible]
      ,[ParentNavigation_Id] as ParentNavigationId
      ,[IconClass]
	  ,(CASE WHEN [Route] = @Url THEN 1 ELSE 0 END) as Activated
	  ,dbo.uf_getExpandMenu(N.Id,@Url) as Expanded
  FROM [dbo].[NavigationAssignment] NA
  LEFT JOIN [dbo].[Navigation] N ON NA.Navigation_Id = N.Id 
  Where NA.IsActive = 1 AND NA.IsDelete != 1 and N.Visible = 1 and (N.IsDeleted is null or N.IsDeleted = 0)
  and Role_Id = @Role_Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetMenu]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_GetMenu]
@Role_Id int
as
begin
SELECT N.[Id]
      ,[Type]
      ,[Name]
      ,[Route]
      ,[Order]
      ,[Visible]
      ,[ParentNavigation_Id] as ParentNavigationId
      ,[IconClass]
	  ,[Visible]
	  ,(CASE WHEN [Route] = '../Inbox/Index' THEN 10 ELSE 0 END) as Jumlah
  FROM [dbo].[NavigationAssignment] NA
  LEFT JOIN [dbo].[Navigation] N ON NA.Navigation_Id = N.Id 
  Where NA.IsActive = 1 AND NA.IsDelete != 1 and N.Visible = 1 and (N.IsDeleted is null or N.IsDeleted = 0)
  and Role_Id = @Role_Id
  order by N.[Order] asc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_GetNoProject_ByDate]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_GetNoProject_ByDate]
@Bulan nvarchar(max) = '',
@Tahun nvarchar(max) = ''

as
declare @Jumlah int,
		@No nvarchar(max)

SELECT @Jumlah = Count(*) + 1
  FROM [dbo].[Tbl_Project]
  Where Month(CreatedTime) = @Bulan and YEAR(CreatedTime) = @Tahun


set @No = @Tahun+@Bulan+right('0000'+convert(varchar(20),@Jumlah),4)
select ISNULL(@No,'-') as Nomor
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTotalProject_ByDate]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
create proc [dbo].[sp_GetTotalProject_ByDate]
@Tanggal nvarchar(max) = ''
as
SELECT Count(*)
  FROM [dbo].[Tbl_Project]
  Where convert(date,CreatedTime) = @Tanggal
GO
/****** Object:  StoredProcedure [dbo].[SP_LoadDataUploadFile_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_LoadDataUploadFile_Count] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	DECLARE @Query VARCHAR(MAX) = 
				'select Count(*)
				FROM [db2TierTemplate].[dbo].[Tbl_UploadFile] UP
				LEFT JOIN [dbo].[Tbl_Pegawai] P ON P.Id = UP.CreatedById
				'

--	@QueryJabatanName varchar(MAX) = '',
--	@QueryUnitName varchar(MAX) = ''

--SELECT @QueryJabatanName = dbo.uf_LookupDynamicQueryGenerator(@JabatanName, 'MJ.Nama')
--SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name')

SET @Query= + @Query 
			--+ @QueryJabatanName
			--+ @QueryUnitName
				
			EXEC(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_LoadDataUploadFile_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_LoadDataUploadFile_View]
	-- Add the parameters for the stored procedure here
	@sortColumn varchar(100)='Id',
	@sortColumnDir varchar(10)='desc',
	@PageNumber INT, 
	@RowsPage INT
AS
BEGIN
	DECLARE 
	@SortField varchar(50),
	@Status varchar (100)

	SET @SortField = 	
			CASE @sortColumn 	
			WHEN 'JabatanName' THEN 'MJ.Nama'
			WHEN 'UnitName' THEN 'U.Name'				 	
			ELSE 'UP.CreatedTime' end; 	 

	DECLARE @Query VARCHAR(MAX) = 
				'SELECT ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
						   UP.[Id]  as id
						  ,UP.[NamaFile] as namaFile
						  ,UP.[DownloadPath] as downloadPath
						  ,P.[Nama] as createdBy
						  ,[dbo].[uf_IndonesianDateTime](UP.[CreatedTime]) as createdTime
					  FROM [db2TierTemplate].[dbo].[Tbl_UploadFile] UP
					  LEFT JOIN [dbo].[Tbl_Pegawai] P ON P.Id = UP.CreatedById
				'
--	@QueryJabatanName varchar(MAX) = '',
--	@QueryUnitName varchar(MAX) = ''

--SELECT @QueryJabatanName = dbo.uf_LookupDynamicQueryGenerator(@JabatanName, 'MJ.Nama')
--SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name')

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				--+ @QueryJabatanName
				--+ @QueryUnitName
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')
				'
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[Sp_LoadKewenanganJabatan_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_LoadKewenanganJabatan_Count] 
	-- Add the parameters for the stored procedure here
	@JabatanName varchar(MAX) ='',
	@UnitName varchar(MAX) =''
AS
BEGIN
	DECLARE @Query VARCHAR(MAX) = 
				'select Count(*)
				FROM Tbl_Mapping_Kewenangan_Jabatan MKJ
					  --LEFT JOIN Tbl_Master_Jabatan MJ ON MJ.Id = MKJ.JabatanId
					  --left join Tbl_Unit U ON U.Id = MKJ.UnitId
					  --LEFT JOIN Tbl_Lookup L ON MKJ.StatusJabatan = L.Value and L.Type = ''StatusRole''
					  where MKJ.IsDeleted != 1
					  GROUP BY MKJ.JabatanId
				',

	@QueryJabatanName varchar(MAX) = '',
	@QueryUnitName varchar(MAX) = ''

SELECT @QueryJabatanName = dbo.uf_LookupDynamicQueryGenerator(@JabatanName, 'MJ.Nama')
SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name')

SET @Query= + @Query 
			+ @QueryJabatanName
			+ @QueryUnitName
				
			EXEC(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[Sp_LoadKewenanganJabatan_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Sp_LoadKewenanganJabatan_View]
	-- Add the parameters for the stored procedure here
	@JabatanName varchar(MAX) ='',
	@UnitName varchar(MAX) ='',
	@sortColumn varchar(100)='Id',
	@sortColumnDir varchar(10)='desc',
	@PageNumber INT, 
	@RowsPage INT
AS
BEGIN
	DECLARE 
	@SortField varchar(50),
	@Status varchar (100)

	SET @SortField = 	
			CASE @sortColumn 	
			WHEN 'JabatanName' THEN 'MJ.Nama'
			WHEN 'UnitName' THEN 'U.Name'				 	
			ELSE 'MKJ.UpdatedTime' end; 	 

	DECLARE @Query VARCHAR(MAX) = 
				'SELECT ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
					  MKJ.JabatanId as Id,
					  MJ.Nama as [JabatanName],
					  MKJ.[UnitId] as Unit_Id,
					  U.[Name] as UnitName,
					  dbo.[uf_GetAllRoleJabatan](MKJ.JabatanId, MKJ.StatusJabatan, MKJ.UnitId) as RoleName,
					  MKJ.StatusJabatan as StatusJabatanId,
					  L.Name as [StatusJabatanName],
					  MKJ.IsActive as IsActive
					  FROM Tbl_Mapping_Kewenangan_Jabatan MKJ
					  LEFT JOIN Tbl_Master_Jabatan MJ ON MJ.Id = MKJ.JabatanId
					  left join Tbl_Unit U ON U.Id = MKJ.UnitId
					  LEFT JOIN Tbl_Lookup L ON MKJ.StatusJabatan = L.Value and L.Type = ''StatusRole''
					  where MKJ.IsDeleted != 1
					  GROUP BY MKJ.JabatanId, MJ.Nama, MKJ.[UnitId], MKJ.IsActive, U.[Name], L.Name, MKJ.StatusJabatan
					  ,MKJ.UpdatedTime
				',
	@QueryJabatanName varchar(MAX) = '',
	@QueryUnitName varchar(MAX) = ''

SELECT @QueryJabatanName = dbo.uf_LookupDynamicQueryGenerator(@JabatanName, 'MJ.Nama')
SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name')

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryJabatanName
				+ @QueryUnitName
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')
				'
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[SP_Login_GetData]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[SP_Login_GetData]
@Username nvarchar(max)
as
begin
declare @VirtualPathImages nvarchar(max)='',
		@ImagesDefault nvarchar(max)=''

select @VirtualPathImages = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathProfile'
select @ImagesDefault = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathImagesDefault'


SELECT convert(nvarchar(10),P.[Id]) as Pegawai_Id
      ,convert(nvarchar(10),P.[Unit_Id]) as Unit_Id
      ,[Id_JenisKelamin]
      ,Npp
      ,P.Nama as Nama_Pegawai
      ,[Alamat]
      ,P.[Email] as Email
      ,P.[Lastlogin]
      ,[Images]
      ,[Tanggal_Lahir]
      ,[No_HP]
	  ,P.IsActive
	  ,P.IsDeleted
	  ,U.Name as Nama_Unit
	  ,L.Name as Jenis_Kelamin
	  ,P.LDAPLogin
	  ,US.Password
	  ,convert(nvarchar(10),US.Id) as [User_Id]
	  ,convert(nvarchar(10),RP.Role_Id) as Role_Id
	  ,convert(nvarchar(10),RP.Unit_Id) as Role_Unit_Id
	  ,MR.Nama as Nama_Role
	  ,UR.Name as Role_Nama_Unit
	  ,L3.Name as Status_Role
	  --,ISNULL(CONCAT(@VirtualPathImages,Images),@ImagesDefault) as Images_User
	  ,(CASE WHEN P.Images is null THEN @ImagesDefault ELSE CONCAT(@VirtualPathImages,Images) END) as Images_User
	  ,convert(nvarchar(10),RP.Id) as User_Role_Id
  FROM [dbo].[Tbl_User] US 
  LEFT JOIN dbo.Tbl_Pegawai P ON US.Pegawai_Id = P.Id
  LEFT JOIN dbo.Tbl_Unit U ON P.Unit_Id = U.Id
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = P.Id_JenisKelamin and L.Type = 'JenisKelamin'
  LEFT JOIN dbo.Tbl_Role_Pegawai RP ON RP.Pegawai_Id = US.Pegawai_Id AND StatusRole = '1' AND (Rp.IsDeleted = 0 OR RP.IsDeleted is null)
  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = RP.Role_Id 
  LEFT JOIN dbo.Tbl_Lookup L3 ON L3.Value = RP.StatusRole and L3.Type = 'StatusRole'
  LEFT JOIN dbo.Tbl_Unit UR ON UR.Id = RP.Unit_Id AND RP.StatusRole = '1'
  Where Npp = @Username and P.IsDeleted = 0
  order by Rp.StatusRole, RP.Role_Id,RP.Unit_Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Login_GetDataRolePegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_Login_GetDataRolePegawai]
@Pegawai_Id int,
@Date nvarchar(50)
as
begin
SELECT RP.[Id] as Value
      ,CONCAT(MR.Nama,' - ',U.Name, ' ( ',L2.Name,' )') as Name
  FROM [dbo].[Tbl_Role_Pegawai] RP
  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = RP.Role_Id
  LEFT JOIN Tbl_Lookup L2 ON RP.StatusRole = L2.Value and L2.Type = 'StatusRole'
  LEFT JOIN dbo.Tbl_Unit U ON U.Id = RP.Unit_Id
  Where Pegawai_Id = @Pegawai_Id and RP.IsDeleted = 0 and (RP.StatusRole=1 OR (RP.StatusRole = 2 AND RP.DateStart <= @Date and RP.DateEnd >= @Date))
  Order by RP.StatusRole, RP.Role_Id asc, Rp.Unit_Id asc
END
GO
/****** Object:  StoredProcedure [dbo].[sp_MappingOperator_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MappingOperator_Count] 	
  @UnitName varchar(50) = '',
  @Npp varchar(50) ='',
  @PegawaiName varchar(50) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
							  FROM [dbo].[Tbl_Mapping_Operator] P
							  LEFT JOIN [dbo].[Tbl_Unit] U ON U.Id = P.UnitId
							  WHERE P.IsDeleted = 0 ',
	@QueryUnitName varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryPegawaiName varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name') 
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@Npp, 'P.Npp') 
SELECT @QueryPegawaiName = dbo.uf_LookupDynamicQueryGenerator(@PegawaiName, 'P.Nama') 

SET @Query =	 @Query 
				+ @QueryUnitName
				+ @QueryNpp
				+ @QueryPegawaiName
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MappingOperator_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MappingOperator_View] 	
  @UnitName varchar(50) = '',
  @Npp varchar(50) ='',
  @PegawaiName varchar(50) ='',
  @sortColumn varchar(100)='MKJ.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Npp' THEN 'P.Npp'
				WHEN 'Nama' THEN 'P.Nama'					 	
				ELSE 'P.Id' end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
									,P.[Id]
									  ,U.[Name] as UnitName
									  ,P.[Npp]
									  ,P.[Nama] as PegawaiName
									  ,L.Name as [Status]
									  ,P.[CreatedTime]
									  ,P.[UpdatedTime]
									  ,P.[DeletedTime]
									  ,P.[CreatedById]
									  ,P.[UpdatedById]
									  ,P.[DeletedById]
									  ,P.[IsActive]
									  ,P.[IsDeleted]
								  FROM [dbo].[Tbl_Mapping_Operator] P
								  LEFT JOIN [dbo].[Tbl_Unit] U ON U.Id = P.UnitId
								  LEFT JOIN (Select * from Tbl_Lookup where Type = ''IsActive'') L ON L.Value = P.IsActive
								  WHERE P.IsDeleted = 0 ',
	@QueryUnitName varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryPegawaiName varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryUnitName = dbo.uf_LookupDynamicQueryGenerator(@UnitName, 'U.Name') 
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@Npp, 'P.Npp') 
SELECT @QueryPegawaiName = dbo.uf_LookupDynamicQueryGenerator(@PegawaiName, 'P.Nama') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryUnitName
				+ @QueryNpp
				+ @QueryPegawaiName
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterBook_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MasterBook_Count] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
							FROM [dbo].[Tbl_Master_Book] MB
							WHERE MB.[IsActive] = 1 ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]')  

SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterBook_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MasterBook_View] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) ='',
  @sortColumn varchar(100)='MJ.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'MB.Id'
				WHEN 'Name' THEN 'MB.Name'
				WHEN 'Author' THEN 'MB.Author'
				WHEN 'RealeaseDate' THEN 'MB.RealeaseDate'
				WHEN 'IsBestSeller' THEN 'MB.IsBestSeller'
				WHEN 'Picture' THEN 'MB.Picture'			
				ELSE @sortColumn end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
							  ,MB.[Id]
							  ,MB.[Name]
							  ,MB.[Author]
							  ,[dbo].[uf_IndonesianDate](MB.[RealeaseDate]) as RealeaseDate
							  ,MB.[IsBestSeller]
							  ,MB.[CreatedTime]
							  ,MB.[UpdatedTime]
							  ,MB.[IsActive]
							  ,MB.[Picture]
							  ,MB.[IsBorrowed]
							  ,MB.[Description]
						  FROM [dbo].[Tbl_Master_Book] MB
						  WHERE MB.[IsActive] = 1',

	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MB.[Name]') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MB.[Author]') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterClient_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterClient_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Client] MSP
								LEFT JOIN dbo.Tbl_Master_Type_Client MTC ON MSP.TypeClientId = MTC.Id
								
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =  @Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterClient_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterClient_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'TypeClient' THEN 'MTC.Nama'

				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MTC.Nama as TypeClient,
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Client] MSP
								LEFT JOIN dbo.Tbl_Master_Type_Client MTC ON MSP.TypeClientId = MTC.Id
								LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataJabatan_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MasterDataJabatan_Count] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
							 FROM [dbo].[Tbl_Master_Jabatan] MJ
								LEFT JOIN dbo.Tbl_Pegawai PC ON MJ.CreatedBy_Id = PC.Id
								LEFT JOIN dbo.Tbl_Pegawai PU ON MJ.UpdatedBy_Id = PU.Id
								LEFT JOIN dbo.Tbl_Pegawai PD ON MJ.DeletedBy_Id = PD.Id
								left join Tbl_Lookup L2 on MJ.IsActive = L2.Value and L2.Type = ''IsActive''
								left join Tbl_Lookup L3 on MJ.IsDeleted = L3.Value and L3.Type = ''IsDelete''
							  WHERE MJ.IsDeleted = 0 ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MJ.Kode') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MJ.Nama') 

SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataJabatan_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_MasterDataJabatan_View] 	
  @Kode varchar(20) ='',
  @Nama varchar(20) ='',
  @sortColumn varchar(100)='MJ.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'MJ.Id'
				WHEN 'orderBy' THEN 'MJ.Order_By'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Kode' THEN 'MJ.Kode'
				WHEN 'Nama' THEN 'MJ.Nama'
				WHEN 'GradeAwal' THEN 'MJ.GradeAwal'
				WHEN 'GradeAkhir' THEN 'MJ.GradeAkhir'				 	
				WHEN 'Keterangan' THEN 'MJ.Keterangan' 					 	
				ELSE @sortColumn end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
								,MJ.[Id]
								,MJ.[Kode]
								,MJ.[Nama]
								,MJ.[GradeAwal]
								,MJ.[GradeAkhir]
								,MJ.[Keterangan]
								,MJ.[Order_By] as OrderBy
								,dbo.[uf_ShortIndonesianDateTime](MJ.[CreatedTime]) as CreatedTime
								,dbo.[uf_ShortIndonesianDateTime](MJ.[UpdatedTime]) as UpdatedTime
								,PC.Nama as CreatedBy
								,PU.Nama as UpdatedBy
								,L2.Name as Status,
								MJ.IsActive as IsActive,
								L2.Name as IsDelete
								FROM [dbo].[Tbl_Master_Jabatan] MJ
								LEFT JOIN dbo.Tbl_Pegawai PC ON MJ.CreatedBy_Id = PC.Id
								LEFT JOIN dbo.Tbl_Pegawai PU ON MJ.UpdatedBy_Id = PU.Id
								LEFT JOIN dbo.Tbl_Pegawai PD ON MJ.DeletedBy_Id = PD.Id
								left join Tbl_Lookup L2 on MJ.IsActive = L2.Value and L2.Type = ''IsActive''
								left join Tbl_Lookup L3 on MJ.IsDeleted = L3.Value and L3.Type = ''IsDelete''
							  WHERE MJ.IsDeleted = 0 ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MJ.Kode') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MJ.Nama') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataPengguna_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterDataPengguna_Count] 	
  @RoleId varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @Npp varchar(MAX) ='',
  @Unit varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								  FROM [dbo].[Tbl_User] U
								  LEFT JOIN dbo.Tbl_Pegawai P ON U.Pegawai_Id = P.Id
								  LEFT JOIN dbo.Tbl_Unit UN ON UN.Id = P.Unit_Id
								  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = P.IsActive and L.Type = ''IsActive''
								  LEFT JOIN dbo.Tbl_Pegawai PC ON P.CreatedBy_Id = PC.Id
								  LEFT JOIN dbo.Tbl_Pegawai PU ON P.UpdatedBy_Id = PU.Id 
								  WHERE (P.IsDeleted = 0 OR P.IsDeleted is null) ',

	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'P.Nama')
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@Npp, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGenerator(@Unit, 'UN.Name') 

SET @Query =	@Query 
				+ @QueryNama
				+ @QueryNpp
				+ @QueryUnit
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataPengguna_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_MasterDataPengguna_GetDataById]
@Id int
as
begin
select U.[Id]
	,P.Nama
	,P.Npp
	,P.Email
	,UN.Name as Unit
	,P.Unit_Id as UnitId
	,P.Role_Id as RoleId
	,dbo.uf_ShortIndonesianDateTime(U.[LastLogin]) as LastLogin
	,P.IsActive
	,P.LDAPLogin as IsDLAP
FROM [dbo].[Tbl_User] U
LEFT JOIN dbo.Tbl_Pegawai P ON U.Pegawai_Id = P.Id
LEFT JOIN dbo.Tbl_Unit UN ON UN.Id = P.Unit_Id
Where P.Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataPengguna_GetRole]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_MasterDataPengguna_GetRole]
@PegawaiId int,
@Date nvarchar(max)= ''
as
SELECT RP.[Id]
      ,[Pegawai_Id]
      ,[Role_Id]
	  ,MR.Nama as [Role]
      ,[Unit_Id]
	  ,U.Name as Unit
      ,CONCAT(dbo.uf_IndonesianDateDDMMYYYY([DateStart]),' - ',dbo.uf_IndonesianDateDDMMYYYY([DateEnd])) as Periode
  FROM [dbo].[Tbl_Role_Pegawai] RP
  LEFT JOIN dbo.Tbl_Unit U ON RP.Unit_Id = U.Id
  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = RP.Role_Id
  Where RP.Pegawai_Id = @PegawaiId and RP.DateEnd >= @Date and RP.IsDeleted = 0
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataPengguna_GetRole_ById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_MasterDataPengguna_GetRole_ById]
@Id int
as
SELECT RP.[Id]
      ,[Pegawai_Id] as PegawaiId
      ,[Role_Id] as RoleId
	  ,MR.Nama as [Role]
      ,[Unit_Id] as UnitId
	  ,Unit_Id as UnitIdTambahan
	  ,U.Name as Unit
      ,CONCAT(dbo.uf_IndonesianDateDDMMYYYY([DateStart]),' - ',dbo.uf_IndonesianDateDDMMYYYY([DateEnd])) as Periode
  FROM [dbo].[Tbl_Role_Pegawai] RP
  LEFT JOIN dbo.Tbl_Unit U ON RP.Unit_Id = U.Id
  LEFT JOIN dbo.Tbl_Master_Role MR ON MR.Id = RP.Role_Id
  Where RP.Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataPengguna_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterDataPengguna_View] 	
  @RoleId varchar(MAX) ='',
  @UnitId varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @Npp varchar(MAX) ='',
  @Unit varchar(MAX) ='',

  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'P.Nama'
				WHEN 'Unit' THEN 'UN.Name'
				WHEN 'LastLogin' THEN ' U.LastLogin'
				WHEN 'Status' THEN ' L.Name'
				WHEN 'CreatedTime' THEN ' P.Created_Date'
				WHEN 'UpdatedTime' THEN ' U.Updated_Date'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'L.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'U.Id' end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									   P.[Id]
									  ,P.Nama
									  ,P.Npp
									  ,P.Email
									  ,P.LDAPLogin as IsLDAP
									  ,UN.Name as Unit
									  ,dbo.uf_ShortIndonesianDateTime(U.[LastLogin]) as LastLogin
									  ,P.IsActive
									  ,ISNULL(L.Name,''Aktif'') as [Status]
									  ,dbo.[uf_GetAllRolePegawai](P.Id) as [Role]
									  ,dbo.[uf_ShortIndonesianDateTime](P.Created_Date) as CreatedTime
									  ,dbo.[uf_ShortIndonesianDateTime](P.Updated_Date) as UpdatedTime
									  ,PC.Nama as CreatedBy
									  ,PU.Nama as UpdatedBy
								  FROM [dbo].[Tbl_User] U
								  LEFT JOIN dbo.Tbl_Pegawai P ON U.Pegawai_Id = P.Id
								  LEFT JOIN dbo.Tbl_Unit UN ON UN.Id = P.Unit_Id
								  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = P.IsActive and L.Type = ''IsActive''
								  LEFT JOIN dbo.Tbl_Pegawai PC ON P.CreatedBy_Id = PC.Id
								  LEFT JOIN dbo.Tbl_Pegawai PU ON P.UpdatedBy_Id = PU.Id 
								  WHERE (P.IsDeleted = 0 OR P.IsDeleted is null) ',
	@QueryNama varchar(MAX) = '',
	@QueryNpp varchar(MAX) = '',
	@QueryUnit varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'P.Nama')
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@Npp, 'P.Npp') 
SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGenerator(@Unit, 'UN.Name') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryNama
				+ @QueryNpp
				+ @QueryUnit
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataUnit_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterDataUnit_Count] 	
  @TypeUnit varchar(MAX) ='',
  @KodeUnit varchar(20) ='',
  @NamaUnit varchar(20) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'SELECT 
									Count(*)
							  FROM [dbo].[Tbl_Unit] U
							  LEFT JOIN dbo.Tbl_Pegawai PC ON U.CreatedBy_Id = PC.Id
							  LEFT JOIN dbo.Tbl_Pegawai PU ON U.UpdatedBy_Id = PU.Id
							  LEFT JOIN [dbo].[Tbl_Unit] P ON U.Parent_Id = P.Id
							  LEFT JOIN (Select * from Tbl_Lookup where Type = ''TypeUnit'') L ON L.Value = U.Type
							  LEFT JOIN (Select * from Tbl_Lookup where Type = ''IsActive'') L2 ON L2.Value = U.IsActive
							  WHERE U.IsDelete = 0 ',
	@QueryTypeUnit varchar(MAX) = '',
	@QueryKodeUnit varchar(MAX) = '',
	@QueryNamaUnit varchar(MAX) = ''
	

SELECT @QueryTypeUnit = dbo.uf_LookupDynamicQueryGenerator(@TypeUnit, 'U.Type')
SELECT @QueryKodeUnit = dbo.uf_LookupDynamicQueryGenerator(@KodeUnit, 'U.Code') 
SELECT @QueryNamaUnit = dbo.uf_LookupDynamicQueryGenerator(@NamaUnit, 'U.Name') 

SET @Query =	 @Query 
				+ @QueryTypeUnit
				+ @QueryKodeUnit
				+ @QueryNamaUnit
				
				exec(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_MasterDataUnit_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterDataUnit_View] 	
  @TypeUnit varchar(MAX) ='',
  @KodeUnit varchar(20) ='',
  @NamaUnit varchar(20) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Type_Unit_Name' THEN 'L.Name'
				WHEN 'Kode_Unit' THEN 'Code'
				WHEN 'Nama_Unit' THEN 'U.Name'
				WHEN 'Parent_Name' THEN 'P.Name'				 	
				WHEN 'No_Telepon' THEN 'U.Telepon'	
				WHEN 'Alamat' THEN 'U.Alamat'	 					 	
				ELSE 'U.Id' end; 	 	
 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'SELECT 
									ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
								  ,U.[Id]
								  ,U.[Parent_Id] as Parent_Id
								  ,ISNULL(P.Name,''-'') as Parent_Name
								  ,L.Name as Type_Unit_Name
								  ,ISNULL(U.Address,''-'') as Alamat
								  ,ISNULL(U.Code,''-'') as Kode_Unit
								  ,U.Name as Nama_Unit
								  ,L2.Name as Status
								  ,U.IsActive as IsActive
								  ,U.Telepon as No_Telepon
								  ,U.Email
								  ,U.ShortName as Short_Name
								  ,dbo.[uf_ShortIndonesianDateTime](U.[CreatedTime]) as CreatedTime
								  ,dbo.[uf_ShortIndonesianDateTime](U.[UpdatedTime]) as UpdatedTime
								  ,PC.Nama as CreatedBy
								  ,PU.Nama as UpdatedBy
							  FROM [dbo].[Tbl_Unit] U
							  LEFT JOIN dbo.Tbl_Pegawai PC ON U.CreatedBy_Id = PC.Id
							  LEFT JOIN dbo.Tbl_Pegawai PU ON U.UpdatedBy_Id = PU.Id
							  LEFT JOIN [dbo].[Tbl_Unit] P ON U.Parent_Id = P.Id
							  LEFT JOIN (Select * from Tbl_Lookup where Type = ''TypeUnit'') L ON L.Value = U.Type
							  LEFT JOIN (Select * from Tbl_Lookup where Type = ''IsActive'') L2 ON L2.Value = U.IsActive
							  WHERE U.IsDelete = 0 ',
	@QueryTypeUnit varchar(MAX) = '',
	@QueryKodeUnit varchar(MAX) = '',
	@QueryNamaUnit varchar(MAX) = ''
	

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryTypeUnit = dbo.uf_LookupDynamicQueryGenerator(@TypeUnit, 'U.Type')
SELECT @QueryKodeUnit = dbo.uf_LookupDynamicQueryGenerator(@KodeUnit, 'U.Code') 
SELECT @QueryNamaUnit = dbo.uf_LookupDynamicQueryGenerator(@NamaUnit, 'U.Name') 


--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryTypeUnit
				+ @QueryKodeUnit
				+ @QueryNamaUnit
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya qurey seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				exec(@Query) 
END 










GO
/****** Object:  StoredProcedure [dbo].[sp_MasterHoliday_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterHoliday_Count] 	
  @Nama varchar(MAX) ='',
  @TanggalAwal varchar(MAX) ='',
  @TanggalAkhir varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Holiday] HP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON HP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON HP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON HP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on HP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on HP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (HP.IsDeleted = 0 OR HP.IsDeleted is null) ',
	@QueryTanggal varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'HP.Tanggal')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'HP.Nama') 
IF(@TanggalAwal != '')
BEGIN
	SELECT @Query = @Query + ' AND HP.Tanggal >= '''+@TanggalAwal+''' AND HP.Tanggal <= '''+@TanggalAkhir+''' '
END


SET @Query =	@Query 
				--+ @QueryTanggal
				+ @QueryNama
			
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterHoliday_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_MasterHoliday_GetDataById]
@Id int
as
begin
SELECT [Id]
      ,dbo.uf_IndonesianDateDDMMYYYY(Tanggal) as Tanggal
      ,[Nama]
      ,[Keterangan]
  FROM [dbo].[Tbl_Holiday]
  Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_MasterHoliday_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterHoliday_View] 	
  @Nama varchar(MAX) ='',
  @TanggalAwal varchar(MAX) ='',
  @TanggalAkhir varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'HP.Id'
				WHEN 'Tanggal' THEN 'HP.Tanggal'
				WHEN 'Nama' THEN 'HP.Nama'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' HP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'HP.IsActive'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    HP.[Id]
									  ,dbo.uf_IndonesianDate(HP.Tanggal) as Tanggal
									  ,HP.[Nama]
									  ,HP.[Keterangan]	  
									    ,L2.Name as Status,
										HP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](HP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](HP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Holiday] HP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON HP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON HP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON HP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on HP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on HP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (HP.IsDeleted = 0 OR HP.IsDeleted is null) ',
	@QueryTanggal varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'HP.Tanggal')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'HP.Nama') 
IF(@TanggalAwal != '')
BEGIN
	SELECT @Query = @Query + ' AND HP.Tanggal >= '''+@TanggalAwal+''' AND HP.Tanggal <= '''+@TanggalAkhir+''' '
END


SET @Query = 'SELECT * FROM (' 
				+ @Query 
				--+ @QueryTanggal
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterJenisProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterJenisProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[Tbl_Master_Jenis_Project] MTC
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MTC.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MTC.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MTC.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MTC.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MTC.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MTC.IsDeleted = 0 OR MTC.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MTC.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MTC.Nama') 

SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterJenisProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterJenisProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MTC.Nama'
				WHEN 'Kode' THEN 'MTC.Kode'
				WHEN 'Keterangan' THEN 'MTC.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MTC.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MTC.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MTC.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MTC.Id as Id,
									    MTC.Nama,
									    MTC.Kode, 
										MTC.Keterangan, 
									    MTC.Order_By,	  
									    L2.Name as Status,
										MTC.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MTC.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MTC.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [dbo].[Tbl_Master_Jenis_Project] MTC
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MTC.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MTC.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MTC.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MTC.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MTC.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MTC.IsDeleted = 0 OR MTC.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MTC.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MTC.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterJobPosition_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterJobPosition_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Job_Position] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =  @Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterJobPosition_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterJobPosition_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Job_Position] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKategoriProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKategoriProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Kategori_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKategoriProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKategoriProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Kategori_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKlasifikasiProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKlasifikasiProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Klasifikasi_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	 @Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKlasifikasiProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKlasifikasiProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Klasifikasi_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKompleksitasProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKompleksitasProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Kompleksitas_Project] MKP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MKP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MKP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MKP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MKP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MKP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MKP.IsDeleted = 0 OR MKP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MKP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MKP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
			
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterKompleksitasProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterKompleksitasProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MKP.Nama'
				WHEN 'Kode' THEN 'MKP.Kode'
				WHEN 'Keterangan' THEN 'MKP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'KategoriKompleksitas' THEN 'LK.Name'
				WHEN 'Order_By' THEN ' MKP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MKP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MKP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MKP.Id as Id,
									    MKP.Nama,
									    MKP.Kode, 
										MKP.Keterangan, 
									    MKP.Order_By,
										LK.Name as KategoriKompleksitas, 
									    L2.Name as Status,
										MKP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MKP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MKP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Kompleksitas_Project] MKP
									left join Tbl_Lookup LK on MKP.KategoriKompleksitasId = LK.Value and LK.Type = ''KategoriKompleksitas'' and LK.IsActive = 1 and LK.IsDeleted = 0
									LEFT JOIN dbo.Tbl_Pegawai PC ON MKP.CreatedBy_Id = PC.Id
									LEFT JOIN dbo.Tbl_Pegawai PU ON MKP.UpdatedBy_Id = PU.Id
									LEFT JOIN dbo.Tbl_Pegawai PD ON MKP.DeletedBy_Id = PD.Id
									left join Tbl_Lookup L2 on MKP.IsActive = L2.Value and L2.Type = ''IsActive'' and L2.IsActive = 1 and L2.IsDeleted = 0
									left join Tbl_Lookup L3 on MKP.IsDeleted = L3.Value and L3.Type = ''IsDelete'' and L3.IsActive = 1 and L3.IsDeleted = 0
									Where (MKP.IsDeleted = 0 OR MKP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MKP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MKP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterPegawaiKelolaan_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterPegawaiKelolaan_Count] 	
  @NamaMember varchar(MAX) ='',
  @NppMember varchar(MAX) ='',
  @RoleId varchar(MAX) ='',
  @PegawaiLoginId varchar(MAX) =''
AS 
BEGIN
	

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								FROM [dbo].[Tbl_Pegawai_Kelolaan] PK
								  LEFT JOIN dbo.Tbl_Pegawai PGA ON PK.AtasanId = PGA.Id
								  LEFT JOIN dbo.Tbl_Pegawai PGM ON PK.PegawaiId = PGM.Id
								  LEFT JOIN dbo.Tbl_Unit UNA ON PK.UnitAtasanId = UNA.Id
								  LEFT JOIN dbo.Tbl_Unit UNM ON PK.UnitPegawaiId = UNM.Id
										 LEFT JOIN dbo.Tbl_Pegawai PC ON PK.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON PK.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON PK.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on PK.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on PK.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (PK.IsDeleted = 0 OR PK.IsDeleted is null) ',
	@QueryNpp varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

IF(@RoleId = '25')
BEGIN
	SET @Query = @Query + ' AND PK.AtasanId = '''+@PegawaiLoginId+''' '
END
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@NppMember, 'PGM.Npp')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@NamaMember, 'PGM.Nama') 

SET @Query =	@Query 
				+ @QueryNpp
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterPegawaiKelolaan_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterPegawaiKelolaan_View] 	
  @NamaMember varchar(MAX) ='',
  @NppMember varchar(MAX) ='',
  @RoleId varchar(MAX) ='',
  @PegawaiLoginId varchar(MAX) ='',
  @sortColumn varchar(100)='PK.Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

declare @VirtualPathImages nvarchar(max)='',
		@ImagesDefault nvarchar(max)=''

select @VirtualPathImages = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathProfile'
select @ImagesDefault = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathImagesDefault'

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'PK.Id'
				WHEN 'UnitAtasan' THEN 'UNA.Name'
				WHEN 'UnitMember' THEN 'UNM.Name'
				WHEN 'NamaAtasan' THEN 'PGA.Nama'
				WHEN 'NppAtasan' THEN 'PGA.Npp'
				WHEN 'NamaMember' THEN ' PGM.Nama'
				WHEN 'NppMember' THEN 'PGM.Npp' 
				WHEN 'Status' THEN 'L2.name' 
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    PK.[Id]
									  ,[AtasanId]
									  ,[PegawaiId]
									  ,[UnitAtasanId]
									  ,[UnitPegawaiId]
									  ,UNA.Name as UnitAtasan
									  ,UNM.Name as UnitMember
									  ,PGA.Nama as NamaAtasan
									  ,PGA.Npp as NppAtasan
									  ,PGM.Nama as NamaMember
									  ,PGM.Npp as NppMember
									   ,(CASE WHEN PGM.Images is null THEN '''+@ImagesDefault+''' ELSE CONCAT('''+@VirtualPathImages+''',PGM.Images) END) as Images
										,dbo.[uf_ShortIndonesianDateTime](PK.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](PK.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
									,L2.name as Status
								FROM [dbo].[Tbl_Pegawai_Kelolaan] PK
								  LEFT JOIN dbo.Tbl_Pegawai PGA ON PK.AtasanId = PGA.Id
								  LEFT JOIN dbo.Tbl_Pegawai PGM ON PK.PegawaiId = PGM.Id
								  LEFT JOIN dbo.Tbl_Unit UNA ON PK.UnitAtasanId = UNA.Id
								  LEFT JOIN dbo.Tbl_Unit UNM ON PK.UnitPegawaiId = UNM.Id
										 LEFT JOIN dbo.Tbl_Pegawai PC ON PK.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON PK.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON PK.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on PK.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on PK.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (PK.IsDeleted = 0 OR PK.IsDeleted is null) ',
	@QueryNpp varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

IF(@RoleId = '25')
BEGIN
	SET @Query = @Query + ' AND PK.AtasanId = '''+@PegawaiLoginId+''' '
END
SELECT @QueryNpp = dbo.uf_LookupDynamicQueryGenerator(@NppMember, 'PGM.Npp')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@NamaMember, 'PGM.Nama') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryNpp
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSistem_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSistem_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Sistem] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSistem_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSistem_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Sistem] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSkorProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSkorProject_Count] 	
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Skor_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = @Query 
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSkorProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSkorProject_View] 	
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Skor' THEN 'MSP.Skor'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Skor, 
										MSP.Keterangan, 
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Skor_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterStatusProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterStatusProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Status_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterStatusProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterStatusProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'AreaKelolaan' THEN 'LA.Name'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
										LA.Name as AreaKelolaan,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,
										MSP.Presentase as Presentase,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Status_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup LA on MSP.AreaWakilPemimpinId = LA.Value and LA.Type = ''AreaWakilPemimpinProjectKelolaan'' and LA.IsActive = 1 and LA.IsDeleted = 0
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSubKategoriProject_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSubKategoriProject_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Sub_Kategori_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSubKategoriProject_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSubKategoriProject_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Sub_Kategori_Project] MSP
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSubSistem_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSubSistem_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @KodeSistem varchar(MAX) ='',
  @NamaSistem varchar(MAX) =''
AS 
BEGIN
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Sub_Sistem] MSP
								LEFT JOIN [Tbl_Master_Sistem] MS ON MSP.[MasterSistemId] = MS.Id
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = '',
	@QueryNamaSistem varchar(MAX) = '',
	@QueryKodeSistem varchar(MAX) = ''


SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 
SELECT @QueryNamaSistem = dbo.uf_LookupDynamicQueryGenerator(@NamaSistem, 'MS.Nama') 
SELECT @QueryKodeSistem = dbo.uf_LookupDynamicQueryGenerator(@KodeSistem, 'MS.Kode') 

SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				+ @QueryNamaSistem
				+ @QueryNamaSistem
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterSubSistem_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterSubSistem_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @KodeSistem varchar(MAX) ='',
  @NamaSistem varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MSP.Nama'
				WHEN 'Kode' THEN 'MSP.Kode'
				WHEN 'Keterangan' THEN 'MSP.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MSP.Id' end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MSP.Id as Id,
										MS.Nama as MasterSistem,
									    MSP.Nama,
									    MSP.Kode, 
										MSP.Keterangan, 
									    MSP.Order_By,	  
									    L2.Name as Status,
										MSP.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MSP.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MSP.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Sub_Sistem] MSP
								LEFT JOIN [Tbl_Master_Sistem] MS ON MSP.[MasterSistemId] = MS.Id
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MSP.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MSP.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MSP.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MSP.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MSP.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MSP.IsDeleted = 0 OR MSP.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = '',
	@QueryNamaSistem varchar(MAX) = '',
	@QueryKodeSistem varchar(MAX) = ''


SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MSP.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MSP.Nama') 
SELECT @QueryNamaSistem = dbo.uf_LookupDynamicQueryGenerator(@NamaSistem, 'MS.Nama') 
SELECT @QueryKodeSistem = dbo.uf_LookupDynamicQueryGenerator(@KodeSistem, 'MS.Kode') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+ @QueryNamaSistem
				+ @QueryNamaSistem
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterTypeClient_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterTypeClient_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [Tbl_Master_Type_Client] MTC
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MTC.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MTC.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MTC.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MTC.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MTC.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MTC.IsDeleted = 0 OR MTC.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MTC.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MTC.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryKode
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MasterTypeClient_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MasterTypeClient_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MTC.Nama'
				WHEN 'Kode' THEN 'MTC.Kode'
				WHEN 'Keterangan' THEN 'MTC.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MTC.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MTC.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MTC.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MTC.Id as Id,
									    MTC.Nama,
									    MTC.Kode, 
										MTC.Keterangan, 
									    MTC.Order_By,	  
									    L2.Name as Status,
										MTC.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](MTC.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MTC.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [Tbl_Master_Type_Client] MTC
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MTC.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MTC.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MTC.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MTC.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MTC.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (MTC.IsDeleted = 0 OR MTC.IsDeleted is null) ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'MTC.Kode')
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MTC.Nama') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MenuTes_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MenuTes_Count] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								  FROM [dbo].[Tbl_Tes]
								  Where IsDeleted = 0 ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'HP.Tanggal')
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'Kode') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'Nama') 


SET @Query =  @Query 
				+ @QueryKode 
				+ @QueryNama
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_MenuTes_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_MenuTes_View] 	
  @Kode varchar(MAX) ='',
  @Nama varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'Id'
				WHEN 'KodeTes' THEN 'Kode'
				WHEN 'NamaTes' THEN 'Nama'			 	
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    [Id]
									  ,[Kode] as KodeTes
									  ,[Nama] as NamaTes
									  ,[Keterangan] as KeteranganTes
								  FROM [dbo].[Tbl_Tes]
								  Where IsDeleted = 0 ',
	@QueryKode varchar(MAX) = '',
	@QueryNama varchar(MAX) = ''

--SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'HP.Tanggal')
SELECT @QueryKode = dbo.uf_LookupDynamicQueryGenerator(@Kode, 'Kode') 
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'Nama') 


SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryKode 
				+ @QueryNama
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PendingTask_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PendingTask_View] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'PM.Id'
				WHEN 'ProjectNo' THEN 'P.ProjectNo'
				WHEN 'Nama' THEN 'P.Nama'
				WHEN 'NamaPegawai' THEN 'PG.Nama'
				WHEN 'NamaUnit' THEN 'UN.Name'
				WHEN 'TargetPenyelesaian' THEN 'PM.StartDate'
				WHEN 'JobDesc' THEN 'PM.Keterangan'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' MSP.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MSP.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE @sortColumn end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									   PM.[Id]
									  ,P.ProjectNo
									  ,P.Nama
									  ,MJP.Nama as JobPosition
									  ,CONCAT(dbo.uf_IndonesianDate(PM.StartDate),'' - '',dbo.uf_IndonesianDate(PM.EndDate)) as TargetPenyelesaian
									  ,PG.Nama as NamaPegawai
									  ,PG.Npp
									  ,UN.Name as NamaUnit
									  --,[PegawaiId]
									  ,PM.[Keterangan] as JobDesc
									  ,[StatusProgressId]
									  ,[CatatanPegawai]
									  ,[SendAsTask]
									  ,DATEDIFF(DAY, getdate(), PM.EndDate) as TotalHari
								  FROM [dbo].[Tbl_Project_Member] PM 
								  LEFT JOIN dbo.Tbl_Project P ON PM.ProjectId = P.Id
								  LEFT JOIN dbo.Tbl_Master_Job_Position MJP ON MJP.Id = PM.JobPositionId
								  LEFT JOIN dbo.Tbl_Pegawai PG ON PG.Id = PM.PegawaiId
								  LEFT JOIN dbo.Tbl_Unit UN ON UN.Id = PM.UnitPegawaiId 
								  WHERE PM.IsActive = 1 and PM.IsDeleted = 0 ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'P.ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'P.Nama') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
	
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanLookup_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanLookup_Count] 	
  @Type varchar(MAX) ='',
  @Name varchar(MAX) =''
AS 
BEGIN


--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from Tbl_Lookup L
										 LEFT JOIN dbo.Tbl_Pegawai PC ON L.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON L.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON L.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on L.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on L.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (L.IsDeleted = 0 OR L.IsDeleted is null) ',
	@QueryType varchar(MAX) = '',
	@QueryName varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryType = dbo.uf_LookupDynamicQueryGenerator(@Type, 'L.Type')
SELECT @QueryName = dbo.uf_LookupDynamicQueryGenerator(@Name, 'L.Name') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query =	@Query 
				+ @QueryType
				+ @QueryName

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanLookup_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanLookup_View] 	
  @Type varchar(MAX) ='',
  @Name varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Type' THEN 'L.Type'
				WHEN 'Name' THEN 'L.Name'
				WHEN 'Value' THEN ' L.Value'
				WHEN 'Order_By' THEN ' L.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'L.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'L.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    L.ID as Id,
									    L.Type, 
									    L.Name, 
									    L.Value, 
									    L.Order_By,	  
									    L2.Name as Status,
										L.IsActive as IsActive,
									    L2.Name as IsDelete
										,dbo.[uf_ShortIndonesianDateTime](L.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](L.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from Tbl_Lookup L
										 LEFT JOIN dbo.Tbl_Pegawai PC ON L.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON L.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON L.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on L.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on L.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (L.IsDeleted = 0 OR L.IsDeleted is null) ',
	@QueryType varchar(MAX) = '',
	@QueryName varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryType = dbo.uf_LookupDynamicQueryGenerator(@Type, 'L.Type')
SELECT @QueryName = dbo.uf_LookupDynamicQueryGenerator(@Name, 'L.Name') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryType
				+ @QueryName
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanMasterRole_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanMasterRole_Count] 	
  @Nama varchar(MAX) ='',
  @Keterangan varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[Tbl_Master_Role] MR
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MR.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MR.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MR.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MR.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MR.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where MR.IsDeleted = 0 ',
	@QueryNama varchar(MAX) = '',
	@QueryKeterangan varchar(MAX) = ''

SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MR.Nama')
SELECT @QueryKeterangan = dbo.uf_LookupDynamicQueryGenerator(@Keterangan, 'MR.Keterangan') 

SET @Query =   @Query 
				+ @QueryNama
				+ @QueryKeterangan
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanMasterRole_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanMasterRole_View] 	
  @Nama varchar(MAX) ='',
  @Keterangan varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Nama' THEN 'MR.Nama'
				WHEN 'Keterangan' THEN 'MR.Keterangan'
				WHEN 'Order_By' THEN ' MR.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'MR.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE 'MR.Id' end; 	 	

--Query ini nantinya akan dijadikan subquery dan fungsi order di taruh di atas dikarenakan subquery
--tidak support dengan order by, untuk itu diakalin dengan sorting number terlebih dahulu
DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    MR.ID as Id,
									    MR.Nama, 
									    MR.Keterangan, 
									    MR.Order_By,	  
									    L2.Name as Status,
										MR.IsActive as IsActive,
									    L2.Name as IsDelete
										,dbo.[uf_ShortIndonesianDateTime](MR.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](MR.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [dbo].[Tbl_Master_Role] MR
										 LEFT JOIN dbo.Tbl_Pegawai PC ON MR.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON MR.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON MR.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on MR.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on MR.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where MR.IsDeleted = 0 ',
	@QueryNama varchar(MAX) = '',
	@QueryKeterangan varchar(MAX) = ''

--Ini digunakan untuk mengeset dynamic kondisi parameter dengan menggunakan bantuan function supaya rapi kodingannya
--Untuk lebih jelasnya baca alur logic function yang dipakai
SELECT @QueryNama = dbo.uf_LookupDynamicQueryGenerator(@Nama, 'MR.Nama')
SELECT @QueryKeterangan = dbo.uf_LookupDynamicQueryGenerator(@Keterangan, 'MR.Keterangan') 

--Setelah mengeset nilai dari semua variabel kemudian kita gabungkan dengan query dibawah ini untuk paging
--data yang di select, pagging digunakan untuk meningkatkan performance query, dikarenakan data yang akan dikirim
--dari sini adalah data cukup data yang dibutuhkan saja, dengan kata lain kita melakukan filterisasi data terlebih
--dahulu dari sisi databasenya sebelum dikirim ke controller
SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryNama
				+ @QueryKeterangan
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				--Untuk mengecek sebenarnya query seperti apa yang akan dieksekusi, 
				--ganti perintah 'EXEC' dibawah dengan menggunakan 'PRINT'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanMenu_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanMenu_Count] 	
  @Name varchar(MAX) ='',
  @Type varchar(MAX) ='',
  @Role varchar(MAX) ='',
  @Parent varchar(MAX) =''
AS 
BEGIN
DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								  FROM [dbo].[Navigation] N
								  LEFT JOIN dbo.Tbl_Lookup L oN N.Type = L.Value and L.Type = ''TypeMenu''
								  LEFT JOIN dbo.Navigation NP ON N.ParentNavigation_Id = NP.Id
								  LEFT JOIN dbo.Tbl_Lookup L2 ON L2.Value = N.Visible AND L2.Type = ''IsActive''
								  LEFT JOIN dbo.Tbl_Pegawai PC ON N.CreatedBy_Id = PC.Id
								  LEFT JOIN dbo.Tbl_Pegawai PU ON N.UpdatedBy_Id = PU.Id
								  LEFT JOIN dbo.Tbl_Pegawai PD ON N.DeletedById = PD.Id
								  Where (N.IsDeleted = 0 OR N.IsDeleted is null)
								  ',
	@QueryName varchar(MAX) = '',
	@QueryType varchar(MAX) = '',
	@QueryParent varchar(MAX) = ''


SELECT @QueryName = dbo.uf_LookupDynamicQueryGenerator(@Name, 'N.Name')
SELECT @QueryType = dbo.uf_LookupDynamicQueryGenerator(@Type, 'L.Name') 
SELECT @QueryParent = dbo.uf_LookupDynamicQueryGenerator(@Parent, 'NP.Name') 

SET @Query = @Query 
				+ @QueryName
				+ @QueryType
			    + @QueryParent
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanMenu_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
create proc [dbo].[sp_PengaturanMenu_GetDataById]
@Id int
as
SELECT [Id]
      ,[Type] as TipeId
      ,[Name] as Nama
      ,[Route] as Route
      ,[Order] as OrderBy
      ,[Visible]
      ,[ParentNavigation_Id] as ParentId
      ,[IconClass] as Icon
  FROM [dbo].[Navigation] 
  Where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_PengaturanMenu_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_PengaturanMenu_View] 	
  @Name varchar(MAX) ='',
  @Type varchar(MAX) ='',
  @Role varchar(MAX) ='',
  @Parent varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 
				WHEN 'Nama' THEN 'L.Name'	
				WHEN 'Tipe' THEN 'N.Type'
				WHEN 'OrderBy' THEN ' N.[Order]'
				WHEN 'Parent' THEN ' NP.Name'
				WHEN 'Icon' THEN ' N.IconClass'
				WHEN 'Route' THEN ' N.Route'

				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'Visible_Name' THEN 'L2.Name'			 				 	
				ELSE 'N.Id' end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
									  ,N.[Id]
									  ,N.[Name] as Nama
									  --,N.[Type] as Type
									  ,N.[Route]
									  ,N.IconClass as Icon
									  ,N.[Order] as OrderBy
									  ,N.[Visible] as IsVisible
									  ,N.ParentNavigation_Id
									  ,ISNULL(NP.Name,''-'') as Parent
									  ,L2.Name as Visible_Name
									  ,L.Name as Tipe
									  ,dbo.[uf_ShortIndonesianDateTime](N.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](N.[UpdatedTime]) as UpdatedTime
									,dbo.[uf_ShortIndonesianDateTime](N.[DeletedTime]) as DeletedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
									,PD.Nama as DeletedBy
									,dbo.[uf_GetAllRoleMenu](N.Id) as Role
								  FROM [dbo].[Navigation] N
								  LEFT JOIN dbo.Tbl_Lookup L oN N.Type = L.Value and L.Type = ''TypeMenu''
								  LEFT JOIN dbo.Navigation NP ON N.ParentNavigation_Id = NP.Id
								  LEFT JOIN dbo.Tbl_Lookup L2 ON L2.Value = N.Visible AND L2.Type = ''IsActive''
								  LEFT JOIN dbo.Tbl_Pegawai PC ON N.CreatedBy_Id = PC.Id
								  LEFT JOIN dbo.Tbl_Pegawai PU ON N.UpdatedBy_Id = PU.Id
								  LEFT JOIN dbo.Tbl_Pegawai PD ON N.DeletedById = PD.Id
								  Where (N.IsDeleted = 0 OR N.IsDeleted is null)
								  ',
	@QueryName varchar(MAX) = '',
	@QueryType varchar(MAX) = '',
	@QueryParent varchar(MAX) = ''

SELECT @QueryName = dbo.uf_LookupDynamicQueryGenerator(@Name, 'N.Name')
SELECT @QueryType = dbo.uf_LookupDynamicQueryGenerator(@Type, 'L.Name') 
SELECT @QueryParent = dbo.uf_LookupDynamicQueryGenerator(@Parent, 'NP.Name') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryName
				+ @QueryType
				+ @QueryParent
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[Sp_Profile_GetDatePegawai]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[Sp_Profile_GetDatePegawai]
@PegawaiId int
as
declare @VirtualPathImages nvarchar(max)=''

select @VirtualPathImages = [Value] from Tbl_SystemParameter where [Key] = 'VirtualPathProfile'

SELECT P.[Id]
      ,[Unit_Id]
      ,[Role_Id]
	  ,MR.Nama as Nama_Role
	  ,U.Name as Nama_Unit
      ,[Id_JenisKelamin]
      ,[Npp]
      ,P.[Nama] as Nama_Pegawai
      ,[Tempat_Lahir]
      ,[Tanggal_Lahir]
      ,[Alamat]
      ,P.[Email]
      ,dbo.uf_IndonesianDateTime(P.[Lastlogin]) as Last_Active
      ,CONCAT(@VirtualPathImages,[Images]) as Images
      ,P.[ImagesFullPath]
      ,[NameImages]
      ,[No_HP]
      ,P.[IsActive]
      ,[LDAPLogin]
  FROM [dbo].[Tbl_Pegawai] P
  LEFT JOIN dbo.Tbl_Master_Role MR ON P.Role_Id = MR.Id
  LEFT JOIN dbo.Tbl_Unit U ON U.Id = P.Unit_Id
  LEFt JOIN dbo.Tbl_User US ON US.Pegawai_Id = P.Id 
  where P.Id = @PegawaiId
GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectLog_GetAllData]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_ProjectLog_GetAllData]
@ProjectId int
as
SELECT PL.[Id]
      ,PL.[ProjectId]
	  ,PJ.ProjectNo
	  ,PJ.Nama as NamaProject
      ,PL.[PegawaiIdFrom]
      ,PL.[PegawaiIdTo]
	  ,P.Nama as NamaPegawaiFrom
	  ,PG.Nama as NamaPegawaiUnit
	  ,PC.Nama as CreatedBy
	  ,U.Name as UnitFrom
	  ,UT.Name as UnitTo
      ,PL.[UnitIdPegawaiIdFrom]
      ,PL.[UnitIdPegawaiIdTo]
      ,PL.[ProjectStatusForm]
      ,PL.[ProjectStatusTo]
      ,PL.[LogActivityId]
      ,ISNULL(PL.[Komentar],' ') as Komentar
      ,ISNULL(PL.[Keterangan],' ') as Keterangan
	  ,L.Name as NamaActivity
	  ,dbo.uf_IndonesianDate(PL.CreatedTime) as CreatedTime
	  ,dbo.[uf_IndonesianTime](PL.CreatedTime) as [Time]
  FROM [dbo].[Tbl_Project_Log] PL
  LEFT JOIN dbo.Tbl_Project PJ ON PL.ProjectId = PJ.Id
  LEFT JOIN dbo.Tbl_Pegawai P ON PL.PegawaiIdFrom = P.Id
  LEFT JOIN dbo.Tbl_Pegawai PG ON PL.[PegawaiIdTo] = PG.Id
  LEFT JOIN dbo.Tbl_Pegawai PC ON PL.CreatedBy_Id = PC.Id
  LEFT JOIN dbo.Tbl_Unit U ON U.Id = PL.[UnitIdPegawaiIdFrom]
  LEFT JOIN dbo.Tbl_Unit UT ON UT.Id = PL.[UnitIdPegawaiIdTo]
  LEFT JOIN dbo.Tbl_Lookup L ON L.Value = PL.[LogActivityId] and L.Type = 'Activity' and L.IsActive= 1 and L.IsDeleted = 0
  WHERE PL.ProjectId = @ProjectId and PL.IsActive = 1 and PL.IsDeleted = 0
  order by PL.Id desc

  --select * from Tbl_Lookup
GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ProjectMember_Count] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @IsDone nvarchar(10) = '',
  @RoleIdLogin nvarchar(10) = '',
  @UnitIdLogin nvarchar(10) = '',
  @PegawaiIdLogin nvarchar(10) = ''
AS 
BEGIN
	 	

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[v_data_project_member] DPM
										 LEFT JOIN dbo.Tbl_Pegawai PC ON DPM.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON DPM.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON DPM.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on DPM.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on DPM.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (DPM.IsDeleted = 0 OR DPM.IsDeleted is null) and DPM.IsActive = 1 ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@IsDone != '')
begin
	SET @Query = @Query + ' AND DPM.IsDone = '''+@IsDone+''''
end

IF(@PegawaiIdLogin != '')
begin
	SET @Query = @Query + ' AND DPM.PegawaiId = '''+@PegawaiIdLogin+''''
end

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'DPM.ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'DPM.NamaProject') 
SET @Query =	@Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
			
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[sp_ProjectMember_GetDataById]
@Id int
as
SELECT [Id]
      ,[ProjectId]
      ,[JobPositionId]
      ,[PegawaiId]
      ,[UnitPegawaiId]
      ,[Keterangan]
      ,[StatusProgressId]
      ,[CatatanPegawai]
      ,[SendAsTask]
      ,CONCAT(dbo.uf_IndonesianDate([StartDate]) , ' - ', dbo.uf_IndonesianDate([EndDate])) as Periode
      ,[IsDone]
      ,dbo.uf_IndonesianDateDDMMYYYY([TanggalPenyelesaian]) as TanggalPenyelesaian
      ,[KeteranganPenyelesaian]
  FROM [dbo].[Tbl_Project_Member]
  Where Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_ProgressKerja_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ProjectMember_ProgressKerja_Count] 	
  @Judul varchar(MAX) ='',
  @ProjectMemberId varchar(MAX) ='',
  @TanggalAwal varchar(MAX) ='',
  @TanggalAkhir varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								from [dbo].[Tbl_Project_Member_ProgressKerja] PK
										 LEFT JOIN dbo.Tbl_Pegawai PC ON PK.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON PK.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON PK.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on PK.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on PK.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (PK.IsDeleted = 0 OR PK.IsDeleted is null) and [ProjectMemberId] = '''+@ProjectMemberId+'''',
	@QueryJudul varchar(MAX) = '',
	@QueryTanggal varchar(MAX) = ''

SELECT @QueryJudul = dbo.uf_LookupDynamicQueryGenerator(@Judul, 'PK.Judul')
SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'PK.TanggalAwal') 

SET @Query =	@Query 
				+ @QueryJudul
				--+ @QueryTanggal
				
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_ProgressKerja_GetDataById]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
create proc [dbo].[sp_ProjectMember_ProgressKerja_GetDataById]
@Id int
as
SELECT [Id]
      ,[Judul]
      ,[Deskripsi]
      ,CONCAT(dbo.uf_IndonesianDateDDMMYYYY([TanggalAwal]),' - ',dbo.uf_IndonesianDateDDMMYYYY([TanggalAkhir])) as Tanggal
  FROM [dbo].[Tbl_Project_Member_ProgressKerja]
  WHERE Id = @Id
GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_ProgressKerja_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ProjectMember_ProgressKerja_View] 	
  @Judul varchar(MAX) ='',
  @ProjectMemberId varchar(MAX) ='',
  @TanggalAwal varchar(MAX) ='',
  @TanggalAkhir varchar(MAX) ='',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'PK.ID'
				WHEN 'Tanggal' THEN 'PK.TanggalAwal'
				WHEN 'Status' THEN 'L2.Name'	 				 	
				ELSE @sortColumn end; 	 	


DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									    PK.ID as Id,
									    PK.Judul, 
									    PK.Deskripsi, 
									    CONCAT(dbo.uf_IndonesianDateDDMMYYYY([TanggalAwal]),'' - '',dbo.uf_IndonesianDateDDMMYYYY([TanggalAkhir])) as Tanggal
									    ,L2.Name as Status,
										PK.IsActive as IsActive,
									    L2.Name as IsDelete
										,dbo.[uf_ShortIndonesianDateTime](PK.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](PK.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [dbo].[Tbl_Project_Member_ProgressKerja] PK
										 LEFT JOIN dbo.Tbl_Pegawai PC ON PK.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON PK.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON PK.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on PK.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on PK.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (PK.IsDeleted = 0 OR PK.IsDeleted is null) and [ProjectMemberId] = '''+@ProjectMemberId+'''',
	@QueryJudul varchar(MAX) = '',
	@QueryTanggal varchar(MAX) = ''

SELECT @QueryJudul = dbo.uf_LookupDynamicQueryGenerator(@Judul, 'PK.Judul')
SELECT @QueryTanggal = dbo.uf_DateRangeDynamicQueryGenerator(@TanggalAwal,@TanggalAkhir, 'PK.TanggalAwal') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryJudul
				--+ @QueryTanggal
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_ProjectMember_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ProjectMember_View] 	
  @ProjectNo varchar(MAX) ='',
  @NamaProject varchar(MAX) ='',
  @IsDone nvarchar(10) = '',
  @RoleIdLogin nvarchar(10) = '',
  @UnitIdLogin nvarchar(10) = '',
  @PegawaiIdLogin nvarchar(10) = '',
  @sortColumn varchar(100)='Id',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'Id' THEN 'DPM.Id'
				WHEN 'Status' THEN 'L2.Name'
				WHEN 'Order_By' THEN ' DPM.Order_By'
				WHEN 'CreatedBy' THEN 'PC.Name'
				WHEN 'UpdatedBy' THEN 'PU.UpdatedBy_Id'
				WHEN 'IsActive' THEN 'DPM.Name'
				WHEN 'IsDelete' THEN 'L2.Name'			 				 	
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number,
									   DPM.[Id]
									  ,[ProjectId]
									  ,[NamaProject]
									  ,[ProjectNo]
									  ,[JobPositionId]
									  ,[JobPosition]
									  ,[PegawaiId]
									  ,[NamaPegawai]
									  ,[NppPegawai]
									  ,[UnitPegawaiId]
									  ,[NamaUnit]
									  ,DPM.[Keterangan]
									  ,[StatusProgressId]
									  ,[CatatanPegawai]
									  ,[SendAsTask]
									  ,[Periode]
									  ,[IsDone]
									  ,[Selisih]
									  ,StatusProject  
									  ,[Warna]
									  ,[JumlahHariPengerjaan]
									  ,[SelisihAngka]
									  ,[TanggalPenyelesaian]
									  ,[JumlahHariPengerjaanAngka]
									  ,StatusProjectDalamPantauan
									    ,L2.Name as Status,
										DPM.IsActive as IsActive
										,dbo.[uf_ShortIndonesianDateTime](DPM.[CreatedTime]) as CreatedTime
									,dbo.[uf_ShortIndonesianDateTime](DPM.[UpdatedTime]) as UpdatedTime
									,PC.Nama as CreatedBy
									,PU.Nama as UpdatedBy
								from [dbo].[v_data_project_member] DPM
										 LEFT JOIN dbo.Tbl_Pegawai PC ON DPM.CreatedBy_Id = PC.Id
										 LEFT JOIN dbo.Tbl_Pegawai PU ON DPM.UpdatedBy_Id = PU.Id
										 LEFT JOIN dbo.Tbl_Pegawai PD ON DPM.DeletedBy_Id = PD.Id
										left join Tbl_Lookup L2 on DPM.IsActive = L2.Value and L2.Type = ''IsActive''
										left join Tbl_Lookup L3 on DPM.IsDeleted = L3.Value and L3.Type = ''IsDelete''
										Where (DPM.IsDeleted = 0 OR DPM.IsDeleted is null) and DPM.IsActive = 1 ',
	@QueryProjectNo varchar(MAX) = '',
	@QueryNamaProject varchar(MAX) = ''

IF(@IsDone != '')
begin
	SET @Query = @Query + ' AND DPM.IsDone = '''+@IsDone+''''
end

IF(@PegawaiIdLogin != '')
begin
	SET @Query = @Query + ' AND DPM.PegawaiId = '''+@PegawaiIdLogin+''''
end

SELECT @QueryProjectNo = dbo.uf_LookupDynamicQueryGenerator(@ProjectNo, 'DPM.ProjectNo')
SELECT @QueryNamaProject = dbo.uf_LookupDynamicQueryGenerator(@NamaProject, 'DPM.NamaProject') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryProjectNo
				+ @QueryNamaProject
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[SP_RoleChanged]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_RoleChanged]
  -- Add the parameters for the stored procedure here
  @id int
AS
BEGIN
  -- SET NOCOUNT ON added to prevent extra result sets from
  -- interfering with SELECT statements.
  SET NOCOUNT ON;

    -- Insert statements for procedure here
  SELECT Role_Id, Unit_Id from Tbl_Role_Pegawai where Id = @id --and StatusRole = 1 and IsDeleted = 0
END
GO
/****** Object:  StoredProcedure [dbo].[sp_SELECTALLDATES]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SELECTALLDATES]
(
@StartDate as date,
@EndDate as date,
@Jumlah int OUTPUT
)
AS
Declare @Current as date = DATEADD(DD, 1, @StartDate);

Create table #tmpDates
(displayDate date,
 nama nvarchar(max)
)

WHILE @Current < @EndDate
BEGIN
insert into #tmpDates
Select @Current, DATENAME(DW,@Current)
set @Current = DATEADD(DD, 1, @Current) -- add 1 to current day
END

select @Jumlah = count(*) from (
Select displayDate,D.nama
	 ,(CASE WHEN H.Nama is null and D.nama NOT IN ('Saturday','Sunday') THEN 0  
			WHEN D.nama IN ('Saturday','Sunday') THEN 1
			ELSE 1
	 END) as FlagHoliday
from #tmpDates D
LEFT JOIN dbo.Tbl_Holiday H ON D.displayDate = H.Tanggal and H.IsActive = 1 and H.IsDeleted = 0 
) tbl
where FlagHoliday = 0

drop table #tmpDates
GO
/****** Object:  StoredProcedure [dbo].[Sp_Utility_GetListUnitIdKelolaan]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Sp_Utility_GetListUnitIdKelolaan]
@UnitId nvarchar(max) = ''
as
begin
declare @AllUnitId nvarchar(max)

SELECT @AllUnitId = STUFF((
            SELECT ',' + convert(nvarchar(max),UnitID)
			  from [dbo].[uf_GetUnitHirarki](@UnitId)
            FOR XML PATH('')
            ), 1, 1, '')

select @AllUnitId as UnitId
end
GO
/****** Object:  StoredProcedure [dbo].[sp_utility_GetTotalProgressTask]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
create proc [dbo].[sp_utility_GetTotalProgressTask]
@PegawaiId int
as
SELECT Count(*) as Jumlah
  FROM [dbo].[v_data_project_member]
  Where IsDone = 0 and PegawaiId = @PegawaiId
GO
/****** Object:  StoredProcedure [dbo].[sp_Utility_Project_DownloadFileAttachment]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/

CREATE proc [dbo].[sp_Utility_Project_DownloadFileAttachment]
@Id int
as
begin
declare @VirtualPath nvarchar(max) = ''
select @VirtualPath = Value from Tbl_SystemParameter where [Key] = 'VirtualPath'

SELECT [Id]
      ,[NamaFile]
      ,[FileExt]
      ,[FileType]
      ,[Size]
      ,[Path]
	  ,CONCAT(@VirtualPath,'',[Path]) as DownloadPath
  FROM [dbo].[Tbl_Project_File]
  Where Id = @Id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Workload_Pegawai_Count]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Workload_Pegawai_Count] 	
  @UnitId varchar(MAX) ='',
  @UnitIdLogin varchar(MAX) ='',
  @RoleIdLogin varchar(MAX) ='',
  @PegawaiIdLogin varchar(MAX) =''
AS 
BEGIN

DECLARE @Query VARCHAR(MAX) = 'select Count(*)
								  FROM [dbo].[vw_workload_pegawai]
								  Where PegawaiId is not null ',
	@QueryUnit varchar(MAX) = ''
IF(@RoleIdLogin = 14) --Pengelola
BEGIN
	SET @Query = @Query + ' AND PegawaiId in (select [PegawaiId] from [dbo].[Tbl_Pegawai_Kelolaan] Where [AtasanId] = '''+@PegawaiIdLogin+''') ' 
END
ELSE IF(@RoleIdLogin = 2) --Pemimpin
BEGIN
	SET @Query = @Query + ' AND Unit_Id in (select [UnitID] from dbo.uf_GetUnitHirarki('''+@PegawaiIdLogin+''')) ' 
END

SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorEqual(@UnitId, 'Unit_Id') 

SET @Query =	 @Query 
				+ @QueryUnit
			
				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[sp_Workload_Pegawai_View]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Workload_Pegawai_View] 	
  @UnitId varchar(MAX) ='',
  @UnitIdLogin varchar(MAX) ='',
  @RoleIdLogin varchar(MAX) ='',
  @PegawaiIdLogin varchar(MAX) ='',
  @sortColumn varchar(100)='PegawaiId',
  @sortColumnDir varchar(10)='desc',
  @PageNumber INT, 
  @RowsPage INT
AS 
BEGIN

--Digunakan ketika akan sort data
DECLARE 
@SortField varchar(50)

SET @SortField = 	
				CASE @sortColumn 	
				WHEN 'PegawaiId' THEN 'PegawaiId'	 				 	
				ELSE @sortColumn end; 	 	

DECLARE @Query VARCHAR(MAX) = 'select ROW_NUMBER() OVER(ORDER BY '+@SortField+' '+@sortColumnDir+') AS Number
									  ,PegawaiId
									  ,[Nama]
									  ,[Npp]
									  ,[Unit_Id]
									  ,[NamaUnit]
									  ,[JumlahSemuaProject]
									  ,[JumlahSelesai]
									  ,[JumlahOnProgress]
								  FROM [dbo].[vw_workload_pegawai]
								  Where PegawaiId is not null ',
	@QueryUnit varchar(MAX) = ''
IF(@RoleIdLogin = 14) --Pengelola
BEGIN
	SET @Query = @Query + ' AND PegawaiId in (select [PegawaiId] from [dbo].[Tbl_Pegawai_Kelolaan] Where [AtasanId] = '''+@PegawaiIdLogin+''') ' 
END
ELSE IF(@RoleIdLogin = 2) --Pemimpin
BEGIN
	SET @Query = @Query + ' AND Unit_Id in (select [UnitID] from dbo.uf_GetUnitHirarki('''+@PegawaiIdLogin+''')) ' 
END

SELECT @QueryUnit = dbo.uf_LookupDynamicQueryGeneratorEqual(@UnitId, 'Unit_Id') 

SET @Query = 'SELECT * FROM (' 
				+ @Query 
				+ @QueryUnit
				+') TBL WHERE NUMBER BETWEEN (('+CONVERT(VARCHAR,@PageNumber)+'-1) * '
				+CONVERT(VARCHAR,@RowsPage)+' + 1) AND ('+CONVERT(VARCHAR,@PageNumber)+'*'+CONVERT(VARCHAR,@RowsPage)+')'

				EXEC(@Query) 
END 

GO
/****** Object:  StoredProcedure [dbo].[Utility_CekProject_PendinganMember_ByNama]    Script Date: 29/05/2021 19:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE proc [dbo].[Utility_CekProject_PendinganMember_ByNama]
@ProjectId int
as
begin
declare @AllName nvarchar(max)
SELECT @AllName = STUFF((
            SELECT ', ' +  P.Nama
			   FROM [dbo].[Tbl_Project_Member] PM
				  LEFT JOIN dbo.Tbl_Pegawai P ON PM.PegawaiId = P.Id
				  Where PM.[SendAsTask] = 1 and PM.[IsDone] = 0 and PM.ProjectId = @ProjectId
            FOR XML PATH('')
            ), 1, 1, '')


SELECT @AllName
end
GO

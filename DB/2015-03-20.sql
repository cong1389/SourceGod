USE [master]
GO
/****** Object:  Database [db]    Script Date: 3/20/2015 4:59:47 PM ******/
CREATE DATABASE [db]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\db_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [db] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db] SET ARITHABORT OFF 
GO
ALTER DATABASE [db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db] SET RECOVERY FULL 
GO
ALTER DATABASE [db] SET  MULTI_USER 
GO
ALTER DATABASE [db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'db', N'ON'
GO
USE [db]
GO
/****** Object:  StoredProcedure [dbo].[Configuration_Update]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
CREATE PROCEDURE [dbo].[Configuration_Update]
    @config_email NVARCHAR(200) ,
    @config_fax NVARCHAR(200) ,
    @config_phone NVARCHAR(200) ,
    @config_sitename NVARCHAR(200) ,
    @config_skypeid NVARCHAR(200) ,
    @config_yahooid NVARCHAR(200) ,
    @config_company_name_vi NVARCHAR(200) ,
    @config_address_vi NVARCHAR(200) ,
    @config_address1_vi NVARCHAR(200) ,
    @config_logoHeader NVARCHAR(200) ,
    @config_logoFooter NVARCHAR(200) ,
    @config_location NVARCHAR(200)
AS 
    BEGIN  
        UPDATE  medical_configuration
        SET     Value_name = @config_email
        WHERE   Key_name = 'config_email' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_fax
        WHERE   Key_name = 'config_fax' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_phone
        WHERE   Key_name = 'config_phone' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_sitename
        WHERE   Key_name = 'config_sitename' ;  
 --UPDATE medical_configuration SET Value_name = @config_email WHERE Key_name = 'config_email';  
        UPDATE  medical_configuration
        SET     Value_name = @config_skypeid
        WHERE   Key_name = 'config_skypeid' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_yahooid
        WHERE   Key_name = 'config_yahooid' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_company_name_vi
        WHERE   Key_name = 'config_company_name_vi' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_address_vi
        WHERE   Key_name = 'config_address_vi' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_address1_vi
        WHERE   Key_name = 'config_address1_vi' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_logoHeader
        WHERE   Key_name = 'config_logoHeader' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_logoFooter
        WHERE   Key_name = 'config_logoFooter' ;  
        UPDATE  medical_configuration
        SET     Value_name = @config_location
        WHERE   Key_name = 'config_location' ;  
    END


GO
/****** Object:  StoredProcedure [dbo].[GetAllAdv]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Author:DGC Generator
 Create date:11/07/2009
 Description:search sd_Adv by some Condition*/
-- =============================================
CREATE proc [dbo].[GetAllAdv]
(
	@name  nvarchar(50) = null,
	@published nchar(1) = null,
	@position int = null,
	@pageName nvarchar(25) = null,
	@pageSize int = 10,
	@pageIndex int = 1,
	@field nvarchar(100) = 'a.Position,a.ordering DESC',
	@total int output 
	)
AS
BEGIN
	DECLARE @select nvarchar(max),
			@Paramlist nvarchar(max);
	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,
					a.* FROM Medical_Adv a WHERE 1=1';
		
	/****@content***/
	if(@name is not null)
	begin
	 set @select = @select + ' and a.Name like ''%'+'+@name2+'+'%''';
	end	
	/**@published**/
	if(@published is not null)
	begin
		set @select = @select + ' and a.Published = @published2 ';
	end
	/*@position*/
	if(@position is not null)
	begin
		set @select = @select + ' and a.Position = @position2';
	end
	
	/**@pageName**/
	if(@pageName is not null)
	begin
		set @select = @select + ' and (select COUNT(*) from dbo.fn_ParseText2Table(a.ArrPageName,'','') where txt_value = @pageName2 ) > 0 ';
	end
	
	-- paging
	declare @table nvarchar(max);
	set @table = 'WITH [RESULT](row, Id, Position, Linkurl, Image,name, Height, Width, ClickCount, ordering, published,
				postdate, updatedate,ArrPageName) AS ( ';

	-- paging 
	declare @pagingSql nvarchar(max);
	declare @startRow int;
    declare @endRow   int;
	
	set @pagingSql	= 'SELECT * FROM [RESULT] WHERE row between ';
	set @startRow	= (@pageIndex - 1) * @pageSize + 1;
	set @endRow		= @startRow + @pageSize - 1;
    set @pagingSql	= @pagingSql + convert(varchar(10), @startRow);
	set @pagingSql	= @pagingSql + ' AND ' ;
	set @pagingSql	= @pagingSql + convert(varchar(10), @endRow);
	
	declare @sql nvarchar(max);
	set  @sql = ' ';
	
	set @sql = @table + @select + ') ' + @pagingsql;
	print @sql
	
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25)';
	exec SP_EXECUTESQL  @sql,
						@Paramlist,
						@name2 = @name,
						@published2 = @published,
						@position2 = @position,
						@pageName2 = @pageName
	
	declare @Total2 int;
	set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25),
						@Total2 int out';
						
	exec sp_executeSql @sql,
						@Paramlist, 
						 @name,
						 @published,
						 @position,
						 @pageName,
						@Total2 out
	set @total = @Total2;
END


GO
/****** Object:  StoredProcedure [dbo].[GetAllBanner]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Author:DGC Generator
 Create date:11/07/2009
 Description:search sd_Banner by some Condition*/
-- =============================================
create proc [dbo].[GetAllBanner]
(
	@name  nvarchar(50) = null,
	@published nchar(1) = null,
	@position int = null,
	@pageName nvarchar(25) = null,
	@pageSize int = 10,
	@pageIndex int = 1,
	@field nvarchar(100) = 'a.Position,a.ordering DESC',
	@total int output 
	)
AS
BEGIN
	DECLARE @select nvarchar(max),
			@Paramlist nvarchar(max);
	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,
					a.* FROM Medical_Banner a WHERE 1=1';
		
	/****@content***/
	if(@name is not null)
	begin
	 set @select = @select + ' and a.Name like ''%'+'+@name2+'+'%''';
	end	
	/**@published**/
	if(@published is not null)
	begin
		set @select = @select + ' and a.Published = @published2 ';
	end
	/*@position*/
	if(@position is not null)
	begin
		set @select = @select + ' and a.Position = @position2';
	end
	
	/**@pageName**/
	if(@pageName is not null)
	begin
		set @select = @select + ' and (select COUNT(*) from dbo.fn_ParseText2Table(a.ArrPageName,'','') where txt_value = @pageName2 ) > 0 ';
	end
	
	-- paging
	declare @table nvarchar(max);
	set @table = 'WITH [RESULT] AS ( ';

	-- paging 
	declare @pagingSql nvarchar(max);
	declare @startRow int;
    declare @endRow   int;
	
	set @pagingSql	= 'SELECT * FROM [RESULT] WHERE row between ';
	set @startRow	= (@pageIndex - 1) * @pageSize + 1;
	set @endRow		= @startRow + @pageSize - 1;
    set @pagingSql	= @pagingSql + convert(varchar(10), @startRow);
	set @pagingSql	= @pagingSql + ' AND ' ;
	set @pagingSql	= @pagingSql + convert(varchar(10), @endRow);
	
	declare @sql nvarchar(max);
	set  @sql = ' ';
	
	set @sql = @table + @select + ') ' + @pagingsql;
	print @sql
	
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25)';
	exec SP_EXECUTESQL  @sql,
						@Paramlist,
						@name2 = @name,
						@published2 = @published,
						@position2 = @position,
						@pageName2 = @pageName
	
	declare @Total2 int;
	set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25),
						@Total2 int out';
						
	exec sp_executeSql @sql,
						@Paramlist, 
						 @name,
						 @published,
						 @position,
						 @pageName,
						@Total2 out
	set @total = @Total2;
END


GO
/****** Object:  StoredProcedure [dbo].[GetAllChildProductCategory]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Congtt
-- Create date: 24/12/2014
-- Description:	Lấy danh sách Product Category
-- =============================================
/*
 Exec GetAllChildProductCategory  35,1
*/
CREATE PROCEDURE [dbo].[GetAllChildProductCategory] @IDCategory INT
	, @unclude_me BIT = 0
	, @total INT OUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM fc_GetAllChildProductCategory(@IDCategory, @unclude_me)

	SET @Total = (
			SELECT count(*)
			FROM fc_GetAllChildProductCategory(@IDCategory, @unclude_me)
			)
END








GO
/****** Object:  StoredProcedure [dbo].[GetAllManagementID]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
/*


	DECLARE @return_value INT
		,@total INT

	EXEC @return_value = [dbo].[GetAllManagementID] @published = N'1'
		,@pageSize = 11
		,@pageIndex = 1
		,@name='block_baseimage'
		,@total = @total OUTPUT

	SELECT @total AS N'@total'

	SELECT 'Return Value' = @return_value
	GO


*/
CREATE PROCEDURE [dbo].[GetAllManagementID] @name NVARCHAR(250) = NULL
	,@published NCHAR(1) = NULL
	,@pageSize INT = 10
	,@pageIndex INT = 1
	,@field NVARCHAR(100) = 'c.ordering DESC '
	,@total INT OUTPUT
AS
BEGIN
	DECLARE @select NVARCHAR(max)
		,@Paramlist NVARCHAR(max);

	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,
					c.* FROM Medical_ManagementID c WHERE 1=1 ';

	/****@content***/
	IF (@name IS NOT NULL)
	BEGIN
		SET @select = @select + ' and c.name like ''%'' + @name2+ ''%''';
	END

	/**@published**/
	IF (@published IS NOT NULL)
	BEGIN
		SET @select = @select + ' and c.published = @published2 ';
	END

	-- paging
	DECLARE @table NVARCHAR(max);

	SET @table = 'WITH [Result](row, id,ordering,postdate,published,updatedate,name,value) AS ( ';

	-- paging 
	DECLARE @pagingSql NVARCHAR(max);
	DECLARE @startRow INT;
	DECLARE @endRow INT;

	SET @pagingSql = 'SELECT * FROM [Result] WHERE row between ';
	SET @startRow = (@pageIndex - 1) * @pageSize + 1;
	SET @endRow = @startRow + @pageSize - 1;
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @startRow);
	SET @pagingSql = @pagingSql + ' AND ';
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @endRow);

	DECLARE @sql NVARCHAR(max);

	SET @sql = ' ';
	SET @sql = @table + @select + ') ' + @pagingsql;
	SET @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1)';

	EXEC SP_EXECUTESQL @sql
		,@Paramlist
		,@name2 = @name
		,@published2 = @published

	DECLARE @Total2 INT;

	SET @sql = @table + @select + ') select @Total2= count(row)  from [Result]';
	SET @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@Total2 int out';

	PRINT @sql

	EXEC sp_executeSql @sql
		,@Paramlist
		,@name
		,@published
		,@Total2 OUT

	SET @total = @Total2;
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllPartners]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Author:DGC Generator
 Create date:11/07/2009
 Description:search sd_Adv by some Condition*/
-- =============================================
create proc [dbo].[GetAllPartners]
(
	@name  nvarchar(50) = null,
	@published nchar(1) = null,
	@position int = null,
	@pageName nvarchar(25) = null,
	@pageSize int = 10,
	@pageIndex int = 1,
	@field nvarchar(100) = 'a.Position,a.ordering DESC',
	@total int output 
	)
AS
BEGIN
	DECLARE @select nvarchar(max),
			@Paramlist nvarchar(max);
	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,
					a.* FROM medical_partners a WHERE 1=1';
		
	/****@content***/
	if(@name is not null)
	begin
	 set @select = @select + ' and a.Name like ''%'+'+@name2+'+'%''';
	end	
	/**@published**/
	if(@published is not null)
	begin
		set @select = @select + ' and a.Published = @published2 ';
	end
	/*@position*/
	if(@position is not null)
	begin
		set @select = @select + ' and a.Position = @position2';
	end
	
	/**@pageName**/
	if(@pageName is not null)
	begin
		set @select = @select + ' and (select COUNT(*) from dbo.fn_ParseText2Table(a.ArrPageName,'','') where txt_value = @pageName2 ) > 0 ';
	end
	
	-- paging
	declare @table nvarchar(max);
	set @table = 'WITH [RESULT](row, Id, Position, Linkurl, Image,name, Height, Width, ClickCount, ordering, published,
				postdate, updatedate,ArrPageName) AS ( ';

	-- paging 
	declare @pagingSql nvarchar(max);
	declare @startRow int;
    declare @endRow   int;
	
	set @pagingSql	= 'SELECT * FROM [RESULT] WHERE row between ';
	set @startRow	= (@pageIndex - 1) * @pageSize + 1;
	set @endRow		= @startRow + @pageSize - 1;
    set @pagingSql	= @pagingSql + convert(varchar(10), @startRow);
	set @pagingSql	= @pagingSql + ' AND ' ;
	set @pagingSql	= @pagingSql + convert(varchar(10), @endRow);
	
	declare @sql nvarchar(max);
	set  @sql = ' ';
	
	set @sql = @table + @select + ') ' + @pagingsql;
	print @sql
	
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25)';
	exec SP_EXECUTESQL  @sql,
						@Paramlist,
						@name2 = @name,
						@published2 = @published,
						@position2 = @position,
						@pageName2 = @pageName
	
	declare @Total2 int;
	set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	set @Paramlist = N'
						@name2  nvarchar(50),
						@published2 nchar(1),
						@position2 int,
						@pageName2 nvarchar(25),
						@Total2 int out';
						
	exec sp_executeSql @sql,
						@Paramlist, 
						 @name,
						 @published,
						 @position,
						 @pageName,
						@Total2 out
	set @total = @Total2;
END


GO
/****** Object:  StoredProcedure [dbo].[GetAllPartnersNew]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*Author:DGC Generator    
 Create date:11/07/2009    
 Description:search sd_Adv by some Condition*/    
-- =============================================    
CREATE proc [dbo].[GetAllPartnersNew]    
(    
 @name  nvarchar(50) = null,    
 @published nchar(1) = null,    
 @position int = null,    
 @pageName nvarchar(25) = null,    
 @pageSize int = 10,    
 @pageIndex int = 1,    
 @total int output,
 @field nvarchar(100) = 'p.ordering DESC'     
 )    
AS    
BEGIN    
 DECLARE @select nvarchar(max),    
   @Paramlist nvarchar(max);    
 SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,p.*    
     FROM medical_partners p WHERE 1=1 and published=1 ';    
 /*@langId*/    
 if(@position=1)    
 begin    
  set @select = @select + ' and p.position in (1,2,3) ';    
 end    
     
 /****@name***/    
 if(@position=4)    
 begin    
  set @select = @select + ' and p.position =4 ';    
 end    
     
 -- paging    
 declare @table nvarchar(max);    
 set @table = 'WITH [RESULT](row, Id, Position, Linkurl, Image,name, Height, Width, ClickCount, ordering, published,    
    postdate, updatedate,ArrPageName) AS ( ';    
    
 -- paging     
 declare @pagingSql nvarchar(max);    
 declare @startRow int;    
    declare @endRow   int;    
     
 set @pagingSql = 'SELECT * FROM [RESULT] WHERE row between ';    
 set @startRow = (@pageIndex - 1) * @pageSize + 1;    
 set @endRow  = @startRow + @pageSize - 1;    
    set @pagingSql = @pagingSql + convert(varchar(10), @startRow);    
 set @pagingSql = @pagingSql + ' AND ' ;    
 set @pagingSql = @pagingSql + convert(varchar(10), @endRow);    
     
 declare @sql nvarchar(max);    
 set  @sql = ' ';    
     
 set @sql = @table + @select + ') ' + @pagingsql;    
 print @sql    
     
 set @Paramlist = N'    
      @name2  nvarchar(50),    
      @published2 nchar(1),    
      @position2 int,    
      @pageName2 nvarchar(25)';    
 exec SP_EXECUTESQL  @sql,    
      @Paramlist,    
      @name2 = @name,    
      @published2 = @published,    
      @position2 = @position,    
      @pageName2 = @pageName    
     
 declare @Total2 int;    
 set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';    
 set @Paramlist = N'    
      @name2  nvarchar(50),    
      @published2 nchar(1),    
      @position2 int,    
      @pageName2 nvarchar(25),    
      @Total2 int out';    
          
 exec sp_executeSql @sql,    
      @Paramlist,     
       @name,    
       @published,    
       @position,    
       @pageName,    
      @Total2 out    
 set @total = @Total2;    
END


GO
/****** Object:  StoredProcedure [dbo].[GetAllProductCategory]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Congtt
-- Create date: 2015-02-20
-- Description:	Get danh muc
-- =============================================
/*

DECLARE @p7 INT

SET @p7 = 3

EXEC [GetAllProductCategory] @langId = 1
	, @name = NULL
	, @published  = NULL
	, @parentid = NULL
	, @tree = 0
	, @pageIndex = 1
	, @pageSize = 100
	, @total = @p7 OUTPUT

SELECT @p7


*/
CREATE PROCEDURE [dbo].[GetAllProductCategory] @langId INT = NULL
	,@name VARCHAR(255) = NULL
	,@parentid INT = NULL
	,@tree BIT = NULL
	,@pageSize INT = 10
	,@pageIndex INT = 1
	,@field NVARCHAR(100) = 'p.pathtree'
	,@published NCHAR(1) = NULL
	,@total INT OUTPUT
AS
BEGIN
	DECLARE @select NVARCHAR(max)
		,@Paramlist NVARCHAR(max);

	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,p.*,pd.id as subId,pd.langid,pd.name,pd.nameurl,pd.Brief
	,pd.Detail,pd.BaseImage,pd.SmallImage,pd.ThumbnailImage,pd.MetaTitle,pd.MetaKeyword,pd.MetaDecription 
	FROM medical_productcategory p 
	INNER JOIN medical_productcategorydesc  pd ON p.id = pd.mainid WHERE 1=1';

	/*@langId*/
	IF (@langId IS NOT NULL)
	BEGIN
		SET @select = @select + ' and pd.langid = @langId2';
	END

	/****@name***/
	IF (@name IS NOT NULL)
	BEGIN
		SET @select = @select + ' and pd.Name like ''%' + '+@name2+' + '%''';
	END

	/*@parentid*/
	IF (@parentid IS NOT NULL)
	BEGIN
		IF (@tree = 1)
			SET @select = @select + ' and p.parentid in (SELECT id FROM dbo.fc_GetAllChildProductCategory(@parentid2,1)) ';
		ELSE
			SET @select = @select + ' and p.parentid = @parentid2 ';
	END

	/**@published**/
	IF (@published IS NOT NULL)
	BEGIN
		SET @select = @select + ' and p.Published = @published2 ';
	END

	DECLARE @table NVARCHAR(max);

	SET @table = 'WITH [RESULT](row, id,parentid,published,ordering,postdate,updatedate,pathtree, subId,langid,name,nameurl
	,Brief,Detail,BaseImage,SmallImage,ThumbnailImage,MetaTitle,MetaKeyword,MetaDecription ) AS ( ';

	-- paging 
	DECLARE @pagingSql NVARCHAR(max);
	DECLARE @startRow INT;
	DECLARE @endRow INT;

	SET @pagingSql = 'SELECT * FROM [RESULT] WHERE row between ';
	SET @startRow = (@pageIndex - 1) * @pageSize + 1;
	SET @endRow = @startRow + @pageSize - 1;
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @startRow);
	SET @pagingSql = @pagingSql + ' AND ';
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @endRow);

	DECLARE @sql NVARCHAR(max);

	SET @sql = ' ';
	SET @sql = @table + @select + ') ' + @pagingsql;
	SET @Paramlist = N'
						@langId2 INT,
						@name2 VARCHAR(255),
						@published2 NCHAR(1),
						@parentid2 INT,
						@tree2 BIT';

	EXEC SP_EXECUTESQL @sql
		,@Paramlist
		,@langId2 = @langId
		,@name2 = @name
		,@published2 = @published
		,@parentid2 = @parentid
		,@tree2 = @tree

	DECLARE @Total2 INT;

	SET @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	SET @Paramlist = N'
						@langId2 INT,
						@name2 VARCHAR(255),
						@published2 NCHAR(1),
						@parentid2 INT,
						@tree2 BIT,
						@Total2 int out';

	PRINT @sql

	EXEC sp_executeSql @sql
		,@Paramlist
		,@langId
		,@name
		,@published
		,@parentid
		,@tree
		,@Total2 OUTPUT

	SET @total = @Total2;
END

GO
/****** Object:  StoredProcedure [dbo].[GetAllUser]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUser]
	@isnewsletter CHAR(1) = null,
	@name  nvarchar(50) = null,
	@published nchar(1) = null,
	@pageSize int = 10,
	@pageIndex int = 1,
	@field nvarchar(100) = 'u.username DESC',
	@total int output      
AS
BEGIN
	DECLARE @select nvarchar(max),
			@Paramlist nvarchar(max);
	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY '+@field+' ) as row,
					u.* ,ld.name as LocationDesc  
					FROM medical_user AS u left join medical_locationdesc ld on u.locationid = ld.mainid WHERE 1=1';
					
	/**@isnewsletter**/
	if(@isnewsletter is not null)
	begin
		set @select = @select + ' and u.isnewsletter = @isnewsletter2 ';
	end
	
	/****@content***/
	if(@name is not null)
	begin
	 set @select = @select + ' and (u.fullname like ''%'+@name+'%'' or  u.username like ''%'+@name+'%'' )';
	end	
	
	/**@published**/
	if(@published is not null)
	begin
		set @select = @select + ' and u.Published = @published2 ';
	end
	
	-- paging
	declare @table nvarchar(max);
	set @table = 'WITH [RESULT](row, id,fullName,username,password,phone,address,email,
				isNewsletter,role,published,postDate,updateDate,token,mobile,locationId,
				LocationDesc ) AS ( ';
	
	-- paging 
	declare @pagingSql nvarchar(max);
	declare @startRow int;
    declare @endRow   int;
	
	set @pagingSql	= 'SELECT * FROM [RESULT] WHERE row between ';
	set @startRow	= (@pageIndex - 1) * @pageSize + 1;
	set @endRow		= @startRow + @pageSize - 1;
    set @pagingSql	= @pagingSql + convert(varchar(10), @startRow);
	set @pagingSql	= @pagingSql + ' AND ' ;
	set @pagingSql	= @pagingSql + convert(varchar(10), @endRow);
	
	declare @sql nvarchar(max);
	set  @sql = ' ';
	
	set @sql = @table + @select + ') ' + @pagingsql;
	
	
	set @Paramlist = N'
						@isnewsletter2 CHAR(1) ,
						@name2  nvarchar(50) ,
						@published2 nchar(1) ';
	exec SP_EXECUTESQL  @sql,
						@Paramlist,
						@isnewsletter2 = @isnewsletter,
						@name2 = @name,
						@published2 = @published
						
	
	declare @Total2 int;
	set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	set @Paramlist = N'
						@isnewsletter2 CHAR(1) ,
						@name2  nvarchar(50) ,
						@published2 nchar(1),
						@Total2 int out';
						
	exec sp_executeSql  @sql,
						@Paramlist, 
						@isnewsletter,
						 @name,
						 @published,
						@Total2 out
	set @total = @Total2;
	print @select
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllLocation]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

/*

	DECLARE @return_value INT
	,@total INT

	EXEC @return_value = [dbo].[sp_GetAllLocation] @langId = 1
	,@published = N'1'
	,@pageSize = 11
	,@pageIndex = 1
	,@total = @total OUTPUT

	SELECT @total AS N'@total'

	SELECT 'Return Value' = @return_value
	GO



*/
CREATE PROCEDURE [dbo].[sp_GetAllLocation]
		@langId INT = null,
		@name VARCHAR(255) = null,
		@published nchar(1) = null,
		@pageSize int = 10,
		@pageIndex int = 1,
		@field nvarchar(100) = 'l.ordering DESC',
		@total int output 
AS
BEGIN
	DECLARE @select nvarchar(max),
			@Paramlist nvarchar(max);
	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,
					l.*,ld.id as subId,ld.langid,ld.name
					 FROM Medical_location l INNER JOIN Medical_locationdesc ld on l.id = ld.mainid WHERE 1=1';
					 
	/*@langId*/
	if(@langId is not null)
	begin
		set @select = @select + ' and ld.langid = @langId2';
	end
	
	/****@content***/
	if(@name is not null)
	begin
	 set @select = @select + ' and ld.Name like ''%'+'+@name2+'+'%''';
	end	
	
	/**@published**/
	if(@published is not null)
	begin
		set @select = @select + ' and l.Published = @published2 ';
	end
	
	declare @table nvarchar(max);
	set @table = 'WITH [RESULT](row, Id, parentid,ordering,postdate,published,updatedate,pathtree,subId,langid,name) AS ( ';
	
	
	-- paging 
	declare @pagingSql nvarchar(max);
	declare @startRow int;
    declare @endRow   int;
	
	set @pagingSql	= 'SELECT * FROM [RESULT] WHERE row between ';
	set @startRow	= (@pageIndex - 1) * @pageSize + 1;
	set @endRow		= @startRow + @pageSize - 1;
    set @pagingSql	= @pagingSql + convert(varchar(10), @startRow);
	set @pagingSql	= @pagingSql + ' AND ' ;
	set @pagingSql	= @pagingSql + convert(varchar(10), @endRow);
	
	declare @sql nvarchar(max);
	set  @sql = ' ';
	
	set @sql = @table + @select + ') ' + @pagingsql;
	
	
	set @Paramlist = N'
						@langId2 INT,
						@name2 VARCHAR(255),
						@published2 nchar(1)';
	exec SP_EXECUTESQL  @sql,
						@Paramlist,
						@langId2 = @langId,
						@name2 = @name,
						@published2 = @published
	
	declare @Total2 int;
	set @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	set @Paramlist = N'
						@langId2 INT,
						@name2 VARCHAR(255),
						@published2 nchar(1),
						@Total2 int out';
						
	exec sp_executeSql @sql,
						@Paramlist, 
						 @langId,
						 @name,
						 @published,
						@Total2 out
	set @total = @Total2;
END


GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllProduct]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================      
-- Author:  <Author,,Name>      
-- Create date: <Create Date,,>      
-- Description: <Description,,>      
-- =============================================    
/*

DECLARE @p7 INT
SET @p7 = 0
EXEC sp_GetAllProduct @langId = 1
	,@name = NULL
	,@published  = 1
	,@pageIndex = 1
	,@pageSize = 100
	,@cateId =Null
	,@Id = Null
	,@hot =  Null
	,@total = @p7 OUTPUT
SELECT @p7

*/
CREATE PROCEDURE [dbo].[sp_GetAllProduct] @langId INT = NULL
	, @name NVARCHAR(max) = NULL
	, @published NCHAR(1) = NULL
	, @cateId VARCHAR(max) = NULL
	, @Id NVARCHAR(max) = NULL
	, @hot VARCHAR(1) = NULL
	, @feature VARCHAR(1) = NULL
	, @post VARCHAR(1) = NULL
	, @userId INT = NULL
	, @pageSize INT = 10
	, @pageIndex INT = 1
	, @field NVARCHAR(100) = 'p.ordering desc'
	, @total INT OUTPUT
AS
BEGIN
	DECLARE @select NVARCHAR(max)
		, @Paramlist NVARCHAR(max);

	SET @select = ' SELECT ROW_NUMBER() OVER (ORDER BY ' + @field + ') as row,      
     p.*,pd.id as subId,pd.langid,pd.title,pd.brief,pd.detail,pd.position,pd.utility,pd.design,pd.pictures,pd.payment,pd.contact
     ,nc.name as categoryDesc,pd.titleurl,nc.nameurl,pd.metatitle,pd.metadescription,pd.metakeyword FROM medical_product p INNER JOIN medical_productdesc  pd ON p.id = pd.mainid  
     left join medical_productcategorydesc nc on p.categoryid=nc.mainid WHERE 1=1';

	/*@langId*/
	IF (@langId IS NOT NULL)
	BEGIN
		SET @select = @select + ' and pd.langid = @langId2 and nc.langid = @langId2';
	END

	/**@published**/
	IF (@published IS NOT NULL)
	BEGIN
		SET @select = @select + ' and p.Published = @published2 ';
	END

	/****@name***/
	IF (@name IS NOT NULL)
	BEGIN
		SET @select = @select + ' and  nameurl =  @name2';
	END

	/*@cateId*/
	IF (@cateId IS NOT NULL)
	BEGIN
		SET @select = @select + ' and p.categoryid  = @cateId2 ';
	END

	/*@Id*/
	IF (@Id IS NOT NULL)
	BEGIN
		SET @select = @select + ' and  titleurl =@Id2 ';
	END

	/*@Hot*/
	IF (@Hot IS NOT NULL)
	BEGIN
		SET @select = @select + ' and  hot =@hot2 ';
	END

	/*@feature*/
	IF (@feature IS NOT NULL)
	BEGIN
		SET @select = @select + ' and feature =@feature2 ';
	END

	/*@post*/
	IF (@post IS NOT NULL)
	BEGIN
		SET @select = @select + ' and post =@post2 ';
	END

	/*@userId*/
	IF (@userId IS NOT NULL)
	BEGIN
		SET @select = @select + ' and userid =@userId2 ';
	END

	-- paging      
	DECLARE @table NVARCHAR(max);

	SET @table = 'WITH [RESULT](row, id,categoryid,image,published,hot,feature,postDate,updateDate,ordering,longitude, latitude, price,area,district,bedroom
	,bathroom,code,status,province,website,post,userid,cost, subId,langid,title,brief,detail,position,utility,design,pictures,payment,contact,categoryDesc,titleurl,nameurl
	,metatitle,metadescription,metakeyword) AS ( ';

	-- paging       
	DECLARE @pagingSql NVARCHAR(max);
	DECLARE @startRow INT;
	DECLARE @endRow INT;

	SET @pagingSql = 'SELECT * FROM [RESULT] WHERE row between ';
	SET @startRow = (@pageIndex - 1) * @pageSize + 1;
	SET @endRow = @startRow + @pageSize - 1;
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @startRow);
	SET @pagingSql = @pagingSql + ' AND ';
	SET @pagingSql = @pagingSql + convert(VARCHAR(10), @endRow);

	DECLARE @sql NVARCHAR(max);

	SET @sql = ' ';
	SET @sql = @table + @select + ') ' + @pagingsql;

	PRINT @sql;

	SET @Paramlist = N'      
		@langId2 INT ,   
		@published2 nchar(1),
		@name2 NVARCHAR(255) ,      
		@cateId2 VARCHAR(1000),
		@Id2 NVARCHAR(255),
		@hot2 VARCHAR(1),
		@feature2 VARCHAR(1),
		@post2 VARCHAR(1),
		@userid2 INT
		';

	EXEC SP_EXECUTESQL @sql
		, @Paramlist
		, @langId2 = @langId
		, @published2 = @published
		, @name2 = @name
		, @cateId2 = @cateId
		, @Id2 = @Id
		, @hot2 = @Hot
		, @feature2 = @feature
		, @post2 = @post
		, @userid2 = @userId

	DECLARE @Total2 INT;

	SET @sql = @table + @select + ') select @Total2= count(row)  from [RESULT]';
	SET @Paramlist = N' 
      @langId2 INT ,    
	  @published2 nchar(1),
      @name2 NVARCHAR(255) ,      
      @cateId2 VARCHAR(1000), 
	  @Id2 NVARCHAR(255),     
	  @hot2 VARCHAR(1), 
	  @feature2 VARCHAR(1), 
	  @post2 VARCHAR(1), 
	  @userid2 INT, 
      @Total2 int out';

	EXEC sp_executeSql @sql
		, @Paramlist
		, @langId
		, @published
		, @name
		, @cateId
		, @Id
		, @Hot
		, @feature
		, @post
		, @userId
		, @Total2 OUTPUT

	SET @total = @Total2;
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdatePathTree]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Congtt
-- Create date: 
-- Description:	
-- =============================================
/*

	EXEC spUpdatePathTree 2,3

*/
CREATE PROCEDURE [dbo].[spUpdatePathTree] @parentId INT = 0
	,@productCategoryId INT = 0
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @idOrdering INT
		,@parentPath VARCHAR(20)
		,@pathTree VARCHAR(20)

	-----------------Neu la danh muc cha thi @parentId=0: @pathTree co dang 0997-1---------------------
	IF (@parentId = 0)
	BEGIN
		SET @idOrdering = (
				SELECT TOP 1 ordering
				FROM medical_productcategory
				WHERE id = @productCategoryId
				)
		SET @pathTree = '.' + (
				SELECT dbo.GetBottomPath(@idOrdering, @productCategoryId)
				)
	END
			-----------------Neu la danh muc con thi @parentId <> 0: @pathTree co dang 0997-1---------------------
	ELSE
	BEGIN
		---------------Get Parent path---------------------
		SET @parentPath = (
				SELECT TOP 1 pathtree
				FROM medical_productcategory
				WHERE id = @parentId
				)
		-----------------Set ordering of id-------------------
		SET @idOrdering = (
				SELECT TOP 1 ordering
				FROM medical_productcategory
				WHERE id = @productCategoryId
				)

		--PRINT @idOrdering
		DECLARE @idPath VARCHAR(20) = (
				SELECT dbo.GetBottomPath(@idOrdering, @productCategoryId)
				)

		--PRINT @idPath
		-----------------Path parent + Path id-------------------
		SET @pathTree = @parentPath + '.' + @idPath
	END

	-----------------Print ket qua kiem tra-------------------
	PRINT '@pathTree ' + @pathTree

	---------------Update column pathtree-------------------
	UPDATE medical_productcategory
	SET pathtree = @pathTree
	WHERE id = @productCategoryId
END

GO
/****** Object:  UserDefinedFunction [dbo].[fc_GetAllChildCateCategory]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--USE [tamsportsnew_udb]  
--GO  
--/****** Object:  UserDefinedFunction [dbo].[fc_GetAllChildNewsCategory]    Script Date: 08/18/2012 23:48:25 ******/  
--SET ANSI_NULLS ON  
--GO  
/*  
select * from fc_GetAllChildNewsCategory(null,null)  
*/  
--SET QUOTED_IDENTIFIER ON  
--GO  
-- =============================================  
-- Author:  <Author,,Name>  
-- Create date: <Create Date,,>  
-- Description: <Description,,>  
-- =============================================  
/*
	select * from [fc_GetAllChildCateCategory] (1,0)

*/

CREATE FUNCTION [dbo].[fc_GetAllChildCateCategory]  
(   
 @IDCategory INT,   
  @unclude_me BIT =0  
)  
RETURNS @tbl_Result TABLE(   
  id       INT,   
  parentid INT )    
AS  
   
BEGIN  
 IF( @unclude_me = 1 )   
        BEGIN   
            INSERT INTO @tbl_Result   
                        (id,   
                         parentid)   
            SELECT id,   
                   parentid   
            FROM  homerepair_catecategory  WITH (nolock)   
            WHERE  id = @IDCategory   
        END   
          
        INSERT INTO @tbl_Result   
                  (id,   
                   parentid)   
      SELECT id,   
             parentid   
      FROM   homerepair_catecategory WITH (nolock)   
      WHERE  parentid = @IDCategory;   
        
      WHILE ( 1 = 1 )   
        BEGIN   
            INSERT INTO @tbl_Result   
                        (id,   
                         parentid)   
            SELECT id,   
                   parentid   
            FROM   homerepair_catecategory WITH (nolock)   
            WHERE  parentid IN (SELECT id   
                                FROM   @tbl_Result)   
                   AND parentid NOT IN (SELECT parentid   
                                        FROM   @tbl_Result);   
  
            IF @@ROWCOUNT <= 0   
              BREAK   
        END   
  RETURN  
END


GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAllChildProductcategory]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[fn_GetAllChildProductcategory] (
	@IDCategory INT
	,@unclude_me BIT = 0
	)
RETURNS @tbl_Result TABLE (
	id INT
	,parentid INT
	)
AS
BEGIN
	IF (@unclude_me = 1)
	BEGIN
		INSERT INTO @tbl_Result (
			id
			,parentid
			)
		SELECT id
			,parentid
		FROM medical_productcategory WITH (NOLOCK)
		WHERE id = @IDCategory
	END

	INSERT INTO @tbl_Result (
		id
		,parentid
		)
	SELECT id
		,parentid
	FROM medical_productcategory WITH (NOLOCK)
	WHERE parentid = @IDCategory;

	WHILE (1 = 1)
	BEGIN
		INSERT INTO @tbl_Result (
			id
			,parentid
			)
		SELECT id
			,parentid
		FROM medical_productcategory WITH (NOLOCK)
		WHERE parentid IN (
				SELECT id
				FROM @tbl_Result
				)
			AND parentid NOT IN (
				SELECT parentid
				FROM @tbl_Result
				);

		IF @@ROWCOUNT <= 0
			BREAK
	END

	RETURN
END

GO
/****** Object:  UserDefinedFunction [dbo].[fuCapitalizeFirstLetter]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Congtt
-- Create date: 
-- Description:	Capitalize First Letter
-- =============================================
CREATE FUNCTION [dbo].[fuCapitalizeFirstLetter] (
	--string need to format
	@string NVARCHAR(200) --increase the variable size depending on your needs.
	)
RETURNS NVARCHAR(200)
AS
BEGIN
	--Declare Variables
	DECLARE @Index INT
		,@ResultString NVARCHAR(200) --result string size should equal to the @string variable size
		--Initialize the variables

	SET @Index = 1
	SET @ResultString = ''

	--Run the Loop until END of the string
	WHILE (@Index < LEN(@string) + 1)
	BEGIN
		IF (@Index = 1) --first letter of the string
		BEGIN
			--make the first letter capital
			SET @ResultString = @ResultString + UPPER(SUBSTRING(@string, @Index, 1))
			SET @Index = @Index + 1 --increase the index
		END
				-- IF the previous character is space or '-' or next character is '-'
		ELSE IF (
				(
					SUBSTRING(@string, @Index - 1, 1) = ' '
					OR SUBSTRING(@string, @Index - 1, 1) = '-'
					OR SUBSTRING(@string, @Index + 1, 1) = '-'
					)
				AND @Index + 1 <> LEN(@string)
				)
		BEGIN
			--make the letter capital
			SET @ResultString = @ResultString + UPPER(SUBSTRING(@string, @Index, 1))
			SET @Index = @Index + 1 --increase the index
		END
		ELSE -- all others
		BEGIN
			-- make the letter simple
			SET @ResultString = @ResultString + LOWER(SUBSTRING(@string, @Index, 1))
			SET @Index = @Index + 1 --incerase the index
		END
	END --END of the loop

	IF (@@ERROR <> 0) -- any error occur return the sEND string
	BEGIN
		SET @ResultString = @string
	END

	-- IF no error found return the new string
	RETURN @ResultString
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetBottomPath]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [dbo].[GetBottomPath]
(
	@ordering int,
	@id int
)
RETURNS nvarchar(20)
AS
BEGIN
	declare @temp int;
	declare @t nvarchar(20);
	set @temp = 1000 - @ordering;
	set @t = REPLACE(STR(@temp, 4), SPACE(1), '0');
	return @t  + '-' + convert(varchar(3), @id);
END


GO
/****** Object:  Table [dbo].[Medical_Banner]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_Banner](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Position] [tinyint] NOT NULL,
	[OutPage] [tinyint] NULL,
	[LinkUrl] [nvarchar](50) NULL,
	[Image] [nvarchar](127) NULL,
	[Name] [nvarchar](100) NULL,
	[Height] [int] NULL,
	[Width] [int] NULL,
	[ClickCount] [int] NULL,
	[Ordering] [int] NOT NULL,
	[Published] [char](1) NULL,
	[PostDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[ArrPageName] [nvarchar](250) NULL,
 CONSTRAINT [PK__Medical_banner__3213E83F7F60ED59] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_Configuration]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_Configuration](
	[Key_Name] [nvarchar](50) NOT NULL,
	[Value_Name] [nvarchar](1000) NULL,
 CONSTRAINT [PK__sd_confi__846621D2182C9B23] PRIMARY KEY CLUSTERED 
(
	[Key_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medical_Location]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Ordering] [int] NULL,
	[PostDate] [datetime2](7) NOT NULL,
	[Published] [char](1) NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[PathTree] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_LocationDesc]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_LocationDesc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MainId] [int] NULL,
	[LangId] [tinyint] NULL,
	[Name] [nvarchar](25) NULL,
 CONSTRAINT [PK__medical___3213E83F60A75C0F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medical_ManagementID]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_ManagementID](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ordering] [int] NULL,
	[PostDate] [datetime] NULL,
	[Published] [char](1) NULL,
	[UpdateDate] [datetime] NULL,
	[Name] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK__sd_count__3213E83F24927208] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_ManagementIDDesc]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_ManagementIDDesc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MainId] [int] NOT NULL,
	[LangId] [tinyint] NOT NULL,
	[Name] [nvarchar](127) NULL,
	[Detail] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medical_Product]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Image] [nvarchar](127) NULL,
	[Published] [char](1) NULL,
	[Hot] [char](1) NULL,
	[Feature] [char](1) NULL,
	[PostDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Ordering] [int] NOT NULL,
	[LongiTude] [nvarchar](50) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Price] [nvarchar](50) NULL,
	[Area] [nvarchar](50) NULL,
	[District] [nvarchar](50) NULL,
	[Bedroom] [int] NULL,
	[Bathroom] [int] NULL,
	[Code] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL,
	[Province] [nvarchar](500) NOT NULL,
	[Website] [nvarchar](50) NULL,
	[Post] [char](1) NULL,
	[Cost] [int] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK_medical_product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_ProductCategory]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_ProductCategory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NULL,
	[Published] [char](1) NULL,
	[Ordering] [int] NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[PathTree] [varchar](50) NULL,
 CONSTRAINT [PK_Medical_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_ProductcategoryDesc]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_ProductcategoryDesc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MainId] [int] NOT NULL,
	[LangId] [tinyint] NOT NULL,
	[Name] [nvarchar](127) NULL,
	[NameUrl] [varchar](127) NULL,
	[Brief] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[BaseImage] [nvarchar](256) NULL,
	[SmallImage] [nvarchar](256) NULL,
	[ThumbnailImage] [nvarchar](256) NULL,
	[MetaTitle] [nvarchar](512) NULL,
	[MetaKeyword] [nvarchar](max) NULL,
	[MetaDecription] [nvarchar](max) NULL,
 CONSTRAINT [PK_medical_productcategorydesc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Medical_ProductDesc]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medical_ProductDesc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MainId] [int] NOT NULL,
	[LangId] [tinyint] NOT NULL,
	[Title] [nvarchar](300) NULL,
	[Brief] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[Require] [nvarchar](max) NULL,
	[Titleurl] [nvarchar](300) NULL,
	[Position] [nvarchar](max) NULL,
	[Utility] [nvarchar](max) NULL,
	[Design] [nvarchar](max) NULL,
	[Pictures] [nvarchar](max) NULL,
	[Payment] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Metadescription] [nvarchar](max) NULL,
	[MetaKeyword] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](max) NULL,
 CONSTRAINT [PK_Medical_ProductDesc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Medical_User]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Medical_User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Phone] [varchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[Email] [varchar](50) NOT NULL,
	[IsNewsletter] [char](1) NULL,
	[Role] [tinyint] NOT NULL,
	[Published] [char](1) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Token] [varchar](32) NULL,
	[Mobile] [varchar](20) NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_medical_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sd_xml]    Script Date: 3/20/2015 4:59:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sd_xml](
	[id] [int] NOT NULL,
	[xmlcontent] [xml] NULL,
 CONSTRAINT [PK__sd_xml__3213E83F3A4CA8FD] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Medical_Banner] ON 

INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (17, 1, 1, N'http://www.google.com', N'Td27BcFn0i.jpg', N'1', NULL, NULL, 1, 6, N'0', CAST(0x0000A20C017EC9E3 AS DateTime), CAST(0x0000A22300E51CB3 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (18, 1, 0, NULL, N'Ht4j7EXn5o.jpg', N'2', NULL, NULL, 1, 5, N'0', CAST(0x0000A20C0180045E AS DateTime), CAST(0x0000A20C0180045E AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (19, 1, 0, NULL, N'm4A3KrNc2k.jpg', N'3', NULL, NULL, 1, 4, N'0', CAST(0x0000A20C01801CD9 AS DateTime), CAST(0x0000A20C01801CD9 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (20, 1, 0, N'4', N'Nk4q7A8WmM.jpg', N'4', NULL, NULL, 1, 3, N'1', CAST(0x0000A20C018038A2 AS DateTime), CAST(0x0000A20C018038A2 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (21, 1, 0, NULL, N't9E2YeMi5f.jpg', N'5', NULL, NULL, 1, 2, N'1', CAST(0x0000A20C01805F54 AS DateTime), CAST(0x0000A20C01805F54 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (22, 1, 0, NULL, N'i1SNp2t5AR.jpg', N'6', NULL, NULL, 1, 1, N'1', CAST(0x0000A20C01807FB8 AS DateTime), CAST(0x0000A20C01807FB8 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (23, 2, 1, N'http://www.google.com', N'Xr5a1F0Mcs.jpg', N'Xe trượt', NULL, NULL, 1, 7, N'1', CAST(0x0000A22400697212 AS DateTime), CAST(0x0000A22400763FBD AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (24, 2, 1, N'httt://www.google.com.vn', N'Gk65Jqa4XQ.jpg', N'Nhà hàng', NULL, NULL, 1, 8, N'1', CAST(0x0000A2240076845E AS DateTime), CAST(0x0000A2240076845E AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (25, 2, 1, N'http://www.google.com', N'p7J6Rrd5KS.jpg', N'Đạp Vịt', NULL, NULL, 1, 9, N'1', CAST(0x0000A2240076B99D AS DateTime), CAST(0x0000A2260098AF75 AS DateTime), N'home')
INSERT [dbo].[Medical_Banner] ([Id], [Position], [OutPage], [LinkUrl], [Image], [Name], [Height], [Width], [ClickCount], [Ordering], [Published], [PostDate], [UpdateDate], [ArrPageName]) VALUES (26, 2, 1, N'http://www.google.com', N't2Z0HrTy9k.jpg', N'Cắm trại', NULL, NULL, 1, 10, N'1', CAST(0x0000A2240076D70E AS DateTime), CAST(0x0000A2240076D70E AS DateTime), N'home')
SET IDENTITY_INSERT [dbo].[Medical_Banner] OFF
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_address_en', N'14 villages, communes Dambri Bao Loc - Lam Dong')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_address_vi', N'Thôn 14 xã Đambri Bảo Lộc - Lâm Đồng')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_address1_en', N'Thôn 14 xã Đambri Bảo Lộc - Lâm Đồng')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_address1_vi', N'Thôn 14 xã Đambri Bảo Lộc - Lâm Đồng')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_company_name_en', N'TRAVEL CORPORATION DAMB''RI')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_company_name_vi', N'CÔNG TY CỔ PHẦN DU LỊCH DAMB''RI')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_description', NULL)
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_email', N'booking@dambritourist.vn')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_fax', N'0633.763.007')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_location', N'634511618223750000Sn83Q.PNG')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_logoFooter', N'635093260234611793c9R4P.png')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_logoHeader', N'635093551279555673Dy6i4.png')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_metakeywords', NULL)
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_phone', N'0633.75.15.17')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_rate', NULL)
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_sitename', N'www.dambritourist.vn')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_skypeid', NULL)
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_time', N'6h00 AM-19h00 PM')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_working', N'Thời gian làm việc')
INSERT [dbo].[Medical_Configuration] ([Key_Name], [Value_Name]) VALUES (N'config_yahooid', N'booking@dambritourist.vn')
SET IDENTITY_INSERT [dbo].[Medical_LocationDesc] ON 

INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (55, 35, 1, N'Việt Nam')
INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (56, 35, 2, N'Viet Nam')
INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (61, 38, 1, N'Singapore')
INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (62, 38, 2, N'Singapore')
INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (63, 39, 1, N'Cập nhật')
INSERT [dbo].[Medical_LocationDesc] ([Id], [MainId], [LangId], [Name]) VALUES (64, 39, 2, N'Cập nhật')
SET IDENTITY_INSERT [dbo].[Medical_LocationDesc] OFF
SET IDENTITY_INSERT [dbo].[Medical_ManagementID] ON 

INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (2, 1, CAST(0x0000A4480186E0C3 AS DateTime), N'1', CAST(0x0000A4480186E0C3 AS DateTime), N'block_baseimage_setSelect_width', N'400')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (3, 2, CAST(0x0000A448018855DD AS DateTime), N'1', CAST(0x0000A448018855DD AS DateTime), N'block_baseimage_setSelect_height', N'230')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (4, 3, CAST(0x0000A448018B3FFC AS DateTime), N'1', CAST(0x0000A448018B3FFC AS DateTime), N'test', N'1')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (5, 4, CAST(0x0000A460017DBA44 AS DateTime), N'1', CAST(0x0000A460017E2AD6 AS DateTime), N'admin_editproductcategory_ProductUploadFolder', N'/resource/upload/Products')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (6, 5, CAST(0x0000A460018676BF AS DateTime), N'1', CAST(0x0000A460018676BF AS DateTime), N'admin_editproductcategory_minwidth', N'165')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (7, 6, CAST(0x0000A46001875D2C AS DateTime), N'1', CAST(0x0000A46001875D2C AS DateTime), N'admin_editproductcategory_minheight', N'330')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (8, 7, CAST(0x0000A46001879C19 AS DateTime), N'1', CAST(0x0000A46001879C19 AS DateTime), N'admin_editproductcategory_maxwidth', N'400')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (9, 8, CAST(0x0000A4600187DF84 AS DateTime), N'1', CAST(0x0000A4600187DF84 AS DateTime), N'admin_editproductcategory_maxheight', N'330')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (10, 9, CAST(0x0000A460018849F6 AS DateTime), N'1', CAST(0x0000A460018849F6 AS DateTime), N'admin_editproductcategory_minwidthbox', N'165')
INSERT [dbo].[Medical_ManagementID] ([Id], [Ordering], [PostDate], [Published], [UpdateDate], [Name], [Value]) VALUES (11, 10, CAST(0x0000A46001887B40 AS DateTime), N'1', CAST(0x0000A460018A0948 AS DateTime), N'admin_editproductcategory_maxheightbox', N'330')
SET IDENTITY_INSERT [dbo].[Medical_ManagementID] OFF
SET IDENTITY_INSERT [dbo].[Medical_ProductCategory] ON 

INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (1, 0, N'1', 5, CAST(0x0000A44401606E01 AS DateTime), CAST(0x0000A457015549FA AS DateTime), N'.0995-1')
INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (2, 0, N'1', 1, CAST(0x0000A444016687D9 AS DateTime), CAST(0x0000A44500BB6400 AS DateTime), N'.0999-2')
INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (3, 2, N'1', 3, CAST(0x0000A4440166A9E7 AS DateTime), CAST(0x0000A4570155238C AS DateTime), N'.0999-2.0997-3')
INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (4, 2, N'1', 2, CAST(0x0000A4440167E79C AS DateTime), CAST(0x0000A446007BB3E5 AS DateTime), N'.0999-2.0998-4')
INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (5, 1, N'1', 6, CAST(0x0000A4440168B8EF AS DateTime), CAST(0x0000A4440168B8EF AS DateTime), N'.0997-1.0994-5')
INSERT [dbo].[Medical_ProductCategory] ([Id], [ParentId], [Published], [Ordering], [PostDate], [UpdateDate], [PathTree]) VALUES (6, 3, N'1', 4, CAST(0x0000A4440168C4CC AS DateTime), CAST(0x0000A4570155313B AS DateTime), N'.0999-2.0997-3.0996-')
SET IDENTITY_INSERT [dbo].[Medical_ProductCategory] OFF
SET IDENTITY_INSERT [dbo].[Medical_ProductcategoryDesc] ON 

INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (1, 1, 1, N'C', N'c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (2, 1, 2, N'c', N'c', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (3, 2, 1, N'phản bộ thầy', N'phan-bo-thay', N'gg', N'<p>
	dddd</p>
', NULL, NULL, NULL, N'1', N'2', N'3')
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (4, 2, 2, N'A', N'a', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (5, 3, 1, N'A1', N'a1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (6, 3, 2, N'A1', N'a1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (7, 4, 1, N'A2', N'a2', N'ggg', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (8, 4, 2, N'A2', N'a2', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (9, 5, 1, N'C1', N'c1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (10, 5, 2, N'C1', N'c1', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (11, 6, 1, N'A11', N'a11', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Medical_ProductcategoryDesc] ([Id], [MainId], [LangId], [Name], [NameUrl], [Brief], [Detail], [BaseImage], [SmallImage], [ThumbnailImage], [MetaTitle], [MetaKeyword], [MetaDecription]) VALUES (12, 6, 2, N'A11', N'a11', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Medical_ProductcategoryDesc] OFF
SET IDENTITY_INSERT [dbo].[Medical_User] ON 

INSERT [dbo].[Medical_User] ([Id], [FullName], [Username], [Password], [Phone], [Address], [Email], [IsNewsletter], [Role], [Published], [PostDate], [UpdateDate], [Token], [Mobile], [LocationId]) VALUES (115, N'Congtt', N'admin', N'f99d8366b35241bd', NULL, NULL, N'truong.thanhcong89@gmail.com', N'0', 1, N'1', CAST(0x0000A20F001037E8 AS DateTime), CAST(0x0000A20F001037E8 AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Medical_User] ([Id], [FullName], [Username], [Password], [Phone], [Address], [Email], [IsNewsletter], [Role], [Published], [PostDate], [UpdateDate], [Token], [Mobile], [LocationId]) VALUES (116, N'Ho&#224;ng Ngọc Thụ (mr)', N'hoangngocthu', N'60a760f9c180f9c700bce846aaa20aa9', NULL, NULL, N'hoangngocthu@gmail.com', N'0', 3, N'1', CAST(0x0000A21D00BC347B AS DateTime), CAST(0x0000A2230080345D AS DateTime), NULL, NULL, NULL)
INSERT [dbo].[Medical_User] ([Id], [FullName], [Username], [Password], [Phone], [Address], [Email], [IsNewsletter], [Role], [Published], [PostDate], [UpdateDate], [Token], [Mobile], [LocationId]) VALUES (117, N'Trương Th&#224;nh C&#244;ng', N'congtt', N'4a7d8a3ed6e79ce1', N'0938511642', NULL, N'truong.thanhcong89@gmail.com', N'1', 3, N'1', CAST(0x0000A21E016CD7C9 AS DateTime), CAST(0x0000A236013C3272 AS DateTime), NULL, N'0938511642', NULL)
SET IDENTITY_INSERT [dbo].[Medical_User] OFF
INSERT [dbo].[sd_xml] ([id], [xmlcontent]) VALUES (1, N'<ConfigCatalog><Configs><Config value="Pages/User/admin_user.ascx" name="user" /><Config value="Pages/User/admin_edituser.ascx" name="edit_user" /><Config value="Pages/home.ascx" name="home" /><Config value="Pages/Config/admin_config.ascx" name="config" /><Config value="Pages/ManagementID/admin_managementid.ascx" name="managementid" /><Config value="Pages/ManagementID/admin_editmanagementid.ascx" name="edit_managementid" /><Config value="Pages/Services/admin_editservices.ascx" name="edit_services" /><Config value="Pages/Services/admin_services.ascx" name="services" /><Config value="Pages/ServicesCategory/admin_editservicescategory.ascx" name="edit_servicescategory" /><Config value="Pages/ServicesCategory/admin_servicescategory.ascx" name="servicescategory" /><Config value="Pages/ProductsCategory/admin_editproductcategory.ascx" name="edit_productcategory" /><Config value="Pages/ProductsCategory/admin_productcategory.ascx" name="productcategory" /><Config value="Pages/Adv/admin_adv.ascx" name="adv" /><Config value="Pages/Adv/admin_editadv.ascx" name="edit_adv" /><Config value="Pages/Partners/admin_partners.ascx" name="partners" /><Config value="Pages/Partners/admin_editpartners.ascx" name="edit_partners" /><Config value="Pages/Banner/admin_banner.ascx" name="banner" /><Config value="Pages/Banner/admin_editbanner.ascx" name="edit_banner" /></Configs></ConfigCatalog>')
INSERT [dbo].[sd_xml] ([id], [xmlcontent]) VALUES (2, N'<ConfigCatalog><Configs><Config value="Pages/home.ascx" name="home" att="Trang Chu" /><Config value="Pages/login.ascx" name="login" att="Trang Ðang nhap" /><Config value="Pages/CategoryManagement/Category.ascx" name="bai-giang" att="Trang bai giang" /><Config value="Pages/Service/service.ascx" name="services" att="Trang dich vu" /><Config value="Pages/Service/servicedetails.ascx" name="servicedetails" att="Trang Chi tiet dich vu" /><Config value="Pages/contact/contact.ascx" name="contact" att="Trang lien he" /></Configs></ConfigCatalog>')
INSERT [dbo].[sd_xml] ([id], [xmlcontent]) VALUES (3, N'<ConfigCatalog><Configs><Config value="Quang cao 1 (575x91)" name="1" att="false" /><Config value="Quang cao 2 (575x91)" name="2" att="false" /><Config value="Quang cao 3 (575x91)" name="3" att="false" /><Config value="Quang cao doi tac... (575x91)" name="4" att="false" /></Configs></ConfigCatalog>')
INSERT [dbo].[sd_xml] ([id], [xmlcontent]) VALUES (4, N'<ConfigCatalog><Configs><Config value="Banner chính (993x286)" name="1" att="false" /></Configs><Configs><Config value="Block 4 Hình (993x286)" name="2" att="false" /></Configs></ConfigCatalog>')
ALTER TABLE [dbo].[Medical_Banner] ADD  CONSTRAINT [DF__Medical_banner__position__014935CB]  DEFAULT ((1)) FOR [Position]
GO
ALTER TABLE [dbo].[Medical_Banner] ADD  CONSTRAINT [DF__Medical_banner__clickcou__023D5A04]  DEFAULT ((1)) FOR [ClickCount]
GO
ALTER TABLE [dbo].[Medical_Banner] ADD  CONSTRAINT [DF__Medical_banner__publishe__03317E3D]  DEFAULT ('0') FOR [Published]
GO
ALTER TABLE [dbo].[Medical_Location] ADD  DEFAULT ((0)) FOR [ParentId]
GO
ALTER TABLE [dbo].[Medical_Location] ADD  DEFAULT ('0') FOR [Published]
GO
ALTER TABLE [dbo].[Medical_Location] ADD  DEFAULT ('1') FOR [PathTree]
GO
ALTER TABLE [dbo].[Medical_ManagementIDDesc] ADD  DEFAULT ((1)) FOR [LangId]
GO
ALTER TABLE [dbo].[Medical_Product] ADD  CONSTRAINT [DF_medical_product_status]  DEFAULT ((0)) FOR [Code]
GO
ALTER TABLE [dbo].[Medical_ProductCategory] ADD  CONSTRAINT [DF_medical_productcategory_pathtree]  DEFAULT ((1)) FOR [PathTree]
GO
ALTER TABLE [dbo].[Medical_ProductDesc] ADD  CONSTRAINT [DF__medical_musicde__langi__6754599E]  DEFAULT ((1)) FOR [LangId]
GO
ALTER TABLE [dbo].[Medical_User] ADD  CONSTRAINT [DF_medical_User_IsNewsletter]  DEFAULT ((0)) FOR [IsNewsletter]
GO
ALTER TABLE [dbo].[Medical_User] ADD  CONSTRAINT [DF_medical_User_Role]  DEFAULT ((1)) FOR [Role]
GO
ALTER TABLE [dbo].[Medical_User] ADD  CONSTRAINT [DF_medical_User_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Medical_User] ADD  CONSTRAINT [DF_medical_User_PostDate]  DEFAULT (getdate()) FOR [PostDate]
GO
ALTER TABLE [dbo].[Medical_User] ADD  CONSTRAINT [DF_medical_User_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
USE [master]
GO
ALTER DATABASE [db] SET  READ_WRITE 
GO

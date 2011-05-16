/****** Object:  StoredProcedure [dbo].[SP_SELECT_COMPANY]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date:	18-09-2008
	Purpose; Select all Company Information
	Select * from tblCompany
*/

CREATE PROC [SP_SELECT_COMPANY]

AS
BEGIN
	SELECT * FROM TBLCOMPANY	WHERE ISNULL(ISDELETE,0) <> 1 order by CompanyName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_COMPANY]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By : Md. Rokanuzzaman Sikder
		Date       : 18/09/2008
		Purpose	   : Update Company Information
		SELECT * FROM TBLCOMPANY
*/

CREATE PROC [SP_UPDATE_COMPANY]
(
	@COMID			INT,
	@COMPANYNAME	VARCHAR(70),
	@ADDRESS		VARCHAR(100),
	@PHONE			VARCHAR(20),
	@WEBSITE		VARCHAR(70),
	@EMAIL			VARCHAR(40),
	@ISACTIVE		BIT
)
AS

BEGIN TRANSACTION
	
	BEGIN TRY
	
		UPDATE TBLCOMPANY SET
							 COMPANYNAME= @COMPANYNAME,
							 ADDRESS	 = @ADDRESS,
							 PHONE		 = @PHONE,
							 WEBSITE	 = @WEBSITE,
							 EMAIL       = @EMAIL,
							 ISACTIVE	 = @ISACTIVE
		WHERE COMID=@COMID	

		COMMIT TRANSACTION
		RETURN 0
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1
	END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_COMPANY]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By: Md. Rokanuzzaman Sikder
		Date	  : 18/09/2008

*/

CREATE PROC [SP_DELETE_COMPANY]
(
	@COMID		INT	
)
AS
BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE TBLCOMPANY SET
						 ISDELETE=1
	WHERE COMID=@COMID


COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH

ROLLBACK TRANSACTION
RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LOADIMAGE]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LOADIMAGE]
	-- Add the parameters for the stored procedure here
(	
	@unitID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select iconName from tblUnits where unitID=@unitID and comID=@comID
END
GO
/****** Object:  StoredProcedure [dbo].[sp_saveBookMark]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********** Created By Rokan **************
	     Date: 21-04-2008

*******************************************/
	     
--exec sp_saveBookMark 25.00021,-100.326522325,4,1

CREATE PROCEDURE [sp_saveBookMark]
(
@Lat varchar(50),
@Lng varchar(50),
@zoom int,
@uID int
)

AS
SET NOCOUNT ON

DECLARE @CurrentError int

    -- start transaction
    BEGIN TRANSACTION

    -- delete record if exists
    if exists (select * from tblbookMark where uID=@uID)
    begin
	delete from TBLBOOKMARK WHERE UID=@UID
	select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    end


    -- create a Asset Type
    INSERT INTO TBLBOOKMARK (bLat,bLng,zoomLevel,uID)
    VALUES(@Lat,@Lng,@zoom,@uID)
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[spVehicleSetup]    Script Date: 09/09/2009 15:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	ALTER  by: Rokan
	Date:27-03-2008
	select * from tblunits
*/

CREATE PROCEDURE [spVehicleSetup]
(
	@unitID		int,
	@unitName	varchar(50),
	@licenseID	varchar(50),
	@driverName	varchar(50),
	@VIN		varchar(30),
	@modelID	int,
	@unitpDate	datetime,
	@levelArmor	varchar(10),
	@wtint		varchar(10),
	@commonPackage	varchar(6),
	@unitColor	varchar(20),
	@keyCode	varchar(50),
	@fuelType	int,
	@purchaseLocation varchar(50),
	@purchaseDate	datetime,
	@unitCost	decimal,
	@unitCategory	int,
	@IED		varchar(50),
	@msgSetup	int,
	@iconName	varchar(50),
	@comID		int,
	@patternID 	int,
	@otherInfo  varchar(200),
	@retVal		int out

)
as
set nocount off
declare @error int
begin transaction


Insert INTO TBLunitS(UnitName,LicenseID,deviceID,driverName,VIN,modelID,unitpurchasedate,levelarmor,wtint,package,unitcolor,
keycode,unitfueltype,devicepurchaselocation,devicepurchasedate,unitcost,typeid,counterIED,iconName,comID,patternID,otherInfo)

values(@unitName,@licenseID,@unitID,@driverName,@VIN,@modelID,@unitpDate,@levelArmor,@wtint,@commonPackage,@unitColor,
@keyCode,@fuelType,@purchaseLocation,@purchaseDate,@unitCost,@unitCategory,@IED,@iconName,@comID,@patternID,@otherInfo)

set	@retVal=(select max(unitid) from tblunits)
select @error=@@error  IF @error != 0 BEGIN GOTO ERROR_HANDLER END

Commit Transaction

    Set NoCOUNT OFF
    Return 0
    
ERROR_HANDLER:
	ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @error
GO
/****** Object:  StoredProcedure [dbo].[SP_CLEAR_DATA]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_CLEAR_DATA]
	-- Add the parameters for the stored procedure here
(
	@UnitID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblUnits SET isDelete=1 WHERE UnitID=@UnitID
END
GO
/****** Object:  StoredProcedure [dbo].[spVehicleUpdate]    Script Date: 09/09/2009 15:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	ALTER  by: Rokan
	Date:27-03-2008
	select * from tblassets
*/
/* Modify by Safiq on 30.06.2008*/

CREATE PROCEDURE [spVehicleUpdate]
(
	@unitID		varchar(50),
	@UnitName        varchar(80),
	@licenseID	varchar(50),
	@driverName	varchar(50),
	@VIN		varchar(30),
	@modelID	int,
	@unitpDate	datetime,
	@levelArmor	varchar(10),
	@wtint		varchar(10),
	@commonPackage	varchar(6),
	@unitColor	varchar(20),
	@keyCode	varchar(50),
	@fuelType	int,
	@purchaseLocation varchar(50),
	@purchaseDate	datetime,
	@unitCost	decimal,
	@unitCategory	int,
	@IED		varchar(50),
	@msgSetup	int,
	@iconName	varchar(50),
	@otherInfo  varchar(200),
	@patternID	int


)
as
begin transaction
set nocount off
begin try



UPDATE TBLUNITS SET
		  unitName= @UnitName,
		  VIN=@VIN,
		  ModelID=@ModelID,
		  unitpurchaseDate=@unitpDate,
		  unitColor=@unitColor,
		  keyCode=@keyCode,
		  unitFuelType=@fuelType,
		  DevicePurchaseLocation=@purchaseLocation,
		  DevicePurchaseDate=@purchaseDate,
		  unitCost=@unitCost,
		  levelArmor=@levelArmor,
		  wtint=@wtint,
		  package=@commonPackage,
		  counterIED=@IED,
		  TYPEID=@unitCategory,
		  DRIVERNAME=@DRIVERNAME,
		  LICENSEID=@LICENSEID,
	      ICONNAME=@ICONNAME,
		  otherInfo=@otherInfo,
		  patternID=@patternID
		  WHERE UNITID=@UNITID
	


Commit Transaction

    Set NoCOUNT OFF
    Return 0
end try

begin catch
  
ROLLBACK TRANSACTION
SET NOCOUNT OFF
RETURN -1

end catch
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_GEOFENCE_NAME]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: Safiqur Rhaman
	DATE		: 20-10-2008
	PURPOSE		: SELECT GEOFENCE DATA

*/

CREATE PROC [SP_SELECT_GEOFENCE_NAME]
(
	@COMID	INT
)
AS
BEGIN

	SELECT ID,NAME FROM TBLGEOFENCE WHERE comID=@COMID order by Name asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_DEVICEID]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_DEVICEID]
	-- Add the parameters for the stored procedure here
(
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select UnitID,UnitName from tblUnits a inner join tblUnitType t on t.typeID=a.typeID where t.comID=@comID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_UNITS_PATTERN]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 20-10-2008
	PURPOSE		: UPDATE UNITS PATTERN
*/

CREATE PROC [SP_UPDATE_UNITS_PATTERN]
(
	@UNITID		INT
)
AS
BEGIN TRANSACTION

BEGIN TRY
		
	UPDATE TBLUNITS SET 
					ISACTIVEPATTERN=1,
					PMAINTAINANCE=GETDATE()
	WHERE UNITID=@UNITID

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	
	ROLLBACK TRANSACTION
	RETURN -1

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNASSIGN_UNITS_3]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: Safiqur Rhaman
	DATE		: 20-10-2008
	PURPOSE		: SELECT Unassigned Unit DATA

*/

CREATE PROC [SP_SELECT_UNASSIGN_UNITS_3]
(
	@COMID	INT
	
)
AS
BEGIN

	
	
	select * from tblunittype where comID=@COMID
	select * from tblunits where comID=@COMID and unitID not in (select unitID from tblunitwiseRules where comid=@COMID)


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_IMAGE]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_IMAGE]
	-- Add the parameters for the stored procedure here
(
	@UnitID int,
	@comID int
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select iconName from tblUnits where unitID=@UnitID and comID=@comID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ASSIGN_RULES_INSERT]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

CREATE   proc [SP_ASSIGN_RULES_INSERT]
      (    @UNITID int,
           @RULESID int,
           @GEOID int,
           @Email varchar(100),
		   @SUBJECT varchar(200),
		   @DES	varchar(200),
           @ISACTIVE int
)
as
Insert INTO tblUnitWiseRules 
(UnitID, RulesID,GeofenceID,Email, Subject, Description,isActive) 
Values (@UNITID,@RULESID,@GEOID,@Email,@SUBJECT,@DES,@ISACTIVE )
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MAXUNITID]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_MAXUNITID]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select max(unitid) from tblunits
END
GO
/****** Object:  StoredProcedure [dbo].[spUnitTypeAdd]    Script Date: 09/09/2009 15:13:57 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- created By shalin, 28 mar 08


create  PROCEDURE [spUnitTypeAdd]

(
@typeName	NVarChar(50),
@comID int 
)

AS
SET NOCOUNT ON

DECLARE @CurrentError int

DECLARE @typeID smallint
SET @typeID=(SELECT isnull(MAX(typeID),0)+1 FROM tblUnitType)

    -- start transaction
    BEGIN TRANSACTION

    -- create a Unit Type
    INSERT INTO tblUnitType (typeID, typeName, comID)
    VALUES(@typeID, @typeName, @comID)
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[spAssetTypeAdd]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- created By shalin, 28 mar 08


CREATE PROCEDURE [spAssetTypeAdd]

(
@typeName	NVarChar(50),
@comID int 
)

AS
SET NOCOUNT ON

DECLARE @CurrentError int

DECLARE @typeID smallint
SET @typeID=(SELECT isnull(MAX(typeID),0)+1 FROM tblAssetType)

    -- start transaction
    BEGIN TRANSACTION

    -- create a Asset Type
    INSERT INTO tblAssetType (typeID, typeName, comID)
    VALUES(@typeID, @typeName, @comID)
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SECURITYQUESTION]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SECURITYQUESTION]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *FROM TBLSECURITYQUESTION
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_TREEcOLOR_2]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_TREEcOLOR_2]
(
	@unitID int
)
AS
BEGIN

select  isnull(rulesID,0) as rulesID from tblunitwiserules where unitid=@unitID and isActive=1;

select  isnull(geofenceid,0) as geofenceid from tblunitwiserules where unitid=@unitID and isGeofenceActive=1

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UnitType_Add]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [sp_UnitType_Add] 
	@typeName	varchar(50),
	@comID		int
AS
BEGIN
	
		insert into tblUnitType(typeName, comID)
						values(@typeName, @comID)

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	return 0
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_TREEcOLOR_4]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_TREEcOLOR_4]
(
	@ComID int,
	@ID int
)
AS
BEGIN

select centerLat,centerLng,radius from tblGeofence where comID=@ComID and ID=@ID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_UNIT]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

create   proc [SP_UPDATE_UNIT]
      (     @PatternID int,
           @UnitID int
)
as
update tblunits 
set patternID=@PatternID,
	isActivePattern = 0
where unitID=@UnitID
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_GEOFENCE_DATA]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_GEOFENCE_DATA]
	-- Add the parameters for the stored procedure here
	
(
	@COMID int,
	@ID int,
	@UNITID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM TBLGEOFENCE where comID=@COMID
	AND ID=@ID;
	SELECT * FROM  TBLUNITWISERULES where unitid=@UNITID
	

END
GO
/****** Object:  StoredProcedure [dbo].[SP_RULES_DATA_UPDATE]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman
-- CREATE date: 01.11.2008
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_RULES_DATA_UPDATE]
( 
	   @UNITID int,
	   @RULESID int,
	   @GEOID int,
	   @EMAIL varchar(100),
	   @SUBJECT varchar(200),
	   @DES	varchar(200),
	   @ISACTIVE int
	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Update tblUnitWiseRules set RulesID=@RULESID,GeofenceID=@GEOID,
Email=@EMAIL,Subject=@SUBJECT,Description=@DES,isActive=@ISACTIVE where UnitID=@UNITID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_OUTSIDEMAIL_STATUS]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_SELECT_OUTSIDEMAIL_STATUS]
	-- Add the parameters for the stored procedure here
(
	@UID int
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ISOUTSIDEMAIL as STATUS from tblunitwiserules where UNITID= @UID
	AND GEOFENCEID != 0 
	AND ISGEOFENCEACTIVE = 1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdatePolice]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
create proc [sp_UpdatePolice]
(
	@uID int,
	@Email	VARCHAR(200),
	@Subject VARCHAR(200),
	@isActive bit
)
AS
BEGIN TRANSACTION

--DECLARE @UID INT

	UPDATE tblPolice SET
			Email=@Email,
			Subject=@Subject,
			isActive=@isActive
			
	WHERE UnitID=@uID

	
COMMIT TRANSACTION
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_ASSIGNED_UNITS]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: Safiqur Rhaman
	DATE		: 30-10-2008
	PURPOSE		: SELECT Assigned Unit DATA

*/

CREATE PROC [SP_SELECT_ASSIGNED_UNITS]
(
	@RULESID INT
)
AS
BEGIN

	
select tblunitwiseRules.unitID,unitName from 
tblunitwiseRules join tblunits on 
(tblunitwiseRules.unitID=tblunits.unitID)
where tblunitwiseRules.unitID in 
(select unitid from tblunitwiserules where Rulesid= @RULESID)


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNITS_Rules_Data]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: Safiqur Rhaman
	DATE		: 30-10-2008
	PURPOSE		: SELECT Unassigned Unit DATA

*/

CREATE PROC [SP_SELECT_UNITS_Rules_Data]
(
	@UNITID	INT
	
)
AS
BEGIN

	select email,subject,description,isActive,geofenceID from tblunitwiserules where unitid =@UNITID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_RULES_DATA_DELETE]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman
-- CREATE date: 01.11.2008
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_RULES_DATA_DELETE]
( 
	@UNITID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Delete tblUnitWiseRules where UnitID=@UNITID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_CONTACT_INFO]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_INSERT_CONTACT_INFO]
(
	@UNAME  Varchar(50),
	@Email  Varchar(50),
	@DPT	Varchar(50),
	@DES	Varchar(500),
	@IMG	Varchar(50)
)
 
AS
BEGIN TRANSACTION

BEGIN TRY

INSERT INTO TBLCONTACTFORM 
(UserName,Email,DepartMent,Description,UploadImage)
VALUES
(@UNAME,@Email,@DPT,@DES,@IMG)

--RETURN 0
COMMIT TRANSACTION

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION
--RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_ADDVIPERACCOUNT]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman
-- Create date: 15.11.2008
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_ADDVIPERACCOUNT] 
(
	@FNAME	varchar(50),
	@LNAME	varchar(50),
	@INI	varchar(10),
	@COM	varchar(100),
	@SADDRESS varchar(200),
	@APT	varchar(50),
	@CITY	varchar(20),
	@STATE  varchar (20),
	@ZIP	varchar (20),
	@COUNTRY varchar(20),
	@HPHONE	 varchar(15),
	@OPHONE	 varchar(15),
	@CPHONE	 varchar(15),
	@EMAIL	 varchar(100),
	@DOB	datetime,
	@SQUESTION varchar(50),
	@SANS	varchar(50) 
)
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO TBLVIPERACCOUNT
	(FirstName,LastName,Initial,Company,CompanyAddress,Apt,
	 City,State,PostalCode,Country,H_Phone,O_Phone,C_Phone,Email,DoB,S_question,S_answer)
    Values(@FNAME,@LNAME,@INI,@COM,@SADDRESS,@APT,@CITY,@STATE,@ZIP,@COUNTRY,@HPHONE,@OPHONE,@CPHONE,@EMAIL,@DOB,@SQUESTION,@SANS)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateRules]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [sp_CreateRules]
(
@UnitID int, 
@RulesID int, 
@GeoID int , 
@Email varchar(100), 
@subject varchar(200),
@Description varchar(200),
@isActive bit 
)
 AS
BEGIN TRANSACTION
	SET  NOCOUNT ON

Insert INTO tblUnitWiseRules (UnitID, RulesID,GeofenceID, Email, Subject, Description,isActive)
Values (@UnitID , @RulesID, @GeoID , @Email , 
@subject , @Description,@isActive )


COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[sp_updateGeofenceUnitWise]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [sp_updateGeofenceUnitWise]
(
	@GEOFENCEID	VARCHAR(10),
	@COMID		INT,
	@UNITID		INT,
	@NAME		VARCHAR(70),
	@EMAIL		VARCHAR(200),
	@CENTERLAT	VARCHAR(25),
	@CENTERLNG	VARCHAR(25),
	@RADIUS		VARCHAR(25),	
	@ISACTIVE	BIT
)

AS
DECLARE @GID	INT

BEGIN  TRANSACTION

BEGIN TRY

	UPDATE 	TBLGEOFENCE SET
				NAME=@NAME,
				CENTERLAT=@CENTERLAT,
				CENTERLNG=@CENTERLNG,
				RADIUS=@RADIUS,
				EMAIL=@EMAIL,
				ISACTIVE=@ISACTIVE
	WHERE ID=@GEOFENCEID AND COMID=@COMID

	
	UPDATE TBLUNITWISERULES SET
					GEOFENCEID=@GEOFENCEID,
					ISGEOFENCEACTIVE=@ISACTIVE
	WHERE UNITID=@UNITID 
	
	

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_UNIT_TYPE]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By: Safiqur Rhaman
		Date	  : 20/09/2008

*/

CREATE PROC [SP_DELETE_UNIT_TYPE]
(
	@typeID	INT	
)
AS
BEGIN TRANSACTION

BEGIN TRY
	DELETE TBLUNITTYPE where typeID = @typeID


COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH

ROLLBACK TRANSACTION
RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_UNIT_TYPE]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By : Safiqur Rhaman
		Date       : 21/09/2008
		Purpose	   : Update Unit Model
		
*/

CREATE PROC [SP_UPDATE_UNIT_TYPE]
(
	@typeName		VARCHAR(50),
	@comID			INT,
	@typeID			INT
	
)
AS

BEGIN TRANSACTION
	
	BEGIN TRY
	
		UPDATE TBLUNITTYPE 
		SET typeName = @typeName, comID = @comID
		WHERE typeID = @typeID

		COMMIT TRANSACTION
		RETURN 0
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1
	END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_UNITTYPEADD]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Alterd By shalin, 28 mar 08


CREATE  PROCEDURE [SP_UNITTYPEADD]

(
@typeName	NVarChar(50),
@comID int 
)

AS


DECLARE @CurrentError int

DECLARE @typeID smallint
SET @typeID=(SELECT isnull(MAX(typeID),0)+1 FROM tblUnitType)

    -- start transaction
    BEGIN TRANSACTION

    -- Alter a Unit Type
    INSERT INTO tblUnitType (typeID, typeName, comID)
    VALUES(@typeID, @typeName, @comID)
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    

    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[SP_DDL_SELECT_COMPANY]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date	  :22/09/2008
	Purpose	  : Select CompanY for DropDownList

*/

CREATE PROC		[SP_DDL_SELECT_COMPANY]
(
	@COMID		INT
)

AS

BEGIN
	
	SELECT * FROM TBLCOMPANY	WHERE ISNULL(ISDELETE,0)<> 1 AND COMID=@COMID order by 
companyName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_NOTLISTEDGROUP]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,> exec SP_SELECT_NOTLISTEDGROUP 1,7
-- =============================================
CREATE PROCEDURE [SP_SELECT_NOTLISTEDGROUP] 
	-- Add the parameters for the stored procedure here
(
	@unitID int,
	@comID int
	
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select grpID,grpName from tblgroup where grpID not in (select groupID from tblgroupwiseunit 
where unitID=@unitID) and tblgroup.comID=@comID and ISNULL(ISDELETE,0) <> 1 order by grpName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LOADLIST]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LOADLIST]
	-- Add the parameters for the stored procedure here
(
	@comID int,
	@unitID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select grpID,grpName from tblgroup g inner join tblgroupwiseunit gu on gu.groupID=g.grpID 
and g.comID=@comID and gu.unitID=@unitID and ISNULL(ISDELETE,0) <> 1 order by grpName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_USERGROUP]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By:		Md. Rokanuzzaman Sikder
	Date	  : 20/09/2008
	Purpose	  : Update User Group 
*/

CREATE PROC [SP_UPDATE_USERGROUP]
(
	@GROUPID		INT,
	@GROUPNAME		VARCHAR(100)
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE	TBLGROUP	SET
						GRPNAME=@GROUPNAME
	WHERE  GRPID=@GROUPID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_USERGROUP]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date	  : 20-09-2008
	Purpose	  :	Select User Group
*/

CREATE PROC [SP_SELECT_USERGROUP]

AS
BEGIN

	SELECT GRPID,GRPNAME,COMPANYNAME,TBLGROUP.ISDELETE,TBLGROUP.COMID AS COMID FROM TBLGROUP INNER JOIN TBLCOMPANY
	ON TBLGROUP.COMID=TBLCOMPANY.COMID WHERE ISNULL(TBLGROUP.ISDELETE,0)<> 1 ORDER BY GRPNAME ASC

END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USERGROUP]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY: MD. ROKANUZZAMAN SIKDER
	DATE	  : 20/09/2008
	PURPOSE	  : DELETE THE USER GROUP, ONLY CHANGE THE ISDELETE FLAG
	
*/

CREATE PROC	[SP_DELETE_USERGROUP]
(
	@GROUPID		INT
)

AS

BEGIN TRANSACTION

BEGIN TRY

	UPDATE TBLGROUP	SET
					ISDELETE=1
	WHERE GRPID=@GROUPID

	COMMIT TRANSACTION
	RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_GROUPLIST]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_GROUPLIST]
	-- Add the parameters for the stored procedure here
(
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select grpID,grpName from tblgroup where comID = @comID and ISNULL(ISDELETE,0) <> 1 order by grpName asc 
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_USERGROUP]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created By: Md. Rokanuzzaman Sikder
	Date	  : 20-09-2008
	Purpose   : Create User Group
	SELECT * FROM TBLGROUP
*/

CREATE PROC [SP_CREATE_USERGROUP]
(
	@GROUPNAME		VARCHAR(100),
	@COMID			INT
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	INSERT INTO TBLGROUP(GRPNAME,COMID)	
	VALUES(@GROUPNAME,@COMID)

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DDL_SELECT_USERGROUP]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date	  :22/09/2008
	Purpose	  : Select USER GROUP for DropDownList
	SELECT * FROM TBLGROUP

*/

CREATE PROC		[SP_DDL_SELECT_USERGROUP]
(
	@COMID		INT
)

AS

BEGIN
	
	SELECT GRPID,GRPNAME FROM TBLGROUP	WHERE ISNULL(ISDELETE,0)<> 1 AND GRPNAME <> 'Administrator' AND COMID=@COMID order 
by GRPNAME asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_NOT_USER_GROUP]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>

-- =============================================
CREATE PROCEDURE [SP_SELECT_NOT_USER_GROUP]
(
	@SchemeID	VARCHAR(50),
	@comID		varchar(20)
)
AS
BEGIN
select * from tblgroup where grpID not in (select groupID from tbluser
where uid in (select userid from tbluserwisescheme where schemeID=@SchemeID and comID=@comID) and comid=@comid)
and ISNULL(ISDELETE,0) <> 1 and comid=@comid order by grpName asc

END




set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_USER_GROUP]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_USER_GROUP]
(
	@SchemeID VARCHAR(50),
	@ComID varchar(50)
)
AS
BEGIN
select * from tblgroup where grpID in (select groupID from tbluser 
where uid in (select userid from tbluserwisescheme where schemeID=@SchemeID)) and comID=@ComID and ISNULL(ISDELETE,0) <> 1 order by grpName asc

END


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_USERGROUP_COMPANYWISE]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date	  : 20-09-2008
	Purpose	  :	Select User Group
*/

CREATE PROC [SP_SELECT_USERGROUP_COMPANYWISE]
(
	@COMID		INT
)
AS
BEGIN

	SELECT * FROM TBLGROUP WHERE ISNULL(ISDELETE,0)<>1 AND COMID=@COMID order by grpName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LISTEDGROUP]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LISTEDGROUP]
	-- Add the parameters for the stored procedure here
(
	@unitID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select grpID,grpName from tblgroup where grpID in (select groupID from tblgroupwiseunit 
where unitID=@unitID) and tblgroup.comID=@comID and ISNULL(ISDELETE,0) <> 1 order by grpName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_FUEL]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_FUEL]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ID, fuelType from tblFuel order by fuelType asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_UPDATE_UNIT_STATUS]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 05 - 03 - 2009
	PURPOSE		: TO INSERT/UPDATE UNIT STATUS
	Select * from tblunitstatus
*/

CREATE PROC [SP_INSERT_UPDATE_UNIT_STATUS]
(
	@UNITID			INT,
	@DEVICEID		VARCHAR(10),
	@STATUSCODE		VARCHAR(10),
	@COMID			INT
)

AS

BEGIN TRANSACTION
BEGIN TRY

		IF EXISTS (SELECT * FROM TBLUNITSTATUS WHERE UNITID = @UNITID AND COMID = @COMID AND DEVICEID = @DEVICEID)
		BEGIN
	
			UPDATE TBLUNITSTATUS SET STATUSCODE = @STATUSCODE
			WHERE UNITID = @UNITID AND COMID = @COMID AND DEVICEID = @DEVICEID			

		END

		ELSE 
		BEGIN

			INSERT INTO TBLUNITSTATUS(UNITID,DEVICEID,STATUSCODE,COMID)	
			VALUES(@UNITID,@DEVICEID,@STATUSCODE,@COMID)

		END
		
	
		

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	SELECT ERROR_MESSAGE()
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DDL_UNITTYPE]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 22-09-2008
*/

CREATE PROC [SP_DDL_UNITTYPE]
(
	@COMID		INT
)
AS

BEGIN
	
	SELECT TYPEID,TYPENAME FROM TBLUNITTYPE WHERE COMID=@COMID order by TYPENAME asc
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_NOTLISTEDUNITGROUP]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_NOTLISTEDUNITGROUP]
	-- Add the parameters for the stored procedure here
(	
	@uID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
--	SELECT TYPEID,TYPENAME FROM TBLUNITTYPE WHERE TYPEID NOT IN 
--	(SELECT UNITGROUPID FROM  tblUserWiseUnitCat WHERE COMID=@COMID AND UID=@UID) AND 


	select typeID,typeName from tblunittype where typeid not in (select distinct t.typeID from 
tblunittype t inner join tblunits u on  t.typeID=u.TypeID and u.unitID in (select unitID from 
tblunituserwise where uID=@uID)) and comID=@comID and ISNULL(ISDELETE,0) <> 1 order by typeName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LISTEDUNITGROUP]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LISTEDUNITGROUP]
	-- Add the parameters for the stored procedure here
(
	@uID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--select distinct t.typeID,t.TypeName from tblunittype t inner join tblunits u on  
--t.typeID=u.TypeID and u.unitID in (select unitid from tblUnitUserWise where uID=@uID) and 
--t.comID=@comID order by t.TypeName
	--select typeID,typeName from tblUnitType inner join tblUserWiseUnitCat
--on unitGroupID=TypeID and uID=@uID and tblUnitType.comID =@comID and ISNULL(ISDELETE,0) <> 1
	
--	SELECT TYPEID,TYPENAME FROM TBLUNITTYPE WHERE TYPEID IN 
--	(SELECT UNITGROUPID FROM  tblUserWiseUnitCat WHERE COMID=@COMID AND UID=@UID) AND 

	
	select typeID,typeName from tblunittype where typeid in 
	(select distinct t.typeID from tblunittype t inner join tblunits u 
	on  t.typeID=u.TypeID and u.unitID in (select unitID from tblunituserwise 
	where uID=@uID)) and comID=@comID and ISNULL(ISDELETE,0) <> 1 order by typeName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_COMPANY_UNIT_TYPE]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Alterd By Safiqur Rhaman
-- 07.10.2008

CREATE PROCEDURE [SP_SELECT_COMPANY_UNIT_TYPE]
(
	@COMID int
)


AS
SET NOCOUNT ON

DECLARE @CurrentError int


    BEGIN TRANSACTION

    SELECT * FROM TBLUNITTYPE WHERE comID = @COMID order by typeName asc
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNIT_TYPE]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Alterd By Safiqur Rhaman
-- 21.9.2008

CREATE  PROCEDURE [SP_SELECT_UNIT_TYPE]



AS
SET NOCOUNT ON

DECLARE @CurrentError int

--DECLARE @typeID smallint
--SET @typeID=(SELECT isnull(MAX(typeID),0)+1 FROM tblUnitType)

    -- start transaction
    BEGIN TRANSACTION

    SELECT typeID,typeName,TBLUNITTYPE.comID,tblcompany.CompanyName as ComName from TBLUNITTYPE
	join tblcompany on(TBLUNITTYPE.comID=tblcompany.comID) 
	where ISNULL(tblcompany.ISDELETE,0) <> 1 order by TYPENAME ASC
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_LOGINFAILURE_MSG]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--use AlopekDB

/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	PURPOSE		: STORE LOGIN FAILURE MESSAGE\
	SELECT * FROM TBLLOGINERROR
*/

CREATE PROC [SP_INSERT_LOGINFAILURE_MSG]
(
	@COMID			INT,
	@COMNAME		VARCHAR(100),
	@LOGINNAME		VARCHAR(100),
	@LOGINERRORMSG	VARCHAR(200)
)

AS
BEGIN TRANSACTION
BEGIN TRY

	INSERT INTO TBLLOGINERROR(COMID,COMNAME,USERNAME,LOGINERRORMSG,TIMESTAMP)
	VALUES(@COMID,@COMNAME,@LOGINNAME,@LOGINERRORMSG,GETDATE())

COMMIT TRANSACTION
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATEUNITMODEL]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created By: Safiq
	Date:	18th Sep 2008
*/

CREATE      proc [SP_CREATEUNITMODEL]
(
	
	@unitModel	varchar(50),
	@description varchar(200),
	@comID		int
	
)
as 

declare @error as int
begin transaction

	insert into tblUnitModel(UnitModel,Description,comID)
	values	(@unitModel,@description,@comID)

	select @error=@@error if @error !=0 begin GOTO Error_handler End
	

Commit Transaction

return 0

Error_Handler:
	RollBack Transaction
	Set NoCount Off
	Return @error
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNIT_MODEL]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Safiqur Rhaman
	Date:	20-09-2008
	Purpose; Select all Unit Model
	Select * from tblUnitmodel
*/

CREATE PROC [SP_SELECT_UNIT_MODEL]

AS
BEGIN
	--SELECT * FROM TBLUNITMODEL order by unitModel asc
	
	SELECT unitModelID,unitModel,Description,TBLUNITMODEL.comID,CompanyName
	FROM tblcompany
	join TBLUNITMODEL on (TBLUNITMODEL.comID=tblcompany.comID)
	where ISNULL(tblcompany.ISDELETE,0) <> 1
	order by unitModel asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_UNIT_MODEL]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By : Safiqur Rhaman
		Date       : 20/09/2008
		Purpose	   : Update Unit Model
		
*/

CREATE PROC [SP_UPDATE_UNIT_MODEL]
(
	@unitModel		VARCHAR(20),
	@Description	VARCHAR(150),
	@comID			INT,
	@unitModelID	INT
)
AS

BEGIN TRANSACTION
	
	BEGIN TRY
	
		UPDATE TBLUNITMODEL SET
		unitModel = @unitModel,
		[Description] = @Description,
		comID		= @comID
		WHERE unitModelID = @unitModelID

		COMMIT TRANSACTION
		RETURN 0
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1
	END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_UNIT_MODEL]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By: Safiqur Rhaman
		Date	  : 20/09/2008

*/

CREATE PROC [SP_DELETE_UNIT_MODEL]
(
	@unitModelID	INT	
)
AS
BEGIN TRANSACTION

BEGIN TRY
	DELETE tblunitmodel where unitModelID = @unitModelID


COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH

ROLLBACK TRANSACTION
RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_AD_UNITMODEL]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_AD_UNITMODEL]
	-- Add the parameters for the stored procedure here
(
	@comId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select unitModelID, unitModel from tblunitModel where comId=@comId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_PATTERN]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_PATTERN]
	-- Add the parameters for the stored procedure here
(
	@patternID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from tblpattern where ID=@patternID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_PATTERN]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_PATTERN]
	-- Add the parameters for the stored procedure here
(
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select id,patternName from tblpattern where comID=@comID and ISNULL(ISDELETE,0) <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UNIT_COUNT_SELECT]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- Select * from tblpattern
-- =============================================
CREATE PROCEDURE [SP_UNIT_COUNT_SELECT]
(
	@ComID int
)
AS
BEGIN
select (select count(patternID) from tblunits u where u.patternID=p.Id) 
as [Count],p.PatternName,p.ID from tblpattern p where comID =@ComID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_PATTERN]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

CREATE   proc [SP_INSERT_PATTERN]
( 

	@comID		int,
	@patternName 	varchar(50)
)

as
set nocount on
DECLARE @CurrentError int
DECLARE @retVal		int
DECLARE @patternID		int

if exists (select * from tblPattern where PatternName=@PatternName and comID=@comID)
begin
	set @retval=1
	select 1 as returnVal
    return 1
end
-- start transaction
begin transaction

-- create Pattern
insert into tblPattern (comID,patternName)
values(@comID,@patternName)

set @patternId=(select max(id) from tblpattern)
select @currenterror=@@error if @CurrentError!=0 begin goto error_handler end
-- end of transaction
set @retVal=0


commit transaction
set nocount off
 -- Reset SET NOCOUNT to OFF

select 1 as returnVal,@patternId as ParrentID
return 0
-- return 0 to indicate success, otherwise the raised error will be returned

error_handler:
	Rollback transaction
	set nocount off
	set @retVal=2
	select @currentError as returnVal
    return @currentError
GO
/****** Object:  StoredProcedure [dbo].[createPattern]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

CREATE   proc [createPattern]
( 

	@comID		int,
	@patternName 	varchar(50),
	@retVal		int out,
	@patternID	int out
)

as
set nocount on
DECLARE @CurrentError int

if exists (select * from tblPattern where PatternName=@PatternName and comID=@comID)
begin
	set @retval=1
	return
end
-- start transaction
begin transaction

-- create Pattern
insert into tblPattern (comID,patternName)
values(@comID,@patternName)

set @patternId=(select max(id) from tblpattern)
select @currenterror=@@error if @CurrentError!=0 begin goto error_handler end
-- end of transaction
set @retVal=0


commit transaction
set nocount off
 -- Reset SET NOCOUNT to OFF

return 0
-- return 0 to indicate success, otherwise the raised error will be returned

error_handler:
	Rollback transaction
	set nocount off
	set @retVal=2
	return @currentError
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_PATTREN]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

CREATE   proc [SP_UPDATE_PATTREN]
( 

	@comID		int,
	@patternID	int,	
	@patternName 	varchar(50)

)

as
set nocount on
DECLARE @CurrentError int


-- start transaction
begin transaction

-- create Pattern
	UPDATE TBLPATTERN SET
			  PATTERNNAME=@patternName
			  WHERE ID=@patternID AND COMID=@COMID

select @currenterror=@@error if @CurrentError!=0 begin goto error_handler end
-- end of transaction



commit transaction
set nocount off
 -- Reset SET NOCOUNT to OFF
select 0 as retval
return 0
-- return 0 to indicate success, otherwise the raised error will be returned

error_handler:
	Rollback transaction
	set nocount off	
	return @currentError
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGroup]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [sp_UserGroup]
(
 @grID int,
  @uID int
)
 AS
BEGIN TRANSACTION
	SET  NOCOUNT ON
declare @unitID int

declare  Unit  cursor for
select unitID from tblGroupwiseUnit where GroupID = @grID

open Unit

fetch next from Unit into @unitID
while @@fetch_status=0
begin
IF not exists (select * from tblUnitUserWise where uID = @uID and unitID = @unitID)

Insert into tblUnitUserWise (unitID,uID)Values (@unitID,@uID)
-- @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT<>0) BEGIN ROLLBACK TRANSACTION SET NOCOUNT OFF RETURN @ERRORCOUNT END

fetch next from Unit into @unitID
end

close Unit
deallocate Unit

COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_UNITUSER]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_UNITUSER] 
	-- Add the parameters for the stored procedure here
(
	@UnitID int,
	@comID int
) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from tblunituserwise 
		where unitID=@UnitID  and  [uid] in (select [uid] from tbluser u inner join tblgroupwiseunit gu on gu.groupid=u.groupid and gu.unitid=@UnitID and gu.COMID=@comID);
	DELETE FROM TBLGROUPWISEUNIT WHERE UNITID=@UnitID and COMID=@comID
END
GO
/****** Object:  StoredProcedure [dbo].[updateGroupWiseUnit]    Script Date: 09/09/2009 15:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC updateGroupWiseUnit 1,3,1
-- SELECT * FROM TBLUNITS SELECT * FROM TBLUNITUSERWISE


CREATE PROCEDURE [updateGroupWiseUnit]
(
	@groupID	int,
	@unitID		int,
	@comID 		int
)
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	
	DECLARE @UID AS INT,@ERRORCOUNT INT

	--DELETE FROM TBLGROUPWISEUNIT WHERE UNITID=@UNITID AND GROUPID=@GROUPID
		

	IF NOT EXISTS(SELECT * FROM TBLGROUPWISEUNIT WHERE GROUPID=@GROUPID AND UNITID=@UNITID AND COMID=@COMID)
	INSERT INTO TBLGROUPWISEUNIT(GROUPID,UNITID,COMID)VALUES(@GROUPID,@UNITID,@COMID)
	SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT<>0) BEGIN ROLLBACK TRANSACTION SET NOCOUNT OFF RETURN @ERRORCOUNT END

	DECLARE USERLIST CURSOR FOR
	SELECT UID FROM TBLUSER WHERE GROUPID=@GROUPID
	
	OPEN USERLIST
	FETCH NEXT FROM USERLIST INTO @UID
	
	WHILE @@FETCH_STATUS=0
	BEGIN
--		SELECT * FROM TBLGROUPWISEUNIT SELECT * FROM TBLUNITUSERWISE
		--SELECT * FROM TBLUNITUSERWISE WHERE UNITID=2 AND UID=1

		PRINT CAST(@UID AS VARCHAR )
		IF NOT EXISTS(SELECT * FROM TBLUNITUSERWISE WHERE UNITID=@UNITID AND UID=@UID)
		BEGIN
			INSERT INTO TBLUNITUSERWISE(UNITID,UID)VALUES(@UNITID,@UID)
			PRINT CAST(@UID AS VARCHAR )+' INSERTED'
			SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT<>0) BEGIN ROLLBACK TRANSACTION SET NOCOUNT OFF RETURN 
@ERRORCOUNT END
		END
		FETCH NEXT FROM USERLIST INTO @UID		
		
	END
CLOSE USERLIST
DEALLOCATE USERLIST
COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[GroupWiseUnit]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [GroupWiseUnit]
(
	@groupID	int,
	@unitID		int,
	@comID 		int
)
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	
	DECLARE @UID AS INT,@ERRORCOUNT INT

	

	INSERT INTO TBLGROUPWISEUNIT(GROUPID,UNITID,COMID)VALUES(@GROUPID,@UNITID,@COMID)
	SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT<>0) BEGIN ROLLBACK TRANSACTION SET NOCOUNT OFF RETURN @ERRORCOUNT END

	DECLARE USERLIST CURSOR FOR
	SELECT UID FROM TBLUSER WHERE GROUPID=@GROUPID
	
	OPEN USERLIST
	FETCH NEXT FROM USERLIST INTO @UID
	
	WHILE @@FETCH_STATUS=0
	BEGIN
--		SELECT * FROM TBLGROUPWISEUNIT
		INSERT INTO TBLUNITUSERWISE(UNITID,UID)VALUES(@UNITID,@UID)
		
		SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT<>0) BEGIN ROLLBACK TRANSACTION SET NOCOUNT OFF RETURN @ERRORCOUNT END
		FETCH NEXT FROM USERLIST INTO @UID		
		
	END
CLOSE USERLIST
DEALLOCATE USERLIST
COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNASSIGN_UNITS]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: Safiqur Rhaman
	DATE		: 20-10-2008
	PURPOSE		: SELECT Unassigned Unit DATA

*/

CREATE PROC [SP_SELECT_UNASSIGN_UNITS]
(
	@COMID	INT,
	@USERID INT
)
AS
BEGIN

	select * from tblunittype where comID=@COMID
	select * from tblunits where comID=@COMID and unitID in (select unitID from tblunituserwise where uID=@USERID  and 
	unitID not in (select unitID from tblunitwiseRules where comid=@COMID))


END
GO
/****** Object:  StoredProcedure [dbo].[SP_USERGROUP_UNIT]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_USERGROUP_UNIT] 
(
	@GROUPID	INT,
	@COMID		INT
 
)
AS

BEGIN TRANSACTION

BEGIN TRY
	
		
			
		DECLARE @UNITID AS INT,@ERRORCOUNT AS INT,@UID INT

		SET @UID=(SELECT MAX(UID) FROM TBLUSER WHERE COMID=@COMID)

		INSERT INTO TBLUSERWISEUNITCAT (COMID,UID,UNITGROUPID)VALUES(@COMID,@UID,@GROUPID)
		
		DECLARE GETUNITS CURSOR FOR 
		SELECT UNITID FROM TBLUNITS WHERE TYPEID=@groupID AND COMID=@COMID
		OPEN 	GETUNITS

		FETCH NEXT FROM GETUNITS INTO @UNITID
		WHILE @@FETCH_STATUS=0
		BEGIN
		
			IF NOT EXISTS(SELECT *  FROM TBLUNITUSERWISE WHERE UID=@UID AND UNITID=@UNITID)
			BEGIN
			 INSERT INTO TBLUNITUSERWISE(UNITID,UID)VALUES(@UNITID,@UID)
			 SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT !=0) BEGIN ROLLBACK TRANSACTION  RETURN @ERRORCOUNT END
			END
			
			FETCH NEXT FROM GETUNITS INTO @UNITID
		
		END
	close GETUNITS
	deallocate GETUNITS

	COMMIT TRANSACTION
	RETURN 0

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_UNIT_INSERT]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 27-09-2008
	PURPOSE		: INSERT UNIT DATA
	SELECT * FROM TBLUNITUSERWISE
*/

CREATE PROC [SP_USER_UNIT_INSERT]
(
	@UNITID		INT,
	@COMID		INT
)
AS
BEGIN TRANSACTION
BEGIN TRY
	DECLARE @USERID AS INT
	
	SET @USERID = ( SELECT MAX(UID) FROM TBLUSER WHERE COMID=@COMID)

	INSERT INTO TBLUNITUSERWISE(UNITID,UID)
	VALUES(@UNITID,@USERID)

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_UNITGROUP_INSERT]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By Rokan
	Date : 18/06/2008
	INSERT UNITS FOR UNIT GROUP
	SELECT * FROM TBLUNITS
	SELECT UNITID,UNITNAME FROM TBLUNITS WHERE TYPEID=1
	SELECT * FROM TBLUSERWISEUNITCAT
*/

CREATE  proc [SP_USER_UNITGROUP_INSERT]
(
	@groupID	int,	
	@comID		int
)
AS

BEGIN TRANSACTION

BEGIN TRY
	
		
			
		DECLARE @UNITID AS INT,@ERRORCOUNT AS INT,@USERID AS INT
		
		SET @USERID = (SELECT MAX(UID) FROM TBLUSER WHERE COMID = @COMID)
		
		INSERT INTO TBLUSERWISEUNITCAT (COMID,UID,UNITGROUPID)VALUES(@COMID,@USERID,@GROUPID)
		
		DECLARE GETUNITS CURSOR FOR 
		SELECT UNITID FROM TBLUNITS WHERE TYPEID=@groupID AND COMID=@COMID
		OPEN 	GETUNITS

		FETCH NEXT FROM GETUNITS INTO @UNITID
		WHILE @@FETCH_STATUS=0
		BEGIN
		
			IF NOT EXISTS(SELECT *  FROM TBLUNITUSERWISE WHERE UID=@USERID AND UNITID=@UNITID)
			BEGIN
			 INSERT INTO TBLUNITUSERWISE(UNITID,UID)VALUES(@UNITID,@USERID)
			 SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT !=0) BEGIN ROLLBACK TRANSACTION  RETURN @ERRORCOUNT END
			END
			
			FETCH NEXT FROM GETUNITS INTO @UNITID
		
		END
	close GETUNITS
	deallocate GETUNITS

	COMMIT TRANSACTION
	RETURN 0

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SCHEMEUNITCOUNT]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- SP_SELECT_SCHEMEUNITCOUNT 187
-- select * from tblsecurityscheme
-- =============================================
CREATE PROCEDURE [SP_SELECT_SCHEMEUNITCOUNT] 
	-- Add the parameters for the stored procedure here
(
	@schemeID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select count(*) as unitCount from tblunits where unitID in 
	(select unitID from tblunituserwise where uID in (select userID 
	from tblUserWiseScheme where schemeID=@schemeID)) and isnull(isdelete,0)<>1
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_UNITUSERWISE]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_INSERT_UNITUSERWISE]
	-- Add the parameters for the stored procedure here
(
	@unitID int,
	@uID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tblunituserwise (unitID,uID)values(@unitID,@uID)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USERUNITWISE]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_USERUNITWISE]
	-- Add the parameters for the stored procedure here
(
	@uid int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if exists(select * from tblunituserwise where uID=@uid)
	begin
		delete from tblunituserwise where uID=@uid
	end
	--delete from tblunituserwise where uID=@uid
END
GO
/****** Object:  StoredProcedure [dbo].[insertUnitGroup]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By Rokan
	Date : 18/06/2008
	INSERT UNITS FOR UNIT GROUP
	SELECT * FROM TBLUNITS
	SELECT UNITID,UNITNAME FROM TBLUNITS WHERE TYPEID=1
	SELECT * FROM TBLUSERWISEUNITCAT
*/

CREATE PROCEDURE [insertUnitGroup]
(
	@groupID	int,
	@userID		int,
	@comID		int
)
AS

BEGIN TRANSACTION

BEGIN TRY
	
		
			
		DECLARE @UNITID AS INT,@ERRORCOUNT AS INT
		
		INSERT INTO TBLUSERWISEUNITCAT (COMID,UID,UNITGROUPID)VALUES(@COMID,@USERID,@GROUPID)
		
		DECLARE GETUNITS CURSOR FOR 
		SELECT UNITID FROM TBLUNITS WHERE TYPEID=@groupID AND COMID=@COMID
		OPEN 	GETUNITS

		FETCH NEXT FROM GETUNITS INTO @UNITID
		WHILE @@FETCH_STATUS=0
		BEGIN
		
			IF NOT EXISTS(SELECT *  FROM TBLUNITUSERWISE WHERE UID=@USERID AND UNITID=@UNITID)
			BEGIN
			 INSERT INTO TBLUNITUSERWISE(UNITID,UID)VALUES(@UNITID,@USERID)
			 SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT !=0) BEGIN ROLLBACK TRANSACTION  RETURN @ERRORCOUNT END
			END
			
			FETCH NEXT FROM GETUNITS INTO @UNITID
		
		END
	close GETUNITS
	deallocate GETUNITS

	COMMIT TRANSACTION
	RETURN 0

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_NOTLISTEDUNITS]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- exec SP_SELECT_NOTLISTEDUNITS 1
-- =============================================
CREATE PROCEDURE [SP_SELECT_NOTLISTEDUNITS]
	-- Add the parameters for the stored procedure here
(
	@uID int,
	@COMID INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select unitID,unitName from tblunits where unitID not  in (select unitID from 
	tblunituserwise where uID=@uID) and ISNULL(ISDELETE,0) <> 1 AND COMID=@COMID order by unitName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LISTEDUNITS]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- exec SP_SELECT_LISTEDUNITS 1
-- =============================================
CREATE PROCEDURE [SP_SELECT_LISTEDUNITS]
	-- Add the parameters for the stored procedure here
(
	@uID	int,
	@COMID	INT
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select unitID,unitName from tblunits where ISNULL(ISDELETE,0) <> 1 and COMID=@COMID AND 
    unitID in (select unitID from tblunituserwise where uID=@uID) order by unitName ASC
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_UNITGROUP]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		CREATED BY	: MD. ROKANUZZAMAN SIKDER
		DATE		: 17-01-2008
		PURPOSE		: TO DELETE ALL USER GROUP
*/

CREATE PROC [SP_DELETE_UNITGROUP]
(
	@USERID		INT,	
	@COMID		INT
)

AS

BEGIN TRANSACTION

BEGIN TRY

		

		DELETE FROM TBLUSERWISEUNITCAT WHERE UID = @USERID AND COMID = @COMID
				
		DELETE FROM TBLUNITUSERWISE WHERE UID=@USERID
		

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_ALL_EVENTS]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 23-12-2008
	PURPOSE		: TO SELECT ALL EVENTS OF A PARTICULAR USER
	SP_SELECT_ALL_EVENTS 13,4
	SELECT * FROM TBLUSER
	SELECT * FROM TBLUNITS
	SELECT * FROM TBLUNITUSERWISE WHERE UNITID= 39
	SELECT * FROM TBLALERT WHERE CONVERT(VARCHAR,ALERTTIME,101) = CONVERT( VARCHAR, GETDATE(), 101)
*/

CREATE PROC [SP_SELECT_ALL_EVENTS]
(
	@USERID		INT,
	@COMID		INT
)
AS
BEGIN

		SELECT DISTINCT ALERTTYPE,ALERTMESSAGE,(SELECT UNITNAME FROM TBLUNITS
		WHERE TBLUNITS.UNITID = AL.UNITID) AS [UNIT NAME],
		(SELECT TOP 1 ALERTTIME FROM TBLALERT ALRT WHERE ALRT.UNITID = AL.UNITID 
		AND COMID = 1 ORDER BY ALERTTIME DESC) AS ALERTTIME  FROM TBLALERT AL
		WHERE UNITID IN (SELECT UNITID FROM TBLUNITUSERWISE WHERE UID = @USERID)
		AND COMID=@COMID AND CONVERT(VARCHAR,ALERTTIME,101) = CONVERT( VARCHAR, GETDATE(), 101)

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATEUSERGROUP]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec SP_UPDATEUSERGROUP 8,1
-- SELECT * FROM TBLUNITUSERWISE
-- SELECT UID FROM TBLUSER WHERE GROUPID=4

CREATE PROCEDURE [SP_UPDATEUSERGROUP]
(
	@GROUPID	INT,
	@UNITID		INT
)
AS

BEGIN TRANSACTION

BEGIN TRY
	DECLARE @USER AS INT

	DECLARE _USER CURSOR FOR 
	SELECT UID FROM TBLUSER WHERE GROUPID=@GROUPID

	OPEN _USER
	FETCH NEXT FROM _USER INTO @USER
	
	WHILE @@FETCH_STATUS=0
	BEGIN
		
		PRINT CAST(@USER AS VARCHAR)
		INSERT INTO TBLUNITUSERWISE VALUES(@UNITID,@USER)
		FETCH NEXT FROM _USER INTO @USER
		
	END
	
	CLOSE _USER
	DEALLOCATE _USER

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	 SELECT
          ERROR_NUMBER() as ErrorNumber,
          ERROR_MESSAGE() as ErrorMessage;
	ROLLBACK TRANSACTION
	CLOSE _USER
	DEALLOCATE _USER
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USERGROUPWISESCHEME]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [SP_USERGROUPWISESCHEME]
(
	@USERGROUPID		BIGINT,
	@COMID				BIGINT	
)
AS

DECLARE @SCHEMEID INT,@USERID INT
BEGIN TRANSACTION
BEGIN TRY

	SET @SCHEMEID=(SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)

	DECLARE _USERLIST CURSOR FOR
	SELECT UID FROM TBLUSER WHERE GROUPID=@USERGROUPID
	
	OPEN _USERLIST
	
	FETCH	NEXT FROM _USERLIST INTO @USERID
	
	WHILE @@FETCH_STATUS=0
	BEGIN
		
		IF EXISTS (SELECT * FROM TBLUSERWISESCHEME WHERE USERID=@USERID AND COMID=@COMID)
			BEGIN
				UPDATE TBLUSERWISESCHEME SET 
						SCHEMEID=@SCHEMEID
				WHERE USERID=@USERID AND COMID=@COMID
			END

		ELSE
			BEGIN
	
				INSERT INTO TBLUSERWISESCHEME(USERID,COMID,SCHEMEID)
				VALUES(@USERID,@COMID,@SCHEMEID)
			END

		FETCH	NEXT FROM _USERLIST INTO @USERID
	END

	CLOSE		_USERLIST
	DEALLOCATE	_USERLIST

	

COMMIT TRANSACTION
select 0 as retval
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	select -1 as retval
	RETURN	-1
END CATCH


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_TIMEZONE]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_UPDATE_TIMEZONE]
	-- Add the parameters for the stored procedure here
(
	@timeZone float,
	@uID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	update tbluser set timeZone=@timeZone where uID=@uID;
	select 0
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_USERS]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_USERS]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select uID,UserName from tblUser where ISNULL(ISDELETE,0) <> 1 order by UserName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USER]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 15TH DEC 2008
	PURPOSE		: DELETE USER
*/

CREATE PROC [SP_DELETE_USER]
(
	@UID		INT
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE TBLUSER SET ISDELETE=1
	WHERE UID=@UID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_DISABLE]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. Safiqur Rhaman
	DATE		: 19TH JAN 2009
	PURPOSE		: DISABLE USER
*/

CREATE PROC [SP_USER_DISABLE]
(
	@UID		INT
	
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE TBLUSER SET ISACTIVE=0
	WHERE UID=@UID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_SELECT]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		AlterD BY: MD. ROKANUZZAMAN SIKDER
		DATE	  : 20-09-2008
		PURPOSE   : SELECT ALL USER

*/

CREATE PROC [SP_USER_SELECT]
(
	@UID		INT
)

AS
BEGIN

	SELECT * FROM TBLUSER WHERE ISNULL(ISDELETE,0)<>1  and UID=@UID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_NOT_LISTED_USER]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_NOT_LISTED_USER]
(
	@SchemeID VARCHAR(50),
	@ComID varchar(50)
)
AS
BEGIN
select uID,login from tbluser where uID not in (select userid from tbluserwisescheme where schemeID=@SchemeID) and comID=@ComID and ISNULL(ISDELETE,0) <> 1 order by login asc
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_RETRIVE_PASSWORD]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 29-10-2008
	PURPOSE		: TO RETRIVE THE PASSWORD

*/

CREATE PROC [SP_RETRIVE_PASSWORD]
(
	@LOGIN			VARCHAR(100),
	@SECURITYQ		INT,
	@SECURITYA		VARCHAR(200)
)
AS
BEGIN

	SELECT PASSWORD FROM TBLUSER WHERE LOGIN=@LOGIN AND
	SECURITYQUESTION=@SECURITYQ AND SECURITYANSWER=@SECURITYA

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LISTED_USER]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LISTED_USER]
(
	@SchemeID VARCHAR(50),
	@ComID varchar(50)
)
AS
BEGIN
select uID,login from tbluser where uID  in (select userid from tbluserwisescheme where schemeID=@SchemeID ) and comID=@ComID and ISNULL(ISDELETE,0) <> 1 order by login asc

END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN_ADMIN]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 30-10-2008
	PURPOSE		: LOG IN USING INTO ADMIN PANEL

*/

CREATE PROC [SP_LOGIN_ADMIN]
(
	@LOGIN		VARCHAR(100),
	@PASSWORD	VARCHAR(150)
)

AS
BEGIN

		SELECT * FROM TBLUSER WHERE LOGIN=@LOGIN AND PASSWORD=@PASSWORD AND ROLEID<>2 AND ISNULL(ISDELETE,0)<>1
	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECTUSERLIST]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECTUSERLIST]
	-- Add the parameters for the stored procedure here
(
	@COMID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select uID,login from tbluser where comID=@COMID and ISNULL(ISDELETE,0) <> 1 order by login asc
END


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [sp_CreateUser]
(
@userName varchar (80), 
@Login varchar(15), 
@comID int ,
@groupID int ,
@pWord varchar(150), 
@Email varchar(80), 
@isActive bit , 
@securityQuestion varchar(200), 
@securityAnswer varchar(200),
@retval int out 
)
 AS
BEGIN TRANSACTION
	SET  NOCOUNT ON

Insert INTO tbluser (userName, Login, comID,groupID,Password, Email, isActive, securityQuestion, securityAnswer)
Values (@userName ,@Login  ,@comID ,@groupID ,@pWord  , @Email, @isActive,@securityQuestion,@securityAnswer )
set @retVal=(select max(uID) from tbluser )

COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATEUSER]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--SELECT * FROM TBLUSER

CREATE  PROC [SP_UPDATEUSER]
(
	@UID		INT,
	@USERNAME	VARCHAR(100),
	@LOGIN		VARCHAR(30),
	@PASS		VARCHAR(30),
	@GROUPID	VARCHAR(10),
	@EMAIL		VARCHAR(70),
	@SQ		VARCHAR(100),
	@SA		VARCHAR(100),
	@ISACTIVE 	BIT
)
AS
BEGIN TRANSACTION

DECLARE @ERRORCOUNT INT
if(@GROUPID is null OR @GROUPID ='0' OR @GROUPID ='')
BEGIN
	UPDATE TBLUSER SET
			LOGIN=@LOGIN,
			USERNAME=@USERNAME,
			[PASSWORD]=@PASS,
			EMAIL=@EMAIL,
			SECURITYQUESTION=@SQ,
			SECURITYANSWER=@SA,
			ISACTIVE=@ISACTIVE
	WHERE UID=@UID
END

ELSE
BEGIN
	UPDATE TBLUSER SET
			LOGIN=@LOGIN,
			USERNAME=@USERNAME,
			[PASSWORD]=@PASS,
			EMAIL=@EMAIL,
			SECURITYQUESTION=@SQ,
			SECURITYANSWER=@SA,
			GROUPID=@GROUPID,
			ISACTIVE=@ISACTIVE
	WHERE UID=@UID
END

	SET @ERRORCOUNT=@@ERROR IF(@ERRORCOUNT !=0) BEGIN ROLLBACK TRANSACTION RETURN @ERRORCOUNT END 
COMMIT TRANSACTION
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_UPDATE]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY: MD. ROKANUZZAMAN SIKDER
	DATE	  : 22-09-2008
	PURPOSE	  : UPDATE USER INFORMATION
	SELECT * FROM TBLUSER

*/

CREATE PROC [SP_USER_UPDATE]
(
	@UID			INT,
	@GROUPID		INT,
	@PASSWORD		VARCHAR(100),
	@USERNAME		VARCHAR(80),
	@EMAIL			VARCHAR(70),
	@SECURITYQ		INT,
	@SECURITYA		VARCHAR(200),
	@SCHEMEID		INT,
    @TIMEZONE       FLOAT(20),
	@ISACTIVE		BIT
)

AS

BEGIN TRANSACTION

	BEGIN TRY
	
			UPDATE TBLUSER SET
							GROUPID			=@GROUPID,
							PASSWORD		=@PASSWORD,
							USERNAME		=@USERNAME,
							EMAIL			=@EMAIL,
							SECURITYQUESTION=@SECURITYQ,
							SECURITYANSWER	=@SECURITYA,
                            TIMEZONE		=@TIMEZONE ,
							ISACTIVE		=@ISACTIVE
			WHERE UID=@UID

			UPDATE DBO.TBLUSERWISESCHEME SET 
							SCHEMEID=@SCHEMEID
			WHERE USERID=@UID


		
	COMMIT TRANSACTION
	RETURN 0
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1
	END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_LOGIN]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 22-09-2008
	PURPOSE		: LOGIN USER 
	SELECT * FROM TBLCOMPANY
	SELECT * FROM TBLUSER

*/

CREATE PROC [SP_USER_LOGIN]
(
	@COMPANYNAME		VARCHAR(100),
	@LOGINNAME			VARCHAR(50),
	@PASSWORD			VARCHAR(100)
)

AS

BEGIN

	SELECT UID,COMID,USERNAME,ROLEID FROM TBLUSER WHERE COMID=(SELECT COMID FROM TBLCOMPANY WHERE COMPANYNAME=@COMPANYNAME)
	AND LOGIN=@LOGINNAME AND PASSWORD=@PASSWORD AND ISACTIVE=1 --AND ISNULL(ISLOGIN,0)=0 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_USER_CREATE]    Script Date: 09/09/2009 15:13:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [SP_USER_CREATE]
(
@userName varchar (80), 
@Login varchar(15), 
@comID int ,
@groupID int ,
@pWord varchar(150), 
@Email varchar(80), 
@isActive bit , 
@securityScheme int,
@securityQuestion int, 
@securityAnswer varchar(200),
@timeZone float (20),
@retval int out 
)
 AS
BEGIN TRANSACTION
	SET  NOCOUNT ON

Insert INTO tbluser (userName, Login, comID,groupID,Password, Email, isActive, securityQuestion, securityAnswer,timeZone)
Values (@userName ,@Login  ,@comID ,@groupID ,@pWord  , @Email, @isActive,@securityQuestion,@securityAnswer,@timeZone )
set @retVal=(select max(uID) from tbluser )

insert into dbo.tblUserWiseScheme (UserID,comID,SchemeID)values(@retVal,@comID,@securityScheme)

COMMIT TRANSACTION
SET NOCOUNT OFF
RETURN @retVal
GO
/****** Object:  StoredProcedure [dbo].[SP_DDL_SELECT_USER]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Alterd By: Md. Rokanuzzaman Sikder
	Date	  :22/09/2008
	Purpose	  : Select CompanY for DropDownList
	SELECT * FROM TBLUSER
	

*/

CREATE PROC		[SP_DDL_SELECT_USER]
(
	@COMID		INT
)

AS

BEGIN
	
	SELECT UID,LOGIN FROM TBLUSER	WHERE ISNULL(ISDELETE,0)<> 1 AND COMID=@COMID order by 
LOGIN asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GEOFENCE_UNIT_INSERT]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [SP_GEOFENCE_UNIT_INSERT]
(
	@COMID		INT,
	@UNITID		INT,
	@NAME		VARCHAR(70),
	@EMAIL		VARCHAR(200),
	@CENTERLAT	VARCHAR(25),
	@CENTERLNG	VARCHAR(25),
	@RADIUS		VARCHAR(25),	
	@ISACTIVE	BIT
)

AS
DECLARE @GID	INT

BEGIN  TRANSACTION

BEGIN TRY

	IF NOT EXISTS(SELECT * FROM TBLGEOFENCE WHERE CENTERLAT=@CENTERLAT AND CENTERLNG=@CENTERLNG AND RADIUS=@RADIUS)
	BEGIN
		INSERT INTO TBLGEOFENCE(COMID,NAME,CENTERLAT,CENTERLNG,RADIUS,EMAIL,ISACTIVE)
		VALUES(@COMID,@NAME,@CENTERLAT,@CENTERLNG,@RADIUS,@EMAIL,@ISACTIVE)
	END 

	SET @GID=(SELECT MAX(ID) FROM TBLGEOFENCE)
	

			
	INSERT INTO TBLUNITWISEGEOFENCE(GEOFENCEID,COMID,UNITID)
	VALUES(@GID,@COMID,@UNITID)
			
	
	IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
		BEGIN
			UPDATE TBLUNITWISERULES SET
					GEOFENCEID=@GID,
					ISGEOFENCEACTIVE=@ISACTIVE
			WHERE UNITID=@UNITID 
		END
	ELSE 
		BEGIN
			INSERT INTO TBLUNITWISERULES(UNITID,GEOFENCEID,ISGEOFENCEACTIVE)
			VALUES(@UNITID,@GID,@ISACTIVE)
			
		END

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_GEOFENCE_UNITGROUP_INSERT]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY: ROKANUZZAMAN
	DATE: 20-08-2008
	PURPOSE: INSERT NEW CREATED GEOFENCE INFORMATION UNITGROUP WISE
	INSERT DATA INTO TBLGEOFENCE,TBLUNITWISEGEOFENCE
 
*/

CREATE proc [SP_GEOFENCE_UNITGROUP_INSERT]
(
	@COMID				INT,
	@UNITGROUPID		INT,
	@NAME				VARCHAR(70),
	@EMAIL				VARCHAR(200),
	@CENTERLAT			VARCHAR(25),
	@CENTERLNG			VARCHAR(25),
	@RADIUS				VARCHAR(25),	
	@ISACTIVE			BIT
)

AS
DECLARE @GID	INT,@UNITID	INT

BEGIN  TRANSACTION

BEGIN TRY
	
	IF NOT EXISTS(SELECT * FROM TBLGEOFENCE WHERE CENTERLAT=@CENTERLAT AND CENTERLNG=@CENTERLNG AND RADIUS=@RADIUS)
	BEGIN
		INSERT INTO TBLGEOFENCE(COMID,NAME,CENTERLAT,CENTERLNG,RADIUS,EMAIL,ISACTIVE)
		VALUES(@COMID,@NAME,@CENTERLAT,@CENTERLNG,@RADIUS,@EMAIL,@ISACTIVE)

	END

	SET @GID=(SELECT MAX(ID) FROM TBLGEOFENCE)	

	DECLARE GETUNIT CURSOR FOR 
	SELECT UNITID FROM TBLUNITS WHERE TYPEID=@UNITGROUPID AND COMID=@COMID AND ISNULL(ISDELETE,0)<>1
	OPEN 	GETUNIT

	FETCH NEXT FROM GETUNIT INTO @UNITID

	WHILE @@FETCH_STATUS=0
	BEGIN
	
--		IF EXISTS (SELECT * FROM TBLUNITWISEGEOFENCE WHERE UNITID=@UNITID AND COMID=@COMID)
--			BEGIN
--				UPDATE TBLUNITWISEGEOFENCE SET 	
--									GEOFENCEID=@GID
--				WHERE UNITID=@UNITID AND COMID=@COMID
--			END
--		ELSE
			INSERT INTO TBLUNITWISEGEOFENCE(GEOFENCEID,COMID,UNITID)
			VALUES(@GID,@COMID,@UNITID)
			

		IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
			BEGIN
				UPDATE TBLUNITWISERULES SET
						GEOFENCEID=@GID,
						ISGEOFENCEACTIVE=@ISACTIVE
				WHERE UNITID=@UNITID 
			END
		ELSE 
			BEGIN
				INSERT INTO TBLUNITWISERULES(UNITID,GEOFENCEID,ISGEOFENCEACTIVE)
				VALUES(@UNITID,@GID,@ISACTIVE)
			END

	FETCH NEXT FROM GETUNIT INTO @UNITID	
	END

CLOSE GETUNIT
DEALLOCATE GETUNIT



COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[sp_crGeofenceUnitGroupWise]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY: ROKANUZZAMAN
	DATE: 20-08-2008
	PURPOSE: INSERT NEW CREATED GEOFENCE INFORMATION UNITGROUP WISE
	INSERT DATA INTO TBLGEOFENCE,TBLUNITWISEGEOFENCE
*/

CREATE proc [sp_crGeofenceUnitGroupWise]
(
	@COMID				INT,
	@UNITGROUPID		INT,
	@NAME				VARCHAR(70),
	@EMAIL				VARCHAR(200),
	@CENTERLAT			VARCHAR(25),
	@CENTERLNG			VARCHAR(25),
	@RADIUS				VARCHAR(25),	
	@ISACTIVE			BIT
)

AS
DECLARE @GID	INT,@UNITID	INT

BEGIN  TRANSACTION

BEGIN TRY

	INSERT INTO TBLGEOFENCE(COMID,NAME,CENTERLAT,CENTERLNG,RADIUS,EMAIL,ISACTIVE)
	VALUES(@COMID,@NAME,@CENTERLAT,@CENTERLNG,@RADIUS,@EMAIL,@ISACTIVE)
	
	SET @GID=(SELECT MAX(ID) FROM TBLGEOFENCE)

	DECLARE GETUNITS CURSOR FOR
	SELECT UNITID FROM TBLUNITS WHERE TYPEID=@UNITGROUPID

	OPEN GETUNITS
	FETCH NEXT FROM GETUNITS INTO @UNITID

	WHILE @@FETCH_STATUS=0
	BEGIN
		IF EXISTS (SELECT * FROM TBLUNITWISEGEOFENCE WHERE UNITID=@UNITID AND COMID=@COMID)
			BEGIN
				UPDATE TBLUNITWISEGEOFENCE SET 	
									GEOFENCEID=@GID
				WHERE UNITID=@UNITID AND COMID=@COMID
			END
		ELSE
			BEGIN
					INSERT INTO TBLUNITWISEGEOFENCE(GEOFENCEID,COMID,UNITID)
					VALUES(@GID,@COMID,@UNITID)
			END

		IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
			BEGIN
				UPDATE TBLUNITWISERULES SET
						GEOFENCEID=@GID,
						ISGEOFENCEACTIVE=@ISACTIVE
				WHERE UNITID=@UNITID 
			END
		ELSE 
			BEGIN
				INSERT INTO TBLUNITWISERULES(UNITID,GEOFENCEID,ISGEOFENCEACTIVE)
				VALUES(@UNITID,@GID,@ISACTIVE)
			END

	FETCH NEXT FROM GETUNITS INTO @UNITID	
	END

CLOSE GETUNITS
DEALLOCATE GETUNITS

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_GEOFENCECE_UNITWISE]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [SP_UPDATE_GEOFENCECE_UNITWISE]
(
	@GEOFENCEID		VARCHAR(10),
	@COMID			INT,
	@UNITID			INT,
	@NAME			VARCHAR(70),
	@EMAIL			VARCHAR(200),
	@OPERATOR		VARCHAR(100),
	@PHONENUMBER	VARCHAR(20),
	@ISSMS			BIT,
	@CENTERLAT		VARCHAR(25),
	@CENTERLNG		VARCHAR(25),
	@RADIUS			VARCHAR(25),	
	@ISACTIVE		BIT
)

AS
DECLARE @GID	INT

BEGIN  TRANSACTION

BEGIN TRY

	UPDATE 	TBLGEOFENCE SET
				NAME=@NAME,
				CENTERLAT	= @CENTERLAT,
				CENTERLNG	= @CENTERLNG,
				RADIUS		= @RADIUS,
				EMAIL		= @EMAIL,
				OPERATOR	= @OPERATOR,
				PHONENUMBER	= @PHONENUMBER,
				ISSMS		= @ISSMS,
				ISACTIVE	= @ISACTIVE
	WHERE ID=@GEOFENCEID AND COMID=@COMID

	
--	UPDATE TBLUNITWISERULES SET
--					GEOFENCEID=@GEOFENCEID,
--					ISGEOFENCEACTIVE=@ISACTIVE
--	WHERE UNITID=@UNITID 
--	
	UPDATE TBLUNITWISEGEOFENCE SET
					GEOFENCEID=@GEOFENCEID,
					ISACTIVE=@ISACTIVE
	WHERE UNITID=@UNITID AND COMID = @COMID AND GEOFENCEID = @GEOFENCEID
	

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_GEOFENCE_UNITWISE]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [SP_CREATE_GEOFENCE_UNITWISE]
(
	@COMID		INT,
	@UNITID		INT,
	@NAME		VARCHAR(70),
	@EMAIL		VARCHAR(200),
	@OPERATOR		VARCHAR(100),
	@PHONENUMBER	VARCHAR(20),
	@ISSMS			BIT,
	@CENTERLAT	VARCHAR(25),
	@CENTERLNG	VARCHAR(25),
	@RADIUS		VARCHAR(25),	
	@ISACTIVE	BIT
)

AS
DECLARE @GID	INT

BEGIN  TRANSACTION

BEGIN TRY

	IF NOT EXISTS(SELECT * FROM TBLGEOFENCE WHERE CENTERLAT=@CENTERLAT AND CENTERLNG=@CENTERLNG AND RADIUS=@RADIUS)
	BEGIN
		INSERT INTO TBLGEOFENCE(COMID,NAME,CENTERLAT,CENTERLNG,RADIUS,EMAIL,ISACTIVE,OPERATOR,PHONENUMBER,ISSMS)
		VALUES(@COMID,@NAME,@CENTERLAT,@CENTERLNG,@RADIUS,@EMAIL,@ISACTIVE,@OPERATOR,@PHONENUMBER,@ISSMS)
	END 
	ELSE
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

	SET @GID=(SELECT MAX(ID) FROM TBLGEOFENCE)
	
	INSERT INTO TBLUNITWISEGEOFENCE(GEOFENCEID,COMID,UNITID,ISACTIVE)
	VALUES(@GID,@COMID,@UNITID,@ISACTIVE)
	
	
--	IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
--		BEGIN
--			UPDATE TBLUNITWISERULES SET
--					GEOFENCEID=@GID,
--					ISGEOFENCEACTIVE=@ISACTIVE
--			WHERE UNITID=@UNITID 
--		END
--	ELSE 
--		BEGIN
--			INSERT INTO TBLUNITWISERULES(UNITID,GEOFENCEID,ISGEOFENCEACTIVE)
--			VALUES(@UNITID,@GID,@ISACTIVE)
--			
--		END

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	SELECT ERROR_MESSAGE()
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SAFETYZONESLIST_SELECT]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 07/10/2008
	PURPOSE		: SELECT SAFETYZONES OF A UNIT
	SP_SAFETYZONESLIST_SELECT 1,74
	select * from TBLUNITWISEGEOFENCE
	SP_SAFETYZONESLIST_SELECT 
	Select * from tblunitstatus where statuscode ='Red'
	select * from tblunits where Unitid = 74
*/

CREATE PROC [SP_SAFETYZONESLIST_SELECT]
(
	@UNITID		INT,
	@COMID		INT
)
AS
BEGIN

	SELECT GEOFENCEID,(SELECT NAME FROM TBLGEOFENCE WHERE ID = GEOFENCEID) AS NAME,ISNULL(ISACTIVE,0)AS ISACTIVE  FROM 
	TBLUNITWISEGEOFENCE WHERE COMID = @COMID AND UNITID = @UNITID
	
--	SELECT ID AS GEOFENCEID,NAME,ISACTIVE FROM TBLGEOFENCE  
--	WHERE ID  IN (SELECT isnull(GEOFENCEID,0) FROM  TBLUNITWISERULES 
--	WHERE UNITID=@UNITID AND ISGEOFENCEACTIVE=1) AND COMID=@COMID
--
--	UNION
--
--	SELECT ID AS GEOFENCEID,NAME,'FALSE' FROM TBLGEOFENCE
--	WHERE ID IN (SELECT ISNULL(GEOFENCEID,0) FROM TBLUNITWISEGEOFENCE
--	WHERE GEOFENCEID NOT IN (SELECT ISNULL(GEOFENCEID,0) FROM TBLUNITWISERULES
--	WHERE UNITID=@UNITID AND ISGEOFENCEACTIVE=1) AND UNITID=@UNITID) AND COMID=@COMID

END
GO
/****** Object:  StoredProcedure [dbo].[sp_crGeofenceUnitWise]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [sp_crGeofenceUnitWise]
(
	@COMID		INT,
	@UNITID		INT,
	@NAME		VARCHAR(70),
	@EMAIL		VARCHAR(200),
	@CENTERLAT	VARCHAR(25),
	@CENTERLNG	VARCHAR(25),
	@RADIUS		VARCHAR(25),	
	@ISACTIVE	BIT
)

AS
DECLARE @GID	INT

BEGIN  TRANSACTION

BEGIN TRY

	IF NOT EXISTS(SELECT * FROM TBLGEOFENCE WHERE CENTERLAT=@CENTERLAT AND CENTERLNG=@CENTERLNG AND RADIUS=@RADIUS)
	BEGIN
		INSERT INTO TBLGEOFENCE(COMID,NAME,CENTERLAT,CENTERLNG,RADIUS,EMAIL,ISACTIVE)
		VALUES(@COMID,@NAME,@CENTERLAT,@CENTERLNG,@RADIUS,@EMAIL,@ISACTIVE)
	END 

	SET @GID=(SELECT MAX(ID) FROM TBLGEOFENCE)
	
	IF EXISTS (SELECT * FROM TBLUNITWISEGEOFENCE WHERE UNITID=@UNITID AND COMID=@COMID)
			BEGIN
				UPDATE TBLUNITWISEGEOFENCE SET 	
									GEOFENCEID=@GID
				WHERE UNITID=@UNITID AND COMID=@COMID
			END
	ELSE
			BEGIN
					INSERT INTO TBLUNITWISEGEOFENCE(GEOFENCEID,COMID,UNITID)
					VALUES(@GID,@COMID,@UNITID)
			END
	
	IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
		BEGIN
			UPDATE TBLUNITWISERULES SET
					GEOFENCEID=@GID,
					ISGEOFENCEACTIVE=@ISACTIVE
			WHERE UNITID=@UNITID 
		END
	ELSE 
		BEGIN
			INSERT INTO TBLUNITWISERULES(UNITID,GEOFENCEID,ISGEOFENCEACTIVE)
			VALUES(@UNITID,@GID,@ISACTIVE)
			
		END

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETEUNITGEOFENCE]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	delete from tblgeofence where ID
	delete from TBLUNITWISEGEOFENCE where geofenceid
	update TBLUNITWISERULES set geofenceID=0,isgeofenceActive=0 where unitid=
	SELECT * FROM TBLGEOFENCE
*/


CREATE PROC [SP_DELETEUNITGEOFENCE]
(
	@UNITID		INT,
	@GEOFENCEID	INT,
	@COMID		INT
)

AS

DECLARE @COUNT INT

BEGIN TRANSACTION

BEGIN TRY

	SET @COUNT=(SELECT COUNT(*) FROM TBLUNITWISERULES WHERE GEOFENCEID=@GEOFENCEID AND UNITID <> @UNITID)
	IF(@COUNT<1)
		BEGIN
			DELETE FROM TBLGEOFENCE WHERE ID=@GEOFENCEID	
		END			
	
	DELETE FROM TBLUNITWISEGEOFENCE  WHERE GEOFENCEID=@GEOFENCEID AND UNITID=@UNITID
	UPDATE TBLUNITWISERULES  SET GEOFENCEID=0,ISGEOFENCEACTIVE=0 WHERE UNITID=@UNITID

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DEFAULT_SCHEME_PERMISSION]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 15-01-2008
	PURPOSE		: INSERT SCHEME PERMISSION
	
select * from tblSecurityScheme
select * from tblSchemePermission
select * from tblForms
select * from tblCompany where isnull(isdelete,0) <> 1
exec SP_DEFAULT_SCHEME_PERMISSION 27
*/

CREATE PROC [SP_DEFAULT_SCHEME_PERMISSION]
(
	@COMID	INT	
)

AS
BEGIN TRANSACTION

BEGIN TRY
	
	DECLARE @SCHEMEID AS INT,@FORMID AS INT
	
	SET @SCHEMEID	= (SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)

	DECLARE FORMID CURSOR FOR SELECT ID FROM TBLFORMS
	
	OPEN FORMID

	FETCH NEXT FROM FORMID INTO @FORMID

	WHILE @@FETCH_STATUS=0
	
	BEGIN
		
		INSERT INTO TBLSCHEMEPERMISSION(SCHEMEID,COMID,FORMID,FULLACCESS,[DELETE],[VIEW],[INSERT],EDIT)
		VALUES(@SCHEMEID,@COMID,@FORMID,1,1,1,1,1)
	
	FETCH NEXT FROM FORMID INTO @FORMID

	END


COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	print ' error from SP_DEFAULT_SCHEME_PERMISSION'
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SAVESCHEMEPERMISSION]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY: MD ROKANUZZAMAN SIKDER
	DATE	  : 26-08-2008
	PURPOSE   : SAVE FORM'S RIGHT INFORMATION OF A PARTICULAR SCHEME
	SELECT * FROM TBLFORMS
	SELECT * FROM TBLSCHEMEPERMISSION
*/

CREATE PROC [SP_SAVESCHEMEPERMISSION]
(
	@COMID		INT,
	@FORMID		INT,
	@FULLACCESS	BIT
)
AS
DECLARE @SCHEMEID INT
BEGIN TRANSACTION

BEGIN TRY
	
	SET	@SCHEMEID=(SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)
	INSERT INTO TBLSCHEMEPERMISSION(SCHEMEID,COMID,FORMID,FULLACCESS)
	VALUES(@SCHEMEID,@COMID,@FORMID,@FULLACCESS)

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SECURITY_SCHEME]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY  : MD. ROKANUZZAMAN SIKDER
	DATE		: 14-12-2008
	PURPOSE		: TO SELECT SECURITY SCHEME

*/

CREATE PROC [SP_SELECT_SECURITY_SCHEME]
(
	@COMID	INT
)
AS
BEGIN
	SELECT * FROM DBO.TBLSECURITYSCHEME WHERE COMID=@COMID;
	SELECT * FROM DBO.TBLSECURITYSCHEME WHERE COMID=@COMID AND DEFAULTSCHEME=1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SCHEMEPERMISSION]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [SP_SCHEMEPERMISSION]
(
	
	@COMID		INT,
	@FORMID		INT,
	@FULLACCESS	BIT,
	@DELETE		BIT,
	@VIEW		BIT,
	@INSERT		BIT,
	@EDIT		BIT
)
AS
DECLARE @SCHEMEID	INT
BEGIN TRANSACTION

BEGIN TRY
	
	SET @SCHEMEID=(SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)
	INSERT INTO TBLSCHEMEPERMISSION(SCHEMEID,COMID,FORMID,FULLACCESS,[DELETE],[VIEW],[INSERT],EDIT)
	VALUES(@SCHEMEID,@COMID,@FORMID,@FULLACCESS,@DELETE,@VIEW,@INSERT,@EDIT)

COMMIT TRANSACTION
select 0 as retval
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	select -1 as retval
	RETURN -1
END CATCH


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_USERWISESCHEME]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROC [SP_USERWISESCHEME]
(
	@USERID		BIGINT,
	@COMID		BIGINT
	
	
)
AS

DECLARE @SCHEMEID INT
BEGIN TRANSACTION
BEGIN TRY

	SET @SCHEMEID=(SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)

	IF EXISTS (SELECT * FROM TBLUSERWISESCHEME WHERE USERID=@USERID AND COMID=@COMID)
		BEGIN
			UPDATE TBLUSERWISESCHEME SET 
					SCHEMEID=@SCHEMEID
			WHERE USERID=@USERID AND COMID=@COMID
		END

	ELSE
		BEGIN
	
			INSERT INTO TBLUSERWISESCHEME(USERID,COMID,SCHEMEID)
			VALUES(@USERID,@COMID,@SCHEMEID)
		END

COMMIT TRANSACTION
select 0 as retval
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	select -1 as retval
	RETURN	-1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETESCHEME]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY:	MD ROKANUZZAMAN SIKDER
	DATE	  : 25-08-2008
	PURPOSE   : DELETE EXISTING SCHEME RECORD
	SELECT * FROM TBLSECURITYSCHEME
	SELECT * FROM TBLUSERWISESCHEME
	SELECT * FROM TBLSCHEMEPERMISSION

*/

CREATE PROCEDURE [SP_DELETESCHEME]
(
	@SCHEMEID	INT,
	@COMID		INT
)

AS
BEGIN TRANSACTION
BEGIN TRY
	
	DELETE FROM TBLSECURITYSCHEME WHERE COMID=@COMID AND ID=@SCHEMEID
	DELETE FROM TBLUSERWISESCHEME WHERE COMID=@COMID AND SCHEMEID=@SCHEMEID
	DELETE FROM TBLSCHEMEPERMISSION WHERE COMID=@COMID AND SCHEMEID=@SCHEMEID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SCHEME_INFO]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SCHEME_INFO]
(
	@SchemeID VARCHAR(50)
)
AS
BEGIN

SELECT ID,SCHEMENAME,defaultScheme FROM TBLSECURITYSCHEME WHERE ID=@SchemeID

END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SAVESCHEMEINFO]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- exec SP_SAVESCHEMEINFO 'Rokan2',1,0
-- =============================================
CREATE PROCEDURE [SP_SAVESCHEMEINFO]
(
	@SCHEMENAME		VARCHAR(100),
	@COMID			INT,
	@DEFUALTSCHEME	BIT
)

AS

DECLARE	@SCHEMEID	INT
BEGIN TRANSACTION

BEGIN  TRY
	
	IF EXISTS (SELECT * FROM TBLSECURITYSCHEME WHERE COMID=@COMID AND SCHEMENAME=@SCHEMENAME)
	BEGIN
		COMMIT TRANSACTION
		SELECT -3 AS RETVAL
		RETURN -3
	END
	
	ELSE
	BEGIN
		INSERT INTO TBLSECURITYSCHEME(SCHEMENAME,COMID)VALUES(@SCHEMENAME,@COMID)
		SET	@SCHEMEID=(SELECT MAX(ID) FROM TBLSECURITYSCHEME WHERE COMID=@COMID)
		
		IF(@DEFUALTSCHEME<>0)
		BEGIN
			UPDATE TBLSECURITYSCHEME SET
				DEFAULTSCHEME=0
				WHERE COMID=@COMID
		    
			UPDATE TBLSECURITYSCHEME SET
				DEFAULTSCHEME=1
				WHERE COMID=@COMID AND ID=@SCHEMEID
		
		END
	END

COMMIT TRANSACTION
SELECT 0 AS RETVAL
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	SELECT -1 AS RETVAL
	RETURN -1
END CATCH



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LOADSCHEME]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LOADSCHEME]
	-- Add the parameters for the stored procedure here
(
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select ID,schemeName,defaultScheme from tblsecurityScheme where comID=@comID order by schemeName asc
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SCHEMEUSERCOUNT]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SCHEMEUSERCOUNT]
	-- Add the parameters for the stored procedure here
(
	@schemeID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select count(*) as UserCount from tbluserwiseScheme where schemeID=@schemeID
END


set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SCHEMEPERMISSION]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman
-- Alter date: <Alter Date,,>
-- Description:	Get user's schemepermission
-- =============================================
CREATE PROCEDURE [SP_SELECT_SCHEMEPERMISSION]
	-- Add the parameters for the stored procedure here
(
	@USERID int
)	
AS
BEGIN
	 select * from tblSchemePermission
	 join  tblforms on(tblSchemePermission.formID = tblforms.ID)
     where schemeID=(select schemeID from tblUserWiseScheme 
	 where userID= @USERID)

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MODULE]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_MODULE]
(
	@SchemeID VARCHAR(50)
)
AS
BEGIN

select * from tblModule;
select formID,schemeID,fullaccess,[delete],[view],[insert],edit,moduleID,formName from tblschemepermission sp inner join tblForms f on f.ID=sp.formID where sp.schemeID=@SchemeID
union
select ID,'',0,0,0,0,0,moduleID,formName from tblForms where ID not in (select formID from tblSchemepermission where schemeID=@SchemeID)
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_GPRS]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_GPRS]
	-- Add the parameters for the stored procedure here
(
	@startDate datetime,
	@deviceID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select isnull(sum(distance),0) as distance from tblgprs where dateadd(ss,recTime,'01/01/2000')=@startDate and deviceID=@deviceID;
	select isnull(sum(distance),0) as distance from tblgprs where dateadd(ss,recTime,'01/01/2000') between @startDate and getDate() and  deviceID=@deviceID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_TREEcOLOR_1]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_TREEcOLOR_1]
(
	@unitID int
)
AS
BEGIN
select distinct Cast(velocity*0.621 as int) as velocity,recTimeRevised,recTime,lat,long  from tblGPRS
		where recTime=(select max(recTime)from tblGPRS 
				where deviceID = (select deviceid from tblunits 
						where unitid=@unitID)) and deviceID =(select deviceid from tblunits where unitid=@unitID)
END
GO
/****** Object:  StoredProcedure [dbo].[spConvertSecondsToDateTime]    Script Date: 09/09/2009 15:13:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [spConvertSecondsToDateTime]
as

-- exec spConvertSecondsToDateTime

declare @recTime as bigint
declare @recID as bigint

declare csrDateTimeAdjust cursor for select recID, recTime from tblGPRS
open csrDateTimeAdjust
fetch next from csrDateTimeAdjust into @recID, @recTime

while @@FETCH_STATUS = 0 
begin
	update tblGPRS set recTimeRevised = (select dateadd(ss,@recTime,'01/01/2000')) where recID = @recID

	fetch next from csrDateTimeAdjust into @recID, @recTime
end
close csrDateTimeAdjust
deallocate csrDateTimeAdjust
GO
/****** Object:  StoredProcedure [dbo].[SP_GPRS1_TO_GPRS]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_GPRS1_TO_GPRS]
 
AS
BEGIN
INSERT INTO tblgprs (deviceID,recTime,Lat,Long,msgType,msgBody,Velocity,Sensor,recTimeRevised)
select deviceID,recTime,Lat,Long,msgType,msgBody,Velocity,Sensor,recTimeRevised from tblgprs1	
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_GPRS1_2_GPRS]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 21 DEC 2008
	PURPOSE		: TO INSERT GPRS1 DATA TO GPRS

SELECT * FROM TBLGPRS where rectime between 283174489 and 283278479 order by rectime desc
SELECT  * FROM TBLGPRS1

insert into tblgprs(deviceid,rectime,lat,long,rectimerevised)
select deviceid,rectime,lat,long,rectimerevised from tblgprs1

*/


CREATE PROC [SP_INSERT_GPRS1_2_GPRS]
(
	@DEVICEID		INT,
	@RECTIME		VARCHAR(50),
	@LAT			DECIMAL(25,12),
	@LONG			DECIMAL(25,13),
	@POSTALCODE		VARCHAR(50),
	@CITY			VARCHAR(50),
	@STATE			VARCHAR(50),
	@COUNTRY		VARCHAR(50),
	@MSGTYPE		VARCHAR(50),
	@MSGBODY		VARCHAR(50),
	@VELOCITY		INT,
	@SENSOR			INT,	
	@RECTIMEREVISED	VARCHAR(50)
)
AS

BEGIN TRANSACTION
BEGIN TRY

	IF NOT EXISTS( SELECT * FROM TBLGPRS WHERE DEVICEID = @DEVICEID AND RECTIME = @RECTIME AND LAT = @LAT AND LONG = @LONG)
	BEGIN
	
		INSERT INTO TBLGPRS(DEVICEID,RECTIME,LAT,LONG,POSTALCODE,
							CITY,STATE,COUNTRY,MSGTYPE,MSGBODY,VELOCITY,
							SENSOR,RECTIMEREVISED)
					VALUES (@DEVICEID,@RECTIME,@LAT,@LONG,@POSTALCODE,
							@CITY,@STATE,@COUNTRY,@MSGTYPE,@MSGBODY,@VELOCITY,
							@SENSOR,@RECTIMEREVISED)

	END

	DELETE FROM TBLGPRS1 WHERE DEVICEID=@DEVICEID AND RECTIME=@RECTIME AND LAT=@LAT AND LONG= @LONG


COMMIT TRANSACTION
RETURN 0
END TRY
BEGIN CATCH

	ROLLBACK TRANSACTION
	RETURN -1

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_GPRS_DATA]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*


	CREATED BY	: MD ROKANUZZAMAN SIKDER
	PURPOSE		: TO DELETE GPRS DATA	
	EXEC SP_DELETE_GPRS_DATA '2008/11/01','2008/11/10'
	
*/

CREATE PROC [SP_DELETE_GPRS_DATA]
(
	@STARTDATE		VARCHAR(50),
	@ENDDATE		VARCHAR(50)
)

AS

BEGIN TRANSACTION 

BEGIN TRY

	IF(@STARTDATE ='' OR @STARTDATE IS NULL)
		BEGIN

			DELETE FROM TBLGPRS WHERE DATEADD(SS,RECTIME,'2000/01/01') < @ENDDATE

		END

	ELSE	
		BEGIN
			DELETE FROM TBLGPRS WHERE DATEADD(SS,RECTIME,'2000/01/01') BETWEEN @STARTDATE AND  @ENDDATE
		END
	
COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION
	RETURN 0

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_FROM_TBLGPRS1_TO_TBLGPRS]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER 300929937 300929938
	PURPOSE		: TO TRANSFER DATA FROM TBLGPRS1 TO TBLGPRS
	SELECT * FROM TBLGPRS WHERE RECTIME = 300929937 AND DEVICEID = 555
*/

CREATE PROC [SP_INSERT_FROM_TBLGPRS1_TO_TBLGPRS]
AS
BEGIN TRANSACTION

DECLARE DATA_GPRS CURSOR FOR
SELECT DEVICEID,LAT,LONG,RECTIME FROM TBLGPRS1

BEGIN TRY

		DECLARE @DEVICEID AS INT ,@LAT AS NUMERIC,@LONG AS NUMERIC,@RECTIME AS INT
		
		
	
		OPEN DATA_GPRS
		
		FETCH NEXT FROM DATA_GPRS INTO @DEVICEID,@LAT,@LONG,@RECTIME

		WHILE @@FETCH_STATUS = 0
		BEGIN

		IF NOT  EXISTS( SELECT * FROM TBLGPRS WHERE DEVICEID = @DEVICEID AND LAT = @LAT AND LONG = @LONG AND RECTIME  = @RECTIME )
		BEGIN
			
			INSERT INTO TBLGPRS(DEVICEID,RECTIME,LAT,LONG,POSTALCODE,CITY,STATE,COUNTRY,MSGTYPE,VELOCITY,SENSOR,RECTIMEREVISED)
			SELECT DEVICEID,RECTIME,LAT,LONG,'','','','',MSGTYPE,VELOCITY,SENSOR,RECTIMEREVISED FROM TBLGPRS1 WHERE DEVICEID = @DEVICEID
			AND RECTIME = @RECTIME

--insert into tblgprs(deviceID,rectime,lat,long,postalCode,city,state,country,msgType,velocity,sensor,recTimerevised)
--select deviceID,recTime,Lat,Long,'','','','',msgType,velocity,sensor,recTimeRevised from tblgprs1


		END

		DELETE FROM TBLGPRS1 WHERE DEVICEID = @DEVICEID AND RECTIME = @RECTIME
	
		FETCH NEXT FROM DATA_GPRS INTO @DEVICEID,@LAT,@LONG,@RECTIME
		END

		CLOSE DATA_GPRS
		DEALLOCATE DATA_GPRS

COMMIT TRANSACTION
END TRY

BEGIN CATCH
	CLOSE DATA_GPRS
	DEALLOCATE DATA_GPRS

	SELECT ERROR_MESSAGE()
	ROLLBACK TRANSACTION
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[DistanceCalculator]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     proc [DistanceCalculator]
as 

begin

declare @dID as bigint,@lat as decimal(25,10),@long as decimal(25,10),@lat2 as decimal(25,10),@long2 as decimal(25,10),@recTime as bigint
declare @distance as decimal(25,10),@i as int
declare @lat1 as decimal(25,10),@long1 as decimal(25,10)

declare  deviceID cursor for
select distinct deviceID from tblGPRS where deviceID is not null

open deviceID

fetch next from deviceID into @dID
while @@fetch_status=0
begin
	set @i=0
	declare  LatLong cursor for
	select recID,Lat,Long from tblGPRS where deviceID = @dID order by recTime
	open LatLong
	

		fetch next from LatLong into @recTime,@lat,@long
		
		while @@fetch_Status=0
		 begin
		  	set @lat2 = @lat
                  	set @long2 = @long
	         	if(@i=0)
		    	begin
				set	@distance= (select dbo.fn_Distance(@lat2,@long2,@lat2,@long2))
			
		    	end
		  	else
			begin
				set @distance=(select dbo.fn_Distance(@lat1,@long1,@lat2,@long2))
                        
			end
		 		Update tblGPRS set Distance =isnull(@distance,0) where deviceID=@dID and recID=@recTime
		 		set @lat1 = @lat2
                 		set @long1 = @long2
 		 
		set @i=@i+1
		fetch next from LatLong into @recTime,@lat,@long	
		end
		 close LatLong
		 deallocate LatLong
	         

fetch next from deviceID into @dID
end
close deviceID
deallocate deviceID

end

--exec DistanceCalculator
-- select distance from tblgprs
GO
/****** Object:  StoredProcedure [dbo].[SP_REVERSEGEOCODING_GPRS]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 22-01-2009
	PURPOSE		: REVERSEGEOCODING ON TBLGPRS

*/

CREATE PROC	[SP_REVERSEGEOCODING_GPRS]
(
	@DEVICEID		INT,
	@RECTIME		VARCHAR(50),
	@POSTALCODE		VARCHAR(50),
	@CITY			VARCHAR(50),
	@STATE			VARCHAR(50),
	@COUNTRY		VARCHAR(50)
)

AS
BEGIN TRANSACTION
BEGIN TRY

	UPDATE TBLGPRS SET 
				   POSTALCODE = @POSTALCODE,
				   CITY		  = @CITY,
				   STATE	  = @STATE,
				   COUNTRY	  = @COUNTRY,
				   ISREVERSEGEOCODING = 1
	WHERE DEVICEID = @DEVICEID AND RECTIME = @RECTIME
COMMIT TRANSACTION
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_ERROR_REPORT]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_ERROR_REPORT]
(
	@ServiceName VARCHAR(50),
	@StartTime VARCHAR(50),
    @EndTime VARCHAR(50)
)
AS
BEGIN
select * from tblErrorLog 
where serviceName= @ServiceName and 
	errorTime between @ServiceName and @ServiceName 
	 order by errorTime desc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_SUPPLIES]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblsupplies

create   proc [SP_DELETE_SUPPLIES]
( 	
	@ID		int,
	@ComID	int
)
as
DELETE FROM tblSupplies
    where ID=@ID and comID = @ComID
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SUPPLIES_PER_PATTERN]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SUPPLIES_PER_PATTERN]
	-- Add the parameters for the stored procedure here
(
	@patternID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select '' as quantity,'' as kminterval,'' as daysinterval,supplies,id,0 as chk from tblsupplies where ID not in (select suppliesID from tblsuppliesperpattern where patternID=@patternID) 
	union all
	select p.quantity,p.kminterval,p.daysinterval,s.supplies,id,1 as chk from tblsuppliesperpattern p
	 right join tblsupplies s on p.suppliesid=s.id where p.patternID=@patternID
END
GO
/****** Object:  StoredProcedure [dbo].[createSupplies]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblsupplies

CREATE  proc [createSupplies]
( 
	@supplies	varchar(50),
	@comID		int,
	@Quantity	int,
	@cost		decimal(13,3),
	@unit		varchar(30),
	@retVal		int out
)

as
set nocount on
DECLARE @CurrentError int

if exists (select * from tblsupplies where supplies=@supplies and comID=@comID)
begin
	set @retval=1
	return
end
-- start transaction
begin transaction

-- create supplies
insert into tblsupplies (comID,supplies,cost,Quantity,unit)
values(@comID,@supplies,@cost,@Quantity,@unit)


select @currenterror=@@error if @CurrentError!=0 begin goto error_handler end
-- end of transaction
set @retVal=0

commit transaction
set nocount off
 -- Reset SET NOCOUNT to OFF

return 0
-- return 0 to indicate success, otherwise the raised error will be returned

error_handler:
	Rollback transaction
	set nocount off
	set @retVal=2
	return @currentError
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_SUPPLIES]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblsupplies

CREATE   proc [SP_INSERT_SUPPLIES]
( 
	@supplies	varchar(50),
	@comID		int,
	@Quantity	int,
	@cost		decimal(13,3),
	@unit		varchar(30),
	@retVal		int out
)

as
set nocount on
DECLARE @CurrentError int

if exists (select * from tblsupplies where supplies=@supplies and comID=@comID)
begin
	set @retval=1
select 1 as retrnval	
return 1
end
-- start transaction
begin transaction

-- create supplies
insert into tblsupplies (comID,supplies,cost,Quantity,unit)
values(@comID,@supplies,@cost,@Quantity,@unit)


select @currenterror=@@error if @CurrentError!=0 begin goto error_handler end
-- end of transaction
set @retVal=0

commit transaction
set nocount off
 -- Reset SET NOCOUNT to OFF
select 0 as retrnval
return 0
-- return 0 to indicate success, otherwise the raised error will be returned

error_handler:
	Rollback transaction
	set nocount off
	set @retVal=2
    select 2 as retrnval
	return @currentError
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SUPPLIES]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SUPPLIES]
(
	@ComID int
)
AS
BEGIN

select * from tblsupplies where comID=@ComID order by supplies asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_SUPPLIES]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblsupplies

create   proc [SP_UPDATE_SUPPLIES]
( 	
	@ID		int,
	@Quantity	int,
	@Cost		decimal(18,0),
	@Unit		varchar(30)
)
as
UPDATE [AlopekDB].[dbo].[tblSupplies]
   SET          Quantity=@Quantity,
                cost=@Cost, 
                unit=@Unit
    where id=@ID
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_SUPPLIES_PER_PATTERN]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec SP_UPDATE_PATTREN 1,'dsf','1'

create   proc [SP_DELETE_SUPPLIES_PER_PATTERN]
( 
	@patternID	varchar(15) 
)

as
delete from tblSuppliesPerPattern where patternID= @patternID
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SUPP_PATERN]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_SUPP_PATERN]
	-- Add the parameters for the stored procedure here
(
	@patternID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from tblsuppliesperpattern where patternID=@patternID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_SUPPLIES_PER_PATTERN]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from tblPattern

create   proc [SP_INSERT_SUPPLIES_PER_PATTERN]
      (     @PatternID int,
           @SuppliesID int,
           @Quantity int,
           @KmInterval int,
           @DaysInterval int
)
as
insert into tblSuppliesPerPattern
  (patternID,suppliesID,Quantity,KmInterval,DaysInterval)
 values(@PatternID,@SuppliesID,@Quantity,@KmInterval,@DaysInterval)
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MANINTAINNANCE_STATUS]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_MANINTAINNANCE_STATUS]
	-- Add the parameters for the stored procedure here
(
	@patternID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select up.patternID,up.unitID,u.UnitName,u.deviceid,u.pMaintainance from tblunitPerpattern up 
	inner join 
	tblunits u on up.unitID=u.unitID and u.isActivePattern=1 and u.patternID=@patternID and u.comID=@comID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_LOADLISTEDUNITS_FOR_SUPPLIES]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_LOADLISTEDUNITS_FOR_SUPPLIES]
	-- Add the parameters for the stored procedure here
(
	@patternID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
select unitID,unitName from tblunits where unitID in (select unitID from tblunitperPattern where patternID=@patternID) and comID=@comID and ISNULL(ISDELETE,0) <> 1

END
GO
/****** Object:  StoredProcedure [dbo].[SP_LOADNOTLISTEDUNITS_FOR_SUPPLIES]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_LOADNOTLISTEDUNITS_FOR_SUPPLIES]
	-- Add the parameters for the stored procedure here
(
	@patternID int,
	@comID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select unitID,unitName from tblunits where unitID not in (select unitID from tblunitperPattern where patternID=@patternID) and comID=@comID and ISNULL(ISDELETE,0) <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_PATTERN_PER_UNIT]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_PATTERN_PER_UNIT]
	-- Add the parameters for the stored procedure here
(
	@patternID int
)
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from tblunitperpattern where patternID=@patternID;
	UPDATE TBLUNITS SET PATTERNID=0,
	ISACTIVEPATTERN=0
	WHERE PATTERNID=@PATTERNID 
	

COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETEANDUPDATE_PATTERN_PER_UNIT]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETEANDUPDATE_PATTERN_PER_UNIT]
	-- Add the parameters for the stored procedure here
(
	@patternID int,
	@unitID int,
	@operation varchar(20)
)
AS
BEGIN TRANSACTION
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF (@operation IS NOT NULL) AND (@operation = 'create')
	BEGIN
		update tblunits set patternID=@patternID,isActivePattern=0  where unitID=@unitID;
		insert into tblunitperpattern (patternID,UnitID)values(@patternID,@unitID);
		select 0
	END

	ELSE IF (@operation IS NOT NULL) AND (@operation = 'update')
	BEGIN
		delete from tblunitperpattern where unitID=@unitID;
		insert into tblunitperpattern (patternID,UnitID)values(@patternID,@unitID);
		update tblunits set patternID=@patternID,pMaintainance=getDate(),isActivePattern=0 where unitID=@unitID
		select 0
	END

COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNIT_PER_PATTERN]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_UNIT_PER_PATTERN]
	-- Add the parameters for the stored procedure here
(
	@patternID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from tblUnitPerpattern where patternID=@patternID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_LOADMODULE]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_LOADMODULE]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from tblModule;
	select id as formid, moduleid,formname,1 as fullaccess, 1 as [delete], 1 as [view], 1 as [insert],1 as edit from tblForms
	
END



set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_ICON]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_ICON]
( 
	@iconId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DELETE from tblIcon where ID=@iconId
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_ICON]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Mostafa Zaman
-- Alter date: 21/09/2008
-- Description:	Select Icon Info from tblicon
-- =============================================
CREATE PROCEDURE [SP_SELECT_ICON]

	
AS
BEGIN
	select id,iconName,'../Icon/'+iconName+'.png' as icon from tblicon
END
GO
/****** Object:  StoredProcedure [dbo].[SP_ICONSETUP]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_ICONSETUP]
(
	-- Add the parameters for the stored procedure here
	@IconName varchar(50),
	@comID int
) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tblIcon(IconName,comID)values(@IconName,@comID)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_USERWISEUNITCAT]    Script Date: 09/09/2009 15:13:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
--select * from tblUserWiseUnitCat

CREATE PROCEDURE [SP_DELETE_USERWISEUNITCAT]
	-- Add the parameters for the stored procedure here
(	
	@userID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if exists(select * from tblUserWiseUnitCat where uID=@userID)
	begin
		delete from tblUserWiseUnitCat where uID=@userID
	end
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_USERWISEUNITCAT]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_INSERT_USERWISEUNITCAT]
	-- Add the parameters for the stored procedure here
(
	@comID int,
	@uID int,
	@unitGroupID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tblUserWiseUnitCat(comID,uID,unitGroupID)values(@comID,@uID,@unitGroupID)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_TREEcOLOR_3]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_TREEcOLOR_3]
(
	@RulesID int
)
AS
BEGIN

select f.rulesfor,r.rulesOperator,r.RulesValue from tblrules r inner join tblrulesfor f on f.rulesforid=r.rulesforid where rulesid=@RulesID
END
GO
/****** Object:  StoredProcedure [dbo].[spRulesAdd]    Script Date: 09/09/2009 15:13:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- shalin 

/*Modify By Safiq on 2nd July 2008
add new field RulesName
*/

CREATE PROCEDURE [spRulesAdd]
(
@rulesForID Int,
@RulesName varchar(50),
@opSign varchar(2),
@opName VarChar(50),
@value varChar(20),
@comID int
)

AS
SET NOCOUNT ON

DECLARE @CurrentError int

DECLARE @rID smallint
SET @rID=(SELECT isnull(MAX(RulesID),0)+1 FROM tblRules)

    -- start transaction
    BEGIN TRANSACTION

    -- create a Asset Type
    INSERT INTO tblRules (RulesID, RulesName,RulesForID, RulesOperator, RulesOperatorName, RulesValue, comID)
    VALUES(@rID, @RulesName,@rulesForID, @opSign, @opName, @value, @comID)
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_SPEEDING_DATA]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
		Alterd By : Safiqur Rhaman
		Date       : 08/10/2008
		Purpose	   : Update Speeding Data
		exec SP_UPDATE_SPEEDING_DATA 'erer','sdsd','sds','12',FALSE,3,11
		SELECT * FROM tblunitwiseRules

*/

CREATE PROC [SP_UPDATE_SPEEDING_DATA]
(
	@EMAIL		VARCHAR(100),
	@PHONENUM	VARCHAR(20),
	@PHONEEMAIL	varchar(40),
	@SUB		VARCHAR(200),
	@DES		VARCHAR(200),
	@RVALUE		VARCHAR(20),
	@STATUS		BIT,
	@ISSMS		BIT,
	@RULESID		INT,
	@UNITID			INT
	
)
AS

BEGIN TRANSACTION
	
	BEGIN TRY
	
		Update tblRules 
		SET RulesValue= @RVALUE
		where RulesID = @RULESID
		
		Update tblunitwiseRules 
		SET Email=@EMAIL ,
		SPEEDPHONENUM = @PHONENUM,
		OPERATOR = @PHONEEMAIL,
		Subject=@SUB, 
		Description=@DES,  
		isActive= @STATUS,
		ISSMS = @ISSMS
		where UnitID=@UNITID
		

		COMMIT TRANSACTION
		RETURN 0
	END TRY

	BEGIN CATCH
		ROLLBACK TRANSACTION
		RETURN -1
	END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_SPEEDING_DATA]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_INSERT_SPEEDING_DATA] 
(
	@UnitID int,   
	@Email varchar(100),
	@PHONENUMBER	VARCHAR(20),
	@PHONEEMAIL		VARCHAR(40),
	@Subject varchar(200),
	@Description varchar(200),
	@isActive bit,
	@RulesName varchar(50),
	@RulesForID int ,
	@ISSMS		BIT,
	@RulesOperator varchar(2),
	@RulesOperatorName varchar(50),
	@RulesValue varchar(20),
	@comID int
	
)
	
AS
DECLARE @RulesID INT

BEGIN  TRANSACTION

BEGIN TRY

	Insert INTO tblRules (RulesName, RulesForID,RulesOperator,RulesOperatorName, RulesValue, comID) 
	Values (@RulesName, @RulesForID,@RulesOperator,@RulesOperatorName, @RulesValue, @comID)
	
	SET @RulesID=(Select max(RulesID)as RulesID from tblRules)
	
	IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
		BEGIN
			UPDATE TBLUNITWISERULES SET
					EMAIL=@Email,
					SPEEDPHONENUM = @PHONENUMBER,
					OPERATOR = @PHONEEMAIL,
					Subject=@Subject,
					Description=@Description,
					isActive=@isActive,
					RulesID=@RulesID,
					ISSMS = @ISSMS					
			WHERE UNITID=@UNITID 
		END
	ELSE 
		BEGIN
			Insert into tblUnitWiseRules (UnitID,RulesID,Email,SPEEDPHONENUM,OPERATOR,ISSMS,Subject,Description,isActive) 
			Values (@UnitID ,
					@RulesID ,
					@Email ,
					@PHONENUMBER,
					@PHONEEMAIL,
					@ISSMS,
					@Subject,
					@Description ,
					@isActive )
			
		END

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_SPEEDING_RULES]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- Alterd By Safiqur Rhaman
-- 07.10.2008

CREATE PROCEDURE [SP_SELECT_SPEEDING_RULES]
(
	@COMID INT,
	@UNITID INT
)


AS
SET NOCOUNT ON

DECLARE @CurrentError int


    BEGIN TRANSACTION

    select * from tblUnitWiseRules join tblRules on 
	(tblRules.RulesID = tblUnitWiseRules.RulesID) 
	where tblRules.comID= @COMID and unitID= @UNITID
    
    select @CurrentError = @@Error IF @CurrentError != 0 BEGIN GOTO ERROR_HANDLER END
    -- end of transaction

    COMMIT TRANSACTION
    -- Reset SET NOCOUNT to OFF
    
    SET NOCOUNT OFF
    
    -- return 0 to indicate success, otherwise the raised error will be returned
    RETURN 0
    
   ERROR_HANDLER:
        ROLLBACK TRANSACTION
        SET NOCOUNT OFF
        RETURN @CurrentError
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertSpeed]    Script Date: 09/09/2009 15:13:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [sp_InsertSpeed] 
(
	@UnitID int,   
	@Email varchar(100),
	@Subject varchar(200),
	@Description varchar(200),
	@isActive bit,
	@RulesName varchar(50),
	@RulesForID int ,
	@RulesOperator varchar(2),
	@RulesOperatorName varchar(50),
	@RulesValue varchar(20),
	@comID int
	
)
	
AS
DECLARE @RulesID INT

BEGIN  TRANSACTION

BEGIN TRY

	Insert INTO tblRules (RulesName, RulesForID,RulesOperator,RulesOperatorName, RulesValue, comID) 
	Values (@RulesName, @RulesForID,@RulesOperator,@RulesOperatorName, @RulesValue, @comID)
	
	SET @RulesID=(Select max(RulesID)as RulesID from tblRules)
	
	IF EXISTS(SELECT * FROM TBLUNITWISERULES WHERE UNITID=@UNITID)
		BEGIN
			UPDATE TBLUNITWISERULES SET
					Email=@Email,
					Subject=@Subject,
					Description=@Description,
					isActive=@isActive,
					RulesID=@RulesID					
			WHERE UNITID=@UNITID 
		END
	ELSE 
		BEGIN
			Insert into tblUnitWiseRules (UnitID,RulesID,Email,Subject,Description,isActive) 
			Values (@UnitID ,
					@RulesID ,
					@Email ,
					@Subject,
					@Description ,
					@isActive )
			
		END

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_RULES]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER]
	DATE		: 18-10-2008
	PURPOSE		: SELECT ALL RULES DATA
select * from TBLRULES
*/

CREATE PROC [SP_SELECT_RULES]
(
	@COMID	INT
)
AS
BEGIN

	SELECT RULESID,'Unit Speed Should '+RULESOPERATORNAME+' '+RULESVALUE+' mph' AS RULES FROM TBLRULES  WHERE COMID=@COMID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_RULES_ADD]    Script Date: 09/09/2009 15:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 19-10-2008
	PURPOSE		: TO INSERT SPEEDING RULES
	SELECT * FROM TBLRULES

*/

CREATE PROC [SP_RULES_ADD]
(
	@RULESFORID			VARCHAR(20),
	@RULESOPERATOR		VARCHAR(10),
	@RULESOPERATORNAME	VARCHAR(30),
	@RULESVALUE			INT,
	@COMID				INT
)
AS

BEGIN TRANSACTION
SET NOCOUNT ON
BEGIN TRY

	INSERT INTO TBLRULES(RULESFORID,RULESOPERATOR,RULESOPERATORNAME,RULESVALUE,COMID)
	VALUES(@RULESFORID,@RULESOPERATOR,@RULESOPERATORNAME,@RULESVALUE,@COMID)
   

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH

	ROLLBACK TRANSACTION
	RETURN -1

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_IMAGE_LOCATION]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_INSERT_IMAGE_LOCATION]
(
	@INAME Varchar(50),
	@IURL  Varchar(100),
	@CID   int,
	@STATUS	bit
)
 
AS
BEGIN
if(@STATUS = 1)
 begin
	Update TBLIMAGE set IsActive = 0 Where comID=@CID
end
INSERT INTO TBLIMAGE(ImageName,ImageURL,ComID,IsActive)
VALUES(@INAME,@IURL,@CID,@STATUS)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_IMAGE_INFO]    Script Date: 09/09/2009 15:13:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman	
-- ALTER date: 27.11.2008
-- Description:	Delete Image info
-- =============================================
CREATE PROCEDURE [SP_DELETE_IMAGE_INFO]
(
	@ID INT
	--@INAME VarChar(100) output
 )
	
AS
BEGIN
	 
	
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    --SET @INAME = (SELECT ImageName from TBLIMAGE where ID = @ID)
    DELETE TBLIMAGE WHERE ID = @ID
	--Return @INAME
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_IMAGE_INFO]    Script Date: 09/09/2009 15:13:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman	
-- ALTER date: 26.11.2008
-- Description:	Select Image info
-- =============================================
CREATE PROCEDURE [SP_SELECT_IMAGE_INFO]
(
	@COMID INT
	
 )
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT ID,'../AdvertisingImages/'+ImageName as Image,ImageURL,IsActive,ImageName  FROM tblImage WHERE ComID = @COMID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_ACTIVE_IMAGE_INFO]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Safiqur Rhaman	
-- ALTER date: 26.11.2008
-- Description:	Select Image info
-- =============================================
CREATE PROCEDURE [SP_SELECT_ACTIVE_IMAGE_INFO]
(
	@COMID INT
	
 )
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 'AdvertisingImages/'+ImageName as Image,IsActive,'http://'+ImageURL as URL FROM tblImage WHERE ComID = @COMID
	AND ISACTIVE=1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_IMAGE_LOCATION]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_UPDATE_IMAGE_LOCATION]
(
	@ID INT,
	@IURL  Varchar(100),
	@COMID INT,
	@STATUS	bit
)
 
AS
BEGIN
if(@STATUS = 1)
 begin
	Update TBLIMAGE set IsActive = 0 Where comID=@COMID
end
	UPDATE TBLIMAGE SET IMAGEURL = @IURL, ISACTIVE=@STATUS where ID =@ID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_INSERT_RPT_TIMEZONE]    Script Date: 09/09/2009 15:13:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_INSERT_RPT_TIMEZONE]
	-- Add the parameters for the stored procedure here
(
	@rptLocation varchar(50),
	@tzValue float
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into tblrptTimeZone(rptLocation,tzValue)values(@rptLocation,@tzValue)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_TIMEZONE]    Script Date: 09/09/2009 15:13:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_TIMEZONE]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select tzID,rptLocation+'(UTC'+convert(varchar,tzvalue)+')' as timezone from tblrptTimezone
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_RPT_TIMEZONE]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_RPT_TIMEZONE]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from tblrptTimeZone
END
GO
/****** Object:  StoredProcedure [dbo].[SP_UPDATE_RPT_TIMEZONE]    Script Date: 09/09/2009 15:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_UPDATE_RPT_TIMEZONE] 
	-- Add the parameters for the stored procedure here
(
	@rptLocation varchar(50),
	@tzValue float,
	@tzID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblrptTimeZone SET rptLocation = @rptLocation, tzValue = @tzValue WHERE tzID = @tzID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_RPT_TIMEZONE]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_DELETE_RPT_TIMEZONE]
	-- Add the parameters for the stored procedure here
(
	@tzID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE FROM  tblrptTimeZone WHERE tzID = @tzID
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TIMEALERT]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SP_TIMEALERT]
(
	@UNITID			INT,
	@COMID			INT,
	@ALERTMESSAGE	VARCHAR(200)
)
AS
BEGIN TRANSACTION

BEGIN TRY
  
		IF EXISTS (SELECT * FROM TBLALERT WHERE UNITID=@UNITID AND COMID=@COMID AND ALERTTYPE='TIME' AND CONVERT(VARCHAR,ALERTTIME,101)=CONVERT(VARCHAR,GETDATE(),101))	
		BEGIN
			UPDATE TBLALERT SET
				   ALERTMESSAGE=@ALERTMESSAGE,
				   ALERTTIME=GETDATE()
			WHERE COMID=@COMID AND UNITID=@UNITID AND CONVERT(VARCHAR,ALERTTIME,101)=CONVERT(VARCHAR,GETDATE(),101) and ALERTTYPE='TIME'
		END

		ELSE 
			INSERT INTO TBLALERT(ALERTTYPE,ALERTMESSAGE,ALERTTIME,COMID,UNITID)
			VALUES('Time',@ALERTMESSAGE,GETDATE(),@COMID,@UNITID)

COMMIT TRANSACTION
RETURN 0

END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_UNIT_ENABLE]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. Safiqur Rhaman
	DATE		: 19TH JAN 2009
	PURPOSE		: DISABLE USER
*/

CREATE PROC [SP_UNIT_ENABLE]
(
	@UID		INT
	
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE TBLUNITS SET ISACTIVE=1
	WHERE UNITID=@UID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_UNIT_DISABLE]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. Safiqur Rhaman
	DATE		: 19TH JAN 2009
	PURPOSE		: DISABLE USER
*/

CREATE PROC [SP_UNIT_DISABLE]
(
	@UID		INT
	
)

AS

BEGIN TRANSACTION

BEGIN TRY
	
	UPDATE TBLUNITS SET ISACTIVE=0
	WHERE UNITID=@UID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_DELETE_UNIT]    Script Date: 09/09/2009 15:13:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 15TH DEC 2008
	PURPOSE		: DELETE UNIT
*/

CREATE PROC [SP_DELETE_UNIT]
(
	@UNITID		INT
)

AS

BEGIN TRANSACTION

BEGIN TRY

	UPDATE TBLUNITS SET ISDELETE=1
	WHERE UNITID=@UNITID

COMMIT TRANSACTION
RETURN 0
END TRY

BEGIN CATCH
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNITS_COMWISE]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_UNITS_COMWISE]
	-- Add the parameters for the stored procedure here
(
	@ComID int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select unitID,unitName from tblunits where comID=@ComID order by unitName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_PATTERN_UNITS]    Script Date: 09/09/2009 15:13:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 20-10-2008
	PURPOSE		: SELECT UNITS OF A PARTICULAR PATTERN
*/

CREATE PROC [SP_SELECT_PATTERN_UNITS]
(
	@PATTERNID		INT
)
AS

BEGIN

	SELECT UNITID,UNITNAME,ISNULL(ISACTIVEPATTERN,'False') AS PATH FROM TBLUNITS WHERE PATTERNID=@PATTERNID	
order by UNITNAME asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNITS_COMPANY]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 27-09-2008
	PURPOSE		: SELECT ALL UNITS OF A PARTICULAR COMPANY

*/

CREATE PROC [SP_SELECT_UNITS_COMPANY]
(
	@COMID		INT
)
AS

BEGIN

	SELECT * FROM TBLUNITS	WHERE COMID=@COMID AND ISNULL(ISDELETE,0)<>1 order by unitName asc

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNIT]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_UNIT]
(
	@comID int
)
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select UnitID,UnitName from tblUnits where comID=@comID and ISNULL(ISDELETE,0) <> 1 order 
by UnitName asc
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNITINFO]    Script Date: 09/09/2009 15:13:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [SP_SELECT_UNITINFO]
	-- Add the parameters for the stored procedure here
(
	@UnitID int
)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select * from tblUnits where UnitID=@UnitID and isnull(isDelete,0)<>1 order by unitName
END
GO
/****** Object:  StoredProcedure [dbo].[SP_CHECK_INFO]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Alter date: <Alter Date,,>
-- Description:	<Description,,>
-- SP_CHECK_INFO 385,17,385
-- =============================================
CREATE PROCEDURE [SP_CHECK_INFO]
	-- Add the parameters for the stored procedure here
(
	@UnitID int,
	@comID int,
	@UNAME varchar(50)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--SELECT deviceID FROM tblUnits  WHERE  deviceID = isnull(@UnitID,0) and comID =@comID
	SELECT deviceID FROM tblUnits  WHERE  comID =@comID AND (deviceID = isnull(@UnitID,0) oR unitName=@UNAME) and isNull(isDelete,0) <> 1
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MINIMAP_DATA]    Script Date: 09/09/2009 15:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: Safiqur Rhaman	
	DATE		: 27-09-2008
	PURPOSE		: SELECT DATA FOR MININMAP,
    SP_SELECT_MINIMAP_DATA 1,1,1
	SP_SELECT_MINIMAP_DATA 93,17,385
select * from vwUnitRecords
select * from tblunits where comid = 17 and isnull(isdelete,0) <>1
	select * from tblUser where comid = 17
*/

CREATE	PROC	[SP_SELECT_MINIMAP_DATA]
(
	@USERID		INT,
	@COMID		INT,
	@UNITID		INT

)

AS

BEGIN

	

--select UnitName, lat, long, deviceID, postalCode,City,State,Country,
--Cast(velocity*0.621 as int) as velocity,rectime ,dateadd(ss,recTime,'01/01/2000') as  recTimeRevised,iconName 
--from vwUnitRecords where recTime IN 
--(SELECT max(recTime) FROM vwUnitRecords where deviceID 
--in (select deviceID from vwUserWiseUnits 
--where uID =@USERID and comID=@COMID  group by deviceID) and comID=@COMID group by deviceID) and comID=@comID and ;

select UnitName, lat, long, deviceID, postalCode,City,State,Country,
Cast(velocity*0.621 as int) as velocity,rectime ,dateadd(ss,recTime,'01/01/2000') 
as  recTimeRevised,iconName from vwUnitRecords where 
recTime=(SELECT max(recTime) FROM vwUnitRecords where deviceID=@unitID and comID=@comID)
and deviceID=@unitID and comID=@comID

select (select t.typeName from tblunittype t where t.typeID=u.typeid and comid=@COMID) as typeName,
u.UnitName, u.LicenseID, u.DriverName, u.deviceId 
from tblunits u where comid=@COMID and deviceid=@UNITID and isnull( u.isdelete,0) <> 1;

select grpName from tblgroup where grpid in (select groupID from tblgroupwiseunit where comid=@COMID
 and unitID=(select unitID from tblunits where deviceid=@UNITID and comiD=@COMID and isNull(isDelete,0) <>1))

END
GO
/****** Object:  StoredProcedure [dbo].[SP_TREE_DATA_SELECT]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 25-09-2008
	PURPOSE		: SELECT DATA FOR UNIT TREE CREATION
	SP_TREE_DATA_SELECT 2,93

	select * from tblcompany
	select * from tbluser where comID=2

*/

CREATE PROC [SP_TREE_DATA_SELECT]
(
	@COMID		INT,
	@UID		INT
)

AS
BEGIN

	SELECT  DISTINCT TYPEID,TYPENAME FROM VWUSERWISEUNITS WHERE UID=@UID
	AND COMID=@COMID ORDER BY TYPENAME;

	SELECT DISTINCT DEVICEID,UNITNAME,TYPEID,UNITID FROM VWUSERWISEUNITS 
	WHERE UID=@UID	AND COMID=@COMID ORDER BY UNITNAME;

	SELECT DISTINCT GP.DEVICEID, MAX(GP.RECTIMEREVISED) AS MAXTIME,MAX(GP.RECTIME)
	AS RECTIME, AUW.UID FROM TBLGPRS GP
	INNER JOIN TBLUNITS U ON U.DEVICEID=GP.DEVICEID
	INNER JOIN TBLUNITUSERWISE AUW ON AUW.UNITID=U.UNITID
	WHERE AUW.UID=@UID GROUP BY GP.DEVICEID, AUW.UID

	SELECT DISTINCT UNITID,STATUSCODE FROM TBLUNITSTATUS
	WHERE UNITID IN (SELECT UNITID FROM TBLUNITUSERWISE WHERE UID = @UID)
	ORDER BY UNITID



END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_DATA_BREADCRAMS1]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZMAN SIKDER
	DATE		: 28-09-2008
	PURPOSE		: SELECT DATA FOR BREADCRAMS 

*/

CREATE PROC [SP_SELECT_DATA_BREADCRAMS1]
(
	@DEVICEID		INT,
	@RECTIME		INT,
	@COMID			INT
)
AS
BEGIN

	SELECT TOP 25 UNITNAME,LAT,LONG,DEVICEID,POSTALCODE,CITY,STATE,COUNTRY,
	CAST(VELOCITY*0.621 AS INT) AS VELOCITY,RECTIME,RECTIMEREVISED,ICONNAME 
	FROM VWUNITRECORDS	WHERE DEVICEID=@DEVICEID AND COMID=@COMID AND
	 RECTIME BETWEEN @RECTIME	AND (SELECT MAX(RECTIME) FROM TBLGPRS 
	WHERE DEVICEID=@DEVICEID) ORDER BY RECTIME DESC



END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_DATA_BREADCRAMS2]    Script Date: 09/09/2009 15:13:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZMAN SIKDER
	DATE		: 28-09-2008
	PURPOSE		: SELECT DATA FOR BREADCRAMS 
	EXEC SP_SELECT_DATA_BREADCRAMS2 502,1
	select * from tblCompany

*/

CREATE PROC [SP_SELECT_DATA_BREADCRAMS2]
(
	@DEVICEID		INT,	
	@COMID			INT
)
AS
BEGIN

	SELECT UNITNAME,LAT,LONG,DEVICEID,POSTALCODE,CITY,STATE,COUNTRY,
	CAST(VELOCITY*0.621 AS INT) AS VELOCITY,RECTIME,RECTIMEREVISED,ICONNAME  FROM 
	VWUNITRECORDS	WHERE RECTIME=(SELECT MAX(RECTIME)  FROM VWUNITRECORDS
	WHERE DEVICEID=@DEVICEID AND COMID=@COMID)	AND COMID=@COMID AND DEVICEID=@DEVICEID


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MAINMAP_DATA_UNITTYPE]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 25-09-2008
	PURPOSE		: SELECT UNIT DATA OF A UNIT TYPE
	exec SP_SELECT_MAINMAP_DATA_UNITTYPE 1,1,2
*/

CREATE PROC [SP_SELECT_MAINMAP_DATA_UNITTYPE]
(
	@COMID		INT,
	@UID		INT,
	@UNITTYPEID	INT
)
AS
BEGIN
	
	SELECT DISTINCT UNITNAME,LAT,LONG,DEVICEID,POSTALCODE,CITY,STATE,COUNTRY,CAST(VELOCITY*0.621 AS INT) AS VELOCITY,
	RECTIME,DATEADD(SS,RECTIME,'01/01/2000') AS RECTIMEREVISED,ICONNAME FROM VWUNITRECORDS 
	WHERE RECTIME IN (SELECT MAX(RECTIME) FROM VWUNITRECORDS WHERE DEVICEID IN 
	(SELECT DEVICEID FROM VWUSERWISEUNITS WHERE UID=@UID AND TYPEID=@UNITTYPEID)
	GROUP BY DEVICEID) AND COMID=@COMID


END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_MAINMAP_DATA]    Script Date: 09/09/2009 15:13:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 24-09-2008
	PURPOSE		: SELECT DATA FOR MAINMAP,
*/

CREATE	PROC	[SP_SELECT_MAINMAP_DATA]
(
	@USERID		INT,
	@COMID		INT

)

AS

BEGIN

	SELECT DISTINCT UNITNAME,LAT,LONG,DEVICEID,POSTALCODE,CITY,STATE,COUNTRY,
	CAST(VELOCITY*0.621 AS INT)AS VELOCITY,RECTIME,RECTIMEREVISED,ICONNAME 
	FROM VWUNITRECORDS VR WHERE RECTIME=(SELECT MAX(RECTIME) 
	FROM VWUNITRECORDS WHERE DEVICEID=(SELECT DISTINCT DEVICEID 
	FROM VWUSERWISEUNITS VU WHERE UID=@USERID AND VR.DEVICEID=VU.DEVICEID
	) AND COMID=@COMID) AND COMID=@COMID ORDER BY UNITNAME

END
GO
/****** Object:  StoredProcedure [dbo].[SP_UNIT_HISTORICALDATA]    Script Date: 09/09/2009 15:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 07/10/2008
	PURPOSE		: SELECT UNIT'S HISTORICAL DATA
	exec SP_UNIT_HISTORICALDATA '7/13/2009 12:00:00 AM','7/13/2009 11:59:00 AM',1313,2
*/

CREATE PROC [SP_UNIT_HISTORICALDATA]
(
	@STARTDATE		DATETIME,
	@ENDDATE		DATETIME,
	@DEVICEID		INT,
	@COMID			INT
)
AS
BEGIN

	begin try
	SELECT distinct UNITNAME,POSTALCODE,CITY,STATE,COUNTRY,LAT,LONG,CAST(isnull(VELOCITY,0)*0.621 AS INT) AS VELOCITY,
	--DATEADD(SS,RECTIME,'01/01/2000') AS RecDateTime,ICONNAME,DISTANCE,RECTIME FROM VWUNITRECORDS
	RECTIMEREVISED AS RecDateTime,ICONNAME,DISTANCE,RECTIME FROM VWUNITRECORDS
	WHERE DEVICEID=@DEVICEID AND RECTIMEREVISED BETWEEN @STARTDATE AND @ENDDATE	
	AND COMID=@COMID    ORDER BY RECTIME DESC

	end try
	begin catch
	select error_message()
	end catch

END
GO
/****** Object:  StoredProcedure [dbo].[SP_SELECT_UNITCOUNT]    Script Date: 09/09/2009 15:13:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	AlterD		: MD. ROKANUZZAMAN SIKDER
	DATE		: 25-09-2008
	PURPOSE		: COUNT NUMBER OF UNIT OF A UNIT TYPE

*/

CREATE PROC [SP_SELECT_UNITCOUNT]
(
	@COMID		INT,
	@UID		INT,
	@TYPEID		INT
)

AS

BEGIN

	SELECT ISNULL(COUNT(DEVICEID),0) as unitCount FROM VWUSERWISEUNITS 
	WHERE UID=@UID AND COMID=@COMID AND TYPEID=@TYPEID

END
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATE_DEFAULTSCHEME]    Script Date: 09/09/2009 15:13:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	CREATED BY	: MD. ROKANUZZAMAN SIKDER
	DATE		: 15-01-2008
	PURPOSE		: CREATE DEFAULT SECURITY SCHEME

select * from tblSecurityScheme
select * from tblSchemePermission
select * from tblForms
SP_CREATE_DEFAULTSCHEME 27
*/

CREATE PROC [SP_CREATE_DEFAULTSCHEME]
(
	@COMID		INT
)
AS

BEGIN TRANSACTION

BEGIN TRY
	
	INSERT INTO TBLSECURITYSCHEME(SCHEMENAME,COMID,DEFAULTSCHEME)
	VALUES('Default Scheme',@COMID,1)


COMMIT TRANSACTION
EXEC SP_DEFAULT_SCHEME_PERMISSION @COMID
RETURN 0
END TRY

BEGIN CATCH
	
	ROLLBACK TRANSACTION
	RETURN -1
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[SP_CREATECOMPANY]    Script Date: 09/09/2009 15:13:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created By: Rokan
	Date:	25th March 2008
	select * from tblCompany
	select * from tblunittype
	select * from tblgroup
	select * from tbluser where comid=27
	createCompany 'nnn','','','','','sd',1
*/

CREATE       proc [SP_CREATECOMPANY]
(
	@comName	varchar(70),
	@address	varchar(100),
	@phone		varchar(20),
	@email		varchar(40),
	@webSite	varchar(80),
	@Password	varchar(150),
	@retVal		int out
)
as 

declare @error as int,@coID as int,@grpID as int,@typeID as int
begin transaction
if exists(select * from tblcompany where companyName=@comName)
begin
	set	@retVal=0
end

	insert into tblCompany(companyName,address,phone,email,website,REGDATE)
	values	(@comName,@address,@phone,@email,@website,GETDATE())

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	
	set @coID=(select max(comID) from tblcompany)


	
	INSERT INTO TBLUNITTYPE(TYPENAME,COMID)VALUES('General',@coID)
	select @error=@@error if @error !=0 begin GOTO Error_handler End
	


	insert into tblGroup(grpName,comID)values('Administrator',@coID)

	select @error=@@error if @error !=0 begin GOTO Error_handler End
	
	set @grpID=(select grpID from tblgroup where comID=@coID)
	
	
	insert into tbluser(comID,groupID,Login,Password,UserName,RoleID,isActive)
	values(@coID,@grpID,'Admin','4EygcWyUSxc=','Administrator',2,1)

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	insert into tblGroup(grpName,comID)values('General',@coID)

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	EXEC SP_CREATE_DEFAULTSCHEME @coID

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	set @retVal=@coID
Commit Transaction

return 0

Error_Handler:
	RollBack Transaction
	Set NoCount Off
	set @retVal=2
	Return @error
GO
/****** Object:  StoredProcedure [dbo].[createCompany]    Script Date: 09/09/2009 15:13:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
	Created By: Rokan
	Date:	25th March 2008
	select * from tblCompany
	select * from tblunittype
	select * from tblgroup
	select * from tbluser

	createCompany 'nnn','','','','','sd',1
*/

CREATE       proc [createCompany]
(
	@comName	varchar(70),
	@address	varchar(100),
	@phone		varchar(20),
	@email		varchar(40),
	@webSite	varchar(80),
	@pass		varchar(100),
	@retVal		int out
)
as 
set nocount on
declare @error as int,@coID as int,@grpID as int,@typeID as int
begin transaction
if exists(select * from tblcompany where companyName=@comName)
begin
	set	@retVal=0
end

	insert into tblCompany(companyName,address,phone,email,website)
	values	(@comName,@address,@phone,@email,@website)

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	
	set @coID=(select max(comID) from tblcompany)


	exec 	spunitTypeAdd 'General',@coID		
	select @error=@@error if @error !=0 begin GOTO Error_handler End
	


	insert into tblGroup(grpName,comID)values('Administrator',@coID)

	select @error=@@error if @error !=0 begin GOTO Error_handler End
	
	set @grpID=(select grpID from tblgroup where comID=@coID)
	
	
	insert into tbluser(comID,groupID,Login,Password,UserName,RoleID,isActive)
	values(@coID,@grpID,'Administrator','4EygcWyUSxc=','Administrator',2,1)

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	insert into tblGroup(grpName,comID)values('General',@coID)

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	EXEC SP_CREATE_DEFAULTSCHEME @coID

	select @error=@@error if @error !=0 begin GOTO Error_handler End

	set @retVal=@coID
Commit Transaction
Set NoCount off
return 0

Error_Handler:
	RollBack Transaction
	Set NoCount Off
	set @retVal=2
	Return @error
GO

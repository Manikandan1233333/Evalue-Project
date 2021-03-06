USE [master]
GO
/****** Object:  Database [Authentication]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Authentication')
BEGIN
CREATE DATABASE [Authentication]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Authentication_Data', FILENAME = N'E:\SQL_Data\Authentication.mdf' , SIZE = 332224KB , MAXSIZE = UNLIMITED, FILEGROWTH = 512000KB )
 LOG ON 
( NAME = N'Authentication_Log', FILENAME = N'N:\SQL_Logs\Authentication_1.ldf' , SIZE = 568896KB , MAXSIZE = UNLIMITED, FILEGROWTH = 512000KB )
END

GO
ALTER DATABASE [Authentication] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Authentication].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [Authentication] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Authentication] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Authentication] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Authentication] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Authentication] SET ARITHABORT OFF 
GO
ALTER DATABASE [Authentication] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Authentication] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Authentication] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Authentication] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Authentication] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Authentication] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Authentication] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Authentication] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Authentication] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Authentication] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Authentication] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Authentication] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Authentication] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Authentication] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Authentication] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Authentication] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Authentication] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Authentication] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Authentication] SET RECOVERY FULL 
GO
ALTER DATABASE [Authentication] SET  MULTI_USER 
GO
ALTER DATABASE [Authentication] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Authentication] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Authentication] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Authentication] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Authentication', N'ON'
GO
USE [Authentication]
GO
/****** Object:  User [PCReportsUser]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'PCReportsUser')
CREATE USER [PCReportsUser] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENT\svc_ent_PaymentTool]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ENT\svc_ent_PaymentTool')
CREATE USER [ENT\svc_ent_PaymentTool] FOR LOGIN [ENT\svc_ent_PaymentTool] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENT\gqgnaik]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ENT\gqgnaik')
CREATE USER [ENT\gqgnaik] FOR LOGIN [ENT\gqgnaik] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENT\gg5ibho]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ENT\gg5ibho')
CREATE USER [ENT\gg5ibho] FOR LOGIN [ENT\gg5ibho] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENT\geskerb]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ENT\geskerb')
CREATE USER [ENT\geskerb] FOR LOGIN [ENT\geskerb] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENT\esegopa]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'ENT\esegopa')
CREATE USER [ENT\esegopa] FOR LOGIN [ENT\esegopa] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [Authentication_role]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Authentication_role' AND type = 'R')
CREATE ROLE [Authentication_role]
GO
ALTER ROLE [db_datareader] ADD MEMBER [PCReportsUser]
GO
ALTER ROLE [Authentication_role] ADD MEMBER [ENT\svc_ent_PaymentTool]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ENT\gqgnaik]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ENT\gg5ibho]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ENT\geskerb]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ENT\esegopa]
GO
/****** Object:  Schema [Authentication_role]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'Authentication_role')
EXEC sys.sp_executesql N'CREATE SCHEMA [Authentication_role]'

GO
/****** Object:  UserDefinedDataType [dbo].[ADDRESSLINE]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'ADDRESSLINE' AND ss.name = N'dbo')
CREATE TYPE [dbo].[ADDRESSLINE] FROM [varchar](100) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[BOOLEAN]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'BOOLEAN' AND ss.name = N'dbo')
CREATE TYPE [dbo].[BOOLEAN] FROM [tinyint] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[EMAILADDRESS]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'EMAILADDRESS' AND ss.name = N'dbo')
CREATE TYPE [dbo].[EMAILADDRESS] FROM [varchar](100) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[LONGDESC]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'LONGDESC' AND ss.name = N'dbo')
CREATE TYPE [dbo].[LONGDESC] FROM [varchar](256) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[NAME]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'NAME' AND ss.name = N'dbo')
CREATE TYPE [dbo].[NAME] FROM [varchar](100) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[PASSWORD]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'PASSWORD' AND ss.name = N'dbo')
CREATE TYPE [dbo].[PASSWORD] FROM [varchar](256) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[PHONE]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'PHONE' AND ss.name = N'dbo')
CREATE TYPE [dbo].[PHONE] FROM [varchar](25) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[RECORDID]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'RECORDID' AND ss.name = N'dbo')
CREATE TYPE [dbo].[RECORDID] FROM [int] NULL
GO
/****** Object:  UserDefinedDataType [dbo].[SHORTNAME]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'SHORTNAME' AND ss.name = N'dbo')
CREATE TYPE [dbo].[SHORTNAME] FROM [varchar](50) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[STATECODE]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'STATECODE' AND ss.name = N'dbo')
CREATE TYPE [dbo].[STATECODE] FROM [varchar](2) NULL
GO
/****** Object:  UserDefinedDataType [dbo].[USERNAME]    Script Date: 8/5/2015 9:52:54 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'USERNAME' AND ss.name = N'dbo')
CREATE TYPE [dbo].[USERNAME] FROM [varchar](50) NULL
GO
/****** Object:  StoredProcedure [dbo].[AUTH_ActiveDOandUser]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ActiveDOandUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_ActiveDOandUser]  
( 
/*                                          
DESCRIPTION:                                          
This procedure will check for Active district offices and users associated with the HUB office

PARAMETERS:
@DOID - District Office number

CREATED:
4/25/2006

*/ 
@DOID varchar(3)  
)  
AS              
SET NOCOUNT ON     
  
--check for active branch office(s)         
IF EXISTS (select DO_Code from CSAA_DO a inner join CSAA_DO_MAPPING b 
                    	  on   a.DO_Code = b.DO 
                          where a.Enabled = 1  and b.HUB = @DOID)  
     BEGIN                    
       RAISERROR(''Cannot be deactivated; associated offices are still active.'', 13,1)                                            
     RETURN                                            
       END    

--check for active users(s)   
IF EXISTS (select USER_NM from CSAA_USERS where USER_ACTIVE=1 and (USER_DO=@DOID or USER_DO in (select DO from CSAA_DO_MAPPING where HUB=@DOID)) ) 
  BEGIN                    
    RAISERROR(''Cannot be deactivated; associated users with the office are still active.'', 14,1)                                            
  RETURN                                            
    END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_Authenticate]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_Authenticate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AUTH_Authenticate]
/*
DESCRIPTION:
Tries to find a user matching the parameters, if so calls AUTH_GetUserInfo to return the
values.
PARAMETERS:
@CurrentUser - The UserId to authenticate
@Password - The encrypted password
@AppName - The application name
@Timeout - The session timeout.
CREATED:
7/30/2003 Jeff McEwen
MODIFICATION HISTORY:
9/9/2003 JOM Added Token generation
9/25/2003 JOM changed to return existing GUID if session is in existance, added @Timeout parameter
2/3/2004 Added the setting of LOCKOUT_COUNTER to the CSAA_USERS table. Bindu.
SSO-Integration - Changes done by Cognizant to handle the SSO integration changes on 09/24/2010
SSO-Integration.Ch1: Added new parameter named @OpenTokenExists on 09/24/2010
SSO-Integration.Ch2: Added new condition to check whether OpenToken is present and its value on 09/24/2010
SSO-Integration.Ch3: Added new logic to return the error messages on 09/29/2010
SSO-Integration.Ch4: Added new logic to return the error message on 09/24/2010
SSO-Integration.Ch5: Added new logic to get the Application Names from Pay_Constant table 09/28/2010
SSO-Integration.Ch6: Added new parameters named @AppNameConst, @UserIdActive and @ErrorMsg on 09/24/2010
SSO-Integration.Ch7: Replaced error messages with new comments on 10/21/2010
SSO-Integration.BugFix1 - Modified the condition by adding new checking for NULL value for @Password on 11/10/2010 by Cognizant
MODIFIED BY COGNIZANT ON 08/29/2011:
67811A0 - PCI Remediation for Payment systems - CH1: Declare @lockcounter and fetch currrent user Lockout count value.
67811A0 - PCI Remediation for Payment systems - CH2: Password Failed attempt message logged along with the lock counter.
--RFC 185138 - AD Integration CH1 : Commented the password input parameter
--RFC 185138 - AD Integration CH2 - Commented the password validation
--RFC 185138 - AD Integration CH4 - Commented the logging activity SP call
*/
@CurrentUser USERNAME,
--RFC 185138 - AD Integration CH1 : Commented the password input parameter
--@Password PASSWORD = NULL,
@AppName VARCHAR(50),
@Timeout INT,
@OpenTokenExists BIT = NULL --SSO-Integration.Ch1: Added new parameter named @OpenTokenExists on 09/24/2010
AS
SET NOCOUNT ON
--SSO-Integration.Ch6: Added new parameters named @AppNameConst, @UserIdActive and @ErrorMsg on 09/24/2010
DECLARE @St VARCHAR(256), @AppId INT, @UserRid INT, @AppNameConst VARCHAR(50),@UserIdActive BIT, @ErrorMsg VARCHAR(256)
--67811A0 - PCI Remediation for Payment systems - CH1: START - Declare @lockcounter and fetch currrent user Lockout count value.
, @lockcounter int 
--67811A0 - PCI Remediation for Payment systems - CH1: END - Declare @lockcounter and fetch currrent user Lockout count value.
SET @St = ''USER '' + @CurrentUser + '' AUTHENTICATION ''
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)
--SSO-Integration.Ch5: Gets the application names to which SSO integration has been done from Constant table 
SELECT @AppNameConst = [Constant_Value] FROM [Payments].[dbo].[PAY_Constants] WHERE Constant_Key =''SSOApplications''
--SSO-Integration.Ch3: Added new new logic to return the error messages on 09/29/2010
--Check if the AppName is present in ApplicationNames in [PAY_Constants] table
IF ( CHARINDEX(@AppName,@AppNameConst,0) > 0 )
BEGIN 
--Check whether the User is active or not / user is valid / user has sufficient access privileges to Payment Tool etc
SET @ErrorMsg = ''''
SELECT @UserIdActive = USER_ACTIVE FROM CSAA_USERS 
WHERE USER_NM = @CurrentUser 
			--RFC 185138 - AD Integration CH2 - Commented the password validation
			--IF ( @UserIdActive IS NOT NULL) 
			--BEGIN 	
--SSO-Integration.BugFix1 - Modified the condition by adding new checking for NULL value for @Password 
				--IF (@Password IS NOT NULL AND @Password <> '''')	
				--BEGIN
				--	SET @UserIdActive = NULL	
				--	SELECT @UserIdActive = USER_ACTIVE FROM CSAA_USERS 
				--	WHERE USER_NM = @CurrentUser AND USER_PASSWORD = @Password	
				--END	
IF ( @UserIdActive IS NULL) 
BEGIN 
--SSO-Integration.Ch7:
SET @ErrorMsg = ''You are not authorized to access this application. Please contact your manager for access set-up.''
END 
IF ( @UserIdActive = 0 )
BEGIN
--SSO-Integration.Ch7:
SET @ErrorMsg = ''Your signon has expired for this application. Please contact IT Help Desk to re-activate.''
END
END
			--ELSE
			--	BEGIN
			--		--SSO-Integration.Ch7:
			--		--SET @ErrorMsg = ''User ID is invalid or does not have access to Payment Tool.''
			--		SET @ErrorMsg = ''You are not authorized to access this application. Please contact your manager for access set-up.''
			--	END
		--END
	--RFC 185138 - AD Integration CH2 - Commented the password validation
--SSO-Integration.Ch2: Added new new condition and code to check whether OpenToken is present and its value on 09/24/2010
--Check whether the OpenTokenExists is true or false
IF ((@OpenTokenExists IS NOT NULL )AND (@OpenTokenExists = 1))
BEGIN 
SET @UserRid = (
SELECT DISTINCT U.USER_RID FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES A ON U.USER_RID = A.USER_RID 
WHERE U.USER_ACTIVE = 1
AND U.USER_NM = @CurrentUser 
AND A.APP_RID = @AppId 
)
END
ELSE 
BEGIN 
SET @UserRid = (
SELECT DISTINCT U.USER_RID FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES A ON U.USER_RID = A.USER_RID 
WHERE U.USER_ACTIVE = 1
AND U.USER_NM = @CurrentUser
				--RFC 185138 - AD Integration CH3 - Commented the password parameter
				--AND U.USER_PASSWORD = @Password
				--RFC 185138 - AD Integration CH3 - Commented the password parameter
AND A.APP_RID = @AppId
) 
END
-- Update Last login and create token
IF @UserRid IS NULL
BEGIN
--67811A0 - PCI Remediation for Payment systems - CH1: START - Declare @lockcounter and fetch currrent user Lockout count value.
	--SELECT @lockcounter = LOCKOUT_COUNTER FROM CSAA_USERS WHERE USER_NM = @CurrentUser
--67811A0 - PCI Remediation for Payment systems - CH1: END - Declare @lockcounter and fetch currrent user Lockout count value.
-- 67811A0 - PCI Remediation for Payment systems - CH2:START - Password Failed attempt message logged along with the lock counter. 
--SET @St = @St + ''FAILED''
	---RFC 185138 - AD Integration - End - CH2- Commented the failed attempt message
	--SET @St = @St + '' FAILED ATTEMPT '' + CONVERT(VARCHAR(1), @lockcounter)	
-- 67811A0 - PCI Remediation for Payment systems - CH2:END - Password Failed attempt message logged along with the lock counter.
-- Update Last login
UPDATE CSAA_USERS 
SET 
DATE_LAST_LOGIN=GETDATE()
--LOCKOUT_COUNTER = ISNULL(LOCKOUT_COUNTER,0) + 1
WHERE USER_NM=@CurrentUser 
--SSO-Integration.Ch4: Added new logic to return the error message on 09/24/2010
IF ( CHARINDEX(@AppName,@AppNameConst,0) > 0 ) 
BEGIN
IF (@ErrorMsg <> '''') 
SELECT @ErrorMsg AS ERRORINFO
ELSE
--SSO-Integration.Ch7: 
SELECT ''You are not authorized to access this application. Please contact your manager for access set-up.'' AS ERRORINFO
END
END
ELSE
BEGIN
DECLARE @Last_Check DATETIME, @Token UNIQUEIDENTIFIER
-- Find the last token check time.
SELECT @Last_Check = TOKEN_CHECKED, @Token=TOKEN FROM CSAA_USERS WHERE USER_NM = @CurrentUser
IF @Token IS NULL OR @Last_Check IS NULL OR DATEDIFF(mi, @Last_Check, GETDATE())>@Timeout
SET @Token = NEWID()
UPDATE CSAA_USERS 
SET 
DATE_LAST_LOGIN=GETDATE(),
TOKEN = @Token,
	TOKEN_CHECKED=GETDATE()
	--LOCKOUT_COUNTER = 0
WHERE USER_RID=@UserRid
	--RFC 185138 - AD Integration CH4 Start - Commented the logging activity SP call
	--SET @St = @St + ''OK''
EXEC dbo.AUTH_GetUserInfo @CurrentUser, @UserRid, @CurrentUser, @AppName, @Token
END
-- STANDARD CALL TO AUDIT ACTIVITY
--EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId
--RFC 185138 - AD Integration CH4 End - Commented the logging activity SP call
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_CheckAdmin]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_CheckAdmin]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROCEDURE [dbo].[AUTH_CheckAdmin]
@UserId VARCHAR(50),
@AppName VARCHAR(50)
/*
DESCRIPTION:
This procedure checks to see if @CurrentUser is an administrator or SU.  Raises
an exception if not.
*/
AS

IF NOT EXISTS (
	SELECT U.USER_RID
	FROM CSAA_USERS AS U 
	INNER JOIN CSAA_USERS_ROLES AS UR ON U.USER_RID=UR.USER_RID
	INNER JOIN CSAA_ROLES AS R ON UR.ROLE_RID=R.ROLE_RID
	INNER JOIN CSAA_APPS AS A ON A.APP_RID=UR.APP_RID
	WHERE ROLE_NM IN (''Administrator'', ''SU'')
	AND USER_NM=@UserId AND APP_NM=@AppName
)
	RAISERROR (''User %s does not have sufficient privlege to perform this operation.'', 16, 1, @UserId)





' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_CheckForInsert]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_CheckForInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_CheckForInsert]                        
/*                                            
DESCRIPTION:                                            
This procedure ensures that the active usercount has not exceeded the maximum limit for an           
application,while trying to add an user.Raises an exception otherwise.          
        
CREATED      
02/05/2006 COGNIZANT FOR CSR4593       
          
PARAMETERS:        
INPUT:        
                                            
@AppName     -   Name of the calling application                                    
@AppRid      -   The ID of the calling application.        
@Active      -   User Status from the application                                        
@CurrentUser -   The currently logged-in user.       
@UserId      -    he UserId  to insert         
      
DATABASES ACCESSED :      
Authentication      
                                    
                                                                                                     
*/          
(      
@AppName  VARCHAR(50),                                  
@AppRid int,                                
@Active BIT,                            
@CurrentUser USERNAME,                       
@UserId Varchar(50)            
)                           
AS                  
SET NOCOUNT ON                                  
DECLARE @ActiveUsers int                                      
DECLARE @MaxLimit int                                
DECLARE @AppDesc varchar(100)                              
            
               
if(@Active = 1)                                
BEGIN                                    
IF EXISTS(SELECT APP_NM FROM CSAA_USERS_LIMIT WHERE APP_NM=@AppName)                                     
BEGIN                                  
 SET @ActiveUsers= (SELECT COUNT(USER_ACTIVE) FROM CSAA_USERS INNER JOIN CSAA_USERS_ROLES ON(CSAA_USERS.USER_RID = CSAA_USERS_ROLES.USER_RID) WHERE (CSAA_USERS_ROLES.APP_RID = @AppRid AND CSAA_USERS.USER_ACTIVE =1 ))                                      
 SET @MaxLimit = (SELECT MAX_LIMIT FROM CSAA_USERS_LIMIT WHERE APP_NM=@AppName)                                      
 SET @AppDesc = (SELECT APP_DESC FROM CSAA_APPS WHERE APP_NM=@AppName)                            
                
  IF(@ActiveUsers >= @MaxLimit)                                      
    BEGIN                              
    RAISERROR(''User cannot be added to %s application .The purchased licenses exceed the number of users'',13,1,@AppDesc) with nowait                                     
    return                
    END                   
END                  
END    
    
  


' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_CheckForUpdate]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_CheckForUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[AUTH_CheckForUpdate]                           
/*                                                            
DESCRIPTION:                                                            
This procedure ensures that the active usercount has not exceeded the maximum limit for an                           
application,while trying to activate an user.Raises an exception otherwise.                          
                        
CREATED:              
02/05/2006  COGNIZANT FOR CSR4593                       
                          
PARAMETERS:                          
INPUT:                                                            
@USER_RID -  User ID of the user to update                                                          
@Active   -  User status from the application                                                          
@AppId    -  ID of the calling application              
              
DATABASES ACCESSED:              
Authentication                                                        
                                                       
*/                                         
@USER_RID int,                                        
@Active BIT,                                        
@AppId int                                        
AS                        
SET NOCOUNT ON                                        
DECLARE @Appr_ID int                                          
DECLARE @AppDesc varchar(50)                                        
DECLARE @Currentstatus int                                      
DECLARE @ActiveUsers int                                        
DECLARE @MaxLimit int              
DECLARE @RolesCount int                                      
--The parameter @RolesCount checks if the user has roles in an application           
SET  @RolesCount =(SELECT COUNT(ROLE_RID)FROM CSAA_USERS_ROLES WHERE APP_RID=@AppId AND USER_RID=@USER_RID)                                    
                                                  
IF(@Active=1)                                        
BEGIN                                        
 SET @Currentstatus=(SELECT USER_ACTIVE FROM CSAA_USERS WHERE USER_RID=@USER_RID)                                        
 IF(@Active=1 AND @Currentstatus=0)                                        
 BEGIN                                        
  DECLARE Cursor_MaxLimit CURSOR FOR SELECT DISTINCT (APP_RID) FROM CSAA_USERS_ROLES WHERE USER_RID=@USER_RID                                           
  OPEN Cursor_MaxLimit                                          
  FETCH  NEXT FROM Cursor_MaxLimit into @Appr_ID                                          
  WHILE @@FETCH_STATUS = 0                                          
  BEGIN                                        
   SET @ActiveUsers= (SELECT COUNT(USER_ACTIVE) FROM CSAA_USERS INNER JOIN CSAA_USERS_ROLES ON(CSAA_USERS.USER_RID = CSAA_USERS_ROLES.USER_RID) WHERE (CSAA_USERS_ROLES.APP_RID = @Appr_ID AND CSAA_USERS.USER_ACTIVE =1 ))                                    
   SET @MaxLimit = (SELECT MAX_LIMIT FROM CSAA_USERS_LIMIT cl WHERE cl.APP_NM=(SELECT APP_NM FROM CSAA_APPS WHERE APP_RID=@Appr_ID))                                         
   SET @AppDesc = (SELECT APP_DESC FROM  CSAA_APPS WHERE APP_RID = @Appr_ID)                                    
   IF(@ActiveUsers >= @MaxLimit)                                      
   BEGIN                                                 
      RAISERROR(''User cannot be activated to %s application.The purchased licenses exceed the number of users'', 14, 1,@AppDesc)                
      RETURN                         
      CLOSE Cursor_MaxLimit                                          
      DEALLOCATE Cursor_MaxLimit                                          
   END                                           
   FETCH NEXT FROM Cursor_MaxLimit into @Appr_ID                                          
  END                                          
  CLOSE Cursor_MaxLimit       
  DEALLOCATE Cursor_MaxLimit                                         
 END            
--Existing active User profile updation does not depend on the maximum user limit count          
 ELSE IF(@Active=1 AND @Currentstatus=1 AND @RolesCount = 0)                                       
  BEGIN          
  SET @ActiveUsers= (SELECT COUNT(USER_ACTIVE) FROM CSAA_USERS INNER JOIN CSAA_USERS_ROLES ON(CSAA_USERS.USER_RID = CSAA_USERS_ROLES.USER_RID) WHERE (CSAA_USERS_ROLES.APP_RID = @AppId AND CSAA_USERS.USER_ACTIVE =1 ))                                      
  SET @MaxLimit = (SELECT MAX_LIMIT from CSAA_USERS_LIMIT cl where cl.APP_NM=(select APP_NM from CSAA_APPS where APP_RID=@AppId))                                         
  IF(@ActiveUsers >= @MaxLimit)                                      
  BEGIN                                                 
     SET @AppDesc = (SELECT APP_DESC FROM  CSAA_APPS WHERE APP_RID = @AppId)                  
     RAISERROR(''User cannot be activated to %s application .The purchased licenses exceed the number of users'', 14, 1,@AppDesc)                                           
     RETURN                                         
  END            
  END                                        
END            
            
          
        
      
    
  


' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_CheckLockoutCounter]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_CheckLockoutCounter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N' 
 
 
 
-- removing the print statements from sproc AUTH_CHECKLOCKOUTCOUNTER
 
CREATE PROCEDURE [dbo].[AUTH_CheckLockoutCounter]
/*
DESCRIPTION:
Returns a recordset of all the possible roles.
MODIFIED BY COGNIZANT ON 08/29/2011:
67811A0 - PCI Remediation for Payment systems - CH1: Declare @St and @AppId.
67811A0 - PCI Remediation for Payment systems - CH2: Get application Id in @AppId and logging message in @St.
67811A0 - PCI Remediation for Payment systems - CH3: Password Locked message logged in CSAA_Authentication_Log table when password locks.
*/
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@Result INT=NULL OUTPUT
AS
SET NOCOUNT ON
DECLARE  @LockoutCounter INT,
                @DateLastLogin DATETIME,
                @Const_RetryTime INT,
                @Const_LockoutCounter INT
-- 67811A0 - PCI Remediation for Payment systems - CH1:START - Declare @St and @AppId.		
, @St VARCHAR(256)
, @AppId INT
-- 67811A0 - PCI Remediation for Payment systems - CH1:END - Declare @St and @AppId.		

-- 67811A0 - PCI Remediation for Payment systems - CH2:START - Get application Id in @AppId and logging message in @St.
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)
-- 67811A0 - PCI Remediation for Payment systems - CH2:END - Get application Id in @AppId and logging message in @St.
SET @Const_RetryTime = 120
SET @Const_LockoutCounter = 3
SET @Result = 0
 
                
SELECT @LockoutCounter = LOCKOUT_COUNTER, @DateLastLogin = DATE_LAST_LOGIN
FROM CSAA_USERS  WHERE USER_NM = @CurrentUser 
 
IF DATEDIFF(mi,@DateLastLogin,getdate()) >= @Const_RetryTime
        BEGIN
        UPDATE CSAA_USERS SET USER_LOCKED_OUT = 0, LOCKOUT_COUNTER = 0 WHERE USER_NM = @CurrentUser 
        RETURN
        END
 
IF @LockoutCounter  >= @Const_LockoutCounter 
        BEGIN
        UPDATE CSAA_USERS SET  USER_LOCKED_OUT = 1, LOCKOUT_COUNTER =  ISNULL(LOCKOUT_COUNTER,0)  + 1  WHERE USER_NM = @CurrentUser 
        --RAISERROR (''User %s is locked out.'', 16, 1, @CurrentUser )
-- 67811A0 - PCI Remediation for Payment systems - CH2:START- Get application Id in @AppId and logging message in @St.	
	SET @St = ''USER '' + @CurrentUser + '' AUTHENTICATION LOCKED OUT ''
-- 67811A0 - PCI Remediation for Payment systems - CH2:END - Get application Id in @AppId and logging message in @St.	
	
-- 67811A0 - PCI Remediation for Payment systems - CH3:START - Password Locked message logged in CSAA_Authentication_Log table when password locks.	
	EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId
-- 67811A0 - PCI Remediation for Payment systems - CH3:END - Password Locked message logged in CSAA_Authentication_Log table when password locks.	
        SET @Result =1 
        RETURN
        END
ELSE IF @LockoutCounter  < @Const_LockoutCounter 
        BEGIN
        UPDATE CSAA_USERS SET  USER_LOCKED_OUT = 0, LOCKOUT_COUNTER =  ISNULL(LOCKOUT_COUNTER,0)  + 1  WHERE USER_NM = @CurrentUser 
        RETURN
        END
ELSE
        RETURN
 
 
 
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_DeleteUser]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_DeleteUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[AUTH_DeleteUser]
/*
DESCRIPTION:
DELETES the User UserRid, and logs the activity

PARAMETERS:
@UserRid - Identity of the user to delete.
@CurrentUser - User Name of the calling user.
@AppName - Name of the calling application.

CREATED:
7/29/2003 Jeff McEwen

MODIFICATION HISTORY:
9/29/2003 JOM Added code to delete roles from @AppName and verify that no other roles
in other apps before trying to delete.

*/
@UserRid INT,
@CurrentUser USERNAME,
@AppName VARCHAR(50)
AS
SET NOCOUNT ON

-- Verify CurrentUser is an administrator.	
EXEC AUTH_CheckAdmin @CurrentUser, @AppName
IF @@ERROR<>0 RETURN

DECLARE @UserName USERNAME, @St VARCHAR(100)
SET @UserName = (SELECT USER_NM FROM CSAA_USERS WHERE USER_RID=@UserRid)

-- Make sure user isn''t trying to delete himself.
IF @UserName=@CurrentUser
	BEGIN
	RAISERROR(''User cannot delete him/herself.'', 16, 1)
	RETURN
	END

-- Verify that the user doesn''t have any existing transactions in Membership or Payments.
DECLARE @Has1 INT, @Has2 INT
EXEC @Has1= csaaapp.dbo.MSC_USER_HAS_ORDERS @UserName
IF @@ERROR<>0 RETURN
EXEC @Has2=Payments.dbo.INS_User_Has_Orders @UserName
IF @@ERROR<>0 RETURN
IF @Has1+@Has2>0
	BEGIN
	RAISERROR(''User %s cannot be deleted because there are transactions in the database;
	Change the status to inactive instead.'', 15, 1, @UserName)
	RETURN
	END

-- Do the delete.
DECLARE @AppID INT
SET @AppID = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)
DELETE FROM CSAA_USERS_ROLES WHERE USER_RID = @UserRid AND APP_RID=@AppID
IF EXISTS(SELECT * FROM CSAA_USERS_ROLES WHERE USER_RID=@UserRid)
	BEGIN
	RAISERROR(''User %s cannot be deleted because he/she has roles in other applications; all roles have been removed for %s application.'', 15, 1, @UserName, @AppName)
	RETURN
	END
ELSE BEGIN
	DELETE FROM CSAA_USERS WHERE USER_RID = @UserRid

	-- STANDARD CALL TO AUDIT ACTIVITY
	SET @St = ''DELETED USER '' + @UserName
	EXEC AUTH_LogActivity @AppName, @CurrentUser, @St
	END


' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_DOActivity]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_DOActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_DOActivity]
(
/*
                                     
DESCRIPTION:                                          
This procedure will log the DO activity (created,updated,activated and deactivated) 
in CSAA_DO_ACTIVITY table

CREATED:
4/20/2006

*/
@DOID VARCHAR(3),
@HUB VARCHAR(2),
@St	LONGDESC,
@CurrentUser USERNAME
)

AS

INSERT INTO CSAA_DO_ACTIVITY (
    DO_Code,
    HUB,
    DO_Activity,
    User_NM
  ) VALUES (
    @DOID,
    @HUB,
    @St,
    @CurrentUser
  )
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetALLDOs]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetALLDOs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_GetALLDOs]
/*  
DESCRIPTION:  
Returns a recordset of the HUB district offices.  
  
CREATED:  
4/17/2006   
  
*/  
AS  
  
SELECT DO_Code AS [ID], Description FROM CSAA_DO WHERE len(DO_Code)<= 2
ORDER BY Description

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetAllUserDetails]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetAllUserDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
ALTER PROCEDURE [dbo].[AUTH_GetAllUserDetails]  


--This will be invoked by the RBAC application to fetch the list of users every 1 hour.

--(
--@CurrentUser VARCHAR(50),
--@AppName VARCHAR(50),
--@AppId  RECORDID = '-1'
--)
AS 

SET NOCOUNT ON

                SELECT DISTINCT
                                U.USER_RID AS USERRID,
                                U.USER_NM AS USERID,
                                U.USER_FNAME AS [FIRST NAME],
                        U.USER_LNAME AS [LAST NAME],
                                U.USER_LNAME + ', ' + U.USER_FNAME AS [NAME],
                                R.Role AS [ROLENAMES],
                                U.USER_EMAIL AS EMAIL,
                                U.USER_PHONE AS PHONE,
                                U.USER_REPID AS REPID,
                                LTRIM(RTRIM(U.USER_DO)) AS DO,
                                --RFC 185138 - AD Integration START- Commented the password updated date and locked out parameter
                                -- U.DATE_PWD_UPDATED,
                                --CAST(U.USER_LOCKED_OUT AS BIT) AS ISLOCKEDOUT,
                                --RFC 185138 - AD Integration END- Commented the password updated date and locked out parameter
                                CAST(U.USER_ACTIVE AS BIT) AS Active,
                                -- U.TOKEN,
                                U.DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
                                /* 'Status' = 
                                CASE
                                                WHEN U.USER_ACTIVE = 1 THEN 'Active'
                                                WHEN U.USER_ACTIVE = 0 THEN 'InActive'
                                                ELSE ''
                                End */
                                R.RoleIDs AS [ROLES]
                                --U.DATE_PWD_UPDATED
  ------- CHG0106404 - MAIG Integration - Added to retrieve AgencyID & AgencyName - Start  
  ,U.AgencyID, U.AgencyName  
  ------- CHG0106404 - MAIG Integration - Added to retrieve AgencyID & AgencyName - End  
        
                FROM CSAA_USERS U
               --ADDED ON MAY 182015, CHANGED FROM INNER TO OUTER JOIN,RFC# CHG0115410
	       	LEFT OUTER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
	       	INNER JOIN USR_ROLE R ON R.USER_RID = U.USER_RID
	--ADDED ON MAY 182015
                
                ORDER BY U.USER_RID

SET NOCOUNT OFF
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetAllUserDetails_bkp1027]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetAllUserDetails_bkp1027]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[AUTH_GetAllUserDetails_bkp1027]
/*
CREATED BY COGNIZANT ON 10/19/2007 FOR RBAC - PAYMENT TOOL INTEGRATION
This procedure will return the attributes for all the users present in the Payment Tool database.

This will be invoked by the RBAC application to fetch the list of users wvery 1 hour.
*/
(
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@AppId  RECORDID = ''-1''
)
AS 

SET NOCOUNT ON

SELECT  DISTINCT U.USER_RID AS UserRid, R.ROLE_RID AS [Role_ID], R.ROLE_NM AS [Description] 
INTO #TUSR
FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID

DECLARE @RoleIDs VARCHAR(100), @Roles VARCHAR(150), @delimiter CHAR, @URID VARCHAR(15)
SET @delimiter = '',''

CREATE TABLE #TmpUsrRoles (UID VARCHAR(15), Role VARCHAR(200), Role_ID VARCHAR(100))

DECLARE USR_ROLES CURSOR  READ_ONLY FOR
SELECT DISTINCT
	UserRid
FROM 
	#TUSR

OPEN USR_ROLES

FETCH NEXT FROM USR_ROLES INTO @URID

WHILE @@FETCH_STATUS = 0
BEGIN
	
	SELECT @Roles = COALESCE(@Roles + @delimiter, '''') + [Description] FROM #TUSR WHERE UserRid = @URID
	SELECT @RoleIDs = COALESCE(@RoleIDs + @delimiter, '''') + CONVERT(VARCHAR(2),[Role_ID]) FROM #TUSR WHERE UserRid = @URID
	INSERT INTO #TmpUsrRoles VALUES (@URID, @Roles, @RoleIDs)
	SET @Roles = NULL
	SET @RoleIDs = NULL
   FETCH NEXT FROM USR_ROLES INTO @URID
END

CLOSE USR_ROLES
DEALLOCATE USR_ROLES


DROP TABLE #TUSR


	SELECT DISTINCT
  		U.USER_RID AS USERRID,
		U.USER_NM AS USERID,
		U.USER_FNAME AS [FIRST NAME],
	        U.USER_LNAME AS [LAST NAME],
		U.USER_LNAME + '', '' + U.USER_FNAME AS [NAME],
		R.Role AS [ROLENAMES],
		U.USER_EMAIL AS EMAIL,
		U.USER_PHONE AS PHONE,
		U.USER_REPID AS REPID,
		LTRIM(RTRIM(U.USER_DO)) AS DO,
		-- U.DATE_PWD_UPDATED,
		CAST(U.USER_LOCKED_OUT AS BIT) AS ISLOCKEDOUT,
		CAST(U.USER_ACTIVE AS BIT) AS Active,
		-- U.TOKEN,
		U.DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
		/* ''Status'' = 
		CASE
			WHEN U.USER_ACTIVE = 1 THEN ''Active''
			WHEN U.USER_ACTIVE = 0 THEN ''InActive''
			ELSE ''''
		End */
		R.Role_ID AS [ROLES],
		U.DATE_PWD_UPDATED
		
	FROM CSAA_USERS U
	INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
	INNER JOIN #TmpUsrRoles R ON R.UID = U.USER_RID
	
	ORDER BY U.USER_RID
	

DROP TABLE #TmpUsrRoles

SET NOCOUNT OFF

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetAllUserDetails_test]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetAllUserDetails_test]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_GetAllUserDetails_test]
/*
CREATED BY COGNIZANT ON 10/19/2007 FOR RBAC - PAYMENT TOOL INTEGRATION
This procedure will return the attributes for all the users present in the Payment Tool database.

This will be invoked by the RBAC application to fetch the list of users wvery 1 hour.
*/
(
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@AppId  RECORDID = ''-1''
)
AS 

SET NOCOUNT ON

SELECT  DISTINCT U.USER_RID AS UserRid, R.ROLE_RID AS [Role_ID], R.ROLE_NM AS [Description] 
INTO #TUSR
FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID

DECLARE @RoleIDs VARCHAR(100), @Roles VARCHAR(150), @delimiter CHAR, @URID VARCHAR(15)
SET @delimiter = '',''

CREATE TABLE #TmpUsrRoles (UID VARCHAR(15), Role VARCHAR(200), Role_ID VARCHAR(100))

DECLARE USR_ROLES CURSOR  READ_ONLY FOR
SELECT DISTINCT
	UserRid
FROM 
	#TUSR

OPEN USR_ROLES

FETCH NEXT FROM USR_ROLES INTO @URID

WHILE @@FETCH_STATUS = 0
BEGIN
	
	SELECT @Roles = COALESCE(@Roles + @delimiter, '''') + [Description] FROM #TUSR WHERE UserRid = @URID
	SELECT @RoleIDs = COALESCE(@RoleIDs + @delimiter, '''') + CONVERT(VARCHAR(2),[Role_ID]) FROM #TUSR WHERE UserRid = @URID
	INSERT INTO #TmpUsrRoles VALUES (@URID, @Roles, @RoleIDs)
	SET @Roles = NULL
	SET @RoleIDs = NULL
   FETCH NEXT FROM USR_ROLES INTO @URID
END

CLOSE USR_ROLES
DEALLOCATE USR_ROLES


DROP TABLE #TUSR


	SELECT DISTINCT
  		U.USER_RID AS USERRID,
		U.USER_NM AS USERID,
		U.USER_FNAME AS [FIRST NAME],
	        U.USER_LNAME AS [LAST NAME],
		U.USER_LNAME + '', '' + U.USER_FNAME AS [NAME],
		R.Role AS [ROLENAMES],
		U.USER_EMAIL AS EMAIL,
		U.USER_PHONE AS PHONE,
		U.USER_REPID AS REPID,
		LTRIM(RTRIM(U.USER_DO)) AS DO,
		-- U.DATE_PWD_UPDATED,
		CAST(U.USER_LOCKED_OUT AS BIT) AS ISLOCKEDOUT,
		CAST(U.USER_ACTIVE AS BIT) AS Active,
		-- U.TOKEN,
		U.DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
		/* ''Status'' = 
		CASE
			WHEN U.USER_ACTIVE = 1 THEN ''Active''
			WHEN U.USER_ACTIVE = 0 THEN ''InActive''
			ELSE ''''
		End */
		R.Role_ID AS [ROLES],
		U.DATE_PWD_UPDATED
		
	FROM CSAA_USERS U
	INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
	INNER JOIN #TmpUsrRoles R ON R.UID = U.USER_RID
	
	ORDER BY U.USER_RID
	

DROP TABLE #TmpUsrRoles

SET NOCOUNT OFF
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetApplications]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetApplications]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[AUTH_GetApplications]
/*
DESCRIPTION: 
Returns a recordset of the applications.

PARAMETERS:
@CurrentUser - The currently logged-in user.  Is used to verify that this user
has permission to get this data (is an Administrator or Super User)
@AppName - The application that is calling.

CREATED: 
9/26/2003 Jeff McEwen

MODIFICATION HISTORY:
*/
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50)
AS

EXEC AUTH_CheckAdmin @CurrentUser, @AppName
IF @@ERROR<>0 RETURN

SELECT DISTINCT APP_NM AS [ID], APP_DESC AS Description 
FROM CSAA_USERS AS U 
INNER JOIN CSAA_USERS_ROLES AS UR ON U.USER_RID=UR.USER_RID
INNER JOIN CSAA_APPS AS A ON UR.APP_RID=A.APP_RID
INNER JOIN CSAA_ROLES AS R ON UR.ROLE_RID=R.ROLE_RID
WHERE U.USER_NM=@CurrentUser
AND ROLE_NM IN (''Administrator'', ''SU'')




' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetAppRoles]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetAppRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[AUTH_GetAppRoles]
/*
DESCRIPTION:
Returns a recordset of all the possible roles.
*/
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@AppId INT = -1
AS

SELECT 
	R.ROLE_RID  AS [ID], 
	APP_DESC + '' -  '' + ROLE_NM AS Description
FROM CSAA_APPS AS A  INNER JOIN CSAA_APPS_ROLES AS AR
 ON AR.APP_RID=A.APP_RID INNER JOIN  CSAA_ROLES AS R  
ON  AR.ROLE_RID=R.ROLE_RID
WHERE (@AppId = -1 OR  AR.APP_RID=@AppId)
AND R.ROLE_RID NOT IN (1,2)
ORDER BY Description

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetApps]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetApps]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE PROCEDURE [dbo].[AUTH_GetApps]
/*
DESCRIPTION:
Returns a recordset of the applications.

CREATED:
7/31/2003 Jeff McEwen

Revised Date		: 3/18/2013
Revised Description : The SP is modified in such that to retrieve all applications except has a part of Payment Central Integration-Phase II Chnages


*/
AS

SELECT APP_RID AS [ID], APP_DESC AS Description FROM CSAA_APPS 
WHERE
	 APP_RID > 2 -- Added to filter out the first two test ones.
AND
	 ENABLED=1  -- Added by cognizant on 03-24-2005. To display appropriate Application in the report
AND APP_RID NOT IN (5)--modified the code to retrieve all applications except has a part of Payment Central Integration-Phase II Chnages
ORDER BY APP_NM

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetDOdetails]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetDOdetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_GetDOdetails]    
/*  
DESCRIPTION:  
Returns a recordset of the district offices.  
  
CREATED:  
4/27/2006   
  
---- Modified by Cognizant on 08/08/2014 - As part of MAIG Integration Project :    
---- CHG0106404 DO ID Length Expansion - changed from 3 to 10  
  
*/  
(
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
@DOID varchar(10)    
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
)
AS  

IF len(@DOID) < 3
	BEGIN
	SELECT DO_Code AS [ID], [Description] ,Enabled FROM CSAA_DO WHERE DO_Code = @DOID
	END 
ELSE
        BEGIN
	SELECT do.DO_Code AS [ID], do.[Description] ,do.Enabled ,map.HUB 
              FROM CSAA_DO do inner join CSAA_DO_MAPPING map
	ON do.DO_CODE = map.DO
	WHERE do.DO_Code = @DOID
        END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetDOInfo]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetDOInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[AUTH_GetDOInfo]                    
          
/*                  
DESCRIPTION:                  
Returns a recordset of the All the district offices or for a specific HUB office              
based on the input parameter DO_CODE      
  
PARAMETERS:  
@DOID : The Branch Office ID   
                          
CREATED:                  
4/20/2006   
Modified:                
6/15/2006 - Included the Order By Clause
---- Modified by Cognizant on 08/08/2014 - As part of MAIG Integration Project :    
---- CHG0106404 DO ID Length Expansion - changed from 3 to 10  
                   
*/   

(    
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
@DOID VARCHAR(10)                  
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
)              
AS              
DECLARE               
              
 @sqlStart NVARCHAR(500),              
 @sqlEnd   NVARCHAR(500),              
 @sqlSt    NVARCHAR(1000)              
     
 --Dynamic Query Based On Request            
 SET @sqlStart=''SELECT DOs.Description AS [Branch Office],DOs.Do_Code AS [DO_Id] ,              
               ISNULL((SELECT Description FROM CSAA_DO WHERE Do_Code=(SELECT HUB FROM CSAA_DO_MAPPING MAP              
                                       WHERE MAP.DO=DOs.Do_Code) ),'''''''') AS [HUB] FROM  CSAA_DO DOs''              
              
 IF @DOID<>''All''  AND  @DOID <> ''''             
              
 --Get The Spoke For The Associated HUB              
    
   BEGIN              
    SET @sqlEnd='' WHERE DOs.Do_Code IN (SELECT DO FROM CSAA_DO_MAPPING WHERE HUB=''+@DOID+'') Order by [Branch Office]''              
    SET @sqlSt=@sqlStart+@sqlEnd              
   END              
              
 ELSE              
              
   BEGIN              
    SET @sqlSt=@sqlStart  +     '' Order by [Branch Office]''   
   END             
  EXEC sp_executesql @sqlSt
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetDOs]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetDOs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[AUTH_GetDOs]
/*
DESCRIPTION:
Returns a recordset of the district offices.

CREATED:
7/31/2003 Jeff McEwen

MODIFICATION HISTORY:
12/26/2006 - Modified by Cognizant for CSR 5595
 --Changed the format of displaying DO to DO name - DO Number

*/
AS

--Changed the format of displaying DO to DO name - DO Number as part of CSR 5595
SELECT DO_Code AS [ID], [Description]+ '' - '' + DO_Code AS Description FROM CSAA_DO
WHERE Enabled=1
ORDER BY Description

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetDOsByApp]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetDOsByApp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

/*
DESCRIPTION:
Returns a recordset of the district offices.

CREATED BY COGNIZANT ON 07/05/2004

MODIFICATION HISTORY:
12/26/2006 - Modified by Cognizant for CSR 5595
 --Changed the format of displaying DO to DO name - DO Number

*/
CREATE PROCEDURE [dbo].[AUTH_GetDOsByApp]
(
@AppId  RECORDID = ''-1''
)
AS
--Changed the format of displaying DO to DO name - DO Number as part of CSR 5595
SELECT 
	DO_Code AS [ID], 
	[Description],
	[Description]+ '' - '' + DO_Code AS  [ID_Description]
FROM 
	CSAA_DO
WHERE 
	Enabled=1
ORDER BY
	[Description]

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetRepIDDO]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetRepIDDO]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'



CREATE PROCEDURE [dbo].[AUTH_GetRepIDDO]  
/*  
DESCRIPTION:  
  
This procedure is used to fetch the respective Rep ID and the District Office(DO) for the  
current user. The Rep ID and DO ID for the current user name are fetched from the  
CSAA_USERS table and the DO description for the respective DO is fetched  
from the CSAA_DO table.  
  
PARAMETERS:
INPUT:  
@CurrentUser : The current user name  
  
OUTPUT:  
This procedure returns a recordset of the Rep ID, District Office (DO) name and the DO ID for the   
current user  
  
CREATED:  
5/17/2004 COGNIZANT  
  
DATABASES ACCESSED:  
Authentication  
  
*/  
  
(  
@CurrentUser VARCHAR(50)  
)  
  
AS  
  
SET NOCOUNT ON  
  
SELECT CSAA_USERS.USER_REPID as [Rep ID],  
  CSAA_USERS.USER_DO as [DO ID],  
  CSAA_DO.Description as [DO Description],
  CSAA_USERS.USER_FNAME AS [First Name],
  CSAA_USERS.USER_LNAME AS [Last Name]
FROM  
 CSAA_USERS,  
 CSAA_DO  
WHERE  
 CSAA_USERS.USER_NM = @CurrentUser AND  
 CSAA_DO.DO_Code = CSAA_USERS.USER_DO AND  
 CSAA_DO.Enabled = 1  
  
SET NOCOUNT OFF



' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetRoles]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_GetRoles]  
/*
DESCRIPTION:
Returns a recordset of all the possible roles.

PARAMETERS:
@CurrentUser - The currently logged-in user.  Is used to verify that this user
has permission to get this data (is an Administrator or Super User)
@AppName - The application that is calling.
@ForAppName - The application for which to get the roles.

CREATED:
7/28/2003 Jeff McEwen

MODIFICATION HISTORY:
9/9/2003 JOM Added CSAA_APPS_ROLES to selection list.
9/29/2003 JOM Added @ForAppName parameter
08/04/2014 - Cognizant : MAIG Integration - Setting alias name for PS User role
  
*/
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@ForAppName VARCHAR(50) = NULL
AS
EXEC AUTH_CheckAdmin @CurrentUser, @AppName
IF @@ERROR<>0 RETURN

DECLARE @AppId INT
IF @ForAppName IS NULL SET @ForAppName=@AppName
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@ForAppName)

DECLARE @SU BIT
SET @SU=0
IF EXISTS (
	SELECT U.USER_RID
	FROM CSAA_USERS AS U 
	INNER JOIN CSAA_USERS_ROLES AS UR ON U.USER_RID=UR.USER_RID
	INNER JOIN CSAA_ROLES AS R ON UR.ROLE_RID=R.ROLE_RID
	WHERE ROLE_NM = ''SU'' 
	AND UR.APP_RID=@AppId
	AND USER_NM=@CurrentUser
) SET @SU=1

SELECT 
	''['' + CAST(R.ROLE_RID AS VARCHAR(5)) + '']'' AS Role, 
-- CHG0106404 - MAIG Integration - Setting alias for PS User role - Start
	Description = CASE       
     WHEN (ROLE_NM = ''pss'') THEN ''PS User''
     ELSE ROLE_NM 
     END
-- CHG0106404 - MAIG Integration - Setting alias for PS User role - End
FROM CSAA_ROLES AS R 
INNER JOIN CSAA_APPS_ROLES AS AR ON AR.ROLE_RID=R.ROLE_RID
WHERE (@SU=1 OR ROLE_NM<>''SU'')
	AND AR.APP_RID=@AppId
ORDER BY Description
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetUser]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[AUTH_GetUser]
/*
DESCRIPTION:
This procedure has two modes of operation.  If no UserId  is supplied,
then it will return recordsets containing all the users.   If either 
is, will return recordsets containing detailed information for just that user.

PARAMETERS:
@UserId - The user name of the user to return detail for.
@ShowAll - If false when displaying all users, will only return users that have roles
in the selecte application.

CREATE:
7/29/2004 Bindu Joseph

*/
(@UserId USERNAME = NULL,
@ShowAll BIT = 0)

AS

-- Return the detailed information for @UserId
IF @UserId IS NOT NULL 
	SELECT 
		USER_LOGIN AS UserId,
		EMPLOYEE_NUM AS EmployeeNumber,
		USER_FIRSTNAME AS FirstName,
		USER_LASTNAME AS LastName,
		USER_EMAIL AS Email,
		USER_PHONE AS Phone,
		USER_DEPTLOCCODE AS LocCode,
		USER_OFFICECODE AS OfficeCode
	FROM CSAA_USERS_NEW 
	WHERE USER_LOGIN = @UserId
ELSE
-- Return summary information for all the users.
	SELECT DISTINCT
		USER_LOGIN AS UserId,
		EMPLOYEE_NUM AS EmployeeNumber,
		USER_FIRSTNAME AS FirstName,
		USER_LASTNAME AS LastName,
		USER_EMAIL AS Email,
		USER_PHONE AS Phone,
		USER_DEPTLOCCODE AS LocCode,
		USER_OFFICECODE AS OfficeCode
	FROM CSAA_USERS_NEW 
	WHERE @ShowAll=1

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_GetUserInfo]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_GetUserInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
        
        
        
        
        
        
        
        
        
        
CREATE PROCEDURE [dbo].[AUTH_GetUserInfo]        
/*
DESCRIPTION:
This procedure has two modes of operation.  If no UserId or UserRid is supplied,
then it will return recordsets containing all the users and their roles.  If either 
is, will return recordsets containing detailed information for just that user.

PARAMETERS:
@UserId - The user name of the user to return detail for.
@UserRid - Alternately, the UserRid of the user to return detail for.
@CurrentUser - The currently logged-in user.
@AppName - The application that is calling.
@ShowAll - If false when displaying all users, will only return users that have roles
in the selecte application.

CREATE:
7/29/2003 Jeff McEwen

MODIFICATION HISTORY:
9/9/2003 Added Token Parameter to allow token retrieval with login.
9/29/2003 Added @Showall parameter and cleaned up code for selecting with an application.
11/12/2003 Added @DO parameter

Modified by Cognizant as part of CSR 5166 on 07/05/2006
CSR 5166.Ch1 Added a new parameter @Status
CSR 5166.Ch2 Get UserRid if its null
CSR 5166.Ch3 Added code to get Username,Last logindate and Status for single user.
CSR 5166.Ch4 Added conditons below to check the Application Id to retrieve user info for single user.
CSR 5166.Ch5 Added conditons below to check the Status and Do to retrieve user info for single user.
CSR 5166.Ch6 Added conditons below to check the Status and Do to retrieve user roles for single user.
CSR 5166.Ch7 Added code to get Last login date and Status
CSR 5166.Ch8 Added condition below to check the Status to retrieve users info
CSR 5166.Ch9 Added condition below to check the Status to retrieve users roles
CSR 5166.Ch10  Added code to handle whether the user id exists for the selected application
CSR 5166.Ch11 Get the count of the user id for the selected application
---RFC 185138 - AD Integration - ---RFC 185138 - AD Integration - CH1,CH2- Commented the locked out parameter check aqnd password updated date
-- Modified by Cognizant on 08/04/2014 - As part of MAIG Integration Project :      
-- 1. CHG0106404 Included AgencyID & AgencyName fields to be updated in the CSAA_USERS table      
-- 2. CHG0106404 DO ID Length Expansion - changed from 3 to 10      
    
*/
@UserId USERNAME = NULL,
@UserRid INT = NULL,
@CurrentUser VARCHAR(50),
@AppName VARCHAR(50),
@Token VARCHAR(255) = NULL,
@ShowAll BIT = 0,
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10       
@DO VARCHAR(10)=NULL,        
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10         
--CSR5166.Ch1 Added new parameter @Status
@Status BOOLEAN = NULL

AS
-- Get the UserId if only UserRid is supplied. Get UserRid if its null
IF @UserId IS NULL AND @UserRid IS NOT NULL
	SET @UserId=(SELECT USER_NM FROM CSAA_USERS WHERE USER_RID=@UserRid)

--CSR5166.Ch2 Added To get UserRid if its null.
IF @UserRid IS NULL
     SET @UserRid=(SELECT USER_RID FROM CSAA_USERS WHERE USER_NM=@UserId)
-- Verify that the current user is allowed to get the information.
/* IF ISNULL(@UserId,'''')<>@CurrentUser
	BEGIN
	EXEC AUTH_CheckAdmin @CurrentUser, @AppName
	IF @@ERROR<>0 RETURN
	END*/
	
DECLARE @AppId INT
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)

-- Return the detailed information for @UserId
IF @UserId IS NOT NULL 
BEGIN
	--CSR 5166.Ch11 Get the count of the user id for the selected application
	DECLARE @count INT
	SELECT @count = COUNT(*) FROM CSAA_USERS_ROLES WHERE APP_RID = @AppId AND USER_RID = @UserRid
		IF @count >0 
		BEGIN
			SELECT  DISTINCT
		  		US.USER_RID AS UserRid,
				US.USER_NM AS UserId,
				US.USER_FNAME AS FirstName,
				US.USER_LNAME AS LastName,
				US.USER_EMAIL AS Email,
				US.USER_PHONE AS Phone,
				US.USER_REPID AS RepId,
				LTRIM(RTRIM(US.USER_DO)) AS DO,
				---RFC 185138 - AD Integration - Start - CH1- Commented the locked out parameter check aqnd password updated date
				--US.DATE_PWD_UPDATED,
				--CAST(US.USER_LOCKED_OUT AS BIT) AS IsLockedOut, 
				---RFC 185138 - AD Integration - End - CH1- Commented the locked out parameter check aqnd password updated date
				CAST(US.USER_ACTIVE AS BIT) AS Active,
				@Token AS Token,
				--CSR5166.Ch3 Added code below to get Username, Last login date and Status
				US.USER_LNAME + '', '' + US.USER_FNAME AS [Name],
				US.DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
    ------- CHG0106404 - MAIG Integration - Added code to retrieve AgencyID & AgencyName - Start      
    US.AgencyID, US.AgencyName,        
    ------- CHG0106404 - MAIG Integration - Added code to retrieve AgencyID & AgencyName - End      
				''Status'' = 
				CASE
					WHEN US.USER_ACTIVE = 1 THEN ''Active''
					WHEN US.USER_ACTIVE = 0 THEN ''InActive''
					ELSE ''''
				End
				
			FROM CSAA_USERS US
			--CSR5166.Ch4 Added conditons below to check the Application Id to retrieve user info
			INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = US.USER_RID
			WHERE (@AppId IS NULL OR UR.APP_RID=@AppId) AND
			US.USER_NM = @UserId
			--CSR5166.Ch5 Added conditons below to check the Status and Do to retrieve user info
			AND (@DO IS NULL OR US.USER_DO=@DO) 
			AND (@Status IS NULL OR US.USER_ACTIVE=@Status)
				
			-- Role Ids recordset
			SELECT DISTINCT R.ROLE_RID AS Role, R.ROLE_NM AS Description,U.USER_RID AS UserRid  --, APP_RID AS AppRid
			FROM CSAA_USERS AS U
			INNER JOIN CSAA_USERS_ROLES AS UR ON UR.USER_RID = U.USER_RID
			INNER JOIN CSAA_ROLES AS R ON R.ROLE_RID = UR.ROLE_RID
		
			WHERE ((@UserId IS NOT NULL AND U.USER_NM = @UserId) OR
				(@UserRid IS NOT NULL AND U.USER_RID = @UserRid))
			AND (@AppId IS NULL OR UR.APP_RID=@AppId)
			
			--CSR5166.Ch6 Added conditons below to check the Status and Do to retrieve user roles
			AND (@DO IS NULL OR USER_DO=@DO)
			AND (@Status IS NULL OR U.USER_ACTIVE=@Status)
		END 
		--CSR 5166.Ch10: START -  Added code to handle whether the user id exists for the selected application
		ELSE IF @count = 0
		BEGIN
			SELECT 
		  		USER_RID AS UserRid,
				USER_NM AS UserId,
				USER_FNAME AS FirstName,
				USER_LNAME AS LastName,
				USER_LNAME + '', '' + USER_FNAME AS [Name],
				USER_EMAIL AS Email,
				USER_PHONE AS Phone,
				USER_REPID AS RepId,
				LTRIM(RTRIM(USER_DO)) AS DO,
				---RFC 185138 - AD Integration - Start - CH2- Commented the locked out parameter check aqnd password updated date
				--DATE_PWD_UPDATED,
				--CAST(USER_LOCKED_OUT AS BIT) AS IsLockedOut,
				---RFC 185138 - AD Integration - End - CH2- Commented the locked out parameter check aqnd password updated date
				CAST(USER_ACTIVE AS BIT) AS Active,
				@Token AS Token,
				DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
    ------- CHG0106404 - MAIG Integration - Added code to retrieve AgencyID & AgencyName - Start      
    AgencyID, AgencyName,       
    ------- CHG0106404 - MAIG Integration - Added code to retrieve AgencyID & AgencyName - End      
				''Status'' = 
				CASE
					WHEN USER_ACTIVE = 1 THEN ''Active''
					WHEN USER_ACTIVE = 0 THEN ''InActive''
					ELSE ''''
				End
			FROM CSAA_USERS 
			WHERE USER_NM = @UserId

			-- Role Ids recordset
			SELECT DISTINCT R.ROLE_RID AS Role, R.ROLE_NM AS Description,U.USER_RID AS UserRid  --, APP_RID AS AppRid
			FROM CSAA_USERS AS U
			INNER JOIN CSAA_USERS_ROLES AS UR ON UR.USER_RID = U.USER_RID
			INNER JOIN CSAA_ROLES AS R ON R.ROLE_RID = UR.ROLE_RID
		
			WHERE ((@UserId IS NOT NULL AND U.USER_NM = @UserId) OR
				(@UserRid IS NOT NULL AND U.USER_RID = @UserRid))
			AND (@AppId IS NULL OR UR.APP_RID=@AppId)
			
			--CSR5166.Ch6 Added conditons below to check the Status and Do to retrieve user roles
			AND (@DO IS NULL OR USER_DO=@DO)
			AND (@Status IS NULL OR U.USER_ACTIVE=@Status)

		END
		--CSR 5166.Ch10: END
	
END
ELSE
-- Return summary information for all the users.
	BEGIN
	SELECT DISTINCT
  		U.USER_RID AS UserRid,
		U.USER_NM AS UserId,
		U.USER_LNAME + '', '' + U.USER_FNAME AS [Name],
		--CSR5166.Ch7 Added code to get Last login date and Status
		U.DATE_LAST_LOGIN AS LAST_LOGIN_DATE,
  ------- CHG0106404 - MAIG Integration -Added code to retrieve AgencyID & AgencyName - Start      
  U.AgencyID, U.AgencyName,        
  ------- CHG0106404 - MAIG Integration - Added code to retrieve AgencyID & AgencyName - End      
		''Status'' = 
		CASE
			WHEN U.USER_ACTIVE = 1 THEN ''Active''
			WHEN U.USER_ACTIVE = 0 THEN ''InActive''
			ELSE ''''
		End
		
	FROM CSAA_USERS U
	INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
	WHERE (UR.APP_RID=@AppId OR @ShowAll=1)
	AND (@DO IS NULL OR USER_DO=@DO) 
	-- CSR5166.Ch8 Added condition below to check the Status to retrieve users info 
	AND (@Status IS NULL OR U.USER_ACTIVE=@Status) 
	
	ORDER BY [Name]
	
	SELECT DISTINCT U.USER_RID AS UserRid, R.ROLE_NM AS Description
	FROM CSAA_USERS U
	INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
	INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID
	WHERE (UR.APP_RID=@AppId OR @ShowAll=1)
	AND (@DO IS NULL OR USER_DO=@DO)
	-- CSR5166.Ch9 Added condition below to check the Status to retrieve users roles
	AND (@Status IS NULL OR U.USER_ACTIVE=@Status)
	
	ORDER BY UserRid, Description
	END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_ListApprovers]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ListApprovers]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
--*******************************************************
--CREATED BY COGNIZANT ON 04/18/2005 AS PART of Cashier 
--Recon Enhancements
--Description :
--This Procedure returns a resultset of all Approvers
--Note:
--Currently ''cashiers'' are in the ''Approvers'' list

/*
MODIFICATION HISTORY:

Modified by Cognizant 07/03/2006 
1. CSR 5166.Ch1: Added the condition to display active users only.

*/
---- Modified by Cognizant on 08/08/2014 - As part of MAIG Integration Project :    
---- CHG0106404 DO ID Length Expansion - changed from 3 to 10  
  
--*******************************************************
CREATE PROCEDURE [dbo].[AUTH_ListApprovers]    
   
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
 @RepDO VARCHAR(10) = ''-1''    
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
AS

---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
DECLARE @vCONST_REPDO VARCHAR(10)    
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
  
SET @vCONST_REPDO = ''-1''

SELECT DISTINCT U.USER_RID		USERID,
		U.USER_NM		USERNAME,
		U.USER_FNAME		FIRSTNAME,
		U.USER_LNAME		LASTNAME,
		U.USER_LNAME + '', '' 
		+ U.USER_FNAME + '' - ''
		+ U.USER_NM 		USERDEF

	FROM CSAA_USERS U
		INNER JOIN CSAA_USERS_ROLES UR ON U.USER_RID = UR.USER_RID
		INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID
	--To add another Role ID as Approver add after IN eg:(''Cashier'',''Manager'')
	WHERE  (R.ROLE_NM IN (''Cashier''))
	--CSR 5166.Ch1: Added the condition below to display active users only
		AND 	U.User_Active = 1
		AND 	(@RepDO  =   @vCONST_REPDO
		OR	USER_DO = @RepDO)
	ORDER BY U.USER_LNAME

--*******************************************************
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_ListAppUsersByRoles]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ListAppUsersByRoles]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

--******************************************************
--*
--*
--******************************************************
/*

MODIFICATION HISTORY:

Modified by Cognizant 07/03/2006 
1. CSR 5166.Ch1: Added the condition to display active users only.

*/
CREATE PROCEDURE [dbo].[AUTH_ListAppUsersByRoles]    
(
	@RequesterNm	USERNAME	=  ''All'',
	@RoleId	RECORDID = NULL,
   
 ---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10   
 @RepDO VARCHAR(10) = ''-1'',    
 ---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
   
	@CurrentUser	USERNAME = NULL,
	@AppName	VARCHAR(50) = NULL,
	@AppId		RECORDID  = ''-1''
)
AS

DECLARE
	@vCONST_ROLE 	VARCHAR(5),	
   
 ---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10    
 @vCONST_REPDO VARCHAR(10),    
 ---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
   
	@vCONST_APPID	VARCHAR(5)
	--@AppId		RECORDID 
--SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)

SET @vCONST_ROLE   = ''-1''
SET @vCONST_REPDO = ''-1''
SET @vCONST_APPID	= ''-1''

--SET @RepDO = ''-1''  -- Set temporarily

SELECT DISTINCT U.USER_RID		USERID,
		U.USER_NM		USERNAME,
		U.USER_FNAME	FIRSTNAME,
		U.USER_LNAME	LASTNAME,
		--UR.ROLE_RID		ROLEID,
		--R.ROLE_NM	ROLENAME,
		U.USER_LNAME + '', '' + U.USER_FNAME + '' - ''  + U.USER_NM 	USERDEF
	FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES UR ON U.USER_RID = UR.USER_RID
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID
WHERE  (@AppId = @vCONST_APPID  
OR	UR.APP_RID = @AppId )

--CSR 5166.Ch1: Added the condition below to display acive users only
AND      U.USER_ACTIVE=1

AND	(@RoleId	 =   @vCONST_ROLE 
OR	R.ROLE_RID = @RoleId)
AND 	(@RepDO  =   @vCONST_REPDO
OR	USER_DO = @RepDO)
AND	(U.USER_NM	= @RequesterNm OR @RequesterNm = ''All'')
ORDER BY U.USER_LNAME
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_ListAppUsersByRolesApp]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_ListAppUsersByRolesApp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
/*
Description :
	This Procedure returns a resultset of User''s List based on Application and RepDo 

CREATED BY COGNIZANT ON 07/05/2004

MODIFICATION HISTORY:

Modified by Cognizant on 07/03/2006
1. CSR 5166.Ch1: Added the condition to display active users only.

*/
CREATE PROCEDURE [dbo].[AUTH_ListAppUsersByRolesApp]      
(  
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
 @RepDO VARCHAR(10) = ''-1'',      
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
   
 @AppId RECORDID   = ''-1''
)  
AS  
  
DECLARE  
 @vCONST_ROLE  VARCHAR(5),   
   
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10  
  @vCONST_REPDO VARCHAR(10),      
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
    
 @vCONST_APPID VARCHAR(5)  

  
SET @vCONST_ROLE   = ''-1''  
SET @vCONST_REPDO = ''-1''  
SET @vCONST_APPID = ''-1''  
  
  
SELECT 
	DISTINCT U.USER_RID  USERID,  
  	U.USER_NM  USERNAME,  
  	U.USER_FNAME FIRSTNAME,  
  	U.USER_LNAME LASTNAME,  
  	U.USER_LNAME + '', '' + U.USER_FNAME + '' - ''  + U.USER_NM  USERDEF  
FROM 
	CSAA_USERS U  
INNER JOIN 
	CSAA_USERS_ROLES UR 
ON 
	U.USER_RID = UR.USER_RID  
INNER JOIN 
	CSAA_ROLES R 
ON 
	R.ROLE_RID = UR.ROLE_RID  
WHERE  
	(@AppId = @vCONST_APPID    
OR 
	UR.APP_RID = @AppId )  
--CSR 5166.Ch1: Added the condition below to display active users only
AND  U.USER_ACTIVE=1
AND  
	(@RepDO  =   @vCONST_REPDO  
OR 
	USER_DO = @RepDO)  

ORDER BY
	U.USER_LNAME
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_LogActivity]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_LogActivity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROCEDURE [dbo].[AUTH_LogActivity]
@AppName VARCHAR(50),
@CurrentUser USERNAME,
@St	LONGDESC,
@AppId INT = NULL
AS
IF @AppId IS NULL
	SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)

INSERT INTO CSAA_AUTHENTICATION_LOG (
    USER_NM,
    APP_RID,
    AUTH_ACTIVITY
  ) VALUES (
    @CurrentUser,
    @AppId,
    @St
  )





' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_MailList]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_MailList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'


CREATE procedure [dbo].[AUTH_MailList]
( 
@DoList varchar(2500) 
)

/*
DESCRIPTION:
This procedure is called by Aged Transaction Batch to generate Manager''s email list.
This procedure is used to list all Active Manager''s email for the given District Office.
This procedure also finds the Parent Office for the Branch Office and generates Manager''s 
email list for those Parent offices too. 
CREATED:
12/19/2006 BY COGNIZANT

*/
AS
BEGIN 
SET NOCOUNT ON   
--Create DO List 
create table #tmpDo([DO] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS)
Insert into #tmpDo([DO])
(select Item from Payments.dbo.Split(@DoList) where Item<>null or Item<>'''')

--Create Manager''s Email List
Create table #tmpmail([Email] varchar(100) COLLATE SQL_Latin1_General_CP1_CI_AS,
[DO] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,[SPOKE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS)

--SPOKE column holds the branch office number for which the email list of its corresponding HUB has to be found
Insert into #tmpmail([Email],[DO],[SPOKE])
(
select USER_EMAIL,USER_DO,'''' from CSAA_USERS U 
inner join CSAA_USERS_ROLES R on U.USER_RID=R.USER_RID 
inner join #tmpDo D on U.USER_DO=D.DO
where R.ROLE_RID=2 and U.USER_ACTIVE=1 and 
U.USER_EMAIL<>''''
)

--Create Manager''s Email List of HUB for its BranchOffice
Insert into #tmpmail([Email],[DO],[SPOKE])
(
select U.USER_EMAIL,M.HUB,D.DO from #tmpDo D 
inner join CSAA_DO_MAPPING M on D.DO=M.DO
inner join CSAA_USERS U on U.USER_DO=M.HUB
inner join CSAA_USERS_ROLES R on U.USER_RID=R.USER_RID
where R.ROLE_RID=2 and Len(D.DO)>2 and U.USER_ACTIVE=1 and
U.USER_EMAIL<>''''
)

--Select all Email List
select * from #tmpmail order by DO

--Drop all temporary tables
drop table #tmpmail

drop table #tmpDo


SET NOCOUNT OFF
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_SetLockoutStatus]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_SetLockoutStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROCEDURE [dbo].[AUTH_SetLockoutStatus]
/*
DESCRIPTION:
Sets or resets the account lockout flag.

PARAMETERS:
@AppName - The name of the calling application
@CurrentUser - UserId of the calling user.
@UserId - UserId of the locked user.
@Lock - Status to set flag to

CREATED:
7/30/2003 Jeff McEwen

MODIFICATION HISTORY:

*/
@UserId USERNAME,
@Lock BIT,
@CurrentUser USERNAME,
@AppName VARCHAR(50)
AS
  
IF @CurrentUser<>@UserId
	BEGIN
	-- Verify CurrentUser is an administrator.	
	EXEC AUTH_CheckAdmin @CurrentUser, @AppName
	IF @@ERROR<>0 RETURN
	END

UPDATE CSAA_USERS
SET USER_LOCKED_OUT = CAST(@Lock AS TINYINT)
WHERE USER_NM = @UserId

DECLARE @St VARCHAR(256)
SET @St = ''LOCKOUT STATUS USERNAME '' + @UserId + '' SET = '' + CAST(@Lock AS CHAR(1))
EXEC AUTH_LogActivity @AppName, @CurrentUser, @St





' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_UpdateDO]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_UpdateDO]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AUTH_UpdateDO]
/*                                          
DESCRIPTION:                                          
This procedure will update a DO''s profile, or if the DO  doesn''t exist, will                                           
create a new one.  

PARAMETERS:
@DORID - Flag which determines insert or update operation
@DOName - Name of the District Office
@DOID - District Office number
@HUB - DO Description
@Active - status flag of District Office
@CurrentUser - username currently logged in.

CREATED:
4/20/2006

---- Modified by Cognizant on 08/08/2014 - As part of MAIG Integration Project :    
---- CHG0106404 DO ID Length Expansion - changed from 3 to 10    
---- CHG0110069 - MAIG Enhancement - DO ID Length Expansion by modidying the condition and error message - changed from 2 to 9 on 3/5/2015
      
*/
@DORID INT,
@DOName varchar(50),
---- CHG0106404 - Begin - DO ID Length Expansion - changed from 3 to 10    
@DOID varchar(10),    
---- CHG0106404 - End - DO ID Length Expansion - changed from 3 to 10  
@HUB varchar(2),
@Active BIT,  
@CurrentUser USERNAME

AS                                          
SET NOCOUNT ON  

DECLARE @St VARCHAR(300),@currentstatus bit

-- Truncates leading zeros
SET @DOID = CASE WHEN @DOID LIKE ''0%''
            THEN Right(@DOID, Len(@DOID)) - (Patindex(''0%'', @DOID) - 1)
            ELSE @DOID
            END

IF @HUB = ''''
BEGIN 
	IF @DORID = 0
	BEGIN  
 ---- CHGXXXXXXX - MAIG Enhancement - Begin - DO ID Length Expansion by modidying the condition and error message - changed from 2 to 9 on 3/5/2015
  IF len(@DOID) > 9      
		BEGIN                  
    RAISERROR(''The Hub Branch Office Number  cannot exceed 9 digits.'', 12,1)    
    ---- CHGXXXXXXX - MAIG Enhancement - END - DO ID Length Expansion by modidying the condition and error message - changed from 2 to 9 on 3/5/2015                                              
  		RETURN                                           
  		END 

  		IF EXISTS(SELECT DO_CODE FROM CSAA_DO WHERE  DO_CODE = @DOID)
	  	BEGIN                  
  		RAISERROR(''The Branch Office Number already  exists.'', 11, 1)                                        
  		RETURN                                           
  	  	END   
	
	      
	END

        IF @DORID=1 
        BEGIN
         ---- CHGXXXXXXX - MAIG Enhancement - Begin - DO ID Length Expansion by modidying the condition and error message - changed from 2 to 9 on 3/5/2015
                 IF len(@DOID) > 9      
		BEGIN                  
    RAISERROR(''The Hub Branch Office Number  cannot exceed 9 digits.'', 12,1)    
     ---- CHGXXXXXXX - MAIG Enhancement - END - DO ID Length Expansion by modidying the condition and error message - changed from 2 to 9 on 3/5/2015                                              
  		RETURN                                           
  		END 
		
	     	SET @currentstatus=(Select Enabled from  CSAA_DO where DO_Code=@DOID) 
	     	IF @currentstatus=1 and @Active=0 -- trying  to deactivate
		BEGIN
		--check for active branch office and users
		EXEC AUTH_ActiveDOandUser @DOID
		IF @@ERROR<>0                     
                               BEGIN                  
                                RETURN    
		    END
	            	END
        END

END

IF @HUB <> ''''
BEGIN
		--trying to insert
		IF @DORID=0 
    		BEGIN
		--check the length
  			IF len(@DOID) < 3
			BEGIN  
	           	             RAISERROR(''The Spoke  Branch Office Number should be 3 digits.'', 15,1)                                           
  			RETURN                                           
  			END 
		--check whether DO exist 
			IF EXISTS (SELECT DO_CODE FROM  CSAA_DO WHERE DO_CODE = @DOID)
			BEGIN 
	      		RAISERROR(''The Branch Office Number  already exists.'', 11, 1)                                         
			RETURN                                           
  			END   

   	       END

	       IF @DORID=1
    	       BEGIN
			SET @currentstatus=(Select Enabled  from CSAA_DO where DO_Code=@DOID) 
			-- trying to deactivate
	   		IF @currentstatus=1 and @Active=0 
	   		BEGIN
	   		--check for active users
	   			IF EXISTS (select USER_NM  from CSAA_USERS where USER_ACTIVE=1 and USER_DO=@DOID) 
	      			BEGIN    
	                	              RAISERROR(''Cannot be deactivated; associated users with the  office are still active.'', 14,1)                                           
  	      			RETURN                                           
  	      			END  
	   		END
   	       END 

	    --check whether Hub is active common for insert  and update
	    IF (SELECT Enabled FROM CSAA_DO where DO_CODE  in (@HUB))=0
	    BEGIN  
	    RAISERROR(''The selected HUB is inactive. Please  select a different one.'', 16, 1)                                         
	    RETURN                                          
  	    END     	
END

BEGIN TRANSACTION
      IF @DORID=0 --Insert new HUB or Brach office
      BEGIN
         INSERT INTO CSAA_DO
         (DO_CODE,[Description],Enabled) VALUES  
         (@DOID,UPPER(@DOName),@Active)
         SET @St = @DOName + '' District Office has been  created''
          IF @@ERROR<>0                     
             BEGIN                  
             ROLLBACK TRANSACTION                   
             RETURN                                
             END
          
      END
      IF @HUB <> '''' AND @DORID=0 --Map if Branch office
      BEGIN
         INSERT INTO CSAA_DO_MAPPING 
         (DO,HUB)VALUES
         (@DOID,@HUB)
            
            IF @@ERROR<>0                     
            BEGIN                  
            ROLLBACK TRANSACTION                   
            RETURN                                
            END
      END
	  
	  
      IF @DORID=1 -- Update HUB or Brach office
      BEGIN
         UPDATE CSAA_DO SET 
         [Description]=UPPER(@DOName),
         Enabled=@Active
         WHERE
         DO_CODE=@DOID
         	 -- trying to deactivate
	 IF @currentstatus=1 and @Active=0
             SET @St = @DOName + '' District Office has been  Deactivated''
	  ELSE IF @currentstatus=0 and @Active=1                                     
		SET @St = @DOName + '' District Office has  been Activated''
	 ELSE
              SET @St = @DOName + '' District Office has  been updated''
	
            IF @@ERROR<>0                     
            BEGIN                  
            ROLLBACK TRANSACTION                   
            RETURN                                
            END
      END
      IF @HUB <> '''' AND @DORID=1 
      BEGIN
	UPDATE CSAA_DO_MAPPING SET
        					DO=@DOID,
       				             HUB=@HUB
       	 WHERE DO=@DOID
       
	 IF @currentstatus=1 and @Active=0 
            		SET @St = @DOName + '' District  Office has been Deactivated''
	 ELSE IF @currentstatus=0 and @Active=1 
		SET @St = @DOName + '' District Office has  been Activated''
	 ELSE
           		 SET @St = @DOName + '' District  Office has been updated''
            IF @@ERROR<>0                     
            BEGIN                  
            ROLLBACK TRANSACTION                   
            RETURN                                
            END
        
       END

EXEC AUTH_DOACTIVITY @DOID,@HUB,@St,@CurrentUser
COMMIT TRANSACTION
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_UpdatePassword]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_UpdatePassword]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N' 
 
 
 
 
CREATE PROCEDURE [dbo].[AUTH_UpdatePassword] 
/*
DESCRIPTION:
Allows for updating a user''s password by either the Rid or Id
 
PARAMETERS:
@AppName - the name of the calling application.
@CurrentUser - the name of the calling user
@UserRid - optional recordId of the user to update.
@UserId - optional userid of the user to update.
@NewPassword - the new password (must be encrypted).
@UpdateDate DATETIME=NULL - the date the update occured.  This can be backdated for resets
   to force changing the password right away.
 
CREATED:
7/29/2003 Jeff McEwen
 
MODIFICATION HISTORY:
2/3/2004 Added LOCKOUT_COUNTER to the Update statements - Bindu.
MODIFIED BY COGNIZANT ON 08/29/2011:
67811A0 - PCI Remediation for Payment systems - CH1: Added userid and current user to the reset password message when admin user reset password.
67811A0 - PCI Remediation for Payment systems - CH2: Added output parameter @Result 
67811A0 - PCI Remediation for Payment systems - CH3: Get the old password for the user
67811A0 - PCI Remediation for Payment systems - CH4: Added code so that user cannot update the old password
67811A0 - PCI Remediation for Payment systems - CH5: Update password only if it is not the old password
*/
@UserRid INT = NULL,
@UserId USERNAME = NULL,
@NewPassword PASSWORD,
@UpdateDate DATETIME=NULL,
@CurrentUser USERNAME,
@AppName VARCHAR(50),
--67811A0 - PCI Remediation for Payment systems - CH2:START- Added output parameter @Result 
@Result INT=NULL OUTPUT
--67811A0 - PCI Remediation for Payment systems - CH2:END
AS
declare @oldpassword PASSWORD
IF @UserRid IS NULL AND @UserId IS NULL
        BEGIN
        RAISERROR (''Must provide either UserRid or UserId to reset password.'', 16, 1)
        END
 
-- 67811A0 - PCI Remediation for Payment systems - CH3:START Get the old password for the user
IF @UserRid IS NULL 
	SELECT @oldpassword = USER_PASSWORD FROM CSAA_USERS WHERE USER_NM = @UserId
ELSE
	SELECT @oldpassword = USER_PASSWORD FROM CSAA_USERS WHERE USER_NM = @UserRid
-- 67811A0 - PCI Remediation for Payment systems - CH3:END
	
-- 67811A0 - PCI Remediation for Payment systems - CH4:START Added code so that user cannot update the old password
IF ((@oldpassword = @NewPassword )AND (@oldpassword <>''1Rl9k8BjorHiLRYwo5t67w==''))
	SET @Result =1	
ELSE
	SET @Result =0
-- 67811A0 - PCI Remediation for Payment systems - CH4:END
-- Check to see if this is being called by the user himself or an administrator
IF @CurrentUser<>@UserId
        BEGIN
        EXEC AUTH_CheckAdmin @CurrentUser, @AppName
        IF @@ERROR<>0 RETURN
        END
 
IF @UpdateDate IS NULL SET @UpdateDate = GETDATE()
 
--67811A0 - PCI Remediation for Payment systems - CH5:START Update password only if it is not the old password
IF @Result=0 
--67811A0 - PCI Remediation for Payment systems - CH5:END 
BEGIN
IF @UserRid IS NULL 
        UPDATE CSAA_USERS SET
                USER_PASSWORD   = @NewPassword,
                DATE_UPDATED    = getdate(),
                DATE_PWD_UPDATED= @UpdateDate,
                LOCKOUT_COUNTER  = 0
        WHERE USER_NM           = @UserId
ELSE
        UPDATE CSAA_USERS SET
                USER_PASSWORD   = @NewPassword,
                DATE_UPDATED    = getdate(),
                DATE_PWD_UPDATED= @UpdateDate,
                LOCKOUT_COUNTER  = 0
        WHERE USER_RID          = @UserRid
 
-- STANDARD CALL TO AUDIT ACTIVITY
DECLARE @St VARCHAR(256)
IF @UserId IS NOT NULL
		-- 67811A0 - PCI Remediation for Payment systems - CH1:START - Added userid and current user to the reset password message when admin user reset password.
		--SET @St = ''PASSWORD UPDATED USERNM='' + @UserId
		IF @CurrentUser<>@UserId	
		SET @St = ''USER '' + @UserId + '' PASSWORD HAS BEEN RESET BY THE USER '' +  @CurrentUser
ELSE 
		SET @St = ''PASSWORD UPDATED USERNM='' + @UserId
		-- 67811A0 - PCI Remediation for Payment systems - CH1:END - Added userid and current user to the reset password message when admin user reset password.
	ELSE 
		-- 67811A0 - PCI Remediation for Payment systems - CH1:START - Added userid and current user to the reset password message when admin user reset password.
		-- SET @St = ''PASSWORD UPDATED USERID='' + CONVERT(VARCHAR(5), @UserRid)
		IF @CurrentUser<>@UserId	
		SET @St = ''USER '' + CONVERT(VARCHAR(5), @UserRid) + '' PASSWORD HAS BEEN RESET BY THE USER '' + @CurrentUser
		ELSE
		SET @St = ''PASSWORD UPDATED USERID='' + CONVERT(VARCHAR(5), @UserRid)
		
	-- 67811A0 - PCI Remediation for Payment systems - CH1:END - Added userid and current user to the reset password message when admin user reset password.

	EXEC AUTH_LogActivity @AppName, @CurrentUser, @St

END
 
' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_UpdateUser]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_UpdateUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE    PROCEDURE [dbo].[AUTH_UpdateUser]                                                
/*                                          
DESCRIPTION:                                          
This procedure will update a user''s profile, or if the user doesn''t exist, will                                          
create a new one.                                          
                                          
PARAMETERS:                                          
@AppName -                                          
@CurrentUser -                                          
@UserId -                                          
@FirstName -                                          
@LastName -                                          
@Email -                                          
@Phone -                                          
@Active -                                          
@Roles -                                          
                                          
CREATED:                                          
7/29/2003 Jeff McEwen                                          
                                          
MODIFICATION HISTORY:                                          
REVISION I:                                          
07/20/2005 - MODIFIED BY COGNIZANT                                           
    - Replaced the sp_LogActivity with AUTH_LogActivity stored procedure                                       
02/05/2006 - MODIFIED BY COGNIZANT for  CSR 4593                                     
-- Included the code to call AUTH_CheckForInsert ,to ensure that the  user count                                      
      has not exceeded the maximum permissible limit for an application,while adding     
      a new user  and to log the exception                                     
--Included the code to call AUTH_CheckForUpdate ,to ensure that  the user count                                      
      has not exceeded the maximum permissible limit for an application,while activating    
     an user   and to log the exception       

12/20/2006 -  MODIFIED BY COGNIZANT for  CSR 5595
  Included the code to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions   
--RFC 185138 - AD Integration CH1 - Commented the input password parameter in case of update/isert new customer.                                             
 --RFC 185138 - AD Integration CH2 - Commented the insertion of Password and lat password updated date in case of update/isert new customer.                                           


10/27/2007 - Modified as part of RBAC - Payment Tool Integration
RBAC-PT.Ch1 - Insert/Update USR_ROLE table                         
---- Modified by Cognizant on 08/08/2014 - As part of MAIG Integration Project :      
---- CHG0106404 : Including AgencyID & AgencyName to be updated to the PT DB  
---- CHG0106404 DO ID Length Expansion - changed from 3 to 10                       
                                            
*/                                          
@UserRid  INT = 0,                                          
@UserId   USERNAME,                                          
@FirstName   NAME,                                          
@LastName   NAME,                                          
@Email   LONGDESC,                                          
@Phone   PHONE,                                          
@Active   BIT,                                          
@DO    varchar(10),   -- MAIG - Added Code to increase the DO Code from 3 to 10 characters                                         
@RepId   INT,                                          
@Roles   VARCHAR(50),                                          

---- CHG0106404 - Begin - Including AgencyID & AgencyName
@AgencyID  INT,  
@AgencyName  VARCHAR(50),                                         
---- CHG0106404 - End - Including AgencyID & AgencyName
  
--@NewPassword PASSWORD=NULL, --RFC 185138 - AD Integration CH1 - Commented the input password parameter in case of update/isert new customer.
@UpdateDate  DATETIME=NULL,                                          
@CurrentUser USERNAME,                                          
@AppName  VARCHAR(50)                                        
                                        
                                        
AS                                          
SET NOCOUNT ON                                         
                                
-- Verify that the user is an administrator.                                          
EXEC AUTH_CheckAdmin @CurrentUser, @AppName                                          
IF @@ERROR<>0 RETURN                                          
      
                             
DECLARE @St VARCHAR(300), @AppId INT, @CurrentDO Varchar(10)  --MAIG -  Added Code to increase the DO Code from 3 to 10 characters                                                                                   
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName) 
-- START Added By Cognizant for CSR 5595   to retrieve the selected  DO   
SET @CurrentDO = (SELECT USER_DO FROM CSAA_USERS WHERE USER_NM = @UserId)  
-- END  CSR 5595                                             
                                          
-- Create a temp table containing the roles.                                          
SET @Roles=REPLACE(REPLACE(@Roles,''['',''''),'']'','''')                                          
CREATE TABLE #Tmp_Roles (Role INT, Description VARCHAR(50))           
IF @Roles<>''''                                          
 BEGIN                                          
 SET @St = ''INSERT INTO #Tmp_Roles SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES WHERE ROLE_RID IN ('' + @Roles + '')''                                          
 EXEC (@St)                                          
 END                                          
                                          
SET @St = ''DROP TABLE #Tmp_Roles''                                          
                                          
IF @UserRid=0                                          
 BEGIN                                       IF EXISTS(SELECT USER_RID FROM CSAA_USERS WHERE USER_NM = @UserId)                                          
  BEGIN                  
  RAISERROR(''%s  already exists.'', 15, 1, @UserId)                                          
  EXEC (@St)                                    RETURN                                          
  END                                          
 -- Insert the user record                    
                                       
BEGIN TRANSACTION                                         
--START  Added By Cognizant for CSR 4593   to call AUTH_CheckForInsert ,to ensure that the  user count                                      
-- has not exceeded the maximum permissible limit for an application,while adding    a new user ,and log the activity    
                                                                        
DECLARE @AppRid INT                 
DECLARE @stmt varchar(50)                                       
SET @AppRid = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)                                    
SET @stmt =''User ''+ @UserId+ '' Addition Failed For ''+ @AppName +''Application''        
EXEC AUTH_CheckForInsert @AppName, @AppRid,@Active,@CurrentUser,@UserId                                        
-- Log the activity           
IF @@ERROR<>0                     
BEGIN                  
ROLLBACK TRANSACTION                   
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END           
--END - CSR 4593     
                                           
  INSERT INTO CSAA_USERS (                                           
  USER_NM,                                          
  USER_FNAME,                                          
USER_LNAME,                                          
--RFC 185138 - AD Integration CH2 STARt- Commented the insertion of Password and lat password updated date in case of update/isert new customer.                                           
  --USER_PASSWORD,                                              
  USER_EMAIL,                                          
  USER_PHONE,                       
  USER_ACTIVE,                                          
  USER_REPID,                                          
  USER_DO,
  
  ---- CHG0106404 - Begin - Including AgencyID & AgencyName  
  AgencyID,  
  AgencyName                                               
  ---- CHG0106404 - End - Including AgencyID & AgencyName 
  --DATE_PWD_UPDATED                                              
 )                                          
 VALUES(                                          
  Lower(@UserId),                                          
  @FirstName,                                          
  @LastName,                                          
 -- @NewPassword,                                              
  @Email,                                          
  @Phone,                                          
  @Active,                                          
  @RepId,                                          
  @DO,  
  ---- CHG0106404 - Begin - Including AgencyID & AgencyName
  @AgencyID,  
  @AgencyName                                              
  ---- CHG0106404 - End - Including AgencyID & AgencyName 
  --@UpdateDate    
  --RFC 185138 - AD Integration CH2 END- Commented the insertion of Password and lat password updated date in case of update/isert new customer.                                                                                     
 )                                          
                                           
 SET @UserRid = @@IDENTITY                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                             
 -- Establish the user''s roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
                                          
 IF @@ERROR<>0                 
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                
 -- Log the activity                                          
 SET @St = ''CREATED USER '' + @UserId                                          
 -- Replace the sp_LogActivity with AUTH_LogActivity                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 COMMIT                                          
 END                                          
ELSE                                          
 BEGIN                                
 IF @UserId=@CurrentUser                                          
  -- A user cannot remove himself from admin or su roles.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                              
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''Administrator'', ''SU'')                                          
  )                                          
   BEGIN                                          
   RAISERROR(''User may not remove him/herself from an administrative role.'', 16, 1)                                       
   EXEC (@St)                                          
   RETURN                                          
   END                                          
                             
 ELSE                                       
  -- A user cannot be removed from the SU role by someone who is not an SU.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                                           
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''SU'')                                          
  ) AND NOT EXISTS (                                          
   SELECT * FROM AUTH_User_Roles AS R                                           
   INNER JOIN CSAA_USERS AS U ON R.USER_RID=U.USER_RID                                          
   WHERE USER_NM=@CurrentUser AND APP_RID=@AppId                                          
   AND ROLE_NM IN (''SU'')                                          
  )                                          
  INSERT INTO #Tmp_Roles (Role, Description)                                         
  SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES                                           
  WHERE ROLE_NM=''SU''               
              
--START  Added By Cognizant for CSR 4593  to invoke Auth_CheckForUpdate,when activating an user     
-- and to log the exception     
SET @stmt =''User ''+ @UserId+ '' Update Failed For ''+ @AppName +''Application''                          
exec Auth_CheckForUpdate @UserRid,@Active,@AppId     
-- Log the activity              
IF @@ERROR<>0                     
BEGIN  
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END               
--END - CSR 4593

-- START Added By Cognizant for CSR 5595 to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions   
IF (@DO != @CurrentDO)    
BEGIN    
 IF EXISTS(SELECT USER_ID FROM Payments.dbo.PAY_payment       
                  WHERE USER_ID = @UserId and Status_Id in (2,3,6)
)    
     
  BEGIN    
 RAISERROR(''User still has open transactions in the associated District office. Please clear the queue for the District office changes .'', 11,1)                                               
   RETURN                                               
   END     
END    
--END - CSR 5595 

  -- Update the user''s record.                                          
 UPDATE CSAA_USERS SET                                          
  USER_FNAME   = @FirstName,                                          
  USER_LNAME   = @LastName,                              
  USER_EMAIL  = @Email,                                          
  USER_PHONE  = @Phone,                                          
  USER_ACTIVE  = @Active,                                          
  USER_REPID  = @RepId,                                          
  USER_DO   = @DO,                                          
  DATE_UPDATED = getdate(),  
  ---- CHG0106404 - Begin - Including AgencyID & AgencyName
  AgencyID = @AgencyID,  
  AgencyName = @AgencyName                                 
  ---- CHG0106404 - End - Including AgencyID & AgencyName          
 WHERE USER_NM = @UserId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Remove any roles that have been removed.                                           
 DELETE FROM CSAA_USERS_ROLES                                           
 WHERE ROLE_RID NOT IN (SELECT Role FROM #Tmp_Roles)                                          
 AND USER_RID=@UserRid AND APP_RID=@AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Add any new roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
 WHERE Role NOT IN (SELECT ROLE_RID FROM CSAA_USERS_ROLES                                          
  WHERE USER_RID=@UserRid AND APP_RID=@AppId)                                          
 IF @@ERROR<>0                
  BEGIN                                          
  ROLLBACK TRANSACTION            
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                    
 -- Log the activity                                          
 SET @St = ''UPDATED USER '' + @UserId                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                    
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 END                                          
                                          
DROP TABLE #Tmp_Roles

--RBAC-PT.Ch1: START - Insert/Update USR_ROLE table 
Declare @UserRoles varchar(200), @UserRoleIDs varchar(100)

Select @UserRoles = COALESCE(@UserRoles + '','', '''') + [Description] 
FROM
(
SELECT  DISTINCT U.USER_RID AS UserRid, R.ROLE_NM AS [Description] 
FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID
WHERE U.USER_RID = @UserRid
) as USERROLES

Select @UserRoleIDs = COALESCE(@UserRoleIDs + '','', '''') + CONVERT(VARCHAR(2),[Role_ID]) 
FROM
(
SELECT  DISTINCT U.USER_RID AS UserRid, R.ROLE_RID AS [Role_ID] 
FROM CSAA_USERS U
INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID
WHERE U.USER_RID = @UserRid
) as UserRoleIDs

IF EXISTS(SELECT * FROM USR_ROLE WHERE USER_RID = @UserRid)
BEGIN
	UPDATE USR_ROLE SET ROLE = @UserRoles, RoleIDs = @UserRoleIDs WHERE USER_RID = @UserRid
END
ELSE
BEGIN
	INSERT INTO USR_ROLE VALUES (@UserRid, @UserRoles,@UserRoleIDs)
END
--RBAC-PT.Ch1: END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_UpdateUser_bkp1027]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_UpdateUser_bkp1027]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[AUTH_UpdateUser_bkp1027]                                          
/*                                          
DESCRIPTION:                                          
This procedure will update a user''s profile, or if the user doesn''t exist, will                                          
create a new one.                                          
                                          
PARAMETERS:                                          
@AppName -                                          
@CurrentUser -                                          
@UserId -                                          
@FirstName -                                          
@LastName -                                          
@Email -                                          
@Phone -                                          
@Active -                                          
@Roles -                                          
                                          
CREATED:                                          
7/29/2003 Jeff McEwen                                          
                                          
MODIFICATION HISTORY:                                          
REVISION I:                                          
07/20/2005 - MODIFIED BY COGNIZANT                                           
    - Replaced the sp_LogActivity with AUTH_LogActivity stored procedure                                       
02/05/2006 - MODIFIED BY COGNIZANT for  CSR 4593                                     
-- Included the code to call AUTH_CheckForInsert ,to ensure that the  user count                                      
      has not exceeded the maximum permissible limit for an application,while adding     
      a new user  and to log the exception                                     
--Included the code to call AUTH_CheckForUpdate ,to ensure that  the user count                                      
      has not exceeded the maximum permissible limit for an application,while activating    
     an user   and to log the exception       

12/20/2006 -  MODIFIED BY COGNIZANT for  CSR 5595
  Included the code to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions                           
                                          
*/                                          
@UserRid  INT = 0,                                          
@UserId   USERNAME,                                          
@FirstName   NAME,                                          
@LastName   NAME,                                          
@Email   LONGDESC,                                          
@Phone   PHONE,                                          
@Active   BIT,                                          
@DO    CHAR(3),                                          
@RepId   INT,                                          
@Roles   VARCHAR(50),                                          
@NewPassword PASSWORD=NULL,                                          
@UpdateDate  DATETIME=NULL,                                          
@CurrentUser USERNAME,                                          
@AppName  VARCHAR(50)                                        
                                        
                                        
AS                                          
SET NOCOUNT ON                                         
                                        
-- Verify that the user is an administrator.                                          
EXEC AUTH_CheckAdmin @CurrentUser, @AppName                                          
IF @@ERROR<>0 RETURN                                          
      
                             
DECLARE @St VARCHAR(300), @AppId INT, @CurrentDO CHAR(3)                                           
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName) 
-- START Added By Cognizant for CSR 5595   to retrieve the selected  DO   
SET @CurrentDO = (SELECT USER_DO FROM CSAA_USERS WHERE USER_NM = @UserId)  
-- END  CSR 5595                                             
                                          
-- Create a temp table containing the roles.                                          
SET @Roles=REPLACE(REPLACE(@Roles,''['',''''),'']'','''')                                          
CREATE TABLE #Tmp_Roles (Role INT, Description VARCHAR(50))           
IF @Roles<>''''                                          
 BEGIN                                          
 SET @St = ''INSERT INTO #Tmp_Roles SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES WHERE ROLE_RID IN ('' + @Roles + '')''                                          
 EXEC (@St)                                          
 END                                          
                                          
SET @St = ''DROP TABLE #Tmp_Roles''                                          
                                          
IF @UserRid=0                                          
 BEGIN                                       IF EXISTS(SELECT USER_RID FROM CSAA_USERS WHERE USER_NM = @UserId)                                          
  BEGIN                  
  RAISERROR(''%s  already exists.'', 15, 1, @UserId)                                          
  EXEC (@St)                                    RETURN                                          
  END                                          
 -- Insert the user record                    
                                       
BEGIN TRANSACTION                                         
--START  Added By Cognizant for CSR 4593   to call AUTH_CheckForInsert ,to ensure that the  user count                                      
-- has not exceeded the maximum permissible limit for an application,while adding    a new user ,and log the activity    
                                                                        
DECLARE @AppRid INT                 
DECLARE @stmt varchar(50)                                       
SET @AppRid = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)                                    
SET @stmt =''User ''+ @UserId+ '' Addition Failed For ''+ @AppName +''Application''        
EXEC AUTH_CheckForInsert @AppName, @AppRid,@Active,@CurrentUser,@UserId                                        
-- Log the activity           
IF @@ERROR<>0                     
BEGIN                  
ROLLBACK TRANSACTION                   
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END           
--END - CSR 4593     
                                           
  INSERT INTO CSAA_USERS (                                           
  USER_NM,                                          
  USER_FNAME,                                          
USER_LNAME,                                          
  USER_PASSWORD,                                          
  USER_EMAIL,                                          
  USER_PHONE,                       
  USER_ACTIVE,                                          
  USER_REPID,                                          
  USER_DO,                                          
  DATE_PWD_UPDATED                                          
 )                                          
 VALUES(                                          
  Lower(@UserId),                                          
  @FirstName,                                          
  @LastName,                                          
  @NewPassword,                                          
  @Email,                                          
  @Phone,                                          
  @Active,                                          
  @RepId,                                          
  @DO,                                          
  @UpdateDate                                          
 )                                          
                                           
 SET @UserRid = @@IDENTITY                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                             
 -- Establish the user''s roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
                                          
 IF @@ERROR<>0                 
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                
 -- Log the activity                                          
 SET @St = ''CREATED USER '' + @UserId                                          
 -- Replace the sp_LogActivity with AUTH_LogActivity                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 COMMIT                                          
 END                                          
ELSE                                          
 BEGIN                                
 IF @UserId=@CurrentUser                                          
  -- A user cannot remove himself from admin or su roles.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                              
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''Administrator'', ''SU'')                                          
  )                                          
   BEGIN                                          
   RAISERROR(''User may not remove him/herself from an administrative role.'', 16, 1)                                       
   EXEC (@St)                                          
   RETURN                                          
   END                                          
                                             
 ELSE                                       
  -- A user cannot be removed from the SU role by someone who is not an SU.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                                           
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''SU'')                                          
  ) AND NOT EXISTS (                                          
   SELECT * FROM AUTH_User_Roles AS R                                           
   INNER JOIN CSAA_USERS AS U ON R.USER_RID=U.USER_RID                                          
   WHERE USER_NM=@CurrentUser AND APP_RID=@AppId                                          
   AND ROLE_NM IN (''SU'')                                          
  )                                          
  INSERT INTO #Tmp_Roles (Role, Description)                                         
  SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES                                           
  WHERE ROLE_NM=''SU''               
              
--START  Added By Cognizant for CSR 4593  to invoke Auth_CheckForUpdate,when activating an user     
-- and to log the exception     
SET @stmt =''User ''+ @UserId+ '' Update Failed For ''+ @AppName +''Application''                          
exec Auth_CheckForUpdate @UserRid,@Active,@AppId     
-- Log the activity              
IF @@ERROR<>0                     
BEGIN  
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END               
--END - CSR 4593

-- START Added By Cognizant for CSR 5595 to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions   
IF (@DO != @CurrentDO)    
BEGIN    
 IF EXISTS(SELECT USER_ID FROM Payments.dbo.PAY_payment       
                  WHERE USER_ID = @UserId and Status_Id in (2,3,6))    
     
  BEGIN    
 RAISERROR(''User still has open transactions in the associated District office. Please clear the queue for the District office changes .'', 11,1)                                               
   RETURN                                               
   END     
END    
--END - CSR 5595 

  -- Update the user''s record.                                          
 UPDATE CSAA_USERS SET                                          
  USER_FNAME   = @FirstName,                                          
  USER_LNAME   = @LastName,                              
  USER_EMAIL  = @Email,                                          
  USER_PHONE  = @Phone,                                          
  USER_ACTIVE  = @Active,                                          
  USER_REPID  = @RepId,                                          
  USER_DO   = @DO,                                          
  DATE_UPDATED = getdate()                                          
 WHERE USER_NM = @UserId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Remove any roles that have been removed.                                           
 DELETE FROM CSAA_USERS_ROLES                                           
 WHERE ROLE_RID NOT IN (SELECT Role FROM #Tmp_Roles)                                          
 AND USER_RID=@UserRid AND APP_RID=@AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Add any new roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
 WHERE Role NOT IN (SELECT ROLE_RID FROM CSAA_USERS_ROLES                                          
  WHERE USER_RID=@UserRid AND APP_RID=@AppId)                                          
 IF @@ERROR<>0                
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                    
 -- Log the activity                                          
 SET @St = ''UPDATED USER '' + @UserId                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                    
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 END                                          
                                          
DROP TABLE #Tmp_Roles



' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_UpdateUser_test]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_UpdateUser_test]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AUTH_UpdateUser_test]                                          
/*                                          
DESCRIPTION:                                          
This procedure will update a user''s profile, or if the user doesn''t exist, will                                          
create a new one.                                          
                                          
PARAMETERS:                                          
@AppName -                                          
@CurrentUser -                                          
@UserId -                                          
@FirstName -                                          
@LastName -                                          
@Email -                                          
@Phone -                                          
@Active -                                          
@Roles -                                          
                                          
CREATED:                                          
7/29/2003 Jeff McEwen                                          
                                          
MODIFICATION HISTORY:                                          
REVISION I:                                          
07/20/2005 - MODIFIED BY COGNIZANT                                           
    - Replaced the sp_LogActivity with AUTH_LogActivity stored procedure                                       
02/05/2006 - MODIFIED BY COGNIZANT for  CSR 4593                                     
-- Included the code to call AUTH_CheckForInsert ,to ensure that the  user count                                      
      has not exceeded the maximum permissible limit for an application,while adding     
      a new user  and to log the exception                                     
--Included the code to call AUTH_CheckForUpdate ,to ensure that  the user count                                      
      has not exceeded the maximum permissible limit for an application,while activating    
     an user   and to log the exception       

12/20/2006 -  MODIFIED BY COGNIZANT for  CSR 5595
  Included the code to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions                           
                                          
*/                                          
@UserRid  INT = 0,                                          
@UserId   USERNAME,                                          
@FirstName   NAME,                                          
@LastName   NAME,                                          
@Email   LONGDESC,                                          
@Phone   PHONE,                                          
@Active   BIT,                                          
@DO    CHAR(3),                                          
@RepId   INT,                                          
@Roles   VARCHAR(50),                                          
@NewPassword PASSWORD=NULL,                                          
@UpdateDate  DATETIME=NULL,                                          
@CurrentUser USERNAME,                                          
@AppName  VARCHAR(50)                                        
                                        
                                        
AS                                          
SET NOCOUNT ON                                         
                                        
-- Verify that the user is an administrator.                                          
EXEC AUTH_CheckAdmin @CurrentUser, @AppName                                          
IF @@ERROR<>0 RETURN                                          
      
                             
DECLARE @St VARCHAR(300), @AppId INT, @CurrentDO CHAR(3)                                           
SET @AppId = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName) 
-- START Added By Cognizant for CSR 5595   to retrieve the selected  DO   
SET @CurrentDO = (SELECT USER_DO FROM CSAA_USERS WHERE USER_NM = @UserId)  
-- END  CSR 5595                                             
                                          
-- Create a temp table containing the roles.                                          
SET @Roles=REPLACE(REPLACE(@Roles,''['',''''),'']'','''')                                          
CREATE TABLE #Tmp_Roles (Role INT, Description VARCHAR(50))           
IF @Roles<>''''                                          
 BEGIN                                          
 SET @St = ''INSERT INTO #Tmp_Roles SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES WHERE ROLE_RID IN ('' + @Roles + '')''                                          
 EXEC (@St)                                          
 END                                          
                                          
SET @St = ''DROP TABLE #Tmp_Roles''                                          
                                          
IF @UserRid=0                                          
 BEGIN                                       IF EXISTS(SELECT USER_RID FROM CSAA_USERS WHERE USER_NM = @UserId)                                          
  BEGIN                  
  RAISERROR(''%s  already exists.'', 15, 1, @UserId)                                          
  EXEC (@St)                                    RETURN                                          
  END                                          
 -- Insert the user record                    
                                       
BEGIN TRANSACTION                                         
--START  Added By Cognizant for CSR 4593   to call AUTH_CheckForInsert ,to ensure that the  user count                                      
-- has not exceeded the maximum permissible limit for an application,while adding    a new user ,and log the activity    
                                                                        
DECLARE @AppRid INT                 
DECLARE @stmt varchar(50)                                       
SET @AppRid = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)                                    
SET @stmt =''User ''+ @UserId+ '' Addition Failed For ''+ @AppName +''Application''        
EXEC AUTH_CheckForInsert @AppName, @AppRid,@Active,@CurrentUser,@UserId                                        
-- Log the activity           
IF @@ERROR<>0                     
BEGIN                  
ROLLBACK TRANSACTION                   
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END           
--END - CSR 4593     
                                           
  INSERT INTO CSAA_USERS (                                           
  USER_NM,                                          
  USER_FNAME,                                          
USER_LNAME,                                          
  USER_PASSWORD,                                          
  USER_EMAIL,                                          
  USER_PHONE,                       
  USER_ACTIVE,                                          
  USER_REPID,                                          
  USER_DO,                                          
  DATE_PWD_UPDATED                                          
 )                                          
 VALUES(                                          
  Lower(@UserId),                                          
  @FirstName,                                          
  @LastName,                                          
  @NewPassword,                                          
  @Email,                                          
  @Phone,                                          
  @Active,                                          
  @RepId,                                          
  @DO,                                          
  @UpdateDate                                          
 )                                          
                                           
 SET @UserRid = @@IDENTITY                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                             
 -- Establish the user''s roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
                                          
 IF @@ERROR<>0                 
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                
 -- Log the activity                                          
 SET @St = ''CREATED USER '' + @UserId                                          
 -- Replace the sp_LogActivity with AUTH_LogActivity                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 COMMIT                                          
 END                                          
ELSE                                          
 BEGIN                                
 IF @UserId=@CurrentUser                                          
  -- A user cannot remove himself from admin or su roles.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                              
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''Administrator'', ''SU'')                                          
  )                                          
   BEGIN                                          
   RAISERROR(''User may not remove him/herself from an administrative role.'', 16, 1)                                       
   EXEC (@St)                                          
   RETURN                                          
   END                                          
                                             
 ELSE                                       
  -- A user cannot be removed from the SU role by someone who is not an SU.                                          
  IF EXISTS (                                          
   SELECT * FROM AUTH_User_Roles                                           
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                          
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                          
   AND ROLE_NM IN (''SU'')                                          
  ) AND NOT EXISTS (                                          
   SELECT * FROM AUTH_User_Roles AS R                                           
   INNER JOIN CSAA_USERS AS U ON R.USER_RID=U.USER_RID                                          
   WHERE USER_NM=@CurrentUser AND APP_RID=@AppId                                          
   AND ROLE_NM IN (''SU'')                                          
  )                                          
  INSERT INTO #Tmp_Roles (Role, Description)                                         
  SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES                                           
  WHERE ROLE_NM=''SU''               
              
--START  Added By Cognizant for CSR 4593  to invoke Auth_CheckForUpdate,when activating an user     
-- and to log the exception     
SET @stmt =''User ''+ @UserId+ '' Update Failed For ''+ @AppName +''Application''                          
exec Auth_CheckForUpdate @UserRid,@Active,@AppId     
-- Log the activity              
IF @@ERROR<>0                     
BEGIN  
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                   
RETURN                                
END               
--END - CSR 4593

-- START Added By Cognizant for CSR 5595 to ensure that the salesrep''s DO cannot
-- be updated if he has active transactions   
IF (@DO != @CurrentDO)    
BEGIN    
 IF EXISTS(SELECT USER_ID FROM Payments.dbo.PAY_payment       
                  WHERE USER_ID = @UserId and Status_Id in (2,3,6))    
     
  BEGIN    
 RAISERROR(''User still has open transactions in the associated District office. Please clear the queue for the District office changes .'', 11,1)                                               
   RETURN                                               
   END     
END    
--END - CSR 5595 

  -- Update the user''s record.                                          
 UPDATE CSAA_USERS SET                                          
  USER_FNAME   = @FirstName,                                          
  USER_LNAME   = @LastName,                              
  USER_EMAIL  = @Email,                                          
  USER_PHONE  = @Phone,                                          
  USER_ACTIVE  = @Active,                                          
  USER_REPID  = @RepId,                                          
  USER_DO   = @DO,                                          
  DATE_UPDATED = getdate()                                          
 WHERE USER_NM = @UserId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Remove any roles that have been removed.                                           
 DELETE FROM CSAA_USERS_ROLES                                           
 WHERE ROLE_RID NOT IN (SELECT Role FROM #Tmp_Roles)                                          
 AND USER_RID=@UserRid AND APP_RID=@AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                           
 -- Add any new roles.                                          
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                          
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                          
 WHERE Role NOT IN (SELECT ROLE_RID FROM CSAA_USERS_ROLES                                          
  WHERE USER_RID=@UserRid AND APP_RID=@AppId)                                          
 IF @@ERROR<>0                
  BEGIN                                          
  ROLLBACK TRANSACTION                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
                                    
 -- Log the activity                                          
 SET @St = ''UPDATED USER '' + @UserId                                          
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId                                          
 IF @@ERROR<>0                                           
  BEGIN                                          
  ROLLBACK TRANSACTION                                    
  SET @St=''DROP TABLE #Tmp_Roles''                                          
  EXEC (@St)                                          
  RETURN                                          
  END                                          
 END                                          
                                          
DROP TABLE #Tmp_Roles


' 
END
GO
/****** Object:  StoredProcedure [dbo].[AUTH_VerifyToken]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_VerifyToken]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE PROCEDURE [dbo].[AUTH_VerifyToken]
/*
DESCRIPTION:
Checks to see if the token is valid, and updates the token''s time stamp if so.
PARAMETERS:
@CurrentUser USERNAME - User to check.
@Token VARCHAR(255) - The token to check, as std GUID format Hex string.
@Timeout DATETIME - The length of time in minutes that a token remains active after.
@AppName VARCHAR(50) - Current Application.
@Result INT - The result output 
	0 - OK
	1 - Timed out
	2 - Invalid token

CREATED:
9/9/2003 Jeff McEwen

MODIFICATION HISTORY:
9/20/2003 JOM changed GUID compare to string to fix bug.
*/
@CurrentUser USERNAME,
@Token VARCHAR(255),
@Timeout INT,
@Result INT=NULL OUTPUT


AS
SET NOCOUNT ON
DECLARE @Last_Check DATETIME
-- Find the last token check time.
SET @Last_Check = (SELECT TOKEN_CHECKED FROM CSAA_USERS 
	WHERE USER_NM = @CurrentUser AND CAST(TOKEN AS VARCHAR(255))=@Token)

IF @Last_Check IS NULL
	-- User & Token not found, invalid token.
	SET @Result = 2
ELSE
	IF DATEDIFF(mi, @Last_Check, GETDATE())<=@Timeout
		-- Its valid and not timed out.
		SET @Result = 0
	ELSE 
		-- Its valid, but timed out.
		SET @Result =1

IF @Result>0 
	UPDATE CSAA_USERS SET TOKEN=NULL, TOKEN_CHECKED=NULL WHERE USER_NM=@CurrentUser
ELSE
	UPDATE CSAA_USERS SET TOKEN_CHECKED=GETDATE() WHERE USER_NM=@CurrentUser







' 
END
GO
/****** Object:  Table [dbo].[CSAA_APPS]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_APPS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_APPS](
	[APP_RID] [dbo].[RECORDID] NOT NULL,
	[APP_NM] [dbo].[SHORTNAME] NOT NULL,
	[APP_DESC] [dbo].[LONGDESC] NOT NULL,
	[DATE_CREATED] [datetime] NOT NULL CONSTRAINT [DF__CSAA_APPS__DATE___7849DB76]  DEFAULT (getdate()),
	[DATE_UPDATED] [datetime] NOT NULL CONSTRAINT [DF__CSAA_APPS__DATE___793DFFAF]  DEFAULT (getdate()),
	[ENABLED] [bit] NULL CONSTRAINT [DF_CSAA_APPS_ENABLED]  DEFAULT (0),
 CONSTRAINT [PK_CSAA_APPS] PRIMARY KEY CLUSTERED 
(
	[APP_RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UC_APP_NM] UNIQUE NONCLUSTERED 
(
	[APP_NM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_APPS_ROLES]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_APPS_ROLES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_APPS_ROLES](
	[APP_RID] [dbo].[RECORDID] NOT NULL,
	[ROLE_RID] [dbo].[RECORDID] NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CSAA_AUTHENTICATION_LOG]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_AUTHENTICATION_LOG]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_AUTHENTICATION_LOG](
	[AUTH_RID] [dbo].[RECORDID] IDENTITY(1,1) NOT NULL,
	[USER_NM] [dbo].[USERNAME] NOT NULL,
	[APP_RID] [dbo].[RECORDID] NOT NULL,
	[AUTH_ACTIVITY] [dbo].[LONGDESC] NOT NULL,
	[AUTH_DATE] [datetime] NOT NULL CONSTRAINT [DF__CSAA_AUTH__AUTH___56B3DD81]  DEFAULT (getdate())
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_DO]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_DO]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_DO](
	[DO_Code] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL CONSTRAINT [DF_CSAA_DO_Enabled]  DEFAULT (1),
 CONSTRAINT [PK_CSAA_DO] PRIMARY KEY CLUSTERED 
(
	[DO_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_DO_ACTIVITY]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_DO_ACTIVITY]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_DO_ACTIVITY](
	[DO_RID] [int] IDENTITY(1,1) NOT NULL,
	[DO_Code] [varchar](3) NOT NULL,
	[HUB] [varchar](3) NULL,
	[DO_Activity] [varchar](256) NOT NULL,
	[User_NM] [varchar](50) NOT NULL,
	[DO_Last_Modified] [datetime] NOT NULL DEFAULT (getdate())
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_DO_Backup]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_DO_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_DO_Backup](
	[DO_Code] [varchar](3) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_DO_MAPPING]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_DO_MAPPING]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_DO_MAPPING](
	[DO] [varchar](3) NOT NULL,
	[HUB] [varchar](2) NOT NULL,
 CONSTRAINT [PK_CSAA_DO_MAPPING] PRIMARY KEY CLUSTERED 
(
	[DO] ASC,
	[HUB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_Managers]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_Managers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_Managers](
	[USER_NM] [varchar](8000) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_ROLES]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_ROLES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_ROLES](
	[ROLE_RID] [dbo].[RECORDID] IDENTITY(1,1) NOT NULL,
	[ROLE_NM] [dbo].[SHORTNAME] NOT NULL,
	[DATE_CREATED] [datetime] NOT NULL CONSTRAINT [DF__CSAA_ROLE__DATE___7C1A6C5A]  DEFAULT (getdate()),
	[DATE_UPDATED] [datetime] NOT NULL CONSTRAINT [DF__CSAA_ROLE__DATE___7D0E9093]  DEFAULT (getdate()),
 CONSTRAINT [PK_CSAA_ROLES] PRIMARY KEY CLUSTERED 
(
	[ROLE_RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_USERS]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_USERS]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_USERS](
	[USER_RID] [dbo].[RECORDID] IDENTITY(1,1) NOT NULL,
	[USER_NM] [dbo].[USERNAME] NOT NULL,
	[USER_FNAME] [dbo].[NAME] NOT NULL,
	[USER_LNAME] [dbo].[NAME] NOT NULL,
	[USER_EMAIL] [dbo].[LONGDESC] NULL,
	[USER_PHONE] [dbo].[PHONE] NULL,
	[USER_ACTIVE] [dbo].[BOOLEAN] NOT NULL,
	[DATE_CREATED] [datetime] NOT NULL CONSTRAINT [DF_CSAA_USERS_DATE_CREATED]  DEFAULT (getdate()),
	[DATE_UPDATED] [datetime] NOT NULL CONSTRAINT [DF_CSAA_USERS_DATE_UPDATED]  DEFAULT (getdate()),
	[DATE_LAST_LOGIN] [datetime] NOT NULL CONSTRAINT [DF_CSAA_USERS_DATE_LAST_LOGIN]  DEFAULT (getdate()),
	[USER_REPID] [bigint] NULL CONSTRAINT [DF_CSAA_USERS_USER_REPID_1]  DEFAULT ((0)),
	[USER_DO] [varchar](10) NOT NULL CONSTRAINT [DF_CSAA_USERS_USER_DO_1]  DEFAULT (98),
	[TOKEN] [uniqueidentifier] NULL,
	[TOKEN_CHECKED] [datetime] NULL,
	[AgencyID] [int] NULL,
	[AgencyName] [varchar](100) NULL,
 CONSTRAINT [PK_CSAA_USERS_1] PRIMARY KEY CLUSTERED 
(
	[USER_NM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_USERS_BeforupdsMay17]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_USERS_BeforupdsMay17]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_USERS_BeforupdsMay17](
	[USER_RID] [dbo].[RECORDID] IDENTITY(1,1) NOT NULL,
	[USER_NM] [dbo].[USERNAME] NOT NULL,
	[USER_FNAME] [dbo].[NAME] NOT NULL,
	[USER_LNAME] [dbo].[NAME] NOT NULL,
	[USER_PASSWORD] [dbo].[PASSWORD] NOT NULL,
	[USER_EMAIL] [dbo].[LONGDESC] NULL,
	[USER_PHONE] [dbo].[PHONE] NULL,
	[USER_ACTIVE] [dbo].[BOOLEAN] NOT NULL,
	[USER_LOCKED_OUT] [dbo].[BOOLEAN] NOT NULL,
	[DATE_CREATED] [datetime] NOT NULL,
	[DATE_UPDATED] [datetime] NOT NULL,
	[DATE_PWD_UPDATED] [datetime] NOT NULL,
	[DATE_LAST_LOGIN] [datetime] NOT NULL,
	[USER_REPID] [int] NOT NULL,
	[USER_DO] [varchar](3) NOT NULL,
	[TOKEN] [uniqueidentifier] NULL,
	[TOKEN_CHECKED] [datetime] NULL,
	[LOCKOUT_COUNTER] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_USERS_LIMIT]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_USERS_LIMIT]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_USERS_LIMIT](
	[APP_NM] [varchar](50) NULL,
	[MAX_LIMIT] [int] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CSAA_USERS_ROLES]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CSAA_USERS_ROLES]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CSAA_USERS_ROLES](
	[USER_RID] [dbo].[RECORDID] NOT NULL,
	[APP_RID] [dbo].[RECORDID] NOT NULL,
	[ROLE_RID] [dbo].[RECORDID] NOT NULL,
 CONSTRAINT [PK_CSAA_USERS_ROLES] PRIMARY KEY CLUSTERED 
(
	[USER_RID] ASC,
	[APP_RID] ASC,
	[ROLE_RID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MP2Users]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MP2Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MP2Users](
	[S No] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[USER_NM] [varchar](50) NULL,
	[SALESX ID] [varchar](50) NULL,
	[PaymentToolAccess] [varchar](50) NULL,
	[SalesXAccess] [varchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MP2Users1]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MP2Users1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MP2Users1](
	[S No] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[USER_NM] [varchar](50) NULL,
	[PaymentToolAccess] [varchar](50) NULL,
	[SalesXAccess] [varchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PTUsers_May17]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PTUsers_May17]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PTUsers_May17](
	[USER_NM] [varchar](50) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[USR_ROLE]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USR_ROLE]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[USR_ROLE](
	[USER_RID] [dbo].[RECORDID] NOT NULL,
	[ROLE] [varchar](200) NULL,
	[RoleIDs] [varchar](100) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[AUTH_User_Roles]    Script Date: 8/5/2015 9:52:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[AUTH_User_Roles]'))
EXEC dbo.sp_executesql @statement = N'




CREATE VIEW [dbo].[AUTH_User_Roles]
AS
SELECT U.*, R.ROLE_NM FROM CSAA_USERS_ROLES AS U 
INNER JOIN CSAA_ROLES AS R ON R.ROLE_RID=U.ROLE_RID





' 
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CSAA_APPS_ROLES_APPRID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CSAA_APPS_ROLES]'))
ALTER TABLE [dbo].[CSAA_APPS_ROLES]  WITH CHECK ADD  CONSTRAINT [FK_CSAA_APPS_ROLES_APPRID] FOREIGN KEY([APP_RID])
REFERENCES [dbo].[CSAA_APPS] ([APP_RID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CSAA_APPS_ROLES_APPRID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CSAA_APPS_ROLES]'))
ALTER TABLE [dbo].[CSAA_APPS_ROLES] CHECK CONSTRAINT [FK_CSAA_APPS_ROLES_APPRID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CSAA_APPS_ROLES_ROLERID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CSAA_APPS_ROLES]'))
ALTER TABLE [dbo].[CSAA_APPS_ROLES]  WITH NOCHECK ADD  CONSTRAINT [FK_CSAA_APPS_ROLES_ROLERID] FOREIGN KEY([ROLE_RID])
REFERENCES [dbo].[CSAA_ROLES] ([ROLE_RID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CSAA_APPS_ROLES_ROLERID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CSAA_APPS_ROLES]'))
ALTER TABLE [dbo].[CSAA_APPS_ROLES] CHECK CONSTRAINT [FK_CSAA_APPS_ROLES_ROLERID]
GO
USE [master]
GO
ALTER DATABASE [Authentication] SET  READ_WRITE 
GO

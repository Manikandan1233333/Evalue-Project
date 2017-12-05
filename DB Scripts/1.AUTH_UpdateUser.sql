USE [Authentication]
GO

/****** Object:  StoredProcedure [dbo].[AUTH_UpdateUser]    Script Date: 5/18/2016 6:32:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER    PROCEDURE [dbo].[AUTH_UpdateUser]                                                
/*                                            
DESCRIPTION:                                            
This procedure will update a user's profile, or if the user doesn't exist, will                                            
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
  Included the code to ensure that the salesrep's DO cannot  
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
SET @Roles=REPLACE(REPLACE(@Roles,'[',''),']','')                                            
CREATE TABLE #Tmp_Roles (Role INT, Description VARCHAR(50))             
IF @Roles<>''                                            
 BEGIN                                            
 SET @St = 'INSERT INTO #Tmp_Roles SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES WHERE ROLE_RID IN (' + @Roles + ')'                                            
 EXEC (@St)                                            
 END                                            
                                            
SET @St = 'DROP TABLE #Tmp_Roles'                                            
                                            
IF @UserRid=0                                            
 BEGIN                                       IF EXISTS(SELECT USER_RID FROM CSAA_USERS WHERE USER_NM = @UserId)                                            
  BEGIN                    
  RAISERROR('%s  already exists.', 15, 1, @UserId)       
                                       
  EXEC (@St)                                    RETURN                                            
  END                                            
 -- Insert the user record                      
                                         
BEGIN TRANSACTION                                           
--START  Added By Cognizant for CSR 4593   to call AUTH_CheckForInsert ,to ensure that the  user count                                        
-- has not exceeded the maximum permissible limit for an application,while adding    a new user ,and log the activity      
                                                                          
DECLARE @AppRid INT                   
DECLARE @stmt varchar(50)                                         
SET @AppRid = (SELECT APP_RID FROM CSAA_APPS WHERE APP_NM=@AppName)                                      
SET @stmt ='User '+ @UserId+ ' Addition Failed For '+ @AppName +'Application'          
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
                                               
 -- Establish the user's roles.                                            
 INSERT INTO CSAA_USERS_ROLES (USER_RID, APP_RID, ROLE_RID)                                            
 SELECT @UserRid, @AppId, Role FROM #Tmp_Roles                                            
                                            
 IF @@ERROR<>0                   
  BEGIN                                            
  ROLLBACK TRANSACTION                                            
  EXEC (@St)                                            
  RETURN                                            
  END                                            
                                  
 -- Log the activity                                            
 SET @St = 'CREATED USER ' + @UserId                                            
 -- Replace the sp_LogActivity with AUTH_LogActivity                                            
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St                                            
 IF @@ERROR<>0                                             
  BEGIN                                            
  ROLLBACK TRANSACTION                                            
  SET @St='DROP TABLE #Tmp_Roles'                                            
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
   AND ROLE_NM IN ('Administrator', 'SU')                                            
  )                                            
   BEGIN                                         
   RAISERROR('User may not remove him/herself from an administrative role.', 16, 1)                                         
   EXEC (@St)                                            
   RETURN                                            
   END                                            
                                               
 ELSE                                         
  -- A user cannot be removed from the SU role by someone who is not an SU.                                            
  IF EXISTS (                                            
   SELECT * FROM AUTH_User_Roles                                             
   LEFT JOIN #Tmp_Roles ON ROLE_RID=Role                                            
   WHERE USER_RID=@UserRid AND APP_RID=@AppId AND Role IS NULL                                            
   AND ROLE_NM IN ('SU')                                            
  ) AND NOT EXISTS (                                            
   SELECT * FROM AUTH_User_Roles AS R                                             
   INNER JOIN CSAA_USERS AS U ON R.USER_RID=U.USER_RID                                            
   WHERE USER_NM=@CurrentUser AND APP_RID=@AppId                                            
   AND ROLE_NM IN ('SU')                                            
  )                                            
  INSERT INTO #Tmp_Roles (Role, Description)                                           
  SELECT ROLE_RID, ROLE_NM FROM CSAA_ROLES                                             
  WHERE ROLE_NM='SU'                 
                
--START  Added By Cognizant for CSR 4593  to invoke Auth_CheckForUpdate,when activating an user       
-- and to log the exception       
SET @stmt ='User '+ @UserId+ ' Update Failed For '+ @AppName +'Application'                            
exec Auth_CheckForUpdate @UserRid,@Active,@AppId       
-- Log the activity                
IF @@ERROR<>0                       
BEGIN    
EXEC AUTH_LOGACTIVITY @AppName,@CurrentUser,@stmt                     
RETURN                                  
END                 
--END - CSR 4593  
  
-- START Added By Cognizant for CSR 5595 to ensure that the salesrep's DO cannot  
-- be updated if he has active transactions     

-- CHGXXXXXXX - added Receipt number not null check as part of upgrade May 2016.
IF (@DO != @CurrentDO)      
BEGIN      
 IF EXISTS(SELECT USER_ID FROM Payments.dbo.PAY_payment         
                  WHERE USER_ID = @UserId and Status_Id in (2,3,6)  and Receipt_Number is not null 
)      
       
  BEGIN      
 RAISERROR('User still has open transactions in the associated District office. Please clear the queue for the District office changes .', 11,1)                                                 
   RETURN                                                 
   END       
END      
--END - CSR 5595   
  
  -- Update the user's record.                                            
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
 SET @St = 'UPDATED USER ' + @UserId                                            
 EXEC AUTH_LogActivity @AppName, @CurrentUser, @St, @AppId                                            
 IF @@ERROR<>0                                             
  BEGIN                                            
  ROLLBACK TRANSACTION                                      
  SET @St='DROP TABLE #Tmp_Roles'                                            
  EXEC (@St)                                            
  RETURN                                            
  END                                            
 END                                            
                                            
DROP TABLE #Tmp_Roles  
  
--RBAC-PT.Ch1: START - Insert/Update USR_ROLE table   
Declare @UserRoles varchar(200), @UserRoleIDs varchar(100)  
  
Select @UserRoles = COALESCE(@UserRoles + ',', '') + [Description]   
FROM  
(  
SELECT  DISTINCT U.USER_RID AS UserRid, R.ROLE_NM AS [Description]   
FROM CSAA_USERS U  
INNER JOIN CSAA_USERS_ROLES UR ON UR.USER_RID = U.USER_RID  
INNER JOIN CSAA_ROLES R ON R.ROLE_RID = UR.ROLE_RID  
WHERE U.USER_RID = @UserRid  
) as USERROLES  
  
Select @UserRoleIDs = COALESCE(@UserRoleIDs + ',', '') + CONVERT(VARCHAR(2),[Role_ID])   
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

GO



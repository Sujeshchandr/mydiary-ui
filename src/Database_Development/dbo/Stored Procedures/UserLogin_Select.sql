-- =============================================
-- Author:		Sujesh.K.C
-- Create date: 2015-06-27
-- Description:	Get user login info
-- EXEC UserLogin_Select 'sujeshchandr@gmail.com','sujesh'
-- =============================================

CREATE PROCEDURE [dbo].[UserLogin_Select] 
	@EmailId NVARCHAR(50),
	@Password NVARCHAR(50)
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT DRY_User_Login.UserId,
	       FirstName,
		   MiddleName,
		   LastName,
		   ImageId ,
		   DRY_User.EmailId,
		   DRY_User.RoleId,
		   DRY_Roles.RoleCode AS RoleName
	FROM DRY_User_Login WITH (NOLOCK)
	     INNER JOIN DRY_User WITH (NOLOCK) ON DRY_User.UserId = DRY_User_Login.UserId 
		 INNER JOIN DRY_Roles WITH (NOLOCK) ON DRY_User.RoleId = DRY_Roles.RoleId
	WHERE DRY_User_Login.EmailId =@EmailId
	      AND Password= @Password
		  AND DRY_User.IsDeleted =0


END

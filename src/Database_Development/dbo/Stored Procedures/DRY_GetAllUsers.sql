-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <19-12-2015 20:47:30 >
-- Description:	<get all users>
-- EXEC DRY_GetAllUsers 
-- =============================================

CREATE PROCEDURE [dbo].[DRY_GetAllUsers] 
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT UserId,	
	       FirstName,	
		   MiddleName,	
		   LastName,	
		   EmailId,	
		   RoleId,	
		   ImageId,	
		   SiteId,
	       SiteUserId	
	FROM  DRY_User WITH (NOLOCK)

END
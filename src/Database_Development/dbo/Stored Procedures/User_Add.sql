-- =============================================
-- Author:		Sujesh.K.C
-- Create date: 2015-06-27
-- Description:	Add a new user entity
-- =============================================
CREATE PROCEDURE [dbo].[User_Add] 
	@FirstName NVARCHAR(50),
	@MiddleName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@EmailId NVARCHAR(50),
	@RoleId INT,
	@SiteId INT = 0,
	@SiteUserId INT = 0 ,
	@ImageId INT =0
AS
BEGIN
	
	SET NOCOUNT ON;

    INSERT INTO DRY_User(FirstName,MiddleName,LastName,EmailId,RoleId,SiteId,SiteUserId,ImageId,CreatedDate,IsDeleted)
	     VALUES         (@FirstName,@MiddleName,@LastName,@EmailId,@RoleId,@SiteId,@SiteUserId,@ImageId,getdate(),0)   

    SELECT  SCOPE_IDENTITY()

END

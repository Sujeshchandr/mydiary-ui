-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<Add a new user info entity>
-- =============================================
CREATE PROCEDURE [dbo].[UserLogin_Add] 
	@UserId INT,
	@EmailId NVARCHAR(50),
	@Password NVARCHAR(50)

AS
BEGIN
	
	SET NOCOUNT ON;

    INSERT INTO DRY_User_Login(UserId,EmailId,Password,CreatedBy,CreatedDate)
	     VALUES              (@UserId,@EmailId,@Password,@UserId,GETDATE())

END

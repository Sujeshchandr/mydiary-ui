-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<Add a new  image entity>
-- =============================================

CREATE PROCEDURE [dbo].[Image_Add] 
	@Image image
AS
BEGIN
	
	SET NOCOUNT ON;

    INSERT INTO [dbo].[DRY_Images]([dbo].[DRY_Images].[Image])
	     VALUES           (@Image)

		 SELECT SCOPE_IDENTITY()

END


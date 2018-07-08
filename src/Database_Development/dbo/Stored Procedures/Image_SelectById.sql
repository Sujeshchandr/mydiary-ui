-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get a image by id>
-- EXEC Image_SelectById 2
-- =============================================

CREATE PROCEDURE [dbo].[Image_SelectById] 
	@ImageId int
AS
BEGIN
	
	SET NOCOUNT ON;

    SELECT ImageId,
	       Image
	FROM  DRY_Images WITH (NOLOCK)
	WHERE ImageId = @ImageId

END


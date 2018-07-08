GO

/****** Object:  StoredProcedure [dbo].[Image_SelectById]    Script Date: 29-08-2015 18:02:39 ******/
DROP PROCEDURE [dbo].[Image_SelectById]
GO

/****** Object:  StoredProcedure [dbo].[Image_SelectById]    Script Date: 29-08-2015 18:02:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


GO



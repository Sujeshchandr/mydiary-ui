GO

/****** Object:  StoredProcedure [dbo].[Image_Add]    Script Date: 29-08-2015 18:02:06 ******/
DROP PROCEDURE [dbo].[Image_Add]
GO

/****** Object:  StoredProcedure [dbo].[Image_Add]    Script Date: 29-08-2015 18:02:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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

    INSERT INTO DRY_Images(Image)
	     VALUES           (@Image)

		 SELECT SCOPE_IDENTITY()

END


GO



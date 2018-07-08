GO

/****** Object:  StoredProcedure [dbo].[IncomeType_Add]    Script Date: 29-08-2015 18:03:27 ******/
DROP PROCEDURE [dbo].[IncomeType_Add]
GO

/****** Object:  StoredProcedure [dbo].[IncomeType_Add]    Script Date: 29-08-2015 18:03:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all incometypes of the user>
-- EXEC IncomeTypes_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[IncomeType_Add] 
	@IncomeType NVARCHAR(MAX),
	@UserId INT

AS
BEGIN
	
	INSERT INTO DRY_IncomeTypes(IncomeType,UserId,CreatedBy,CreatedDate,IsDeleted)
	VALUES (@IncomeType,@UserId,@UserId,GETDATE(),0)

	SELECT SCOPE_IDENTITY()

END


GO



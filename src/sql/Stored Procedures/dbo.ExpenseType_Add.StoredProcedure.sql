GO

/****** Object:  StoredProcedure [dbo].[ExpenseType_Add]    Script Date: 29-08-2015 18:00:55 ******/
DROP PROCEDURE [dbo].[ExpenseType_Add]
GO

/****** Object:  StoredProcedure [dbo].[ExpenseType_Add]    Script Date: 29-08-2015 18:00:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<add an expensetype entity>
-- =============================================

CREATE PROCEDURE [dbo].[ExpenseType_Add] 
	@ExpenseType NVARCHAR(MAX),
	@UserId INT

AS
BEGIN
	
	INSERT INTO DRY_ExpenseTypes(ExpenseType,UserId,CreatedBy,CreatedDate,IsDeleted)
	VALUES (@ExpenseType,@UserId,@UserId,GETDATE(),0)

	SELECT SCOPE_IDENTITY()

END


GO



GO

/****** Object:  StoredProcedure [dbo].[ExpenseTypes_SelectByUserId]    Script Date: 29-08-2015 18:01:37 ******/
DROP PROCEDURE [dbo].[ExpenseTypes_SelectByUserId]
GO

/****** Object:  StoredProcedure [dbo].[ExpenseTypes_SelectByUserId]    Script Date: 29-08-2015 18:01:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all incometypes of the user>
-- EXEC ExpenseTypes_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[ExpenseTypes_SelectByUserId] 
	@UserId int
AS
BEGIN
	
	SET NOCOUNT OFF;
SELECT ExpenseTypeId,
       ExpenseType
FROM DRY_ExpenseTypes
WHERE  UserId = @UserId
      AND  IsDeleted =0

END


GO



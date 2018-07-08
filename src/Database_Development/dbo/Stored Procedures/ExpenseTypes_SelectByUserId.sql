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



-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all expenses of the user>
-- EXEC Expenses_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[Expenses_SelectByUserId] 
	@UserId int
AS
BEGIN
	
	SET NOCOUNT ON;

SELECT DRY_Expenses.ExpenseId,  
       DRY_Expenses.ExpenseDate,
	   DRY_Expenses.Description,
	   DRY_Expenses.Comments,
	   DRY_Expenses.Amount,
	   DRY_Expenses.UserId,
       DRY_ExpenseTypes.ExpenseType,
       DRY_ExpenseTypes.ExpenseTypeId,
	   DRY_Expenses.CreatedBy,
	   DRY_Expenses.CreatedDate,
	   DRY_Expenses.ModifiedBy,
	   DRY_Expenses.ModifiedDate


FROM DRY_Expenses WITH(NOLOCK)
     INNER JOIN DRY_ExpenseTypes WITH(NOLOCK) ON  DRY_Expenses.ExpenseTypeId = DRY_ExpenseTypes.ExpenseTypeId
WHERE DRY_Expenses.UserId =@UserId
      AND DRY_Expenses.IsDeleted =0

END
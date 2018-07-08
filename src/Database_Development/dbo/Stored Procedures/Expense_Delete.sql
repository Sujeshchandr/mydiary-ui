-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <05-07-2015>
-- Description:	<delte an expesne entity of the user by expense id>
-- =============================================

CREATE PROCEDURE [dbo].[Expense_Delete] 
    @ExpenseId int	
AS
BEGIN
	
	SET NOCOUNT OFF;

   DELETE DRY_Expenses
  
  WHERE ExpenseId = @ExpenseId

END


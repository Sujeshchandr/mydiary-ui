-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <05-07-2015>
-- Description:	<update an expesne entity of the user>
-- =============================================

CREATE PROCEDURE [dbo].[Expense_Update] 
    @ExpenseId int,
	@UserId int,
	@ExpenseTypeId int,
	@Amount float,
	@ExpenseDate datetime,
	@Description nvarchar(max),
	@Comments nvarchar(max) = '',
	@ModifiedBy int
AS
BEGIN
	
	SET NOCOUNT OFF;

   UPDATE DRY_Expenses
   SET ExpenseTypeId = @ExpenseTypeId,
       Amount = @Amount,
	   ExpenseDate = @ExpenseDate,
	   Description = @Description,
	   Comments= @Comments,
	   ModifiedBy = @ModifiedBy,
	   ModifiedDate =GETDATE()
  WHERE ExpenseId = @ExpenseId
        AND UserId = @UserId
        
      


END


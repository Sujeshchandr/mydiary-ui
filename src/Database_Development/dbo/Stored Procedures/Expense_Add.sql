-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <05-07-2015>
-- Description:	<add an expesne entity>
-- =============================================

CREATE PROCEDURE [dbo].[Expense_Add] 
	@UserId int,
	@ExpenseTypeId int,
	@Amount float,
	@ExpenseDate datetime,
	@Description nvarchar(max),
	@Comments nvarchar(max) = ''

AS
BEGIN
	
	SET NOCOUNT OFF;

    INSERT INTO DRY_Expenses(UserId,ExpenseTypeId,Amount,ExpenseDate,Description,Comments,CreatedBy,CreatedDate,IsDeleted)
	     VALUES (@UserId,@ExpenseTypeId,@Amount,@ExpenseDate,@Description,@Comments,@UserId,getdate(),0)

    SELECT SCOPE_IDENTITY();

END


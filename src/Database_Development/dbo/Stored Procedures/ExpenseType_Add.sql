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


GO

/****** Object:  StoredProcedure [dbo].[Expense_Add]    Script Date: 29-08-2015 17:59:31 ******/
DROP PROCEDURE [dbo].[Expense_Add]
GO

/****** Object:  StoredProcedure [dbo].[Expense_Add]    Script Date: 29-08-2015 17:59:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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


GO



USE [MyDiary_Development]
GO
/****** Object:  StoredProcedure [dbo].[Expense_Update]    Script Date: 15-11-2015 21:24:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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


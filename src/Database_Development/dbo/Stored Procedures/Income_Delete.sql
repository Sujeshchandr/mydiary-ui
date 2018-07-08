-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <05-07-2015>
-- Description:	<delete an income entity of the user by income id>
-- =============================================

CREATE PROCEDURE [dbo].[Income_Delete] 
    @IncomeId int	
AS
BEGIN
	
	SET NOCOUNT OFF;

   DELETE DRY_Incomes
  
  WHERE IncomeId = @IncomeId

END
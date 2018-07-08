-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all incomes of the user>
-- EXEC Incomes_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[Incomes_SelectByUserId] 
	@UserId int
AS
BEGIN
	
	SET NOCOUNT ON;

SELECT *
FROM DRY_Incomes WITH(NOLOCK)
     INNER JOIN DRY_IncomeTypes WITH(NOLOCK) ON  DRY_Incomes.IncomeTypeId = DRY_IncomeTypes.IncomeTypeId
WHERE DRY_Incomes.UserId =@UserId
      AND DRY_Incomes.IsDeleted =0

END
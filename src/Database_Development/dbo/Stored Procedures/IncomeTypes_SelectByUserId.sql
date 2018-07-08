-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all incometypes of the user>
-- EXEC IncomeTypes_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[IncomeTypes_SelectByUserId] 
	@UserId int
AS
BEGIN
	
	SET NOCOUNT ON;
SELECT IncomeTypeId,
       IncomeType
FROM DRY_IncomeTypes
WHERE UserId =@UserId
      AND IsDeleted =0
END


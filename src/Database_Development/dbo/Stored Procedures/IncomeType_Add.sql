-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <2015-06-27 10:58:24.063>
-- Description:	<get all incometypes of the user>
-- EXEC IncomeTypes_SelectByUserId 6
-- =============================================

CREATE PROCEDURE [dbo].[IncomeType_Add] 
	@IncomeType NVARCHAR(MAX),
	@UserId INT

AS
BEGIN
	
	INSERT INTO DRY_IncomeTypes(IncomeType,UserId,CreatedBy,CreatedDate,IsDeleted)
	VALUES (@IncomeType,@UserId,@UserId,GETDATE(),0)

	SELECT SCOPE_IDENTITY()

END


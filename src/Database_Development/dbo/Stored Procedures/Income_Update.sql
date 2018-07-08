-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <08-12-2015>
-- Description:	<update an income entity of the user>
-- =============================================

CREATE PROCEDURE [dbo].[Income_Update] 
    @IncomeId int,
	@UserId int,
	@IncomeTypeId int,
	@Amount float,
	@IncomeDate datetime,
	@Description nvarchar(max),
	@Comments nvarchar(max) = '',
	@ModifiedBy int
AS
BEGIN
	
	SET NOCOUNT OFF;

   UPDATE DRY_Incomes
   SET IncomeTypeId = @IncomeTypeId,
       Amount = @Amount,
	   IncomeDate = @IncomeDate,
	   Description = @Description,
	   Comments= @Comments,
	   ModifiedBy = @ModifiedBy,
	   ModifiedDate =GETDATE()
  WHERE IncomeId = @IncomeId
        AND UserId = @UserId
        
      


END
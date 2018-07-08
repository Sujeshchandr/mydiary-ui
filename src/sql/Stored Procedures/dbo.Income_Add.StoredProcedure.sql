GO

/****** Object:  StoredProcedure [dbo].[Income_Add]    Script Date: 29-08-2015 18:03:04 ******/
DROP PROCEDURE [dbo].[Income_Add]
GO

/****** Object:  StoredProcedure [dbo].[Income_Add]    Script Date: 29-08-2015 18:03:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Sujesh.K.C>
-- Create date: <05-07-2015>
-- Description:	<add an income entity>
-- =============================================

CREATE PROCEDURE [dbo].[Income_Add] 
	@UserId int,
	@IncomeTypeId int,
	@Amount float,
	@IncomeDate datetime,
	@Description nvarchar(max),
	@Comments nvarchar(max) = ''

AS
BEGIN
	
	SET NOCOUNT OFF;

    INSERT INTO DRY_Incomes(UserId,IncomeTypeId,Amount,IncomeDate,Description,Comments,CreatedBy,CreatedDate,IsDeleted)
	     VALUES (@UserId,@IncomeTypeId,@Amount,@IncomeDate,@Description,@Comments,@UserId,getdate(),0)

    SELECT SCOPE_IDENTITY();

END


GO



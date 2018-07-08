CREATE TABLE [dbo].[DRY_ExpenseTypes] (
    [ExpenseTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [ExpenseType]   NVARCHAR (MAX) NOT NULL,
    [UserId]        INT            NOT NULL,
    [CreatedBy]     INT            NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [ModifiedBy]    INT            NULL,
    [ModifiedDate]  DATETIME       NULL,
    [IsDeleted]     BIT            NOT NULL
);


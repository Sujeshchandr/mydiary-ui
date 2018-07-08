CREATE TABLE [dbo].[DRY_Incomes] (
    [IncomeId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId]       INT            NOT NULL,
    [IncomeTypeId] INT            NOT NULL,
    [Amount]       FLOAT (53)     NOT NULL,
    [IncomeDate]   DATETIME       NOT NULL,
    [Description]  NVARCHAR (MAX) NOT NULL,
    [Comments]     NVARCHAR (MAX) NULL,
    [CreatedBy]    INT            NOT NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [ModifiedBy]   INT            NULL,
    [ModifiedDate] DATETIME       NULL,
    [IsDeleted]    BIT            NULL,
    CONSTRAINT [PK_DRY_Incomes] PRIMARY KEY CLUSTERED ([IncomeId] ASC)
);


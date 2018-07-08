CREATE TABLE [dbo].[DRY_IncomeTypes] (
    [IncomeTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [IncomeType]   NVARCHAR (MAX) NOT NULL,
    [UserId]       INT            NOT NULL,
    [CreatedBy]    INT            NOT NULL,
    [CreatedDate]  DATETIME       NOT NULL,
    [ModifiedBy]   INT            NULL,
    [ModifiedDate] DATETIME       NULL,
    [IsDeleted]    BIT            NULL,
    CONSTRAINT [PK_DRY_IncomeTypes] PRIMARY KEY CLUSTERED ([IncomeTypeId] ASC),
    CONSTRAINT [FK_DRY_IncomeTypes_DRY_IncomeTypes] FOREIGN KEY ([IncomeTypeId]) REFERENCES [dbo].[DRY_IncomeTypes] ([IncomeTypeId])
);


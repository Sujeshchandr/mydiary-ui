CREATE TABLE [dbo].[DRY_Expenses] (
    [ExpenseId]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId]        INT            NOT NULL,
    [ExpenseTypeId] INT            NOT NULL,
    [Amount]        FLOAT (53)     NOT NULL,
    [ExpenseDate]   DATETIME       NOT NULL,
    [Description]   NVARCHAR (MAX) NOT NULL,
    [Comments]      NVARCHAR (MAX) NULL,
    [CreatedBy]     INT            NOT NULL,
    [CreatedDate]   DATETIME       NOT NULL,
    [ModifiedBy]    INT            NULL,
    [ModifiedDate]  DATETIME       NULL,
    [IsDeleted]     BIT            NOT NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [NonClusteredIndex-20150711-223552]
    ON [dbo].[DRY_Expenses]([ExpenseId] ASC, [UserId] ASC);


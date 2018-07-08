CREATE TABLE [dbo].[DRY_Roles] (
    [RoleId]          INT            IDENTITY (1, 1) NOT NULL,
    [RoleCode]        NVARCHAR (10)  NOT NULL,
    [RoleDescription] NVARCHAR (MAX) NOT NULL,
    [CreatedBy]       INT            NOT NULL,
    [CreatedDate]     DATETIME       NOT NULL,
    [ModifiedBy]      INT            NULL,
    [ModifiedDate]    DATETIME       NULL,
    [IsDeleted]       BIT            NOT NULL,
    CONSTRAINT [PK_DRY_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


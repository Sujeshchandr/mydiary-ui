CREATE TABLE [dbo].[DRY_User_Login] (
    [UserId]       INT           NOT NULL,
    [EmailId]      NVARCHAR (50) NOT NULL,
    [Password]     NVARCHAR (50) NOT NULL,
    [CreatedBy]    INT           NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [ModifiedBy]   INT           NULL,
    [ModifiedDate] DATETIME      NULL,
    CONSTRAINT [PK_DRY_User_Login] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


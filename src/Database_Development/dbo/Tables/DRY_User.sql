CREATE TABLE [dbo].[DRY_User] (
    [UserId]       INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50) NOT NULL,
    [MiddleName]   NVARCHAR (50) NOT NULL,
    [LastName]     NVARCHAR (50) NOT NULL,
    [EmailId]      NVARCHAR (50) NOT NULL,
    [RoleId]       INT           NOT NULL,
    [ImageId]      INT           NULL,
    [SiteId]       INT           NULL,
    [SiteUserId]   INT           NOT NULL,
    [CreatedBy]    INT           NULL,
    [CreatedDate]  DATETIME      NULL,
    [ModifiedBy]   INT           NULL,
    [ModifiedDate] DATETIME      NULL,
    [IsDeleted]    BIT           NULL,
    CONSTRAINT [PK_DRY_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


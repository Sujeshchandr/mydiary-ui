GO

/****** Object:  Table [dbo].[DRY_Expenses]    Script Date: 29-08-2015 17:49:16 ******/
DROP TABLE [dbo].[DRY_Expenses]
GO

/****** Object:  Table [dbo].[DRY_Expenses]    Script Date: 29-08-2015 17:49:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_Expenses](
	[ExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ExpenseTypeId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[ExpenseDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DRY_ExpenseTypes]    Script Date: 29-08-2015 17:50:22 ******/
DROP TABLE [dbo].[DRY_ExpenseTypes]
GO

/****** Object:  Table [dbo].[DRY_ExpenseTypes]    Script Date: 29-08-2015 17:50:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_ExpenseTypes](
	[ExpenseTypeId] [int] IDENTITY(1,1) NOT NULL,
	[ExpenseType] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DRY_Images]    Script Date: 29-08-2015 17:50:51 ******/
DROP TABLE [dbo].[DRY_Images]
GO

/****** Object:  Table [dbo].[DRY_Images]    Script Date: 29-08-2015 17:50:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_Images](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DRY_Incomes]    Script Date: 29-08-2015 17:54:28 ******/
DROP TABLE [dbo].[DRY_Incomes]
GO

/****** Object:  Table [dbo].[DRY_Incomes]    Script Date: 29-08-2015 17:54:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_Incomes](
	[IncomeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[IncomeTypeId] [int] NOT NULL,
	[Amount] [float] NOT NULL,
	[IncomeDate] [datetime] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DRY_Incomes] PRIMARY KEY CLUSTERED 
(
	[IncomeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[DRY_IncomeTypes] DROP CONSTRAINT [FK_DRY_IncomeTypes_DRY_IncomeTypes]
GO

/****** Object:  Table [dbo].[DRY_IncomeTypes]    Script Date: 29-08-2015 17:54:53 ******/
DROP TABLE [dbo].[DRY_IncomeTypes]
GO

/****** Object:  Table [dbo].[DRY_IncomeTypes]    Script Date: 29-08-2015 17:54:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_IncomeTypes](
	[IncomeTypeId] [int] IDENTITY(1,1) NOT NULL,
	[IncomeType] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DRY_IncomeTypes] PRIMARY KEY CLUSTERED 
(
	[IncomeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[DRY_IncomeTypes]  WITH CHECK ADD  CONSTRAINT [FK_DRY_IncomeTypes_DRY_IncomeTypes] FOREIGN KEY([IncomeTypeId])
REFERENCES [dbo].[DRY_IncomeTypes] ([IncomeTypeId])
GO

ALTER TABLE [dbo].[DRY_IncomeTypes] CHECK CONSTRAINT [FK_DRY_IncomeTypes_DRY_IncomeTypes]
GO

/****** Object:  Table [dbo].[DRY_OpenSites]    Script Date: 29-08-2015 17:55:41 ******/
DROP TABLE [dbo].[DRY_OpenSites]
GO

/****** Object:  Table [dbo].[DRY_OpenSites]    Script Date: 29-08-2015 17:55:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_OpenSites](
	[OpenSiteId] [int] IDENTITY(1,1) NOT NULL,
	[SiteName] [nvarchar](50) NOT NULL
) ON [PRIMARY]


GO

/****** Object:  Table [dbo].[DRY_Roles]    Script Date: 29-08-2015 17:56:06 ******/
DROP TABLE [dbo].[DRY_Roles]
GO

/****** Object:  Table [dbo].[DRY_Roles]    Script Date: 29-08-2015 17:56:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleCode] [nvarchar](10) NOT NULL,
	[RoleDescription] [nvarchar](max) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DRY_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DRY_User]    Script Date: 29-08-2015 17:56:44 ******/
DROP TABLE [dbo].[DRY_User]
GO

/****** Object:  Table [dbo].[DRY_User]    Script Date: 29-08-2015 17:56:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[ImageId] [int] NULL,
	[SiteId] [int] NULL,
	[SiteUserId] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DRY_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


GO

/****** Object:  Table [dbo].[DRY_User_Login]    Script Date: 29-08-2015 17:57:11 ******/
DROP TABLE [dbo].[DRY_User_Login]
GO

/****** Object:  Table [dbo].[DRY_User_Login]    Script Date: 29-08-2015 17:57:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DRY_User_Login](
	[UserId] [int] NOT NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_DRY_User_Login] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO










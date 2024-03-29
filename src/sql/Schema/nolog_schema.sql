/****** Object:  Table [dbo].[system_logging]    Script Date: 04/21/2010 17:05:06 ******/ 
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO

SET ANSI_PADDING ON 
GO

--CREATE TABLE [dbo].[system_logging]( 
--    [system_logging_guid] [uniqueidentifier] ROWGUIDCOL  NOT NULL, 
--    [entered_date] [datetime] NULL, 
--    [log_application] [varchar](200) NULL, 
--    [log_date] [varchar](100) NULL, 
--    [log_level] [varchar](100) NULL, 
--    [log_logger] [varchar](8000) NULL, 
--    [log_message] [varchar](8000) NULL, 
--    [log_machine_name] [varchar](8000) NULL, 
--    [log_user_name] [varchar](8000) NULL, 
--    [log_call_site] [varchar](8000) NULL, 
--    [log_thread] [varchar](100) NULL, 
--    [log_exception] [varchar](8000) NULL, 
--    [log_stacktrace] [varchar](8000) NULL, 
--CONSTRAINT [PK_system_logging] PRIMARY KEY CLUSTERED 
--( 
--    [system_logging_guid] ASC 
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] 
--) ON [PRIMARY]

GO

SET ANSI_PADDING OFF 
GO

--ALTER TABLE [dbo].[system_logging] ADD  CONSTRAINT [DF_system_logging_system_logging_guid]  DEFAULT (newid()) FOR [system_logging_guid] 
GO

--ALTER TABLE [dbo].[system_logging] ADD  CONSTRAINT [DF_system_logging_entered_date]  DEFAULT (getdate()) FOR [entered_date] 
GO



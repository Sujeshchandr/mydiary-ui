CREATE TABLE [dbo].[system_logging] (
    [system_logging_guid] UNIQUEIDENTIFIER CONSTRAINT [DF_system_logging_system_logging_guid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [entered_date]        DATETIME         CONSTRAINT [DF_system_logging_entered_date] DEFAULT (getdate()) NULL,
    [log_application]     VARCHAR (200)    NULL,
    [log_date]            VARCHAR (100)    NULL,
    [log_level]           VARCHAR (100)    NULL,
    [log_logger]          VARCHAR (8000)   NULL,
    [log_message]         VARCHAR (8000)   NULL,
    [log_machine_name]    VARCHAR (8000)   NULL,
    [log_user_name]       VARCHAR (8000)   NULL,
    [log_call_site]       VARCHAR (8000)   NULL,
    [log_thread]          VARCHAR (100)    NULL,
    [log_exception]       VARCHAR (8000)   NULL,
    [log_stacktrace]      VARCHAR (8000)   NULL,
    CONSTRAINT [PK_system_logging] PRIMARY KEY CLUSTERED ([system_logging_guid] ASC)
);


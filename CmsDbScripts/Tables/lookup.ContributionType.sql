CREATE TABLE [lookup].[ContributionType]
(
[Id] [int] NOT NULL,
[Code] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Description] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [lookup].[ContributionType] ADD CONSTRAINT [PK_ContributionType] PRIMARY KEY CLUSTERED  ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrgSchedule] ADD CONSTRAINT [PK_OrgSchedule] PRIMARY KEY CLUSTERED  ([OrganizationId], [Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

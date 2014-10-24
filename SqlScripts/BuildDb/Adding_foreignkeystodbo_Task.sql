ALTER TABLE [dbo].[Task] WITH NOCHECK  ADD CONSTRAINT [CoTasks__CoTaskList] FOREIGN KEY ([CoListId]) REFERENCES [dbo].[TaskList] ([Id])
ALTER TABLE [dbo].[Task] WITH NOCHECK  ADD CONSTRAINT [Tasks__TaskList] FOREIGN KEY ([ListId]) REFERENCES [dbo].[TaskList] ([Id])
ALTER TABLE [dbo].[Task] WITH NOCHECK  ADD CONSTRAINT [FK_Task_TaskStatus] FOREIGN KEY ([StatusId]) REFERENCES [lookup].[TaskStatus] ([Id])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO

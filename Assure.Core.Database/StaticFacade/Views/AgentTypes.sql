
CREATE VIEW [StaticFacade].[AgentTypes]
WITH SCHEMABINDING
AS
	SELECT AgentTypeId
			,[Name]
		FROM StaticModel.AgentTypes
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [StaticFacade].[TR_AgentTypes_IOD]
   ON [StaticFacade].[AgentTypes]
   INSTEAD OF DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM [StaticModel].[AgentTypes] 
		WHERE AgentTypeId IN (SELECT AgentTypeId FROM deleted)

END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [StaticFacade].[TR_AgentTypes_IOU]
   ON [StaticFacade].[AgentTypes]
   INSTEAD OF UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE [AgentTypes] 
			SET [Name] = inserted.[Name]
		FROM [StaticModel].[AgentTypes] 
		INNER JOIN inserted
				ON agentTypes.AgentTypeId = inserted.AgentTypeId

END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [StaticFacade].[TR_AgentTypes_IOI]
   ON [StaticFacade].[AgentTypes]
   INSTEAD OF INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO [StaticModel].[AgentTypes] ([Name])
		SELECT [Name]	
			FROM inserted

END
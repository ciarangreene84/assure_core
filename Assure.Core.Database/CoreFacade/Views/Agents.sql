CREATE VIEW [CoreFacade].[Agents]
WITH SCHEMABINDING
AS
	SELECT Agents.AgentId
			,AgentTypes.[Name] AS [Type]
			,Agents.[Name]
			,Agents.ObjectDocument
			,Agents.ObjectHash
		FROM CoreModel.Agents 
		INNER JOIN StaticModel.AgentTypes 
				ON Agents.AgentTypeId = AgentTypes.AgentTypeId
		WHERE AgentId IN 
		(
			SELECT AgentId 
				FROM SecurityModel.PrincipalAgents
				WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
		)
GO

CREATE TRIGGER [CoreFacade].[TR_Agents_IOD]
   ON [CoreFacade].[Agents]
   INSTEAD OF DELETE
AS 
BEGIN
	SET NOCOUNT ON;

	DELETE FROM CoreModel.Agents
		WHERE AgentId IN (SELECT AgentId FROM deleted)
END
GO

CREATE TRIGGER [CoreFacade].[TR_Agents_IOI]
   ON [CoreFacade].[Agents]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Agents (AgentTypeId, [Name], [ObjectDocument], ObjectHash)
		SELECT agentTypes.AgentTypeId
				,inserted.[Name]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
			INNER JOIN StaticModel.AgentTypes
					ON inserted.[Type] = agentTypes.[Name]
END
GO

CREATE TRIGGER [CoreFacade].[TR_Agents_IOU]
   ON [CoreFacade].[Agents]
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

	UPDATE agents
			SET [Name] = inserted.[Name]
			   ,ObjectDocument = inserted.ObjectDocument
			   ,ObjectHash = inserted.[ObjectHash]
		FROM CoreModel.Agents
		INNER JOIN inserted
				ON agents.AgentId = inserted.AgentId
END
GO


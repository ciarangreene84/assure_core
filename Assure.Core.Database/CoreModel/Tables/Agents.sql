CREATE TABLE [CoreModel].[Agents] (
    [AgentId]        INT           IDENTITY (1, 1) NOT NULL,
    [AgentTypeId]    INT           NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY CLUSTERED ([AgentId] ASC),
    CONSTRAINT [CK_Agents_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Agents_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [FK_Agents_AgentTypes] FOREIGN KEY ([AgentTypeId]) REFERENCES [StaticModel].[AgentTypes] ([AgentTypeId]),
    CONSTRAINT [IX_Agents_AgentId] UNIQUE NONCLUSTERED ([AgentId] ASC),
    CONSTRAINT [UQ_Agents_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);












GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Agents_AI]
   ON CoreModel.Agents
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalAgents (PrincipalId, AgentId)
		SELECT DATABASE_PRINCIPAL_ID()
				,AgentId
			FROM inserted

END
GO
CREATE NONCLUSTERED INDEX [IX_Agents_ObjectHash]
    ON [CoreModel].[Agents]([ObjectHash] ASC);


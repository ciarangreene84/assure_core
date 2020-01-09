CREATE TABLE [SecurityModel].[PrincipalAgents] (
    [PrincipalId] INT NOT NULL,
    [AgentId]     INT NOT NULL,
    CONSTRAINT [PK_PrincipalAgents] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [AgentId] ASC),
    CONSTRAINT [FK_PrincipalAgents_Agents] FOREIGN KEY ([AgentId]) REFERENCES [CoreModel].[Agents] ([AgentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalAgents_PrincipalAgents] FOREIGN KEY ([PrincipalId], [AgentId]) REFERENCES [SecurityModel].[PrincipalAgents] ([PrincipalId], [AgentId])
);




GO
CREATE NONCLUSTERED INDEX [IX_PrincipalAgents_PrincipalId]
    ON [SecurityModel].[PrincipalAgents]([PrincipalId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PrincipalAgents_AgentId]
    ON [SecurityModel].[PrincipalAgents]([AgentId] ASC);


CREATE TABLE [CoreModel].[AgentRelationships] (
    [AgentRelationshipId] INT IDENTITY (1, 1) NOT NULL,
    [ParentAgentId]       INT NOT NULL,
    [ChildAgentId]        INT NOT NULL,
    CONSTRAINT [PK_AgentRelationships] PRIMARY KEY CLUSTERED ([AgentRelationshipId] ASC),
    CONSTRAINT [FK_AgentRelationships_Agents_Child] FOREIGN KEY ([ChildAgentId]) REFERENCES [CoreModel].[Agents] ([AgentId]),
    CONSTRAINT [FK_AgentRelationships_Agents_Parent] FOREIGN KEY ([ParentAgentId]) REFERENCES [CoreModel].[Agents] ([AgentId])
);




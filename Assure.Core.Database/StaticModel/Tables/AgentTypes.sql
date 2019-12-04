CREATE TABLE [StaticModel].[AgentTypes] (
    [AgentTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_AgentTypes] PRIMARY KEY CLUSTERED ([AgentTypeId] ASC),
	CONSTRAINT [CK_AgentTypes_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [UQ_AgentTypes_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


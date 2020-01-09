CREATE TABLE [SecurityModel].[PrincipalLeads] (
    [PrincipalId] INT NOT NULL,
    [LeadId]      INT NOT NULL,
    CONSTRAINT [PK_PrincipalLeads] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [LeadId] ASC),
    CONSTRAINT [FK_PrincipalLeads_Leads] FOREIGN KEY ([LeadId]) REFERENCES [CoreModel].[Leads] ([LeadId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalLeads_PrincipalLeads] FOREIGN KEY ([PrincipalId], [LeadId]) REFERENCES [SecurityModel].[PrincipalLeads] ([PrincipalId], [LeadId])
);


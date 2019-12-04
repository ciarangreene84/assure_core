CREATE TABLE [SecurityModel].[PrincipalRequests] (
    [PrincipalId] INT NOT NULL,
    [RequestId]   INT NOT NULL,
    CONSTRAINT [PK_PrincipalRequests] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [RequestId] ASC),
    CONSTRAINT [FK_PrincipalRequests_Principals] FOREIGN KEY ([PrincipalId], [RequestId]) REFERENCES [SecurityModel].[PrincipalRequests] ([PrincipalId], [RequestId]),
    CONSTRAINT [FK_PrincipalRequests_Requests] FOREIGN KEY ([RequestId]) REFERENCES [CoreModel].[Requests] ([RequestId]) ON DELETE CASCADE
);


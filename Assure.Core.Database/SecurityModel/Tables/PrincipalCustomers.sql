CREATE TABLE [SecurityModel].[PrincipalCustomers] (
    [PrincipalId] INT NOT NULL,
    [CustomerId]  INT NOT NULL,
    CONSTRAINT [PK_PrincipalCustomers] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [CustomerId] ASC),
    CONSTRAINT [FK_PrincipalCustomers_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [CoreModel].[Customers] ([CustomerId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalCustomers_PrincipalCustomers] FOREIGN KEY ([PrincipalId], [CustomerId]) REFERENCES [SecurityModel].[PrincipalCustomers] ([PrincipalId], [CustomerId])
);


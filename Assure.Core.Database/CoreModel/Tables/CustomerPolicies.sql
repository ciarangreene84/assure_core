CREATE TABLE [CoreModel].[CustomerPolicies] (
    [CustomerPolicyId] INT IDENTITY (1, 1) NOT NULL,
    [CustomerId]       INT NOT NULL,
    [PolicyId]         INT NOT NULL,
    CONSTRAINT [PK_CustomerPolicies] PRIMARY KEY CLUSTERED ([CustomerPolicyId] ASC),
    CONSTRAINT [FK_CustomerPolicies_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [CoreModel].[Customers] ([CustomerId]),
    CONSTRAINT [FK_CustomerPolicies_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [CoreModel].[Policies] ([PolicyId]) ON DELETE CASCADE
);




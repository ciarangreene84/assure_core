CREATE TABLE [CoreModel].[CustomerPolicyBenefits] (
    [CustomerPolicyBenefitId] INT IDENTITY (1, 1) NOT NULL,
    [CustomerId]            INT NOT NULL,
    [PolicyId]              INT NOT NULL,
    [BenefitId]               INT NOT NULL,
    CONSTRAINT [PK_CustomerPolicyBenefits] PRIMARY KEY CLUSTERED ([CustomerPolicyBenefitId] ASC),
    CONSTRAINT [FK_CustomerPolicyBenefits_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [CoreModel].[Customers] ([CustomerId]),
    CONSTRAINT [FK_CustomerPolicyBenefits_PolicyBenefits] FOREIGN KEY ([PolicyId]) REFERENCES [CoreModel].[Policies] ([PolicyId]) ON DELETE CASCADE
);


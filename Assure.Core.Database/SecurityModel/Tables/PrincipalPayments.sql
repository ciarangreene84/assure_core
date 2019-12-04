CREATE TABLE [SecurityModel].[PrincipalPayments] (
    [PrincipalId] INT NOT NULL,
    [PaymentId]   INT NOT NULL,
    CONSTRAINT [PK_PrincipalPayments] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [PaymentId] ASC),
    CONSTRAINT [FK_PrincipalPayments_Payments] FOREIGN KEY ([PaymentId]) REFERENCES [CoreModel].[Payments] ([PaymentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalPayments_PrincipalPayments] FOREIGN KEY ([PrincipalId], [PaymentId]) REFERENCES [SecurityModel].[PrincipalPayments] ([PrincipalId], [PaymentId])
);


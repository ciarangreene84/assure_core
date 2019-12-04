CREATE TABLE [SecurityModel].[PrincipalInvoices] (
    [PrincipalId] INT NOT NULL,
    [InvoiceId]   INT NOT NULL,
    CONSTRAINT [PK_PrincipalInvoices] PRIMARY KEY CLUSTERED ([PrincipalId] ASC, [InvoiceId] ASC),
    CONSTRAINT [FK_PrincipalInvoices_Invoices] FOREIGN KEY ([InvoiceId]) REFERENCES [CoreModel].[Invoices] ([InvoiceId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PrincipalInvoices_PrincipalInvoices] FOREIGN KEY ([PrincipalId], [InvoiceId]) REFERENCES [SecurityModel].[PrincipalInvoices] ([PrincipalId], [InvoiceId])
);


CREATE TABLE [CoreModel].[CustomerCards] (
    [CustomerId] INT NOT NULL,
    [CardId]     INT NOT NULL,
    CONSTRAINT [FK_CustomerCards_Cards] FOREIGN KEY ([CardId]) REFERENCES [CoreModel].[Cards] ([CardId]),
    CONSTRAINT [FK_CustomerCards_Customers] FOREIGN KEY ([CustomerId]) REFERENCES [CoreModel].[Customers] ([CustomerId])
);


CREATE TABLE [StaticModel].[Products] (
    [ProductId]      INT           NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([ProductId] ASC),
    CONSTRAINT [CK_Products_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Products_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Products_ProductId] UNIQUE NONCLUSTERED ([ProductId] ASC),
    CONSTRAINT [UQ_Products_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);








GO
CREATE NONCLUSTERED INDEX [IX_Products_ObjectHash]
    ON [StaticModel].[Products]([ObjectHash] ASC);


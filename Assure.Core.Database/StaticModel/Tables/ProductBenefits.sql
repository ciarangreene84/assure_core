CREATE TABLE [StaticModel].[ProductBenefits] (
    [ProductBenefitId] INT IDENTITY (1, 1) NOT NULL,
    [ProductId]      INT NOT NULL,
    [BenefitId]        INT NOT NULL,
    CONSTRAINT [PK_ProductBenefits] PRIMARY KEY CLUSTERED ([ProductBenefitId] ASC),
    CONSTRAINT [FK_ProductBenefits_Products] FOREIGN KEY ([ProductId]) REFERENCES [StaticModel].[Products] ([ProductId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProductBenefits_Benefits] FOREIGN KEY ([BenefitId]) REFERENCES [StaticModel].[Benefits] ([BenefitId]) ON DELETE CASCADE
);




CREATE TABLE [StaticModel].[Benefits] (
    [BenefitId]        INT           NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Benefits] PRIMARY KEY CLUSTERED ([BenefitId] ASC),
    CONSTRAINT [CK_Benefits_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Benefits_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Benefits_BenefitId] UNIQUE NONCLUSTERED ([BenefitId] ASC),
    CONSTRAINT [UQ_Benefits_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);






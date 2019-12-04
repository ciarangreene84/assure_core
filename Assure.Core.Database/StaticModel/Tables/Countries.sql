CREATE TABLE [StaticModel].[Countries] (
    [Alpha2]      CHAR (2)     NOT NULL,
    [Alpha3]      CHAR (3)     NOT NULL,
    [NumericCode] SMALLINT     NOT NULL,
    [Name]        VARCHAR (64) NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([Alpha2] ASC),
    CONSTRAINT [CK_Countries_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [UQ_Countries_Alpha3] UNIQUE NONCLUSTERED ([Alpha3] ASC),
    CONSTRAINT [UQ_Countries_Name] UNIQUE NONCLUSTERED ([Name] ASC),
    CONSTRAINT [UQ_Countries_NumericCode] UNIQUE NONCLUSTERED ([NumericCode] ASC)
);








GO
CREATE NONCLUSTERED INDEX [IX_Countries]
    ON [StaticModel].[Countries]([Alpha2] ASC);
GO

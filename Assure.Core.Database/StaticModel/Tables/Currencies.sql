CREATE TABLE [StaticModel].[Currencies] (
    [Alpha3] CHAR(3) NOT NULL,
    [Name] VARCHAR(64) NOT NULL, 
    CONSTRAINT [PK_Currencies] PRIMARY KEY CLUSTERED ([Alpha3] ASC),
	CONSTRAINT [CK_Currencies_Alpha3] CHECK ((3)=len([Alpha3])),
	CONSTRAINT [CK_Currencies_Name] CHECK ((0)<len([Name])),
	CONSTRAINT [UQ_Currencies_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


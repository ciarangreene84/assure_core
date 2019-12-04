CREATE TABLE [CoreModel].[Quotes] (
    [QuoteId]        INT           IDENTITY (1, 1) NOT NULL,
    [ProductId]      INT           NOT NULL,
	[StartDateTime]	DATETIMEOFFSET NOT NULL,
	[EndDateTime]	DATETIMEOFFSET NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED ([QuoteId] ASC),
    CONSTRAINT [CK_Quotes_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
	CONSTRAINT [CK_Quotes_StartDateTime_EndDateTime] CHECK ([StartDateTime] < [EndDateTime]),
    CONSTRAINT [FK_Quotes_Products] FOREIGN KEY ([ProductId]) REFERENCES [StaticModel].[Products] ([ProductId]),
    CONSTRAINT [IX_Quotes_QuoteId] UNIQUE NONCLUSTERED ([QuoteId] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_Quotes_ObjectHash]
    ON [CoreModel].[Quotes]([ObjectHash] ASC);


GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Quotes_AI]
   ON CoreModel.Quotes
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalQuotes (PrincipalId, QuoteId)
		SELECT DATABASE_PRINCIPAL_ID()
				,QuoteId
			FROM inserted

END
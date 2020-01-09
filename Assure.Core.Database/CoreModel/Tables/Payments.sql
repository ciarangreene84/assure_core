CREATE TABLE [CoreModel].[Payments] (
    [PaymentId]      INT                IDENTITY (1, 1) NOT NULL,
    [Identifier]     VARCHAR (64)       NOT NULL,
    [CurrencyAlpha3] CHAR (3)           NOT NULL,
    [Amount]         DECIMAL (28, 8)    NOT NULL,
    [DateTime]       DATETIMEOFFSET (7) NOT NULL,
    [ObjectDocument] VARCHAR (MAX)      NOT NULL,
    [ObjectHash]     INT                NOT NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([PaymentId] ASC),
    CONSTRAINT [FK_Payments_Currencies] FOREIGN KEY ([CurrencyAlpha3]) REFERENCES [StaticModel].[Currencies] ([Alpha3]),
    CONSTRAINT [UQ_Payments_Identifier] UNIQUE NONCLUSTERED ([Identifier] ASC)
);




GO



-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Payments_AI]
   ON [CoreModel].[Payments]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalPayments (PrincipalId, PaymentId)
		SELECT DATABASE_PRINCIPAL_ID()
				,PaymentId
			FROM inserted

END
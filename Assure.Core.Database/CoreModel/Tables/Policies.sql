CREATE TABLE [CoreModel].[Policies] (
    [PolicyId]       INT           IDENTITY (1, 1) NOT NULL,
    [ProductId]      INT           NOT NULL,
	[StartDateTime]	DATETIMEOFFSET NOT NULL,
	[EndDateTime]	DATETIMEOFFSET NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Policies] PRIMARY KEY CLUSTERED ([PolicyId] ASC),
    CONSTRAINT [CK_Policies_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
	CONSTRAINT [CK_Policies_StartDateTime_EndDateTime] CHECK ([StartDateTime] < [EndDateTime]),
    CONSTRAINT [FK_Policies_Products] FOREIGN KEY ([ProductId]) REFERENCES [StaticModel].[Products] ([ProductId]),
    CONSTRAINT [IX_Policies_PolicyId] UNIQUE NONCLUSTERED ([PolicyId] ASC)
);






GO
CREATE NONCLUSTERED INDEX [IX_Policies_ObjectHash]
    ON [CoreModel].[Policies]([ObjectHash] ASC);


GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Policies_AI]
   ON [CoreModel].[Policies]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalPolicies (PrincipalId, PolicyId)
		SELECT DATABASE_PRINCIPAL_ID()
				,PolicyId
			FROM inserted

END
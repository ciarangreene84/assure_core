CREATE TABLE [CoreModel].[Claims] (
    [ClaimId]        INT           IDENTITY (1, 1) NOT NULL,
    [PolicyId]       INT           NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Claims] PRIMARY KEY CLUSTERED ([ClaimId] ASC),
    CONSTRAINT [CK_Claims_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [FK_Claims_Policies] FOREIGN KEY ([PolicyId]) REFERENCES [CoreModel].[Policies] ([PolicyId]),
    CONSTRAINT [IX_Claims_ClaimId] UNIQUE NONCLUSTERED ([ClaimId] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_Claims_ObjectHash]
    ON [CoreModel].[Claims]([ObjectHash] ASC);


GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Claims_AI]
   ON [CoreModel].[Claims]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalClaims (PrincipalId, ClaimId)
		SELECT DATABASE_PRINCIPAL_ID()
				,ClaimId
			FROM inserted

END
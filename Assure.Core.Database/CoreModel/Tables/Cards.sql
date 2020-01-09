CREATE TABLE [CoreModel].[Cards] (
    [CardId]         INT           IDENTITY (1, 1) NOT NULL,
    [Number]         INT           NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Cards] PRIMARY KEY CLUSTERED ([CardId] ASC),
    CONSTRAINT [CK_Cards_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Cards_CardId] UNIQUE NONCLUSTERED ([CardId] ASC),
    CONSTRAINT [UQ_Cards_Number] UNIQUE NONCLUSTERED ([Number] ASC)
);




GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Cards_AI]
   ON [CoreModel].[Cards]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalCards (PrincipalId, CardId)
		SELECT DATABASE_PRINCIPAL_ID()
				,CardId
			FROM inserted

END
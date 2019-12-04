CREATE TABLE [CoreModel].[Requests] (
    [RequestId]      INT           IDENTITY (1, 1) NOT NULL,
    [Type]           VARCHAR (32)  NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED ([RequestId] ASC),
    CONSTRAINT [CK_Requests_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [CK_Requests_Type] CHECK ((0)<len([Type])),
    CONSTRAINT [IX_Requests_RequestId] UNIQUE NONCLUSTERED ([RequestId] ASC)
);


GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Requests_AI]
   ON [CoreModel].[Requests]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalRequests (PrincipalId, RequestId)
		SELECT DATABASE_PRINCIPAL_ID()
				,RequestId
			FROM inserted

END
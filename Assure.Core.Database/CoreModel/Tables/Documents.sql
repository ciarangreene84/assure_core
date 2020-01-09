CREATE TABLE [CoreModel].[Documents] (
    [DocumentId] UNIQUEIDENTIFIER   NOT NULL,
    [Name]       VARCHAR (256)      NOT NULL,
    [Type]       VARCHAR (8)        NOT NULL,
    [LastWrite]  DATETIMEOFFSET (7) NOT NULL,
    [Data]       VARBINARY (MAX)    NOT NULL,
    CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [CK_Documents_FileNameExtension] CHECK ((0)<charindex('.',[Name]))
);






GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Documents_AI]
   ON [CoreModel].[Documents]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalDocuments (PrincipalId, DocumentId)
		SELECT DATABASE_PRINCIPAL_ID()
				,DocumentId
			FROM inserted

END
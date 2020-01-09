CREATE TABLE [CoreModel].[Leads] (
    [LeadId]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (128) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Leads] PRIMARY KEY CLUSTERED ([LeadId] ASC),
    CONSTRAINT [CK_Leads_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [CK_Leads_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [IX_Leads_LeadId] UNIQUE NONCLUSTERED ([LeadId] ASC),
    CONSTRAINT [UQ_Leads_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);




GO
CREATE NONCLUSTERED INDEX [IX_Leads_ObjectHash]
    ON [CoreModel].[Leads]([ObjectHash] ASC);


GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [CoreModel].[TR_Leads_AI]
   ON [CoreModel].[Leads]
   AFTER INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO SecurityModel.PrincipalLeads (PrincipalId, LeadId)
		SELECT DATABASE_PRINCIPAL_ID()
				,LeadId
			FROM inserted

END
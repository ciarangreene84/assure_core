CREATE TABLE [CoreModel].[CommissionAgreements] (
    [CommissionAgreementId] INT           IDENTITY (1, 1) NOT NULL,
    [ObjectDocument]        VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_CommissionAgreements] PRIMARY KEY CLUSTERED ([CommissionAgreementId] ASC)
);


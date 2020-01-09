


CREATE VIEW [CoreFacade].[Payments]
WITH SCHEMABINDING
AS
	SELECT Payments.PaymentId
		  ,Payments.[Identifier]
		  ,Payments.[CurrencyAlpha3]
		  ,Payments.[Amount]
		  ,Payments.[DateTime]
		  ,Payments.[ObjectDocument]
		  ,Payments.[ObjectHash]
		FROM CoreModel.Payments 
		WHERE [Identity].[IsUserInRole]('Payment Administrator') = 'true'
		   OR PaymentId IN 
			(
				SELECT PaymentId 
					FROM SecurityModel.PrincipalPayments
					WHERE [PrincipalId] = DATABASE_PRINCIPAL_ID()
			)
GO


CREATE TRIGGER [CoreFacade].[TR_Payments_IOI]
   ON [CoreFacade].[Payments]
   INSTEAD OF INSERT
AS 
BEGIN
	SET NOCOUNT ON;

	INSERT INTO CoreModel.Payments 
			(
				 [Identifier]
				,[CurrencyAlpha3]
				,[Amount]
				,[DateTime]
				,[ObjectDocument]
				,[ObjectHash]
			)
		SELECT inserted.[Identifier]
				,inserted.[CurrencyAlpha3]
				,inserted.[Amount]
				,inserted.[DateTime]
				,inserted.[ObjectDocument]
				,inserted.[ObjectHash]
			FROM inserted
END
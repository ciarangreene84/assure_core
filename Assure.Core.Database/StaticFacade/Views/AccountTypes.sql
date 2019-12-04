CREATE VIEW [StaticFacade].[AccountTypes]
WITH SCHEMABINDING
AS
	SELECT AccountTypeId
			,Name
		FROM StaticModel.AccountTypes
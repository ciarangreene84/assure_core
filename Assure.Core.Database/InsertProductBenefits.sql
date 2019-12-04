INSERT INTO [StaticModel].[ProductBenefits] ([ProductId], [BenefitId])
	SELECT [ProductId], [BenefitId]
		FROM [StaticModel].[Products], [StaticModel].[Benefits]
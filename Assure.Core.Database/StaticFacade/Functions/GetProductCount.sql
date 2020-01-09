CREATE FUNCTION [StaticFacade].[GetProductCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int

	SET @ItemCount = (SELECT COUNT(ProductId) FROM StaticModel.Products) 

	-- Return the result of the function
	RETURN @ItemCount;

END
GO


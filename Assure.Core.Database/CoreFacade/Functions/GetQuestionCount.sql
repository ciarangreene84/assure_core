

CREATE FUNCTION [CoreFacade].[GetQuestionCount]
(

)
RETURNS int
AS
BEGIN

	DECLARE @ItemCount int
	SET @ItemCount = (SELECT COUNT(QuestionId) FROM StaticModel.Questions) 

	-- Return the result of the function
	RETURN @ItemCount;

END
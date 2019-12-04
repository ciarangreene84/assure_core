
CREATE VIEW [StaticFacade].[Questions]
WITH SCHEMABINDING
AS
	SELECT QuestionId
			,[Text]
			,[ObjectDocument]
			,[ObjectHash]
		FROM StaticModel.Questions
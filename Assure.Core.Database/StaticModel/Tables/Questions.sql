CREATE TABLE [StaticModel].[Questions] (
    [QuestionId]     INT           IDENTITY (1, 1) NOT NULL,
    [Text]           VARCHAR (256) NOT NULL,
    [ObjectDocument] VARCHAR (MAX) NOT NULL,
    [ObjectHash]     INT           NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED ([QuestionId] ASC),
    CONSTRAINT [CK_Questions_ObjectDocument] CHECK (isjson([ObjectDocument])>(0)),
    CONSTRAINT [CK_Questions_Text] CHECK ((0)<len([Text])),
    CONSTRAINT [IX_Questions_QuestionId] UNIQUE NONCLUSTERED ([QuestionId] ASC),
    CONSTRAINT [UQ_Questions_Text] UNIQUE NONCLUSTERED ([Text] ASC)
);


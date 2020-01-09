CREATE TABLE [StaticModel].[AccountTypes] (
    [AccountTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (32) NOT NULL,
    CONSTRAINT [PK_AccountTypes] PRIMARY KEY CLUSTERED ([AccountTypeId] ASC),
    CONSTRAINT [CK_AccountTypes_Name] CHECK ((0)<len([Name])),
    CONSTRAINT [UQ_AccountTypes_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


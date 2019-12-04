-- User: sysadmin; Password: A55uR3c0R3!
INSERT INTO [Identity].[Users] 
	(
		[UserId]
		,[UserName]
		,[NormalizedUserName]
		,[Email]
		,[NormalizedEmail]
		,[PasswordHash]
		,[SecurityStamp]
		,[ConcurrencyStamp]
		,[AccessFailedCount]
		,[EmailConfirmed]
		,[PhoneNumberConfirmed]
		,[TwoFactorEnabled]
		,[LockoutEnabled]
	) 
	VALUES
	(
		'F1F86C06-6AA7-42E5-2EA9-08D61FB87E4D'
		,'sysadmin'
		,'SYSADMIN'
		,'sysadmin@assurecore.fake'
		,'SYSADMIN@ASSURECORE.FAKE'
		,'AQAAAAEAACcQAAAAEP+KKK4+5uNy0AmVCGF57V5iHnmfGhtxbANzUOYH0Kn8uLPXyNjNKX2djevxgla5cg=='
		,'PTPHVEPOUWQLRFRRR42PFQECLD5NGTCF'
		,'4fe327c3-e548-4ba9-96dd-a11130b1025c'
		,0
		,0
		,0
		,0
		,0
	);
GO

CREATE USER [f1f86c06-6aa7-42e5-2ea9-08d61fb87e4d] WITHOUT LOGIN;
GO

ALTER ROLE [AssureSystemAdministrator] ADD MEMBER [f1f86c06-6aa7-42e5-2ea9-08d61fb87e4d];
GO

INSERT INTO [StaticModel].[AccountTypes] ([Name]) VALUES ('Test')
INSERT INTO [StaticModel].[AgentTypes] ([Name]) VALUES ('Test')

GO
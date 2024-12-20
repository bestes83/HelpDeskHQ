IF OBJECT_ID('Account') IS NOT NULL
	DROP TABLE Account

CREATE TABLE Account
(
	AccountId INT,
	Username INT,
	[Password] NVARCHAR(100),
	[Salt] NVARCHAR(25)
)
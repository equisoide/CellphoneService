USE CellphoneService;

-- Clean up all tables
DELETE FROM [dbo].[Settings];
DELETE FROM [dbo].[AspNetUserRoles];
DELETE FROM [dbo].[AspNetUsers];
DELETE FROM [dbo].[AspNetRoles];

-- Settings
INSERT INTO [dbo].[Settings]([PricePerMinute]) VALUES(100);

-- Roles
INSERT INTO [dbo].[AspNetRoles]([Id], [Name]) VALUES('004DC29B-168D-452B-942F-D8AE223D9137', 'Customer');
INSERT INTO [dbo].[AspNetRoles]([Id], [Name]) VALUES('8B7D7E39-791D-4CF2-9B42-B039D1DA54CF', 'Employee');

-- Users
INSERT INTO [dbo].[AspNetUsers]
(
	[Id],
	[Email],
	[EmailConfirmed],
	[PasswordHash],
	[SecurityStamp],
	[PhoneNumber],
	[PhoneNumberConfirmed],
	[TwoFactorEnabled],
	[LockoutEndDateUtc],
	[LockoutEnabled],
	[AccessFailedCount],
	[UserName]
)
VALUES
(
	'661a3411-62b5-4d52-80bf-ecb1638bfe84',
    'jcuartas@bitgray.co',
    0,
    'ABefBd1KB641jB9FoVwGTXhPYrKzS66GVYFAbh7bhSY++AgQ3UfFNRcbnphhYQfoYA==',
    '7acec2f3-2be7-47ef-9ca1-e16aab6a78ba',
    NULL,
    0,
    0,
    NULL,
    0,
	0,
    'jcuartas@bitgray.co'
);
INSERT INTO [dbo].[AspNetUsers]
(
	[Id],
	[Email],
	[EmailConfirmed],
	[PasswordHash],
	[SecurityStamp],
	[PhoneNumber],
	[PhoneNumberConfirmed],
	[TwoFactorEnabled],
	[LockoutEndDateUtc],
	[LockoutEnabled],
	[AccessFailedCount],
	[UserName]
)
VALUES
(
	'827bcb16-84f4-4e82-b956-df1ce380609d',
    'bacero@bitgray.co',
    0,
    'AGNhyusID4gHUrMi8OtYIvXgmSs+aDgCZ/RPJhapmAzBt0WMlZqri4TnZxIGV1Wc8w==',
    '1c36e740-3a7d-42b8-87d9-ae46f8c6a409',
    NULL,
    0,
    0,
    NULL,
    0,
	0,
    'bacero@bitgray.co'
);

-- Users x Roles
INSERT INTO [dbo].[AspNetUserRoles]([UserId], [RoleId]) VALUES('661a3411-62b5-4d52-80bf-ecb1638bfe84', '004DC29B-168D-452B-942F-D8AE223D9137');
INSERT INTO [dbo].[AspNetUserRoles]([UserId], [RoleId]) VALUES('827bcb16-84f4-4e82-b956-df1ce380609d', '8B7D7E39-791D-4CF2-9B42-B039D1DA54CF');

-- Test
SELECT * FROM [dbo].[Settings]
SELECT * FROM [dbo].[AspNetRoles];
SELECT * FROM [dbo].[AspNetUsers];
SELECT * FROM [dbo].[AspNetUserRoles];
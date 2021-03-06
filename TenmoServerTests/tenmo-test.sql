USE [tenmo]

DELETE FROM transfers;
DELETE FROM accounts;
DELETE FROM users;


SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (1, N'a', N'qUYshWmt7tOZB0hrQqoeRccc8tQ=', N'TMiMoTDT8no=')
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (2, N'b', N'VmxiJsnR8E6rSnnopp+CNQgJT3Y=', N'NfaEHC1+oLA=')
INSERT [dbo].[users] ([user_id], [username], [password_hash], [salt]) VALUES (3, N'c', N'mc4JjqYB+V3uhIzXVNcfQrtDWuY=', N'CU6cdkXG5rQ=')
SET IDENTITY_INSERT [dbo].[users] OFF

SET IDENTITY_INSERT [dbo].[accounts] ON 

INSERT [dbo].[accounts] ([account_id], [user_id], [balance]) VALUES (1, 1, CAST(1500.54 AS Decimal(13, 2)))
INSERT [dbo].[accounts] ([account_id], [user_id], [balance]) VALUES (2, 2, CAST(493.49 AS Decimal(13, 2)))
INSERT [dbo].[accounts] ([account_id], [user_id], [balance]) VALUES (3, 3, CAST(1005.99 AS Decimal(13, 2)))
SET IDENTITY_INSERT [dbo].[accounts] OFF

SET IDENTITY_INSERT [dbo].[transfers] ON 

INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (2, 2, 2, 1, 2, CAST(999.00 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (3, 2, 2, 2, 1, CAST(1500.25 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (4, 2, 2, 2, 1, CAST(5.26 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (5, 2, 2, 2, 1, CAST(0.01 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (6, 2, 2, 2, 1, CAST(0.01 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (7, 2, 2, 3, 1, CAST(500.00 AS Decimal(13, 2)))
INSERT [dbo].[transfers] ([transfer_id], [transfer_type_id], [transfer_status_id], [account_from], [account_to], [amount]) VALUES (8, 2, 2, 1, 3, CAST(505.99 AS Decimal(13, 2)))
SET IDENTITY_INSERT [dbo].[transfers] OFF

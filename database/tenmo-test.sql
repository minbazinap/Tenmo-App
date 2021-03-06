USE [master]
GO
/****** Object:  Database [tenmo]    Script Date: 3/5/2021 9:09:52 PM ******/
CREATE DATABASE [tenmo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'tenmo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\tenmo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'tenmo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\tenmo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [tenmo] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [tenmo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [tenmo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tenmo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tenmo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tenmo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tenmo] SET ARITHABORT OFF 
GO
ALTER DATABASE [tenmo] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [tenmo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tenmo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tenmo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tenmo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [tenmo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tenmo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tenmo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tenmo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tenmo] SET  ENABLE_BROKER 
GO
ALTER DATABASE [tenmo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tenmo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [tenmo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [tenmo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [tenmo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tenmo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [tenmo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [tenmo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [tenmo] SET  MULTI_USER 
GO
ALTER DATABASE [tenmo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [tenmo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [tenmo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [tenmo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [tenmo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [tenmo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [tenmo] SET QUERY_STORE = OFF
GO
USE [tenmo]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 3/5/2021 9:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[balance] [decimal](13, 2) NOT NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfer_statuses]    Script Date: 3/5/2021 9:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfer_statuses](
	[transfer_status_id] [int] IDENTITY(1,1) NOT NULL,
	[transfer_status_desc] [varchar](10) NOT NULL,
 CONSTRAINT [PK_transfer_statuses] PRIMARY KEY CLUSTERED 
(
	[transfer_status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfer_types]    Script Date: 3/5/2021 9:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfer_types](
	[transfer_type_id] [int] IDENTITY(1,1) NOT NULL,
	[transfer_type_desc] [varchar](10) NOT NULL,
 CONSTRAINT [PK_transfer_types] PRIMARY KEY CLUSTERED 
(
	[transfer_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfers]    Script Date: 3/5/2021 9:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfers](
	[transfer_id] [int] IDENTITY(1,1) NOT NULL,
	[transfer_type_id] [int] NOT NULL,
	[transfer_status_id] [int] NOT NULL,
	[account_from] [int] NOT NULL,
	[account_to] [int] NOT NULL,
	[amount] [decimal](13, 2) NOT NULL,
 CONSTRAINT [PK_transfers] PRIMARY KEY CLUSTERED 
(
	[transfer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 3/5/2021 9:09:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[accounts]  WITH CHECK ADD  CONSTRAINT [FK_accounts_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([user_id])
GO
ALTER TABLE [dbo].[accounts] CHECK CONSTRAINT [FK_accounts_user]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_accounts_from] FOREIGN KEY([account_from])
REFERENCES [dbo].[accounts] ([account_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_accounts_from]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_accounts_to] FOREIGN KEY([account_to])
REFERENCES [dbo].[accounts] ([account_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_accounts_to]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_transfer_statuses] FOREIGN KEY([transfer_status_id])
REFERENCES [dbo].[transfer_statuses] ([transfer_status_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_transfer_statuses]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [FK_transfers_transfer_types] FOREIGN KEY([transfer_type_id])
REFERENCES [dbo].[transfer_types] ([transfer_type_id])
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [FK_transfers_transfer_types]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [CK_transfers_amount_gt_0] CHECK  (([amount]>(0)))
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [CK_transfers_amount_gt_0]
GO
ALTER TABLE [dbo].[transfers]  WITH CHECK ADD  CONSTRAINT [CK_transfers_not_same_account] CHECK  (([account_from]<>[account_to]))
GO
ALTER TABLE [dbo].[transfers] CHECK CONSTRAINT [CK_transfers_not_same_account]
GO
USE [master]
GO
ALTER DATABASE [tenmo] SET  READ_WRITE 
GO

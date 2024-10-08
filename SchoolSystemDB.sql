USE [master]
GO
/****** Object:  Database [SchoolSystemdb]    Script Date: 25/9/2024 12:28:57 p. m. ******/
CREATE DATABASE [SchoolSystemdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolSystemdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SchoolSystemdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolSystemdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SchoolSystemdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SchoolSystemdb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolSystemdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolSystemdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolSystemdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolSystemdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SchoolSystemdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolSystemdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET RECOVERY FULL 
GO
ALTER DATABASE [SchoolSystemdb] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolSystemdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolSystemdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolSystemdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolSystemdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolSystemdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchoolSystemdb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SchoolSystemdb', N'ON'
GO
ALTER DATABASE [SchoolSystemdb] SET QUERY_STORE = OFF
GO
USE [SchoolSystemdb]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 25/9/2024 12:28:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 25/9/2024 12:28:57 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](60) NOT NULL,
	[LastName] [varchar](60) NOT NULL,
	[Age] [int] NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[CourseId] [int] NULL,
	[Address] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (2, N'Octavo', N'WalterOctavo')
INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (3, N'Segundo', N'secundaria')
INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (7, N'Primero', N'Curso de primaria')
INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (8, N'tercero ', N'primaria')
INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (9, N'cuarto', N'Secundaria')
INSERT [dbo].[Course] ([Id], [Name], [Description]) VALUES (11, N'Noveno', N'Secundaria')
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (6, N'Fernando', N'Fernando', 32, CAST(N'2024-09-01' AS Date), 2, N'Santo Domingo')
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (9, N'Juan jose', N'Amparo', 50, CAST(N'2024-09-02' AS Date), 2, N'La julia')
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (12, N'Walter', N'garcia', 70, CAST(N'2024-09-02' AS Date), 3, N'Santo Domingo')
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (13, N'Julia ', N'Robers', 90, CAST(N'2024-09-01' AS Date), 2, N'Sainagua')
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (14, N'Felipe', N'Perez', 18, CAST(N'2024-09-02' AS Date), 2, N'Santo Domingo')
INSERT [dbo].[Student] ([Id], [FirstName], [LastName], [Age], [DateOfBirth], [CourseId], [Address]) VALUES (15, N'Paloma', N'Perez', 20, CAST(N'2024-09-02' AS Date), 2, N'Santo Domingo')
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
GO
USE [master]
GO
ALTER DATABASE [SchoolSystemdb] SET  READ_WRITE 
GO

USE [master]
GO
/****** Object:  Database [QPManagement]    Script Date: 27-02-2024 19:12:25 ******/
CREATE DATABASE [QPManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QPManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QPManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QPManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QPManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QPManagement] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QPManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QPManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QPManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QPManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QPManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QPManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [QPManagement] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QPManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QPManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QPManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QPManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QPManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QPManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QPManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QPManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QPManagement] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QPManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QPManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QPManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QPManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QPManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QPManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QPManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QPManagement] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QPManagement] SET  MULTI_USER 
GO
ALTER DATABASE [QPManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QPManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QPManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QPManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QPManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QPManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QPManagement] SET QUERY_STORE = OFF
GO
USE [QPManagement]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[QuestionID] [int] NOT NULL,
	[AnswerText] [nvarchar](max) NOT NULL,
	[SubmissionTimestamp] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Approvals]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Approvals](
	[ApprovalID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionPaperID] [int] NOT NULL,
	[ApproverID] [int] NOT NULL,
	[ApprovalStatus] [nvarchar](50) NOT NULL,
	[ApprovalTimestamp] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ApprovalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[CanCreateQuestionPaper] [bit] NOT NULL,
	[CanEditQuestionPaper] [bit] NOT NULL,
	[CanDeleteQuestionPaper] [bit] NOT NULL,
	[CanReviewQuestionPaper] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionPapers]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionPapers](
	[QuestionPaperID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[CreatorID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionPaperID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionText] [nvarchar](max) NOT NULL,
	[Option1] [nvarchar](max) NOT NULL,
	[Option2] [nvarchar](max) NOT NULL,
	[Option3] [nvarchar](max) NOT NULL,
	[Option4] [nvarchar](max) NOT NULL,
	[CorrectAnswer] [nvarchar](max) NOT NULL,
	[QuestionPaperID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 27-02-2024 19:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](255) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[QuestionPapers] ON 

INSERT [dbo].[QuestionPapers] ([QuestionPaperID], [Title], [Description], [CreationDate], [Status], [CreatorID]) VALUES (14, N'Science', N'Org', CAST(N'2024-02-20T19:51:04.1716869' AS DateTime2), N'Rejected', 18)
INSERT [dbo].[QuestionPapers] ([QuestionPaperID], [Title], [Description], [CreationDate], [Status], [CreatorID]) VALUES (15, N'Maths', N'alge', CAST(N'2024-02-20T19:52:52.0904926' AS DateTime2), N'Rejected', 18)
INSERT [dbo].[QuestionPapers] ([QuestionPaperID], [Title], [Description], [CreationDate], [Status], [CreatorID]) VALUES (16, N'English', N'articles', CAST(N'2024-02-20T20:02:19.1761033' AS DateTime2), N'Approved', 19)
INSERT [dbo].[QuestionPapers] ([QuestionPaperID], [Title], [Description], [CreationDate], [Status], [CreatorID]) VALUES (17, N'Economics', N'eco', CAST(N'2024-02-20T20:54:56.0000000' AS DateTime2), N'Created', 19)
SET IDENTITY_INSERT [dbo].[QuestionPapers] OFF
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (11, N'oxygen', N'a', N'v', N'c', N'o', N'o', 14)
INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (12, N'hydrogen', N't', N'h', N'u', N'e', N'h', 14)
INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (13, N'1+1', N'1', N'2', N'3', N'4', N'2', 15)
INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (15, N'which is not a article', N'a', N'an', N'the', N'is', N'is', 16)
INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (17, N'what is the name of our country?', N'India', N'Pakistan', N'Iran', N'Iraq', N'India', 17)
INSERT [dbo].[Questions] ([QuestionID], [QuestionText], [Option1], [Option2], [Option3], [Option4], [CorrectAnswer], [QuestionPaperID]) VALUES (18, N'What is our currency?', N'rupees', N'rubbel', N'dollars', N'yen', N'rupees', 17)
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Email], [Role]) VALUES (16, N'Student1', N'$2a$11$1ZA/bd14oe3qgCmN.wZ2TexPhVl7VMqX6NV5BWlFmfFaPAeEDUFVO', N'student1@example.com', N'Student')
INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Email], [Role]) VALUES (18, N'Admin', N'$2a$11$mmtT0BUJ8cCVGE4AtQhKXu7sESuB4wjx5h2v.5KbyNR/hFW7xGqvq', N'admin@example.com', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Email], [Role]) VALUES (19, N'Teacher1', N'$2a$11$az5jbBRmXSy0WktV46Q4se7VVZNxI.mlhknqYcvWP6Isrk3b4uXV.', N'teacher1@example.com', N'Teacher')
INSERT [dbo].[Users] ([UserID], [Username], [PasswordHash], [Email], [Role]) VALUES (20, N'sample', N'$2a$11$87PAYs6HjxDLUtAvhNeQXunMCtL6u5HcabsUwJXcZrFAJzRRebPSm', N'sample@example.com', N'Student')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_email]    Script Date: 27-02-2024 19:12:25 ******/
CREATE UNIQUE NONCLUSTERED INDEX [unique_email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answers] ADD  DEFAULT (getdate()) FOR [SubmissionTimestamp]
GO
ALTER TABLE [dbo].[Approvals] ADD  DEFAULT (getdate()) FOR [ApprovalTimestamp]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((0)) FOR [CanCreateQuestionPaper]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((0)) FOR [CanEditQuestionPaper]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((0)) FOR [CanDeleteQuestionPaper]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT ((0)) FOR [CanReviewQuestionPaper]
GO
ALTER TABLE [dbo].[QuestionPapers] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[QuestionPapers] ADD  DEFAULT ('Draft') FOR [Status]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([QuestionID])
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Approvals]  WITH CHECK ADD FOREIGN KEY([ApproverID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Approvals]  WITH CHECK ADD FOREIGN KEY([QuestionPaperID])
REFERENCES [dbo].[QuestionPapers] ([QuestionPaperID])
GO
ALTER TABLE [dbo].[QuestionPapers]  WITH CHECK ADD FOREIGN KEY([CreatorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD FOREIGN KEY([QuestionPaperID])
REFERENCES [dbo].[QuestionPapers] ([QuestionPaperID])
GO
USE [master]
GO
ALTER DATABASE [QPManagement] SET  READ_WRITE 
GO

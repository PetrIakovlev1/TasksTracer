USE [master]
GO
/****** Object:  Database [CC_Report_Requests]    Script Date: 6/19/2023 2:13:34 AM ******/
CREATE DATABASE [CC_Report_Requests]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CC_Report_Requests', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL15.SQLASMSOL01\MSSQL\Data\CC_Report_Requests.mdf' , SIZE = 512000KB , MAXSIZE = UNLIMITED, FILEGROWTH = 524288KB )
 LOG ON 
( NAME = N'CC_Report_Requests_log', FILENAME = N'I:\Program Files\Microsoft SQL Server\MSSQL15.SQLASMSOL01\MSSQL\Log\CC_Report_Requests_log.ldf' , SIZE = 102400KB , MAXSIZE = 2048GB , FILEGROWTH = 524288KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CC_Report_Requests] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CC_Report_Requests].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CC_Report_Requests] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET ARITHABORT OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CC_Report_Requests] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CC_Report_Requests] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CC_Report_Requests] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CC_Report_Requests] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CC_Report_Requests] SET  MULTI_USER 
GO
ALTER DATABASE [CC_Report_Requests] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CC_Report_Requests] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CC_Report_Requests] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CC_Report_Requests] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CC_Report_Requests] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CC_Report_Requests', N'ON'
GO
ALTER DATABASE [CC_Report_Requests] SET QUERY_STORE = OFF
GO
USE [CC_Report_Requests]
GO
/****** Object:  User [symreport]    Script Date: 6/19/2023 2:13:37 AM ******/
CREATE USER [symreport] FOR LOGIN [symreport] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [pwirsmt_reader]    Script Date: 6/19/2023 2:13:37 AM ******/
CREATE USER [pwirsmt_reader] FOR LOGIN [pwirsmt_reader] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [symreport]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [symreport]
GO
ALTER ROLE [db_datareader] ADD MEMBER [pwirsmt_reader]
GO
/****** Object:  UserDefinedTableType [dbo].[AttachedFiles]    Script Date: 6/19/2023 2:13:38 AM ******/
CREATE TYPE [dbo].[AttachedFiles] AS TABLE(
	[File_Info] [varchar](1000) NULL,
	[File_Value] [varbinary](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[AttachedFiles2]    Script Date: 6/19/2023 2:13:38 AM ******/
CREATE TYPE [dbo].[AttachedFiles2] AS TABLE(
	[File_Info] [varchar](1000) NULL,
	[File_Value] [varbinary](max) NULL,
	[File_Guid] [varchar](50) NULL
)
GO
/****** Object:  Table [dbo].[tblAttachements]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAttachements](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[File_Info] [nvarchar](1000) NULL,
	[File_Value] [varbinary](max) NULL,
	[Request_id] [int] NULL,
	[SessionGuid] [varchar](50) NULL,
	[AttachementWritenAt] [datetime] NULL,
 CONSTRAINT [PK_tblAttachements] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBU]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBU](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BU_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblBUList] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBusinessImpact]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBusinessImpact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[BI_Name] [varchar](50) NULL,
 CONSTRAINT [PK_BusinessImpact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblComplexity]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblComplexity](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Complexity_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblComplexity] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDataSource]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDataSource](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DS_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblDataSource] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFilters_forHomePage]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFilters_forHomePage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RequestType] [varchar](50) NULL,
	[NameForSearch] [varchar](50) NULL,
 CONSTRAINT [PK_tblRequestTypes_forHomePage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblFunctionalArea]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblFunctionalArea](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FA_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblFunctionalArea] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPriority]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPriority](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Priority_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblPriority] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblReportArea]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReportArea](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RA_Name] [varchar](50) NULL,
	[DataSource_id] [int] NULL,
 CONSTRAINT [PK_tblReportArea] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRequests]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRequests](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RequestName] [varchar](255) NULL,
	[RequestDescription] [varchar](1000) NULL,
	[Requestor] [varchar](255) NULL,
	[BU_id] [int] NULL,
	[FunctionalArea_id] [int] NULL,
	[BusinessOwner] [varchar](255) NULL,
	[WE_id] [int] NULL,
	[RequestType_id] [int] NULL,
	[LinkToExistingReport] [varchar](1000) NULL,
	[DataSource_id] [int] NULL,
	[ReportArea_id] [int] NULL,
	[LinkToExternalDataSource] [varchar](1000) NULL,
	[BusinessImpact_id] [int] NULL,
	[BusinessImpactDescription] [varchar](1000) NULL,
	[TacticalProject_id] [int] NULL,
	[TacticalProjectName] [varchar](255) NULL,
	[DesiredDueDate] [date] NULL,
	[Status_id] [int] NULL,
	[Priority_id] [int] NULL,
	[Complexity_id] [int] NULL,
	[DevelopmentHours] [int] NULL,
	[AssignedTo] [varchar](255) NULL,
	[OnHold] [bit] NULL,
	[HoldDescription] [varchar](500) NULL,
	[PromiseDate] [date] NULL,
	[StartDate] [date] NULL,
	[UserTestStart] [date] NULL,
	[UserTestEnd] [date] NULL,
	[CancelledDate] [date] NULL,
	[CompletionDate] [date] NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [varchar](255) NULL,
	[Approved] [bit] NULL,
	[LastChangesDate] [datetime] NULL,
	[RequestGuid] [varchar](50) NULL,
	[ApproveDate] [date] NULL,
 CONSTRAINT [PK_tblRequests] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblRequestType]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblRequestType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RT_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblRequestType] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStatus]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Status_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTacticalProject]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTacticalProject](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[TacticalProjectFlag] [varchar](50) NULL,
 CONSTRAINT [PK_tblTacticalProject] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblWE]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblWE](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[WE_Name] [varchar](50) NULL,
 CONSTRAINT [PK_tblWE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblAttachements]  WITH CHECK ADD  CONSTRAINT [FK_tblAttachements_tblRequests] FOREIGN KEY([Request_id])
REFERENCES [dbo].[tblRequests] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblAttachements] CHECK CONSTRAINT [FK_tblAttachements_tblRequests]
GO
/****** Object:  StoredProcedure [dbo].[uspApproveRequest]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspApproveRequest]
@requestGuid varchar(50),
@approveMessage varchar(255) OUTPUT

AS
BEGIN
DECLARE
	@whenApproved date

SET NOCOUNT ON;


IF	(SELECT
		COUNT(id)
	FROM
		[dbo].[tblRequests] R
	WHERE
		R.[RequestGuid] = @requestGuid
	)=0
	BEGIN
		SET @approveMessage = 'Error: Wrong Request GUID'
		return
	END

SET @whenApproved = (
				SELECT
					ApproveDate
				FROM
					[dbo].[tblRequests] R
				WHERE
					R.[RequestGuid] = @requestGuid
			)
IF @whenApproved IS NOT NULL
	SET @approveMessage = 'Already been approved on ' + CONVERT(varchar(50),  @whenApproved, 113) 
ELSE
	BEGIN
		UPDATE
			R
		SET
			R.Status_id = 2,
			R.Approved = 1,
			R.ApproveDate = GETDATE()
		FROM
			[dbo].[tblRequests] R
		WHERE
			R.[RequestGuid] = @requestGuid

	SET @approveMessage = 'Successfully approved'
	END


END
GO
/****** Object:  StoredProcedure [dbo].[uspCleanAttachementsTable]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspCleanAttachementsTable]
AS
BEGIN
	SET NOCOUNT ON;

--=== Clean attachements table: delete records with no parents and older than 1 hour=====
DELETE
FROM
	[dbo].[tblAttachements]
WHERE
	DATEDIFF(hour, AttachementWritenAt, GETDATE())>1
	AND [Request_id] IS NULL

--===End Clean attachements table: delete records with no parents and older than 1 hour=====
END
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteExistAttachment]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteExistAttachment]
@fileName varchar(1000),
@requestId int
AS
BEGIN

	SET NOCOUNT ON;

DELETE
FROM
	[dbo].[tblAttachements] 
WHERE
	[File_Info] = @fileName
	AND [Request_id]=@requestId


END
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteFile]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteFile]
@sessionGuid varchar(50),
@fileName varchar(1000)
AS
BEGIN

	SET NOCOUNT ON;
DELETE
FROM
	[dbo].[tblAttachements] 
WHERE
	[File_Info] = @fileName
	AND [SessionGuid] = @sessionGuid



END
GO
/****** Object:  StoredProcedure [dbo].[uspDeleteRequest]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspDeleteRequest]
@id int
AS
BEGIN

	SET NOCOUNT ON;

DELETE
FROM
	[dbo].[tblRequests]
WHERE
	[id] = @id
	AND ISNULL([Approved], 0)=0 


END
GO
/****** Object:  StoredProcedure [dbo].[uspGetAllDictionaries]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetAllDictionaries]
AS
BEGIN

	SET NOCOUNT ON;

SELECT 
	[id]
	,[BU_Name]
	,'[tblBU]'
FROM 
	[CC_Report_Requests].[dbo].[tblBU]
UNION ALL
SELECT 
	[id]
	,[BI_Name]
	,'[tblBusinessImpact]'
FROM 
	[CC_Report_Requests].[dbo].[tblBusinessImpact]
UNION ALL
SELECT 
	[id]
	,[Complexity_Name]
	,'[tblComplexity]'
FROM 
	[CC_Report_Requests].[dbo].[tblComplexity]
UNION ALL
SELECT 
	[id]
	,[DS_Name]
	,'[tblDataSource]'
FROM 
	[CC_Report_Requests].[dbo].[tblDataSource]
UNION ALL
SELECT 
	[id]
	,[FA_Name]
	,'[tblFunctionalArea]'
FROM 
	[CC_Report_Requests].[dbo].[tblFunctionalArea]
UNION ALL
SELECT 
	[id]
	,[Priority_Name]
	,'[tblPriority]'
FROM 
	[CC_Report_Requests].[dbo].[tblPriority]
UNION ALL
SELECT 
	[id]
	,[RT_Name]
	,'[tblRequestType]'
FROM 
	[CC_Report_Requests].[dbo].[tblRequestType]
UNION ALL
SELECT 
	[id]
	,[Status_Name]
	,'[tblStatus]'
FROM 
	[CC_Report_Requests].[dbo].[tblStatus]
UNION ALL
SELECT 
	[id]
	,[WE_Name]
	,'[tblWE]'
FROM 
	[CC_Report_Requests].[dbo].[tblWE]
UNION ALL
SELECT 
	[id]
	,[TacticalProjectFlag]
	,'[tblTacticalProject]'
FROM 
	[CC_Report_Requests].[dbo].[tblTacticalProject]

END
GO
/****** Object:  StoredProcedure [dbo].[uspGetAttachedFileValue]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetAttachedFileValue]
@requestId int,
@fileName varchar(255)
AS
BEGIN
	SET NOCOUNT ON;

SELECT
	A.[File_Value]
FROM
	[dbo].[tblAttachements] A
WHERE
	A.[Request_id]=@requestId
	AND A.File_Info=@fileName

END
GO
/****** Object:  StoredProcedure [dbo].[uspGetAttachementsById]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetAttachementsById]
@id int
AS
BEGIN
	SET NOCOUNT ON;

SELECT 
	[File_Info]
FROM 
	[CC_Report_Requests].[dbo].[tblAttachements]
where
	Request_id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[uspGetIsTaskApproved]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetIsTaskApproved]
@requestId int,
@IsTaskApproved bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

SET @IsTaskApproved = 0;

IF
	(
	SELECT 
		COUNT(id)
	FROM
		[dbo].[tblRequests] R
	WHERE
		R.id=@requestId
		AND R.[Approved]=1
	)>0
SET @IsTaskApproved = 1;


END
GO
/****** Object:  StoredProcedure [dbo].[uspGetReportArea]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetReportArea]
@id int
AS
BEGIN

	SET NOCOUNT ON;

SELECT 
	[id]
	,[RA_Name]
	,'tblReportArea'
	
FROM
	[CC_Report_Requests].[dbo].[tblReportArea]
WHERE
	[DataSource_id]=@id
END
GO
/****** Object:  StoredProcedure [dbo].[uspGetRequestById]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetRequestById]
@id int
AS
BEGIN

	SET NOCOUNT ON;
SELECT
		[id]
		,[RequestGuid]
		,[RequestName]
		,[RequestDescription]
		,[Requestor]
		,[BU_id]
		,[FunctionalArea_id]
		,[BusinessOwner]
		,[WE_id]
		,[RequestType_id]
		,[LinkToExistingReport]
		,[DataSource_id]
		,[ReportArea_id]
		,[LinkToExternalDataSource]
		,[BusinessImpact_id]
		,[BusinessImpactDescription]
		,[TacticalProject_id]
		,[TacticalProjectName]
		,[DesiredDueDate]
		,[Status_id]
		,[Priority_id]
		,[Complexity_id]
		,[DevelopmentHours]
		,[AssignedTo]
		,[OnHold]
		,[HoldDescription]
		,[PromiseDate]
		,[StartDate]
		,[UserTestStart]
		,[UserTestEnd]
		,[CancelledDate]
		,[CompletionDate]
		,[CreatedDate]
		,[CreatedBy]
		,[Approved]
FROM 
		[CC_Report_Requests].[dbo].[tblRequests]
WHERE
		[id] = @id
END
GO
/****** Object:  StoredProcedure [dbo].[uspGetRequestList]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetRequestList]
@filterName varchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;

DECLARE 
	@qry varchar(2000),
	@whr varchar(255)

-- the full list of filter values see in tblRequestTypes_forHomePage
if @filterName='emptyAssigned'
	SET @whr = ' WHERE [AssignedTo] IS NULL'

if @filterName='openOnly'
	SET @whr = ' WHERE [CompletionDate] IS NULL AND [CancelledDate] IS NULL'

if @filterName='overdue'
	SET @whr = ' WHERE DATEDIFF(day, [PromiseDate], GETDATE())>0'




SET @qry = 
'
SELECT  
	  R.[id]
      ,[RequestName]
      ,REPLACE([Requestor], ''@emerson.com'', '''') as [Requestor]
	  ,REPLACE([BusinessOwner], ''@emerson.com'', '''') as [BusinessOwner]
      ,BI.BI_Name
      ,TP.TacticalProjectFlag
      ,[DesiredDueDate]
      ,P.Priority_Name
	  ,REPLACE([AssignedTo], ''@emerson.com'', '''') as [AssignedTo]
      ,[OnHold]
      ,[PromiseDate]
	  ,[Approved]
	  ,[CreatedDate]
  FROM 
	[CC_Report_Requests].[dbo].[tblRequests] R
	LEFT JOIN [dbo].[tblBusinessImpact] BI ON R.BusinessImpact_id=BI.id
	LEFT JOIN [dbo].[tblTacticalProject] TP ON R.TacticalProject_id=TP.id
	LEFT JOIN [dbo].[tblPriority] P ON R.Priority_id=P.id
  '+ISNULL(@whr, '')+
  ' ORDER BY id desc'

PRINT @qry
EXECUTE (@qry)

END
GO
/****** Object:  StoredProcedure [dbo].[uspGetRequestTypeForFilter]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetRequestTypeForFilter]
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
      [RequestType]
      ,[NameForSearch]
  FROM 
	[CC_Report_Requests].[dbo].[tblFilters_forHomePage]
END
GO
/****** Object:  StoredProcedure [dbo].[uspGetSelectedReportArea]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetSelectedReportArea]
@requestid int,
@selectedReportArea int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		@selectedReportArea = R.ReportArea_id
	FROM
		[dbo].[tblRequests] R
	WHERE
		R.id=@requestid


END
GO
/****** Object:  StoredProcedure [dbo].[uspGetTaskExecutor]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetTaskExecutor]
@requestGuid varchar(50),
@taskExecutor varchar(255) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		@taskExecutor = R.AssignedTo
	FROM
		[dbo].[tblRequests] R
	WHERE
		R.RequestGuid=@requestGuid


END
GO
/****** Object:  StoredProcedure [dbo].[uspSaveAttachements]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSaveAttachements]
	
	@attached_Files AttachedFiles READONLY,
	@sessionGuid varchar(50),
	@ErrorMessage varchar(2000) OUTPUT
AS
BEGIN
DECLARE
@ErrorSeverity  tinyint,
@ErrorState     tinyint

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON;
	
--========Delete previously saved files	during this SessionGuid==----
	DELETE
	FROM
		[CC_Report_Requests].[dbo].[tblAttachements]
	WHERE
		SessionGuid = @sessionGuid
	
--========End Delete previously saved files	during this SessionGuid==----


	INSERT INTO 
		[CC_Report_Requests].[dbo].[tblAttachements]
		( 
		File_Info,
		File_Value,
		SessionGuid,
		AttachementWritenAt
		)
	SELECT
		File_Info,
		File_Value,
		@sessionGuid,
		GETDATE()
	FROM
		@attached_Files

	SET @ErrorMessage  = 'No Errors'
	COMMIT
END TRY

BEGIN CATCH
    SET @ErrorMessage  = ERROR_MESSAGE()
--RollBack copy for previous archive---------------- 	
	IF @@TRANCOUNT > 0
        ROLLBACK
--END RollBack copy for previous archive---------------- 	

END CATCH  	
END

GO
/****** Object:  StoredProcedure [dbo].[uspSaveEditedRequest]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSaveEditedRequest]
	
	@id int,
	@sessionGuid varchar(50),

	@requestName varchar(255),
	@requestDescription varchar(1000),
	@requestor varchar(255),
	@bU_id int,
	@functionalArea_id int,
	@businessOwner varchar(255),
	@wE_id int,
	@requestType_id int,
	@linkToExistingReport varchar(1000),
	@dataSource_id int,
	@reportArea_id int,
	@linkToExternalDataSource varchar(1000),
	@businessImpact_id int,
	@businessImpactDescription varchar(1000),
	@tacticalProject_id int,
	@tacticalProjectName varchar(255),
	@desiredDueDate date,
	
	@status_id int,
	@priority_id int,
	@complexity_id int,
	@developmentHours int,
	@assignedTo varchar(255),
	@onHold bit,
	@holdDescription varchar(500),
	@promiseDate date,
	@startDate date,
	@userTestStart date,
	@userTestEnd date,
	@cancelledDate date,
	@completionDate date,
	--@createdDate date,
	--@createdBy varchar(255),
	@approved bit,
	
	@ErrorMessage varchar(2000) OUTPUT
AS
BEGIN
DECLARE
@ErrorSeverity  tinyint,
@ErrorState     tinyint

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON;
		
UPDATE
	R
SET
	[RequestName]=@RequestName,
	[RequestDescription]=@RequestDescription,
	[Requestor]=@Requestor,
	[BU_id]=@BU_id,
	[FunctionalArea_id]=@FunctionalArea_id,
	[BusinessOwner]=@BusinessOwner,
	[WE_id]=@WE_id,
	[RequestType_id]=@RequestType_id,
	[LinkToExistingReport]=@LinkToExistingReport,
	[DataSource_id]=@DataSource_id,
	[ReportArea_id]=@ReportArea_id,
	[LinkToExternalDataSource]=@LinkToExternalDataSource,
	[BusinessImpact_id]=@BusinessImpact_id,
	[BusinessImpactDescription]=@BusinessImpactDescription,
	[TacticalProject_id]=@TacticalProject_id,
	[TacticalProjectName]=@TacticalProjectName,
	[DesiredDueDate]=@DesiredDueDate,
	[Status_id]=@Status_id,
	[Priority_id]=@Priority_id,
	[Complexity_id]=@Complexity_id,
	[DevelopmentHours]=@DevelopmentHours,
	[AssignedTo]=@AssignedTo,
	[OnHold]=@OnHold,
	[HoldDescription]=@HoldDescription,
	[PromiseDate]=@PromiseDate,
	[StartDate]=@StartDate,
	[UserTestStart]=@UserTestStart,
	[UserTestEnd]=@UserTestEnd,
	[CancelledDate]=@CancelledDate,
	[CompletionDate]=@CompletionDate,
	--[CreatedDate]=@CreatedDate,
	--[CreatedBy]=@CreatedBy,
	--[Approved]=@Approved,
	[LastChangesDate]=GETDATE()
FROM
	[dbo].[tblRequests] R
WHERE
	R.id=@id

--===Update Title ID to attachements table===============
UPDATE
	A
SET
	A.Request_id=@id
FROM
	[dbo].[tblAttachements] A
WHERE
	A.SessionGuid=@sessionGuid
--===End Update Title ID to attachements table===============

--=== Clean attachements table: delete records with no parents and older than 1 hour=====
Exec uspCleanAttachementsTable
--=== End Clean attachements table: delete records with no parents and older than 1 hour=====


	SET @ErrorMessage  = 'No Errors'
	
	COMMIT
END TRY

BEGIN CATCH
    SET @ErrorMessage  = ERROR_MESSAGE()
--RollBack copy for previous archive---------------- 	
	IF @@TRANCOUNT > 0
        ROLLBACK
--END RollBack copy for previous archive---------------- 	

END CATCH  	
END

GO
/****** Object:  StoredProcedure [dbo].[uspSaveNewRequest]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSaveNewRequest]
	
	@requestName varchar(255),
	@requestDescription varchar(1000),
	@requestor varchar(255),
	@bU_id int,
	@functionalArea_id int,
	@businessOwner varchar(255),
	@wE_id int,
	@requestType_id int,
	@linkToExistingReport varchar(1000),
	@dataSource_id int,
	@reportArea_id int,
	@linkToExternalDataSource varchar(1000),
	@businessImpact_id int,
	@businessImpactDescription varchar(1000),
	@tacticalProject_id int,
	@tacticalProjectName varchar(255),
	@desiredDueDate date,
	@sessionGuid varchar(50),
	
	@ErrorMessage varchar(2000) OUTPUT,
	@record_id int OUTPUT
AS
BEGIN
DECLARE
@ErrorSeverity  tinyint,
@ErrorState     tinyint

BEGIN TRY
	BEGIN TRANSACTION
		SET NOCOUNT ON;
		

--===Insert Title information========================
		
		INSERT INTO 
			[CC_Report_Requests].[dbo].[tblRequests] 
			( [RequestName]
			  ,[RequestDescription]
			  ,[Requestor]
			  ,[BU_id]
			  ,[FunctionalArea_id]
			  ,[BusinessOwner]
			  ,[WE_id]
			  ,[RequestType_id]
			  ,[LinkToExistingReport]
			  ,[DataSource_id]
			  ,[ReportArea_id]
			  ,[LinkToExternalDataSource]
			  ,[BusinessImpact_id]
			  ,[BusinessImpactDescription]
			  ,[TacticalProject_id]
			  ,[TacticalProjectName]
			  ,[DesiredDueDate]
			  ,[CreatedDate]
			  ,[RequestGuid]
			 )
		SELECT
				@RequestName, 
				@RequestDescription, 
				@Requestor, 
				@BU_id, 
				@FunctionalArea_id, 
				@BusinessOwner, 
				@WE_id, 
				@RequestType_id, 
				@LinkToExistingReport, 
				@DataSource_id, 
				@ReportArea_id, 
				@LinkToExternalDataSource, 
				@BusinessImpact_id, 
				@BusinessImpactDescription, 
				@TacticalProject_id, 
				@TacticalProjectName, 
				@DesiredDueDate ,
				GETDATE(),
				@sessionGuid

SET @record_id = @@IDENTITY

--===End Insert Title information========================

--===Update Title ID to attachements table===============
UPDATE
	A
SET
	A.Request_id=@record_id
FROM
	[dbo].[tblAttachements] A
WHERE
	A.SessionGuid=@sessionGuid
--===End Update Title ID to attachements table===============

--=== Clean attachements table: delete records with no parents and older than 1 hour=====
Exec uspCleanAttachementsTable
--=== End Clean attachements table: delete records with no parents and older than 1 hour=====

	SET @ErrorMessage  = 'No Errors'
	
	COMMIT
END TRY

BEGIN CATCH
    SET @ErrorMessage  = ERROR_MESSAGE()
--RollBack copy for previous archive---------------- 	
	IF @@TRANCOUNT > 0
        ROLLBACK
--END RollBack copy for previous archive---------------- 	

END CATCH  	
END

GO
/****** Object:  StoredProcedure [dbo].[uspSendEmail_ApproveTask]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSendEmail_ApproveTask]
@targetEmailAddress varchar(255),
@requestGuid varchar(50),
@taskNumber int,
@taskName varchar(50),
@taskDescription varchar (1000)
AS
BEGIN

	SET NOCOUNT ON;

declare @results varchar(max)
declare @subjectText varchar(max)
declare @databaseName VARCHAR(255)
SET @subjectText = 'Waiting for you Approval'
SET @results = '
Dear Sir/Madam. 

A task http://usstlaspengap54:8080/Home/EditRequest/'+CAST(@taskNumber as varchar(25)) +' is waiting for you Approval:
task title: '+ @taskName+'
task description: '+ @taskDescription+'

To approve this task, please click on link http://usstlaspengap54:8080/Home/ApproveRequest?requestGuid='+@requestGuid+' 

Best Wishes

p.s. This message was generated automatically. Please, do not reply on it. 
'
-- write the Trigger JOB
EXEC msdb.dbo.sp_send_dbmail
 @profile_name = 'SQL Mail',
 @recipients = @targetEmailAddress,
 @body = @results,
 @subject = @subjectText,
 @exclude_query_output = 1 --Suppress 'Mail Queued' message
END
GO
/****** Object:  StoredProcedure [dbo].[uspSendEmail_newTask]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspSendEmail_newTask]
@targetEmailAddress varchar(255)
AS
BEGIN

	SET NOCOUNT ON;

declare @results varchar(max)
declare @subjectText varchar(max)
declare @databaseName VARCHAR(255)
SET @subjectText = 'A new task was created'
SET @results = '
Dear Sir/Madam. 

A new task was created.
Please check  the task lisk on http://usstlaspengap54:8080/Home/Index

Best Wishes

p.s. This message was generated automatically. Please, do not reply on it. 
'
-- write the Trigger JOB
EXEC msdb.dbo.sp_send_dbmail
 @profile_name = 'SQL Mail',
 @recipients = @targetEmailAddress,
 @body = @results,
 @subject = @subjectText,
 @exclude_query_output = 1 --Suppress 'Mail Queued' message
END
GO
/****** Object:  StoredProcedure [dbo].[uspUpdateAttachements]    Script Date: 6/19/2023 2:13:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspUpdateAttachements]
	
	@id int, 
	@sessionGuid varchar(50),
	@ErrorMessage varchar(2000) OUTPUT
AS
BEGIN

BEGIN TRY
		SET NOCOUNT ON;
	
	UPDATE
		A
	SET
		A.Request_id=@id
	FROM
		[CC_Report_Requests].[dbo].[tblAttachements] A
	WHERE
		SessionGuid = @sessionGuid

	SET @ErrorMessage  = 'No Errors'
END TRY

BEGIN CATCH
    SET @ErrorMessage  = ERROR_MESSAGE()

END CATCH  	
END

GO
USE [master]
GO
ALTER DATABASE [CC_Report_Requests] SET  READ_WRITE 
GO

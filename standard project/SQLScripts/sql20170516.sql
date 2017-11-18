CREATE TABLE [dbo].[CoWorker](
	[cwID] [int] NOT NULL,
	[cwTypeID] [int] NULL,
	[cwRelatedID] [int] NULL,
	[cwOperatorID] [int] NULL,
	[cwAddOperatorID] [int] NULL,
	[cwAddDate] [datetime] NULL,
 CONSTRAINT [PK_Cooperate] PRIMARY KEY CLUSTERED 
(
	[cwID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CoWorker', @level2type=N'COLUMN',@level2name=N'cwTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CoWorker', @level2type=N'COLUMN',@level2name=N'cwRelatedID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'协作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CoWorker', @level2type=N'COLUMN',@level2name=N'cwOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CoWorker', @level2type=N'COLUMN',@level2name=N'cwAddOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CoWorker', @level2type=N'COLUMN',@level2name=N'cwAddDate'
GO


/****** Object:  Table [dbo].[BillComment]    Script Date: 05/16/2017 16:03:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BillComment](
	[bcID] [int] NOT NULL,
	[bcSourceID] [int] NULL,
	[bcContent] [varchar](500) NULL,
	[bcTypeID] [int] NULL,
	[bcOperatorID] [int] NULL,
	[bcDate] [datetime] NULL,
	[bcRemark] [varchar](500) NULL,
 CONSTRAINT [PK_BillComment] PRIMARY KEY CLUSTERED 
(
	[bcID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'来源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcSourceID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcContent'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcTypeID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BillComment', @level2type=N'COLUMN',@level2name=N'bcRemark'
GO


/****** Object:  Table [dbo].[CustomerClue]    Script Date: 05/16/2017 16:03:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CustomerClue](
	[ccID] [int] NOT NULL,
	[ccCustomerName] [varchar](200) NULL,
	[ccName] [varchar](50) NULL,
	[ccTel] [varchar](50) NULL,
	[ccMobile] [varchar](50) NULL,
	[ccAddress] [varchar](500) NULL,
	[ccStatusID] [int] NULL,
	[ccRemark] [text] NULL,
	[ccOperatorID] [int] NULL,
	[ccDepartmentID] [int] NULL,
	[ccAddOperatorID] [int] NULL,
	[ccAddDate] [datetime] NULL,
	[ccModifyOperatorID] [int] NULL,
	[ccModifyDate] [datetime] NULL,
	[ccCustomerID] [int] NULL,
 CONSTRAINT [PK_CustomerClue] PRIMARY KEY CLUSTERED 
(
	[ccID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccCustomerName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccTel'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccMobile'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccAddress'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccStatusID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccRemark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccDepartmentID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccAddOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccAddDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccModifyOperatorID'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccModifyDate'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联客户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CustomerClue', @level2type=N'COLUMN',@level2name=N'ccCustomerID'
GO

alter table CustomerFollowHistory add cfhClueID int null
go

alter table CustomerFollowPlan add cfpClueID int null
go

alter table CustomerFollowPlan add cfpFilePath varchar(500) null
go


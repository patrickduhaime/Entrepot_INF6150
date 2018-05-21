USE [EntrepotDB]
GO

/****** Object:  Table [dbo].[Sample]    Script Date: 2018-05-21 15:57:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sample](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SerialNumber] [nvarchar](50) NOT NULL,
	[Quantity] [int] NULL,
	[FK_Article] [int] NOT NULL,
	[FK_Location] [int] NULL,
 CONSTRAINT [PK_Sample] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY],
 CONSTRAINT [U_Sample_SerialNumber] UNIQUE (SerialNumber)
GO

ALTER TABLE [dbo].[Sample]  WITH NOCHECK ADD  CONSTRAINT [FK_Sample_Article] FOREIGN KEY([FK_Article])
REFERENCES [dbo].[Article] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Sample] CHECK CONSTRAINT [FK_Sample_Article]
GO

ALTER TABLE [dbo].[Sample]  WITH NOCHECK ADD  CONSTRAINT [FK_Sample_Location] FOREIGN KEY([Id])
REFERENCES [dbo].[Location] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Sample] CHECK CONSTRAINT [FK_Sample_Location]
GO


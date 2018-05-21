USE [EntrepotDB]
GO

/****** Object:  Table [dbo].[Location]    Script Date: 2018-05-21 15:43:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FK_Cell] [int] NOT NULL,
	[SerialNumber] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY],
 CONSTRAINT [U_Location_SerialNumber] UNIQUE (SerialNumber)
GO

ALTER TABLE [dbo].[Location]  WITH NOCHECK ADD  CONSTRAINT [FK_Location_Cell] FOREIGN KEY([FK_Cell])
REFERENCES [dbo].[Cell] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Cell]
GO


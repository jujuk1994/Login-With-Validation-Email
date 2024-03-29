USE [master]
GO
/****** Object:  Database [latihanLogin]    Script Date: 09-Oct-19 10:17:55 ******/
CREATE DATABASE [latihanLogin]

CREATE TABLE [dbo].[_Login](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IsValue] [bit] NULL,
 CONSTRAINT [PK__Login] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [latihanLogin] SET  READ_WRITE 
GO

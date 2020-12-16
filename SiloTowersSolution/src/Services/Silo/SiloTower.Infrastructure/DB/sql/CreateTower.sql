USE [SiloTower]
GO

/****** Object:  Table [dbo].[Tower]    Script Date: 16.12.2020 22:57:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tower](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WeightId] [int] NULL,
	[LevelId] [int] NULL,
 CONSTRAINT [PK_Tower] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tower]  WITH CHECK ADD  CONSTRAINT [FK_Tower_IndicatorValues_LevelId] FOREIGN KEY([LevelId])
REFERENCES [dbo].[IndicatorValues] ([Id])
GO

ALTER TABLE [dbo].[Tower] CHECK CONSTRAINT [FK_Tower_IndicatorValues_LevelId]
GO

ALTER TABLE [dbo].[Tower]  WITH CHECK ADD  CONSTRAINT [FK_Tower_IndicatorValues_WeightId] FOREIGN KEY([WeightId])
REFERENCES [dbo].[IndicatorValues] ([Id])
GO

ALTER TABLE [dbo].[Tower] CHECK CONSTRAINT [FK_Tower_IndicatorValues_WeightId]
GO

ALTER TABLE [dbo].[Tower]  WITH CHECK ADD  CONSTRAINT [CK_Tower] CHECK  (([Id]<(9)))
GO

ALTER TABLE [dbo].[Tower] CHECK CONSTRAINT [CK_Tower]
GO



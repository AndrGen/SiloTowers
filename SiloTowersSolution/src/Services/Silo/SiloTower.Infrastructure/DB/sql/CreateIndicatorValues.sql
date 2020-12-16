USE [SiloTower]
GO

/****** Object:  Table [dbo].[IndicatorValues]    Script Date: 16.12.2020 22:56:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IndicatorValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Value] [numeric](18, 0) NOT NULL,
	[MinValue] [numeric](18, 0) NOT NULL,
	[MaxValue] [numeric](18, 0) NOT NULL,
	[Type] [smallint] NULL,
	[Date] [datetime] NULL,
 CONSTRAINT [PK_WeightValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[IndicatorValues] ADD  CONSTRAINT [DF_IndicatorValues_Date]  DEFAULT (getdate()) FOR [Date]
GO

ALTER TABLE [dbo].[IndicatorValues]  WITH CHECK ADD  CONSTRAINT [FK_WeightValues_WeightValues] FOREIGN KEY([Id])
REFERENCES [dbo].[IndicatorValues] ([Id])
GO

ALTER TABLE [dbo].[IndicatorValues] CHECK CONSTRAINT [FK_WeightValues_WeightValues]
GO



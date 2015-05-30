-- Start checking possibility of using
DECLARE @newDbVersion      INT
DECLARE @expectedDbVersion INT
SET @newDbVersion = 5
SET @expectedDbVersion = 4

DECLARE @currentDbVersion  INT
SET @currentDbVersion = dbo.GetCurrentDbVersionAsString()

DECLARE @errorMessage NVARCHAR(500);

IF (@expectedDbVersion <> @currentDbVersion) OR (dbo.CompareDbVersionWithCurrent(@newDbVersion) <> 1)
BEGIN
    SET @errorMessage = 'Cannot install script ' + @newDbVersion + '. DB version ' + @currentDbVersion + ' expected.'
    RAISERROR(@errorMessage, 20, -1) WITH LOG
END
GO
-- End checking possibility of using
-- BEGIN CHANGES


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EconomyIndustrySectorQuantiles](
    [Category] [nvarchar](300) NOT NULL,
    [SubCategory] [nvarchar](300) NOT NULL,
    [Year] [int] NOT NULL,
    [pct_10] [float] NOT NULL,
    [pct_25] [float] NOT NULL,
    [pct_50] [float] NOT NULL,
    [pct_75] [float] NOT NULL,
    [pct_90] [float] NOT NULL,
    [Count] [int] NOT NULL,
    [DV] [nvarchar](100) NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ETL_EconomyIndustrySectorQuantiles]    Script Date: 01.09.2014 18:56:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ETL_EconomyIndustrySectorQuantiles](
    [sub_category] [nvarchar](300) NULL,
    [year] [int] NULL,
    [pct_10] [varchar](50) NULL,
    [pct_25] [varchar](50) NULL,
    [pct_50] [varchar](50) NULL,
    [pct_75] [varchar](50) NULL,
    [pct_90] [varchar](50) NULL,
    [count] [int] NULL,
    [dv] [nvarchar](100) NULL,
    [category] [nvarchar](300) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1988, -0.3513289264, -0.1316765575, 0.0104031209, 0.0602269662, 0.11260155114, 47, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1989, -0.4633419588, -0.143865374, -0.019947811, 0.03082591235, 0.12095992176, 43, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1990, -0.4193155723, -0.149482334, -0.0180420905, 0.026880299525, 0.0892153652, 42, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1991, -0.6525409782, -0.263636234, -0.026697211, 0.058972260775, 0.10422944688, 42, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1992, -0.3723837207, -0.21306755225, 0.0035756039, 0.038661364, 0.16928310952, 40, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1993, -0.1503459996, -0.04323227075, 0.01424269425, 0.06685245375, 0.12255104707, 44, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1994, -0.331006344, -0.09482035625, 0.0085676344, 0.07255439255, 0.11468469621, 48, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1995, -0.1856400294, -0.044218551, 0.0142431269, 0.0637583893, 0.15285542042, 57, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1996, -0.119574595, -0.0541165505, 0.0151975199, 0.063107380475, 0.12019964562, 60, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1997, -0.140271493, -0.044078085, 0.0124452409, 0.0549230349, 0.0982616382, 61, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1998, -0.3734272192, -0.0692923275, 0.01597368725, 0.063599623725, 0.10855392366, 62, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 1999, -0.7133536318, -0.1758164705, -0.026192069, 0.03340055965, 0.12777121774, 63, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 2000, -1.097653773, -0.406960858, -0.0521837465, 0.0607672323, 0.1669348078, 64, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 2001, -0.7184786069, -0.16417033175, -0.007378245, 0.064246752525, 0.15461322613, 58, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 2002, -0.8198583128, -0.224375044, -0.008879154, 0.095286725025, 0.18500712576, 48, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 2003, -0.4202365652, -0.043914613, 0.0202734559, 0.106473663, 0.23882084262, 45, N'"Return on Assets"')
INSERT [dbo].[EconomyIndustrySectorQuantiles] ([Category], [SubCategory], [Year], [pct_10], [pct_25], [pct_50], [pct_75], [pct_90], [Count], [DV]) VALUES (N'"Industry"', N'"PUBLIC SECTOR"', 2004, -0.504178273, -0.063660477, 0.0252025203, 0.093256059, 0.19627814, 41, N'"Return on Assets"')


-- END CHANGES

-- Start writing new version info 
GO
IF @@ERROR <> 0
BEGIN
    DECLARE @errorMessage VARCHAR(MAX)
    SET @errorMessage = ERROR_MESSAGE()
    RAISERROR(@errorMessage, 16, 1)
END
ELSE
BEGIN
    DECLARE @newDbVersion      INT
    SET @newDbVersion = 5
    BEGIN
        EXEC AppendDbVersionInfo @newDbVersion, 'Contents table was added to store dynamic text'
         
        PRINT 'Completed successfully'
    END
END
GO
-- End writing new version info
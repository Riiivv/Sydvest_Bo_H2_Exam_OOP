USE [Sydvest_Bo]
GO
/****** Object:  Table [dbo].[Ejer]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ejer](
	[EjerID] [int] IDENTITY(1,1) NOT NULL,
	[Navn] [nvarchar](100) NOT NULL,
	[Adresse] [nvarchar](255) NOT NULL,
	[Telefonnummer] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[EjerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inspektør]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inspektør](
	[InspektørID] [int] IDENTITY(1,1) NOT NULL,
	[Navn] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Telefon] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[InspektørID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lejlighed]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lejlighed](
	[LejlighedID] [int] IDENTITY(1,1) NOT NULL,
	[Adresse] [nvarchar](255) NOT NULL,
	[KompleksID] [int] NULL,
	[AntalVærelser] [int] NOT NULL,
	[PrisPrMåned] [decimal](10, 2) NOT NULL,
	[SæsonID] [int] NULL,
	[InspektørID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LejlighedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lejlighedskompleks]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lejlighedskompleks](
	[KompleksID] [int] IDENTITY(1,1) NOT NULL,
	[Navn] [nvarchar](100) NOT NULL,
	[Adresse] [nvarchar](255) NULL,
	[InspektørID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[KompleksID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Område]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Område](
	[OmrådeID] [int] IDENTITY(1,1) NOT NULL,
	[OmrådeNavn] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OmrådeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationID] [int] IDENTITY(1,1) NOT NULL,
	[LejlighedID] [int] NULL,
	[StartDato] [date] NOT NULL,
	[SlutDato] [date] NOT NULL,
	[Pris] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sommerhus]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sommerhus](
	[HusID] [int] IDENTITY(1,1) NOT NULL,
	[Adresse] [nvarchar](255) NOT NULL,
	[AntalSenge] [int] NOT NULL,
	[Standard] [nvarchar](50) NULL,
	[EjerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[HusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SæsonKategori]    Script Date: 30-01-2025 01:31:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SæsonKategori](
	[SæsonID] [int] IDENTITY(1,1) NOT NULL,
	[KategoriNavn] [nvarchar](100) NOT NULL,
	[UgeStart] [int] NOT NULL,
	[UgeSlut] [int] NOT NULL,
	[Pris] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SæsonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Ejer] ON 

INSERT [dbo].[Ejer] ([EjerID], [Navn], [Adresse], [Telefonnummer]) VALUES (1, N'Anders Jensen', N'Vesterbrogade 10, København', N'12345678')
INSERT [dbo].[Ejer] ([EjerID], [Navn], [Adresse], [Telefonnummer]) VALUES (2, N'Karin Østergaard', N'Løgstørvej 5, Aalborg', N'87654321')
SET IDENTITY_INSERT [dbo].[Ejer] OFF
GO
SET IDENTITY_INSERT [dbo].[Inspektør] ON 

INSERT [dbo].[Inspektør] ([InspektørID], [Navn], [Email], [Telefon]) VALUES (1, N'Maria Hansen', N'maria@mail.com', N'87654321')
INSERT [dbo].[Inspektør] ([InspektørID], [Navn], [Email], [Telefon]) VALUES (2, N'Lars Østergaard', N'lars@mail.com', N'23456789')
INSERT [dbo].[Inspektør] ([InspektørID], [Navn], [Email], [Telefon]) VALUES (3, N'1', N'1', N'1')
SET IDENTITY_INSERT [dbo].[Inspektør] OFF
GO
SET IDENTITY_INSERT [dbo].[Lejlighed] ON 

INSERT [dbo].[Lejlighed] ([LejlighedID], [Adresse], [KompleksID], [AntalVærelser], [PrisPrMåned], [SæsonID], [InspektørID]) VALUES (1, N'Lejlighed 101', 1, 3, CAST(7500.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[Lejlighed] ([LejlighedID], [Adresse], [KompleksID], [AntalVærelser], [PrisPrMåned], [SæsonID], [InspektørID]) VALUES (2, N'Lejlighed 202', 1, 2, CAST(6000.00 AS Decimal(10, 2)), NULL, 1)
INSERT [dbo].[Lejlighed] ([LejlighedID], [Adresse], [KompleksID], [AntalVærelser], [PrisPrMåned], [SæsonID], [InspektørID]) VALUES (3, N'Lejlighed 301', 2, 4, CAST(8500.00 AS Decimal(10, 2)), NULL, 2)
SET IDENTITY_INSERT [dbo].[Lejlighed] OFF
GO
SET IDENTITY_INSERT [dbo].[Lejlighedskompleks] ON 

INSERT [dbo].[Lejlighedskompleks] ([KompleksID], [Navn], [Adresse], [InspektørID]) VALUES (1, N'Havneparken', N'55', 1)
INSERT [dbo].[Lejlighedskompleks] ([KompleksID], [Navn], [Adresse], [InspektørID]) VALUES (2, N'Bycenteret', N'Centergade 5, Aarhus', 2)
INSERT [dbo].[Lejlighedskompleks] ([KompleksID], [Navn], [Adresse], [InspektørID]) VALUES (3, N'1', N'1', 1)
INSERT [dbo].[Lejlighedskompleks] ([KompleksID], [Navn], [Adresse], [InspektørID]) VALUES (4, N'1', N'1', 1)
INSERT [dbo].[Lejlighedskompleks] ([KompleksID], [Navn], [Adresse], [InspektørID]) VALUES (5, N'as', N'asdw1', 2)
SET IDENTITY_INSERT [dbo].[Lejlighedskompleks] OFF
GO
SET IDENTITY_INSERT [dbo].[Område] ON 

INSERT [dbo].[Område] ([OmrådeID], [OmrådeNavn]) VALUES (1, N'Nordjylland')
INSERT [dbo].[Område] ([OmrådeID], [OmrådeNavn]) VALUES (2, N'Sydjylland')
INSERT [dbo].[Område] ([OmrådeID], [OmrådeNavn]) VALUES (3, N'Sjælland')
INSERT [dbo].[Område] ([OmrådeID], [OmrådeNavn]) VALUES (4, N'Bornholm')
SET IDENTITY_INSERT [dbo].[Område] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([ReservationID], [LejlighedID], [StartDato], [SlutDato], [Pris]) VALUES (1, 1, CAST(N'2025-07-01' AS Date), CAST(N'2025-07-15' AS Date), CAST(12000.00 AS Decimal(10, 2)))
INSERT [dbo].[Reservation] ([ReservationID], [LejlighedID], [StartDato], [SlutDato], [Pris]) VALUES (2, 2, CAST(N'2025-08-01' AS Date), CAST(N'2025-08-10' AS Date), CAST(12000.00 AS Decimal(10, 2)))
INSERT [dbo].[Reservation] ([ReservationID], [LejlighedID], [StartDato], [SlutDato], [Pris]) VALUES (4, 1, CAST(N'2025-03-10' AS Date), CAST(N'2025-03-20' AS Date), CAST(12000.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Reservation] OFF
GO
SET IDENTITY_INSERT [dbo].[Sommerhus] ON 

INSERT [dbo].[Sommerhus] ([HusID], [Adresse], [AntalSenge], [Standard], [EjerID]) VALUES (1, N'Skagenvej 12, Skagen', 6, N'Høj', 1)
INSERT [dbo].[Sommerhus] ([HusID], [Adresse], [AntalSenge], [Standard], [EjerID]) VALUES (2, N'Fanøvej 7, Fanø', 4, N'Mellem', 2)
SET IDENTITY_INSERT [dbo].[Sommerhus] OFF
GO
SET IDENTITY_INSERT [dbo].[SæsonKategori] ON 

INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (1, N'Højsæson', 25, 32, CAST(12000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (2, N'Lavsæson', 1, 24, CAST(8000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (3, N'Mellemperiode', 33, 52, CAST(10000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (4, N'Forår', 10, 20, CAST(5000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (5, N'Vinter', 1, 9, CAST(4000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (6, N'Forår', 10, 20, CAST(5000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (7, N'Sommer', 21, 35, CAST(8000.00 AS Decimal(10, 2)))
INSERT [dbo].[SæsonKategori] ([SæsonID], [KategoriNavn], [UgeStart], [UgeSlut], [Pris]) VALUES (8, N'Efterår', 36, 52, CAST(4500.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[SæsonKategori] OFF
GO
ALTER TABLE [dbo].[Lejlighed]  WITH CHECK ADD FOREIGN KEY([InspektørID])
REFERENCES [dbo].[Inspektør] ([InspektørID])
GO
ALTER TABLE [dbo].[Lejlighed]  WITH CHECK ADD FOREIGN KEY([KompleksID])
REFERENCES [dbo].[Lejlighedskompleks] ([KompleksID])
GO
ALTER TABLE [dbo].[Lejlighedskompleks]  WITH CHECK ADD FOREIGN KEY([InspektørID])
REFERENCES [dbo].[Inspektør] ([InspektørID])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([LejlighedID])
REFERENCES [dbo].[Lejlighed] ([LejlighedID])
GO
ALTER TABLE [dbo].[Sommerhus]  WITH CHECK ADD FOREIGN KEY([EjerID])
REFERENCES [dbo].[Ejer] ([EjerID])
GO

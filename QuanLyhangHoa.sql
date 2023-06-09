USE [QuanLyHangHoa]
GO
/****** Object:  Table [dbo].[HangHoa]    Script Date: 25/04/2023 7:17:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HangHoa](
	[MaHangHoa] [nvarchar](5) NOT NULL,
	[TenHangHoa] [nvarchar](50) NULL,
	[SoLuong] [int] NULL,
	[DonGia] [float] NULL,
	[MaNCC] [nvarchar](5) NULL,
 CONSTRAINT [PK__HangHoa__9737FBA80016D90D] PRIMARY KEY CLUSTERED 
(
	[MaHangHoa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 25/04/2023 7:17:35 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [nvarchar](5) NOT NULL,
	[TenNCC] [nvarchar](50) NULL,
 CONSTRAINT [PK__NhaCungC__3A185DEB4626D6FF] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[HangHoa] ([MaHangHoa], [TenHangHoa], [SoLuong], [DonGia], [MaNCC]) VALUES (N'HH01', N'Dầu ăn', 2, 50000, N'NCC01')
INSERT [dbo].[HangHoa] ([MaHangHoa], [TenHangHoa], [SoLuong], [DonGia], [MaNCC]) VALUES (N'HH02', N'Coca', 13, 18000, N'NCC03')
INSERT [dbo].[HangHoa] ([MaHangHoa], [TenHangHoa], [SoLuong], [DonGia], [MaNCC]) VALUES (N'HH03', N'7 Up', 7, 17000, N'NCC02')
INSERT [dbo].[HangHoa] ([MaHangHoa], [TenHangHoa], [SoLuong], [DonGia], [MaNCC]) VALUES (N'HH05', N'Coca', 10, 180000, N'NCC01')
GO
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC01', N'Nhà cung cấp 1')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC02', N'Nhà cung cấp 2')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC]) VALUES (N'NCC03', N'Nhà cung cấp 3')
GO
ALTER TABLE [dbo].[HangHoa]  WITH CHECK ADD  CONSTRAINT [FK_HangHoa_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[HangHoa] CHECK CONSTRAINT [FK_HangHoa_NhaCungCap]
GO




USE master
GO

CREATE DATABASE QUANLY_KHACHSACN_LAN1
ON PRIMARY
(
	NAME = QUANLY_KHACHSACN_LAN1_PRIMARY,
	FILENAME = 'D:\Năm 3\CNN\Đồ Án\QUANLY_KHACHSACN_LAN1_PRIMARY.mdf',
	size = 10mb,
	maxsize = 50mb,
	filegrowth = 10%
)
LOG ON
(
	NAME = QUANLY_KHACHSACN_LAN1_LOG,
	FILENAME = 'D:\Năm 3\CNN\Đồ Án\QUANLY_KHACHSACN_LAN1_LOG.ldf',
	size = 5mb,
	maxsize = 25mb,
	filegrowth = 10%
)
GO

USE QUANLY_KHACHSACN_LAN1
GO

-- DICHVU --
CREATE TABLE DICHVU 
(
	MA_DV                int				IDENTITY(1,1),
	TEN_DV               nvarchar(50)		not null	unique,
	GIA_DV               decimal(19,2)		null		check(GIA_DV > 0),
	constraint PK_DICHVU primary key (MA_DV)
)
GO

-- KHACHHANG --
CREATE TABLE KHACHHANG 
(
	MA_KH               int					IDENTITY(1,1),
	HOTEN_KH            nvarchar(50)        not null,
	DIACHI_KH			nvarchar(50)		null		default'Chưa xác nhận',
	CCCD_KH             varchar(12)         not null	check(LEN(CCCD_KH) = 12),
	SDT_KH              varchar(11)         not null	check(LEN(SDT_KH) >= 10),
	GIOITINH			nvarchar(10)		not null	check(GIOITINH in(N'Nam', N'Nữ')),
	constraint PK_KHACHHANG primary key (MA_KH)
)
GO

-- NHANVIEN --
CREATE TABLE NHANVIEN 
(
	TENDANGNHAP         varchar(10),
	HOTEN_NV            nvarchar(50)        not null,
	DIACHI_NV           nvarchar(50)        null		default N'Chưa xác nhận',
	SDT_NV              varchar(11)         not null	check(LEN(SDT_NV) >= 10),
	CCCD_NV				varchar(12)         not null	check(LEN(CCCD_NV) = 12),
	MATKHAU				varchar(15)			not null	check(LEN(MATKHAU) >= 8 and MATKHAU like '%[0-9]%' and MATKHAU like '%[A-Z]%')	unique,
	CHUCVU				nvarchar(15)		null		check(CHUCVU IN(N'Quản lý', N'Nhân viên')) default N'Nhân viên',
	constraint PK_NHANVIEN primary key (TENDANGNHAP)
)
GO

-- LOAIPHONG --
CREATE TABLE LOAIPHONG
(
	MALOAI_P			int					IDENTITY(1,1),
	TENLOAI_P			nvarchar(20)		not null	unique,	
	GIA					decimal(19,2)		null		check(GIA > 0),
	MOTA				nvarchar(80)		null,
	constraint PK_LOAIPHONG primary key (MALOAI_P)
)

-- PHONG --
CREATE TABLE PHONG 
(
	MA_P				int					IDENTITY(1,1),
	TENPHONG			varchar(10)			not null	unique	check(LEFT(TENPHONG, 1) in('A', 'B', 'C')),
	MALOAI_P			int					not null,
	TINHTRANG_P         nvarchar(15)		null		check(TINHTRANG_P in(N'Còn trống', N'Đã được đặt'))	default N'Còn trống',
	SOKHACHTOIDA		int					not null	check(SOKHACHTOIDA between 1 and 6),
	constraint PK_PHONG primary key (MA_P)
)
GO

-- PHIEUTHUEPHONG --
CREATE TABLE PHIEUTHUEPHONG
(
	MA_PTP				int					IDENTITY(1,1),
	MA_P				int					not null,
	MA_KH				int					not null,
	SONGUOI				int					null		check(SONGUOI between 1 and 6),
	NGAYDEN				date				not null	check(NGAYDEN >= getdate())		default getdate(),
	TINHTRANG_PTP		nvarchar(15)		null		check(TINHTRANG_PTP in(N'Đã vào ở', N'Chưa vào ở'))	default N'Chưa vào ở',
	constraint PK_PHIEUTHUEPHONG primary key (MA_PTP)
)
GO

-- SUDUNGDV --
CREATE TABLE SUDUNGDV 
(
	MA_SD				int					IDENTITY(1,1),
	TONGTIEN_DV			decimal(19,2)		null		check(TONGTIEN_DV > 0),
	SOLUONG				int					null		check(SOLUONG > =0)	default 1,
	NGAYSUDUNG			date				not null,
	TINHTRANG_DV		nvarchar(15)		null		check(TINHTRANG_DV in(N'Đã sử dụng', N'Chưa sử dụng'))	default N'Chưa sử dụng',
	MA_DV				int					not null,
	MA_PTP				int					not null,
	constraint PK_SUDUNGDV primary key (MA_SD)
)
GO

---- CHITIET_SDDV --
--CREATE TABLE CHITIET_SDDV
--(
--	MA_SD				int,					
--	MA_DV				int,
--	NGAYSUDUNG_DV		date				not null	check(NGAYSUDUNG_DV >= GETDATE()),
--	SOLANDUNG_DV		int					null		check(SOLANDUNG_DV >= 1),
--	THANHTIEN_DV		decimal(19,2)		null		check(THANHTIEN_DV > 0),
--	constraint PK_CHITIET_SDDV primary key (MA_SD, MA_DV)
--)
--GO

-- HOADON --
CREATE TABLE HOADON 
(
   MA_HD                int					IDENTITY(1,1),
   MA_PTP				int					not null	unique,			
   MA_SD				int					null,
   --MA_P				int					not null,
   --MA_KH				int					not null,
   TENDANGNHAP			varchar(10)			not null,
   SONGAYTHUE			int					not null	check(SONGAYTHUE > 0),
   NGAYTHANHTOAN_HD		date				null,
   TONGTIEN_HD			decimal(19,2)		null		check(TONGTIEN_HD > 0),
   TINHTRANG_HD			nvarchar(15)		null		check(TINHTRANG_HD in(N'Đã thanh toán', N'Chưa thanh toán'))	default N'Chưa thanh toán',
   constraint PK_HOADON primary key (MA_HD)
)
GO

/*------------------------------------------------------------------------------------*/
/*---------------------------------- TAO KHOA NGOAI ----------------------------------*/
/*------------------------------------------------------------------------------------*/
-- PHONG --
ALTER TABLE PHONG
ADD constraint FK_PHONG_CO_LOAIPHONG foreign key (MALOAI_P) references LOAIPHONG (MALOAI_P)
GO

-- PHIEUTHUEPHONG --
ALTER TABLE PHIEUTHUEPHONG
ADD constraint FK_PHIEUTHUEPHONG_THUOC_KHACHHANG foreign key (MA_KH) references KHACHHANG (MA_KH),
	constraint FK_PHIEUTHUOCPHONG_CO_PHONG foreign key (MA_P) references PHONG (MA_P)
GO

-- SUDUNGDV --
ALTER TABLE SUDUNGDV
ADD constraint FK_SUDUNGDV_THUOC_PHIEUTHUEPHONG foreign key (MA_PTP) references PHIEUTHUEPHONG (MA_PTP),
	constraint FK_SUDUNGDV_THUOC_DICHVU foreign key (MA_DV) references DICHVU (MA_DV)
GO

---- CHITIET_SDDV --
--ALTER TABLE CHITIET_SDDV
--ADD constraint FK_CHITIET_SDDV_THUOC_SUDUNGDV foreign key (MA_SD) references SUDUNGDV (MA_SD),
--	constraint FK_CHITIET_SDDV_THUOC_DICHVU foreign key (MA_DV) references DICHVU (MA_DV)
--GO

-- HOADON --
ALTER TABLE HOADON
ADD constraint FK_HOADON_CO_PHIEUTHUEPHONG foreign key (MA_PTP) references PHIEUTHUEPHONG (MA_PTP),
	--constraint FK_HOADON_CO_PHIEUTHUEPHONG2 foreign key (MA_KH) references PHIEUTHUEPHONG (MA_KH),
	--constraint FK_HOADON_CO_PHIEUTHUEPHONG3 foreign key (MA_P) references PHIEUTHUEPHONG (MA_P),
	constraint FK_HOADON_CO_NHANVIEN foreign key (TENDANGNHAP) references NHANVIEN (TENDANGNHAP),
	constraint FK_HOADON_CO_SUDUNGDV foreign key (MA_SD) references SUDUNGDV (MA_SD)
GO

/*------------------------------------------------------------------------------------*/
/*------------------------------ TAO RANG BUOC PHUC TAP ------------------------------*/
/*------------------------------------------------------------------------------------*/
----- CAC LOI CAN SUA ------
--1/ TAO TRIGGER KIEM TRA NGAYTHANHTOAN_HD >= NGAYDEN AND NGAYTHANHTOAN_HD <= (NGAYDEN + SONGAYTHUE)
--3/ 1 KHACH HANG CHI CO 1 PHIEU THUE PHONG ?? SAI
--4/ TINHTRANG_PTP = DA VAO O KHI NGAYDEN = GETDATE()
--5/ Ngaythanhtoan default = ngayden + songayo
--6/ 1 PHONG CO NHIEU KHACH THUE PHONG NHUNG PHAI KHAC NGAY DEN VA < NGAYDEN + SONGAYO CUA
--7/ masd thuoc ve hoa don co phong do
----- NV DA HOAN THANH -----
--1/ UPDATE DEFAULT CHO NGAYTHANHTOAN_HD
CREATE TRIGGER DF_NGAYTHANHTOAN_HD
ON HOADON
FOR insert, update
AS
BEGIN
	UPDATE HOADON SET NGAYTHANHTOAN_HD= (SELECT DATEADD(DAY, SONGAYTHUE, NGAYDEN) 
												FROM PHIEUTHUEPHONG, inserted HD, PHONG
												WHERE PHIEUTHUEPHONG.MA_PTP = HD.MA_PTP
													and PHONG.MA_P = PHIEUTHUEPHONG.MA_P)
	WHERE HOADON.MA_PTP = (SELECT inserted.MA_PTP FROM inserted)
END
GO

--2/ CHECK SONGUOI DAT PHONG <= SOKHACHTOIDA CUA PHONG DO
CREATE TRIGGER CK_SONGUOIO
ON PHIEUTHUEPHONG
FOR insert, update
AS
BEGIN
	IF (SELECT SONGUOI FROM inserted) <= (SELECT SOKHACHTOIDA 
											FROM PHONG, inserted
											WHERE PHONG.MA_P = inserted.MA_P)
		COMMIT TRAN
	ELSE
	BEGIN
		ROLLBACK TRAN
		PRINT N'Số người ở không hợp lệ!'
	END
END
GO

--5/ DELETE LOAIPHONG
CREATE TRIGGER DEL_LOAIPHONG
ON LOAIPHONG
INSTEAD OF DELETE
AS
BEGIN
	-- XOA HOA DON
	DELETE HOADON WHERE HOADON.MA_PTP IN(SELECT HOADON.MA_PTP
										FROM deleted, PHONG, PHIEUTHUEPHONG, HOADON
										WHERE deleted.MALOAI_P = PHONG.MALOAI_P
											and PHONG.MA_P = PHIEUTHUEPHONG.MA_P
											and HOADON.MA_PTP = PHIEUTHUEPHONG.MA_PTP
										GROUP BY HOADON.MA_PTP)
	-- XOA PHIEU THUE PHONG
	DELETE PHIEUTHUEPHONG WHERE PHIEUTHUEPHONG.MA_P IN(SELECT PHIEUTHUEPHONG.MA_P
															FROM deleted, PHONG, PHIEUTHUEPHONG
															WHERE deleted.MALOAI_P = PHONG.MALOAI_P
																	and PHONG.MA_P = PHIEUTHUEPHONG.MA_P
															GROUP BY PHIEUTHUEPHONG.MA_P)
	-- XOA CHI TIET SU DUNG DV
	DELETE CHITIET_SDDV WHERE CHITIET_SDDV.MA_SD IN(SELECT CHITIET_SDDV.MA_SD
													FROM SUDUNGDV, PHONG, deleted, CHITIET_SDDV
													WHERE
														 PHONG.MA_P = SUDUNGDV.MA_P
														and deleted.MALOAI_P = PHONG.MALOAI_P
														and CHITIET_SDDV.MA_SD = SUDUNGDV.MA_SD
													GROUP BY CHITIET_SDDV.MA_SD)
	-- XOA SU DUNG DV
	DELETE SUDUNGDV WHERE SUDUNGDV.MA_P IN(SELECT PHONG.MA_P
											FROM SUDUNGDV, PHONG, deleted
											WHERE PHONG.MA_P = SUDUNGDV.MA_P
												and deleted.MALOAI_P = PHONG.MALOAI_P
											GROUP BY PHONG.MA_P)
	-- XOA PHONG
	DELETE PHONG WHERE PHONG.MALOAI_P IN(SELECT PHONG.MALOAI_P 
											FROM deleted, PHONG
											WHERE deleted.MALOAI_P = PHONG.MALOAI_P
											GROUP BY PHONG.MALOAI_P )
	-- XOA LOAI PHONG
	DELETE LOAIPHONG WHERE LOAIPHONG.MALOAI_P IN(SELECT deleted.MALOAI_P FROM deleted)
END
GO

--6/ DELETE PHONG
CREATE TRIGGER DEL_PHONG
ON PHONG
INSTEAD OF DELETE
AS
BEGIN
	-- XOA HOA DON
	DELETE HOADON WHERE HOADON.MA_PTP IN(SELECT HOADON.MA_PTP 
											FROM deleted, PHIEUTHUEPHONG, HOADON
											WHERE deleted.MA_P = PHIEUTHUEPHONG.MA_P
												and HOADON.MA_PTP = PHIEUTHUEPHONG.MA_PTP
												GROUP BY HOADON.MA_PTP)
	-- XOA PHIEU THUE PHONG
	DELETE PHIEUTHUEPHONG WHERE PHIEUTHUEPHONG.MA_P IN(SELECT PHIEUTHUEPHONG.MA_P
														FROM deleted, PHIEUTHUEPHONG
														WHERE deleted.MA_P = PHIEUTHUEPHONG.MA_P
														GROUP BY PHIEUTHUEPHONG.MA_P)
	-- XOA CHI TIET SU DUNG DV
	DELETE CHITIET_SDDV WHERE CHITIET_SDDV.MA_SD IN(SELECT CHITIET_SDDV.MA_SD
													FROM SUDUNGDV, deleted, CHITIET_SDDV
													WHERE deleted.MA_P = SUDUNGDV.MA_P
														and CHITIET_SDDV.MA_SD = SUDUNGDV.MA_SD
													GROUP BY CHITIET_SDDV.MA_SD)
	-- XOA SU DUNG DV
	DELETE SUDUNGDV WHERE SUDUNGDV.MA_P IN(SELECT SUDUNGDV.MA_P
											FROM SUDUNGDV, deleted
											WHERE deleted.MA_P = SUDUNGDV.MA_P
											GROUP BY SUDUNGDV.MA_P)
	-- XOA PHONG
	DELETE PHONG WHERE (SELECT deleted.MA_P FROM deleted) = PHONG.MA_P
END
GO

--7/ DELETE PHIEUTHUEPHONG
CREATE TRIGGER DEL_PHIEUTHUEPHONG
ON PHIEUTHUEPHONG
INSTEAD OF DELETE 
AS
BEGIN
	-- XOA HOA DON
	DELETE HOADON WHERE HOADON.MA_PTP IN(SELECT HOADON.MA_PTP FROM deleted, HOADON
											WHERE deleted.MA_PTP = HOADON.MA_PTP
											GROUP BY HOADON.MA_PTP)
	-- XOA PHIEUTHUEPHONG
	DELETE PHIEUTHUEPHONG WHERE PHIEUTHUEPHONG.MA_PTP IN(SELECT deleted.MA_PTP FROM deleted)
	UPDATE PHONG SET TINHTRANG_P = N'Còn trống' WHERE (SELECT MA_P FROM deleted) = PHONG.MA_P
END
GO
DROP TRIGGER DEL_PHIEUTHUEPHONG
/*------------------------------------------------------------------------------------*/
/*----------------------------------- INSERT TABLE -----------------------------------*/
/*------------------------------------------------------------------------------------*/
INSERT INTO KHACHHANG (HOTEN_KH, DIACHI_KH, CCCD_KH, SDT_KH, GIOITINH)
VALUES
(N'Lâm Chấn Khang',			N'Gò Vấp, TP.HCM',			'070071242456',	'0909206923', N'Nam'),
(N'Hồ Thanh Tùng',			N'Quận Tân Bình, TP.HCM',	'021123123553', '0909232139', N'Nam'),
(N'Nguyễn Phương Việt',		N'Quận Tân Phú, TP.HCM',	'074212341245', '0554233454', N'Nam'),
(N'Đặng Ngọc Thịnh',		N'Quận 10, TP.HCM',			'095221231455', '0743321233', N'Nam'),
(N'Nguyễn Lê Hữu Thắng',	N'Quận 1, TP.HCM',			'072231234554', '0955443334', N'Nam'),
(N'Hồ Thị Ánh',				N'Quận 2, TP.HCM',			'075355444533', '0931432453', N'Nữ'),
(N'Ngô Anh Đào',			N'Quận 5, TP.HCM',			'070703434234', '0999923232', N'Nữ'),
(N'Tờ Ri Ơ',				N'Bản Hồ, Cao Bằng',		'076554345345', '0124333333', N'Nam'),
(N'Lâm Bình Chi',			N'Quảng Châu, Trung Quốc',	'075436456456', '0673894345', N'Nam'),
(N'Dương Hoá',				N'Giang Nam, Trung Quốc',	'075445433344', '0922344415', N'Nam')
SELECT * FROM KHACHHANG
GO

INSERT INTO LOAIPHONG (TENLOAI_P, GIA, MOTA)
VALUES
(N'Single Room',200000, N'Phòng có 1 giường cho 1 người ngủ'),
(N'Twin Room',	400000, N'Phòng có 2 giường cho 2 người ngủ'),
(N'Double Room',500000, N'Phòng có 1 giường lớn cho 2 người ở ngủ'),
(N'Triple Room',600000, N'Phòng 3 giường nhỏ hoặc 1 giường lớn + 1 giường nhỏ cho 3 người ngủ'),
(N'Quad Room',	800000, N'Phòng có 2 giường lớn cho 4 người ở ngủ'),
(N'Extra Room', 1400000, N'Phòng thiết kế thêm giường'),
(N'Deluxe Room',1000000, N'Phòng có view hướng ra biển, núi')
SELECT * FROM LOAIPHONG
GO

INSERT INTO PHONG (TENPHONG, MALOAI_P, SOKHACHTOIDA)
VALUES
('A101', '1', 1),
('B205', '1', 1),
('C310', '2', 3),
('B202', '2', 3),
('A210', '3', 2),
('A106', '3', 2),
('C308', '4', 6),
('C203', '4', 6),
('B104', '5', 6),
('B209', '5', 6),
('B111', '6', 6),
('A302', '6', 6),
('C207', '7', 2),
('B208', '7', 2)
SELECT * FROM PHONG
GO

SET DATEFORMAT DMY
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (1, 1, 1, '25/12/2022') 
GO
SET DATEFORMAT DMY
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (1, 1, 1, '11/01/2023') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (2, 2, 1, '05/01/2023') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (2, 2, 1, '19/01/2023') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (3, 3, 3, '24/12/2022') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (3, 3, 2, '31/12/2022') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (5, 3, 2, '28/12/2022') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (7, 4, 5, '21/12/2022') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (13, 4, 1, '15/01/2023') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (14, 4, 2, '09/01/2023') 
GO
INSERT INTO PHIEUTHUEPHONG(MA_P, MA_KH, SONGUOI, NGAYDEN)
VALUES (14, 5, 2, '25/12/2022') 
GO
SELECT * FROM PHIEUTHUEPHONG
GO

INSERT INTO NHANVIEN
VALUES
('NV001', N'Hái Thứ Hiêu',	N'Quận Tân Phú, TP.HCM',		'0909206990', '072309998334', 'NV001mknv',N'Nhân viên'),
('NV002', N'Jack Sparrow',	N'Tiểu bang Ohio, Hoa kỳ',		'0823131233', '076002233321', 'NV002mkql',N'Quản lý'),
('NV003', N'Amber Heard',	N'Tiểu bang California, Hoa kỳ','0944228883', '085443334444', 'NV003mknv',N'Nhân viên'),
('NV004', N'Giang Ca',		N'Giang đông, Trung Quốc',		'0123344888', '093221123344', 'NV004mknv',N'Nhân viên'),
('NV005', N'Cao Văn Tú',	N'Quận Tân Bình, TP.HCM',		'0521233323', '092112345532', 'NV005mknv',N'Nhân viên'),
('NV006', N'Nguyễn Văn Nam',N'Quận 8, TP.HCM',				'0954889993', '091122331455', 'NV006mkql',N'Quản lý'),
('NV007', N'Hồ Quỳnh Hương',N'Quận 6, TP.HCM',				'0823434334', '092231145543', 'NV007mknv',N'Nhân viên')
SELECT * FROM NHANVIEN
GO

INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('1', null, 'NV001', 10)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('2', null, 'NV001', 7)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('3', null, 'NV001', 11)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('4', null, 'NV001', 5)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('5', null, 'NV001', 15)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('4', null, 'NV003', 8)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('5', null, 'NV004', 9)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('6', null, 'NV005', 2)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('7', null, 'NV005', 3)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('8', null, 'NV005', 5)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('9', null, 'NV006', 5)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('10', null, 'NV006', 5)
GO
INSERT INTO HOADON (MA_PTP, MA_SD, TENDANGNHAP, SONGAYTHUE) 
VALUES ('11', null, 'NV007', 6)
GO
DELETE HOADON WHERE MA_PTP = 10
SELECT * FROM HOADON
GO

INSERT INTO DICHVU
VALUES 
(N'Phục vụ ăn sáng',50000),
(N'Thuê bảo vệ',100000),
(N'Rửa xe',50000),
(N'Ăn buffet',200000),
(N'Dọn phòng',70000),
(N'Phục vụ ăn tối',50000),
(N'Spa',200000),
(N'Gym',60000),
(N'Karaoke',100000),
(N'Casino',100000)
SELECT * FROM DICHVU
GO

INSERT INTO SUDUNGDV(MA_PTP, NGAYSUDUNG, MA_DV)
VALUES
('1', '2022-12-25', 1),
('1', '2022-12-25', 2),
('2', '2023-01-11', 4),
('2', '2023-01-11', 2),
('3', '2023-01-05', 5),
('3', '2023-01-05', 7),
('4', '2023-01-19', 4),
('6', '2022-12-31', 10),
('7', '2022-12-28', 9)
SELECT * FROM SUDUNGDV
GO

/*------------------------------------------------------------------------------------*/
/*----------------------------------- CAC CHUC NANG ----------------------------------*/
/*------------------------------------------------------------------------------------*/
-- 1/ Click button cap nhat tinh trang phong (UPDATE TINH TRANG PHONG KHI MIN(NGAYDEN) = GETDATE)
CREATE PROC UPD_TINHTRANG_PROC @ma_p_proc int
AS
BEGIN
	DECLARE @ma_p_dec int
	SET @ma_p_dec = @ma_p_proc
	IF (SELECT a.NGAYDEN 
		FROM PHIEUTHUEPHONG a
		WHERE a.NGAYDEN <= ALL (SELECT NGAYDEN FROM PHIEUTHUEPHONG b WHERE b.MA_P = @ma_p_dec)
			and a.MA_P = @ma_p_dec) = GETDATE()
		SELECT 1 as code
	ELSE
		SELECT 0 as code
END
GO

-- 2/ Dang nhap
CREATE PROC [dbo].[DangNhap]
@Username nvarchar(20),
@Password nvarchar(20)
as
begin
    if exists (select * from NHANVIEN where TENDANGNHAP = @Username and MATKHAU = @Password and CHUCVU = N'Quản lý')
        select 1 as code
    else if exists (select * from NHANVIEN where TENDANGNHAP = @Username and MATKHAU = @Password and CHUCVU = N'Nhân viên')
        select 2 as code
    else select 0 as code
end
/*------------------------------------------------------------------------------------*/
/*---------------------------------- DROP KHOA NGOAI ---------------------------------*/
/*------------------------------------------------------------------------------------*/
-- PHONG --
ALTER TABLE PHONG
DROP constraint FK_PHONG_CO_LOAIPHONG
GO

-- PHIEUTHUEPHONG --
ALTER TABLE PHIEUTHUEPHONG
DROP constraint FK_PHIEUTHUEPHONG_THUOC_KHACHHANG,
	constraint FK_PHIEUTHUOCPHONG_CO_PHONG
GO

-- SUDUNGDV --
ALTER TABLE SUDUNGDV
DROP constraint FK_SUDUNGDV_THUOC_PHONG
GO

-- CHITIET_SDDV --
ALTER TABLE CHITIET_SDDV
DROP constraint FK_CHITIET_SDDV_THUOC_SUDUNGDV,
	constraint FK_CHITIET_SDDV_THUOC_DICHVU
GO

-- HOADON --
ALTER TABLE HOADON
DROP constraint FK_HOADON_CO_PHIEUTHUEPHONG,
	--constraint FK_HOADON_CO_PHIEUTHUEPHONG2 foreign key (MA_KH) references PHIEUTHUEPHONG (MA_KH),
	--constraint FK_HOADON_CO_PHIEUTHUEPHONG3 foreign key (MA_P) references PHIEUTHUEPHONG (MA_P),
	constraint FK_HOADON_CO_NHANVIEN,
	constraint FK_HOADON_CO_SUDUNGDV
GO

/*------------------------------------------------------------------------------------*/
/*------------------------------------- DROP TABLE -----------------------------------*/
/*------------------------------------------------------------------------------------*/
DROP TABLE CHITIET_SDDV, DICHVU, HOADON, KHACHHANG, LOAIPHONG, NHANVIEN, PHIEUTHUEPHONG, PHONG, SUDUNGDV
GO

-- TEST --
DBCC CHECKIDENT (PHIEUTHUEPHONG, RESEED, 0);
GO

SELECT * FROM LOAIPHONG
SELECT * FROM PHONG
SELECT * FROM PHIEUTHUEPHONG
SELECT * FROM HOADON
SELECT * FROM SUDUNGDV
SELECT * FROM CHITIET_SDDV

DELETE PHIEUTHUEPHONG WHERE MA_P = 1
DELETE PHIEUTHUEPHONG WHERE MA_P = 2

DELETE PHONG WHERE MA_P = 

DELETE LOAIPHONG WHERE MALOAI_P = 2


	-- XOA HOA DON
	DELETE HOADON WHERE HOADON.MA_PTP IN(SELECT HOADON.MA_PTP
										FROM LOAIPHONG, PHONG, PHIEUTHUEPHONG, HOADON
										WHERE LOAIPHONG.MALOAI_P = PHONG.MALOAI_P
											and PHONG.MA_P = PHIEUTHUEPHONG.MA_P
											and HOADON.MA_PTP = PHIEUTHUEPHONG.MA_PTP
											and LOAIPHONG.MALOAI_P = 2
										GROUP BY HOADON.MA_PTP)
	-- XOA PHIEU THUE PHONG
	DELETE PHIEUTHUEPHONG WHERE PHIEUTHUEPHONG.MA_P IN(SELECT PHIEUTHUEPHONG.MA_P
															FROM LOAIPHONG, PHONG, PHIEUTHUEPHONG
															WHERE LOAIPHONG.MALOAI_P = PHONG.MALOAI_P
																	and PHONG.MA_P = PHIEUTHUEPHONG.MA_P
																	and LOAIPHONG.MALOAI_P = 2
															GROUP BY PHIEUTHUEPHONG.MA_P)
	-- XOA CHI TIET SU DUNG DV
	DELETE CHITIET_SDDV WHERE CHITIET_SDDV.MA_SD IN(SELECT CHITIET_SDDV.MA_SD
													FROM SUDUNGDV, PHONG, LOAIPHONG, CHITIET_SDDV
													WHERE
														 PHONG.MA_P = SUDUNGDV.MA_P
														and LOAIPHONG.MALOAI_P = PHONG.MALOAI_P
														and CHITIET_SDDV.MA_SD = SUDUNGDV.MA_SD
														and LOAIPHONG.MALOAI_P = 2
													GROUP BY CHITIET_SDDV.MA_SD)
	-- XOA SU DUNG DV
	DELETE SUDUNGDV WHERE SUDUNGDV.MA_P IN(SELECT PHONG.MA_P
											FROM SUDUNGDV, PHONG, LOAIPHONG
											WHERE
												 PHONG.MA_P = SUDUNGDV.MA_P
												and LOAIPHONG.MALOAI_P = PHONG.MALOAI_P
												and LOAIPHONG.MALOAI_P = 2
											GROUP BY PHONG.MA_P)
	-- XOA PHONG
	DELETE PHONG WHERE PHONG.MALOAI_P IN(SELECT LOAIPHONG.MALOAI_P 
											FROM LOAIPHONG, PHONG
											WHERE LOAIPHONG.MALOAI_P = PHONG.MALOAI_P
												and LOAIPHONG.MALOAI_P = 2
											GROUP BY LOAIPHONG.MALOAI_P)
	-- XOA LOAI PHONG
	DELETE LOAIPHONG WHERE LOAIPHONG.MALOAI_P IN(SELECT LOAIPHONG.MALOAI_P FROM LOAIPHONG
												WHERE LOAIPHONG.MALOAI_P = 2)

	DELETE HOADON WHERE (SELECT HOADON.MA_PTP FROM PHIEUTHUEPHONG, HOADON
							WHERE HOADON.MA_P = 1
								and PHIEUTHUEPHONG.MA_PTP = HOADON.MA_PTP
							GROUP BY HOADON.MA_PTP) = HOADON.MA_PTP
	
	DELETE HOADON WHERE HOADON.MA_PTP IN(SELECT HOADON.MA_PTP FROM PHIEUTHUEPHONG, HOADON
											WHERE PHIEUTHUEPHONG.MA_PTP = HOADON.MA_PTP
												and PHIEUTHUEPHONG.MA_P = 1
											GROUP BY HOADON.MA_PTP)

	DELETE PHIEUTHUEPHONG WHERE PHIEUTHUEPHONG.MA_PTP IN(SELECT PHIEUTHUEPHONG.MA_PTP FROM PHIEUTHUEPHONG
															WHERE PHIEUTHUEPHONG.MA_P = 1)

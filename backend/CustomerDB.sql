use master
go

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Customer')
BEGIN
    DROP DATABASE [Customer];
END;
GO

CREATE DATABASE [Customer];
GO

USE [Customer];
GO

-- Table: Users
CREATE TABLE Users (
    userId INT IDENTITY(1,1) NOT NULL,
    fullname NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NULL,
    phone NVARCHAR(20) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
    isStaff BIT NOT NULL DEFAULT 0,
    roleName NVARCHAR(50) NULL,
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (userId)
);

-- Table: RoomType
CREATE TABLE RoomType (
    roomTypeId INT IDENTITY(1,1) NOT NULL,
	roomTypeName NVARCHAR(255) NOT NULL,
    roomDesc NVARCHAR(255) NULL,
    roomFeatures NVARCHAR(MAX) NULL,
    roomAmenities NVARCHAR(MAX) NULL,
    roomImg NVARCHAR(255) NULL,
    roomPrice DECIMAL(10,2) NOT NULL,
    CONSTRAINT PK_RoomType PRIMARY KEY CLUSTERED (roomTypeId)
);

-- Table: Rooms
CREATE TABLE Rooms (
    roomId INT IDENTITY(1,1) NOT NULL,
    roomTitle NVARCHAR(100) NOT NULL,
    roomTypeId INT NOT NULL,
    roomDescription NVARCHAR(MAX) NULL,
    roomImage NVARCHAR(255) NULL,
    roomStatus NVARCHAR(50) NOT NULL DEFAULT 'available',
    CONSTRAINT PK_Rooms PRIMARY KEY CLUSTERED (roomId),
    CONSTRAINT FK_Rooms_RoomType FOREIGN KEY (roomTypeId) REFERENCES RoomType(roomTypeId)
);

-- Table: Bookings
CREATE TABLE Bookings (
    bookingId INT IDENTITY(1,1) NOT NULL,
    userId INT NULL,
    fullname NVARCHAR(255) NULL,
    email NVARCHAR(255) NULL,
    phone NVARCHAR(20) NULL,
    checkInDate DATE NOT NULL,
    checkOutDate DATE NOT NULL,
    totalPrice DECIMAL(10,2) NOT NULL,
    paymentStatus NVARCHAR(50) NOT NULL DEFAULT 'pending',
    CONSTRAINT PK_Bookings PRIMARY KEY CLUSTERED (bookingId),
    CONSTRAINT FK_Bookings_Users FOREIGN KEY (userId) REFERENCES Users(userId)
);

-- Table: BookingDetails
CREATE TABLE BookingDetails (
    bookingDetailId INT IDENTITY(1,1) NOT NULL,
    bookingId INT NOT NULL,
    roomId INT NOT NULL,
    CONSTRAINT PK_BookingDetails PRIMARY KEY CLUSTERED (bookingDetailId),
    CONSTRAINT FK_BookingDetails_Bookings FOREIGN KEY (bookingId) REFERENCES Bookings(bookingId),
    CONSTRAINT FK_BookingDetails_Rooms FOREIGN KEY (roomId) REFERENCES Rooms(roomId)
);

-- Table: Reviews
CREATE TABLE Reviews (
    reviewId INT IDENTITY(1,1) NOT NULL,
    userId INT NOT NULL,
    bookingId INT NOT NULL,
    reviewContent NVARCHAR(MAX) NULL,	
    rating INT NOT NULL,
    CONSTRAINT PK_Reviews PRIMARY KEY CLUSTERED (reviewId),
    CONSTRAINT FK_Reviews_Users FOREIGN KEY (userId) REFERENCES Users(userId),
    CONSTRAINT FK_Reviews_Bookings FOREIGN KEY (bookingId) REFERENCES Bookings(bookingId)
);

-- Table: RoomService
CREATE TABLE RoomService (
    roomServiceId INT IDENTITY(1,1) NOT NULL,
    roomId INT NOT NULL,
    ServiceDateTime DATETIME NOT NULL,
    isCleaningDone BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_RoomService PRIMARY KEY CLUSTERED (roomServiceId),
    CONSTRAINT FK_RoomService_Rooms FOREIGN KEY (roomId) REFERENCES Rooms(roomId)
);

-- Table: ParkingService
CREATE TABLE ParkingService (
    parkingServiceId INT IDENTITY(1,1) NOT NULL,
    bookingId INT NOT NULL,
    parkingPlateNo NVARCHAR(20) NOT NULL,
    CONSTRAINT PK_ParkingService PRIMARY KEY CLUSTERED (parkingServiceId),
    CONSTRAINT FK_ParkingService_Bookings FOREIGN KEY (bookingId) REFERENCES Bookings(bookingId)
);

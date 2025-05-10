using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models;

public partial class CustomerContext : DbContext
{
    public CustomerContext()
    {
    }

    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingDetail> BookingDetails { get; set; }

    public virtual DbSet<ParkingService> ParkingServices { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomService> RoomServices { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=Customer;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.CheckInDate).HasColumnName("checkInDate");
            entity.Property(e => e.CheckOutDate).HasColumnName("checkOutDate");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("pending")
                .HasColumnName("paymentStatus");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalPrice");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Bookings_Users");
        });

        modelBuilder.Entity<BookingDetail>(entity =>
        {
            entity.Property(e => e.BookingDetailId).HasColumnName("bookingDetailId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.RoomId).HasColumnName("roomId");

            entity.HasOne(d => d.Booking).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_Bookings");

            entity.HasOne(d => d.Room).WithMany(p => p.BookingDetails)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingDetails_Rooms");
        });

        modelBuilder.Entity<ParkingService>(entity =>
        {
            entity.ToTable("ParkingService");

            entity.Property(e => e.ParkingServiceId).HasColumnName("parkingServiceId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ParkingPlateNo)
                .HasMaxLength(20)
                .HasColumnName("parkingPlateNo");

            entity.HasOne(d => d.Booking).WithMany(p => p.ParkingServices)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ParkingService_Bookings");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.ReviewId).HasColumnName("reviewId");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewContent).HasColumnName("reviewContent");
            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Booking).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Bookings");

            entity.HasOne(d => d.Room).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Rooms");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.RoomDescription).HasColumnName("roomDescription");
            entity.Property(e => e.RoomImage)
                .HasMaxLength(255)
                .HasColumnName("roomImage");
            entity.Property(e => e.RoomStatus)
                .HasMaxLength(50)
                .HasDefaultValue("available")
                .HasColumnName("roomStatus");
            entity.Property(e => e.RoomTitle)
                .HasMaxLength(100)
                .HasColumnName("roomTitle");
            entity.Property(e => e.RoomTypeId).HasColumnName("roomTypeId");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rooms_RoomType");
        });

        modelBuilder.Entity<RoomService>(entity =>
        {
            entity.ToTable("RoomService");

            entity.Property(e => e.RoomServiceId).HasColumnName("roomServiceId");
            entity.Property(e => e.IsCleaningDone).HasColumnName("isCleaningDone");
            entity.Property(e => e.RoomId).HasColumnName("roomId");
            entity.Property(e => e.ServiceDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomServices)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoomService_Rooms");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.ToTable("RoomType");

            entity.Property(e => e.RoomTypeId).HasColumnName("roomTypeId");
            entity.Property(e => e.RoomAmenities).HasColumnName("roomAmenities");
            entity.Property(e => e.RoomDesc)
                .HasMaxLength(255)
                .HasColumnName("roomDesc");
            entity.Property(e => e.RoomFeatures).HasColumnName("roomFeatures");
            entity.Property(e => e.RoomImg)
                .HasMaxLength(255)
                .HasColumnName("roomImg");
            entity.Property(e => e.RoomPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("roomPrice");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .HasColumnName("fullname");
            entity.Property(e => e.IsStaff).HasColumnName("isStaff");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("roleName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

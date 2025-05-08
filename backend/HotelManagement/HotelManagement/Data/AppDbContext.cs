using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }
        public DbSet<ParkingService> ParkingServices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình quan hệ cho Review
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Booking)
                .WithMany()
                .HasForeignKey(r => r.BookingId)
                .OnDelete(DeleteBehavior.Restrict); // Chặn xóa Booking nếu có Review

            // Cấu hình quan hệ cho ParkingService
            modelBuilder.Entity<ParkingService>()
                .HasOne(p => p.Booking)
                .WithMany()
                .HasForeignKey(p => p.BookingId);
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Models
{
    [Table("RoomType")]
    public partial class RoomType
    {
        [Key]
        [Column("roomTypeId")]
        public int RoomTypeId { get; set; }

        [Column("roomName")]
        [StringLength(255)]
        public string RoomTypeName { get; set; }

        [Column("roomDesc")]
        [StringLength(255)]
        public string? RoomDesc { get; set; }

        [Column("roomFeatures")]
        public string? RoomFeatures { get; set; }

        [Column("roomAmenities")]
        public string? RoomAmenities { get; set; }

        [Column("roomImg")]
        [StringLength(255)]
        public string? RoomImg { get; set; }

        [Column("roomPrice", TypeName = "decimal(10, 2)")]
        public decimal RoomPrice { get; set; }

        [InverseProperty("RoomType")]
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
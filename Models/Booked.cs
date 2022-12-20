
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace HOTEL1.Models
{
    public class Booked
    {       [Key]
            public int Id { get; set; }
            public int Quantity { get; set; }
            public BookedState Status { get; set; }
            public DateTime LastUpdated { get; set; }
        // Hotel
        [ForeignKey("Hotels")]
        public int HotelId { get; set; }
        public virtual Hotel Hotels { get; set; }

        // Room
        [ForeignKey("Rooms")]
            public int RoomId { get; set; }
            public virtual Room Rooms { get; set; }
            // User
            [ForeignKey("Users")]
            public string UserId { get; set; }
            public virtual IdentityUser Users { get; set; }
        }
    public enum BookedState
    {
        InCart,
        OrderPlaced,
        Verifying,
        Inprocess,
        Delivered
    }
}


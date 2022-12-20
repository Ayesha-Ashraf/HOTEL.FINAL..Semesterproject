using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HOTEL1.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string RoomNo { get; set; }

        public string FloorNo { get; set; }
        public string Image { get; set; }
        public string Price { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}

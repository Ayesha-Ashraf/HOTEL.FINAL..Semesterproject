using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HOTEL1.Models
{
    public class Hotel
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string URL { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public string location { get; set; }

        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        public virtual Room Rooms { get; set; }
        public List<Customer> Customers { get; set; }
    }
}

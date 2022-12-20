using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HOTEL1.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public string Phoneno { get; set; }
        public string Nationaity { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}

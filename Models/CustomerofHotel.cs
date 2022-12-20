using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HOTEL1.Models
{
    public class CustomerofHotel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Hotels")]
        public int HotelId { get; set; }
        public Hotel Hotels { get; set; }
        [ForeignKey("Customers")]

        public int CustomerId { get; set; }
        public Customer Customers { get; set; }
    }
}

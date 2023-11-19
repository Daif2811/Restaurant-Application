using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models.DTO
{
    public class OrderUpdateDto
    {

        [Required, Display(Name = "Quantity"), Range(1, 250, ErrorMessage = "Sorry, Quantity should be between 1 and 250 orders")]
        public byte OrderQuantity { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }



        [Required]
        public int MealId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Restaurant.Models.DTO
{
    public class OrderDto
    {

        [Required, Display(Name = "Quantity"), Range(1, 250, ErrorMessage = "Sorry, Quantity should be between 1 and 250 orders")]
        public byte OrderQuantity { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }



        [Required, Display(Name = "Meal Id")]
        public int MealId { get; set; }
    }
}

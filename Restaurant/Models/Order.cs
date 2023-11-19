using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required, Display(Name = "Quantity") , Range(1, 250, ErrorMessage ="Sorry, Quantity should be between 1 and 250 orders")]
        public byte OrderQuantity { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Meal Id")]
        public int MealId { get; set; }


        public DateTime OrderDate { get; set; }

        [Required]
        public string UserId { get; set; }


        public virtual ApplicationUser User { get; set; }
        public virtual Meal Meal { get; set; }
    }

}

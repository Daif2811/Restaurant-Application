using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class MealDTO
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        public int CategoryId { get; set; }


    }
}

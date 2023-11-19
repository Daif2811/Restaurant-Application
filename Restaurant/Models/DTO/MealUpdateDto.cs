using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class MealUpdateDto
    {
        
        public string Name { get; set; }

        public string Description { get; set; }

       
        public float? Price { get; set; }

        public int? CategoryId { get; set; }

        
        public IFormFile Image { get; set; }


    }
}

using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile Image { get; set; }

    }
}

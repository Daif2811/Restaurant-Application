using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class CategoryUpdateDto
    {
        
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}

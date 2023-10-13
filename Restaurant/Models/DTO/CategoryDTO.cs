using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models.DTO
{
    public class CategoryDTO
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

    }
}

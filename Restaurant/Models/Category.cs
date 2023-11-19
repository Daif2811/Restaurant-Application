using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Category
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }
    }
}

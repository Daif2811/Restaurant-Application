﻿using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Meal
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        public int CategoryId { get; set; }

        [Required]
        public string Image { get; set; }



        public virtual Category Category { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

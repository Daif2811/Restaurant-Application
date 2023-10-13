using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Restaurant.Models;

namespace Restaurant.DAL
{
    public class RestaurantContext : IdentityDbContext<ApplicationUser>
    {
        public RestaurantContext(DbContextOptions options ) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Meal> Meals { get; set; }





    }
}

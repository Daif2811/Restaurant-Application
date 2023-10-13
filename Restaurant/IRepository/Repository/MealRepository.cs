using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.IRepository.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly RestaurantContext _context;

        public MealRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Meal>> GetAll()
        {
            var meals = await _context.Meals.ToListAsync();
            return meals;
        }

        public async Task<Meal> GetById(int id)
        {
            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.Id == id);
            return meal;
        }

        public async Task<Meal> GetByName(string name)
        {
            var meal = await _context.Meals.SingleOrDefaultAsync(m => m.Name == name);
            return meal;
        }


        public async Task Add(Meal meal)
        {
            _context.Meals.Add(meal);
            await _context.SaveChangesAsync();
        }

       

        public async Task Update(Meal meal)
        {
            _context.Meals.Update(meal);
            await _context.SaveChangesAsync();
        }



        public async Task Delete(int id)
        {
            var meal = await GetById(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
        }
    }
}

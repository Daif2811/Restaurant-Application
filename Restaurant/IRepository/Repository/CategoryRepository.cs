using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.IRepository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantContext _context;

        public CategoryRepository(RestaurantContext context)
        {
            _context = context;
        }

       

        public async Task<ICollection<Category>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> GetById(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Category> GetByName(string name)
        {
           var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == name);
            return category;
        }

        public async Task Add(Category category)
        {
           _context.Categories.Add(category);
           await _context.SaveChangesAsync();
        }

       

        public async Task Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync(); 
        }



        public async Task Delete(int id)
        {
            var category = await GetById(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

    }
}

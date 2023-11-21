using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.IRepository.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantContext _context;

        public OrderRepository(RestaurantContext context)
        {
            this._context = context;
        }

        public async Task<ICollection<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Order>> GetByMealId(int mealId)
        {
            return await _context.Orders.Where(o => o.MealId == mealId).ToListAsync();
        }

        public async Task<ICollection<Order>> GetByUserId(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task Add(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            Order order = await GetById(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}

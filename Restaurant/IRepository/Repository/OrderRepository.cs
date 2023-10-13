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

        public ICollection<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Order> GetByMealId(int mealId)
        {
            return _context.Orders.Where(o => o.MealId == mealId).ToList();
        }

        public ICollection<Order> GetByUserId(string userId)
        {
            return _context.Orders.Where(o => o.UserId == userId).ToList();
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
            Order order = GetById(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}

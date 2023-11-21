using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface IOrderRepository
    {
        Task<ICollection<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<ICollection<Order>> GetByUserId(string userId);

        Task<ICollection<Order>> GetByMealId(int mealId);

        Task Add (Order order);
        Task Update (Order order);
        Task Delete (int id);




    }
}

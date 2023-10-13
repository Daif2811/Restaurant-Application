using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAll();
        Order GetById(int id);
        ICollection<Order> GetByUserId(string userId);

        ICollection<Order> GetByMealId(int mealId);

        Task Add (Order order);
        Task Update (Order order);
        Task Delete (int id);




    }
}

using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface IMealRepository
    {
        Task<ICollection<Meal>> GetAll();
        Task<Meal> GetById(int id);
        Task<Meal> GetByName(string name);
        Task Add(Meal meal);
        Task Update(Meal meal);
        Task Delete(int id);
    }
}

using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAll();
        Task<Category> GetById(int id);
        Task<Category> GetByName(string name);
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);


    }
}

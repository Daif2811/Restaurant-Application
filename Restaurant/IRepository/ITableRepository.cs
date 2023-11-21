using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface ITableRepository
    {
        Task<ICollection<Table>> GetAll();
        Task<Table> GetById(int id);
        Task Add(Table table);
        Task Update(Table table);
        Task Delete(int id);


    }
}

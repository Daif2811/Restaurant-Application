using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.IRepository.Repository
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantContext _context;

        public TableRepository(RestaurantContext context)
        {
            this._context = context;
        }

      

        public async Task<ICollection<Table>> GetAll()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Table> GetById(int id)
        {
            return await _context.Tables.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Table table = await GetById(id);
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;

namespace Restaurant.IRepository.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantContext _context;

        public ReservationRepository(RestaurantContext context)
        {
            this._context = context;
        }

        public async Task<ICollection<Reservation>> GetAll()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _context.Reservations.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Reservation reserve = await GetById(id);
            _context.Reservations.Remove(reserve);
            await _context.SaveChangesAsync();
        }
    }
}

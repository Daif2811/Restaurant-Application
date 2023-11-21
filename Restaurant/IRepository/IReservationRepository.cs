using Restaurant.Models;

namespace Restaurant.IRepository
{
    public interface IReservationRepository
    {
        Task<ICollection<Reservation>> GetAll();
        Task<Reservation> GetById (int id);
        Task Add (Reservation reservation);
        Task Update (Reservation reservation);
        Task Delete (int id);


    }
}

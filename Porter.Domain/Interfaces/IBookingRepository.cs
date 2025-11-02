namespace Porter.Domain.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAll();

        Task<Booking?> GetById(int id);

        Task<int> Register(Booking booking);
        
        Task<int> GetBookingCountByRoomAndPeriod(int roomId, int? bookingId, DateTime startDate, DateTime endDate);

        Task<int> Delete(int Id);

        Task<IList<Booking>> GetBookingListByRoomAndPeriod(int roomId, DateTime startDate, DateTime endDate);

        Task<int> Update(Booking bookingToUpdate);
    }
}

namespace Porter.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAll();

        Task<Room?> GetByName(string name);

        Task<int> GetCountByName(string name);

        Task<int> Register(Room room);
    }
}

namespace Porter.Domain.Interfaces
{
    public interface IUserPorterRepository
    {
        Task<List<UserPorter>> GetAll();
    }
}

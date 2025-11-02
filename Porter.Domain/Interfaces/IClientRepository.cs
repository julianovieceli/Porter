namespace Porter.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll();
    }
}

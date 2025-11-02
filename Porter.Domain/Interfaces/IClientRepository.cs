namespace Porter.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<List<Client>> GetAll();

        Task<Client?> GetByDocto(string docto);

        Task<int> GetCountByDocto(string docto);

        Task<int> Register(Client client);
    }
}

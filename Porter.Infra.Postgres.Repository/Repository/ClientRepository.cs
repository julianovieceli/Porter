using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;

namespace Porter.Infra.Postgres.Repository.Repository
{
    public class ClientRepository : IClientRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        // The AppDbContext is injected here
        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Client>> GetAll()
        {
            try
            {
                var clients = await _context.Clients.ToListAsync();

                
                _logger.LogInformation($"Returned {clients.Count} clients.");


                return clients;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "An error occurred while retrieving clients.");
                throw;
            }
        }

        public async Task<Client?> GetByDocto(string docto)
        {
            try
            {

                var client = await _context.Clients.FirstOrDefaultAsync(p => p.Docto == docto);

                
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                throw;
            }
        }

        public async Task<int> GetCountByDocto(string docto)
        {
            try
            {

                var total = await _context.Clients.CountAsync(p => p.Docto == docto);
                return total;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                throw;
            }
        }

        public async Task<int> Register(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                int clientId = await _context.SaveChangesAsync();

                if (clientId > 0)
                    _logger.LogInformation($"Client {clientId} success saved!");
                else
                    _logger.LogInformation($"Erro ao cadastrar um cliente!");


                return clientId;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "An error occurred while saving client.");
                throw;
            }
        }

    }
}

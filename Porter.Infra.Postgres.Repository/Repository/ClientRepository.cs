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
      
    }
}

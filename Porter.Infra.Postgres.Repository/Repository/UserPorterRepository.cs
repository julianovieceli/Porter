using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;

namespace Porter.Infra.Postgres.Repository.Repository
{
    public class UserPorterRepository: IUserPorterRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<UserPorterRepository> _logger;

        // The AppDbContext is injected here
        public UserPorterRepository(AppDbContext context, ILogger<UserPorterRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<UserPorter>> GetAll()
        {
            try
            {
                var users = await _context.Users.ToListAsync();

                _logger.LogInformation($"Returned {users.Count} users.");


                return users;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "An error occurred while retrieving users.");
                throw;
            }
        }
      
    }
}

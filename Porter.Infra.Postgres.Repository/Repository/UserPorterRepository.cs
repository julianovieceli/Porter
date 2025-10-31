using Microsoft.EntityFrameworkCore;
using Porter.Domain;
using Porter.Domain.Interfaces;

namespace Porter.Infra.Postgres.Repository.Repository
{
    public class UserPorterRepository: IUserPorterRepository
    {

        private readonly AppDbContext _context;

        // The AppDbContext is injected here
        public UserPorterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserPorter>> GetAll()
        {
            // Use the injected context to perform the query
            return await _context.Users.ToListAsync();
        }
      
    }
}

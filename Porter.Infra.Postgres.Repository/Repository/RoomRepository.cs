using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;

namespace Porter.Infra.Postgres.Repository.Repository
{
    public class RoomRepository : IRoomRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(AppDbContext context, ILogger<RoomRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Room>> GetAll()
        {
            try
            {
                var rooms = await _context.Rooms.ToListAsync();

                
                _logger.LogInformation($"Returned {rooms.Count} rooms.");


                return rooms;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao retornar salas.");
                throw;
            }
        }

        public async Task<Room?> GetByName(string name)
        {
            try
            {

                var room = await _context.Rooms.FirstOrDefaultAsync(p => p.Name == name);

                
                return room;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                throw;
            }
        }

        public async Task<int> GetCountByName(string name)
        {
            try
            {

                var total = await _context.Rooms.CountAsync(p => p.Name == name);
                return total;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                throw;
            }
        }

        public async Task<int> Register(Room room)
        {
            try
            {
                _context.Rooms.Add(room);
                int roomId = await _context.SaveChangesAsync();

                if (roomId > 0)
                    _logger.LogInformation($"Room {room.Id} salvo com sucesso!");
                else
                    _logger.LogInformation($"Erro ao cadastrar uma sala!");


                return room.Id;
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao gravar uma sala.");
                throw;
            }
        }

    }
}

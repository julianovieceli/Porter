using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;

namespace Porter.Infra.Postgres.Repository.Repository
{
    public class LogRepository : ILogRepository
    {

        private readonly AppDbContext _context;
        private readonly ILogger<LogRepository> _logger;

        public LogRepository(AppDbContext context, ILogger<LogRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

  

        public async Task Register(Log log)
        {
            try
            {
                _context.Logs.Add(log);
                int logId = await _context.SaveChangesAsync();

                if (logId > 0)
                    _logger.LogInformation($"Room {log.Id} salvo com sucesso!");
                else
                    _logger.LogInformation($"Erro ao cadastrar uma sala!");

            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                _logger.LogError(ex, "Erro ao gravar uma sala.");
            }
        }

    }
}

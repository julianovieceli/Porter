using Microsoft.Extensions.Logging;
using Porter.Common.Domain;
using Porter.Common.Domain.Interfaces;

namespace Porter.Common.EF.Repository
{
    public class LogRepository : ILogRepository
    {

        private readonly DbContextBase _context;
        private readonly ILogger<LogRepository> _logger;

        public LogRepository(ILogger<LogRepository> logger, DbContextBase context)
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

using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;
using Porter.Infra.Postgres.Repository;

namespace Porter.Common.EF.Repository
{
    public class LogRepository_ : ILogRepository
    {

        private readonly DbContextBase _context;
        private readonly ILogger<LogRepository_> _logger;

        public LogRepository_(ILogger<LogRepository_> logger)
        {
          //  _context = context;
            _logger = logger;
        }

  

        public async Task Register(Log log)
        {
            await Task.FromResult(0);

            //try
            //{
            //    _context.Logs.Add(log);
            //    int logId = await _context.SaveChangesAsync();

            //    if (logId > 0)
            //        _logger.LogInformation($"Room {log.Id} salvo com sucesso!");
            //    else
            //        _logger.LogInformation($"Erro ao cadastrar uma sala!");

            //}
            //catch (Exception ex)
            //{
            //    // Handle exceptions as needed
            //    _logger.LogError(ex, "Erro ao gravar uma sala.");
            //}
        }

    }
}

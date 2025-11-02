using AutoMapper;
using Microsoft.Extensions.Logging;
using Porter.Domain;
using Porter.Domain.Interfaces;
using System.Text.Json;
using static Porter.Domain.Log;

namespace Porter.Application.Services
{
    public abstract class BaseService
    {
        protected ILogger<BaseService> _logger;
        protected readonly IMapper _dataMapper;

        private readonly ILogRepository _logRepository;
        public BaseService(ILogger<BaseService> logger, IMapper dataMapper, ILogRepository logRepository)
        {
            _logger = logger;
            _logRepository = logRepository;
            _dataMapper = dataMapper;
        }

        protected async Task LogList(BaseDomain entity, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, "entity");

                Log log = new Log(ACTION.LIST, entity.GetType(), methodName);

                await _logRepository.Register(log);

            }
            catch (Exception e)
            {
                //return Result.Failure("666", e.Message);
            }
        }

        protected async Task LogView(BaseDomain entity, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, "entity");

                var data = JsonSerializer.Serialize(entity);
                Log log = new Log(ACTION.LIST, entity.GetType(), methodName, data);

                await _logRepository.Register(log);

            }
            catch (Exception e)
            {
                //return Result.Failure("666", e.Message);
            }
        }

        protected async Task LogUpdate(BaseDomain entity, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, "entity");

                var data = JsonSerializer.Serialize(entity);

                Log log = new Log(ACTION.UPDATE, entity.GetType(), methodName, data);


                await _logRepository.Register(log);

            }
            catch (Exception e)
            {
                //return Result.Failure("666", e.Message);
            }
        }

        protected async Task LogInsert(BaseDomain entity, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, "entity");

                var data = JsonSerializer.Serialize(entity);

                Log log = new Log(ACTION.INSERT, entity.GetType(), methodName,  data);


                await _logRepository.Register(log);

            }
            catch (Exception e)
            {
                //return Result.Failure("666", e.Message);
            }
        }

        protected async Task LogDelete(BaseDomain entity, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity, "entity");

                var data = JsonSerializer.Serialize(entity);

                Log log = new Log(ACTION.DELETE, entity.GetType(), methodName, data);


                await _logRepository.Register(log);

            }
            catch (Exception e)
            {
                //return Result.Failure("666", e.Message);
            }
        }
    }
}

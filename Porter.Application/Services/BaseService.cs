using AutoMapper;
using Microsoft.Extensions.Logging;
using Personal.Common.Services;

namespace Porter.Application.Services
{
    public abstract class BaseService
    {
        protected ILogger<BaseService> _logger;
        protected readonly IMapper _dataMapper;

        protected readonly ILogService _logService;
        public BaseService(ILogger<BaseService> logger, IMapper dataMapper, ILogService logService): this(logger, dataMapper)
        {
            _logService = logService;
        }

        public BaseService(ILogger<BaseService> logger, IMapper dataMapper)
        {
            _logger = logger;
            _dataMapper = dataMapper;
        }


    }
}

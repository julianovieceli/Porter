using AutoMapper;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Services
{
    public class ClientService: IClientService
    {
        private readonly IClientRepository _clientRepository;
        private ILogger<ClientService> _logger;
        private readonly IMapper _dataMapper;

        public ClientService(ILogger<ClientService> logger, IMapper dataMapper, IClientRepository clientRepository)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _dataMapper = dataMapper;
        }


        public async Task<Result> GetAll()
        {
            try
            {
                var clientList = await _clientRepository.GetAll();

                IList<ResponseClientDto> listToReturn = clientList.Select(u => _dataMapper.Map<ResponseClientDto>(u)).ToList();

                return Result<IList<ResponseClientDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "An error occurred while fetching clients");
            }
        }
    }
}

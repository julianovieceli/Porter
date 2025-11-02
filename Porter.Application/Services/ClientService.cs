using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Domain;
using Porter.Domain.Interfaces;
using Porter.Domain.Validators;
using Porter.Dto;

namespace Porter.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private ILogger<ClientService> _logger;
        private readonly IValidator<RequestRegisterClientDto> _clientValidator;
        private readonly IMapper _dataMapper;

        public ClientService(ILogger<ClientService> logger, IMapper dataMapper, IClientRepository clientRepository,
            IValidator<RequestRegisterClientDto> clientValidator)
        {
            _logger = logger;
            _clientRepository = clientRepository;
            _dataMapper = dataMapper;
            _clientValidator = clientValidator;
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
                return Result.Failure("666", "Erro ao consultar clientes");
            }
        }

        public async Task<Result> GetByDocto(string docto)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(docto, "docto");

                if (DocumentValidator.IsCpfCnpjValid(docto) == false)
                    return Result.Failure("400", "Documento inválido");

                var client = await _clientRepository.GetByDocto(docto);

                if (client is null)
                    return Result.Failure("400", "Cliente não encontrado"); //erro nao encontrado
                else
                {
                    var clientDto = _dataMapper.Map<ResponseClientDto>(client);

                    return Result<ResponseClientDto>.Success(clientDto);
                }

            }
            catch(ArgumentNullException exArg)
            {
                return Result.Failure("400", "Documento obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                return Result.Failure("666", "Erro ao consultar um Cliente");
            }


        }

        public async Task<Result> Register(RequestRegisterClientDto clientRequest)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(clientRequest, "clientRequest");

                var validatorResult = _clientValidator.Validate(clientRequest);
                if (!validatorResult.IsValid)
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                if (await _clientRepository.GetCountByDocto(clientRequest.Docto) == 0)
                {

                    Client client = new Client(clientRequest.Name, clientRequest.Docto);

                    int clientRegistered = await _clientRepository.Register(client);

                    if (clientRegistered > 0)
                    {
                        var response = _dataMapper.Map<ResponseClientDto>(client);
                        return Result<ResponseClientDto>.Success(response);
                    }
                    else
                        return Result.Failure("666", "Erro ao cadastrar um cliente");

                }
                else
                    return Result.Failure("400", "Ja existe cliente com este documento");//Erro q usuario ja existe com este documento.
            }
            catch (Exception e)
            {
                return Result.Failure("666", e.Message);
            }
        }
    }
}

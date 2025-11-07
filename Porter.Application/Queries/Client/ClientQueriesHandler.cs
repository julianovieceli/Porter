using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Personal.Common;
using Personal.Common.Domain;
using Porter.Application.Services;
using Porter.Domain.Interfaces;
using Porter.Domain.Validators;
using Porter.Dto;

namespace Porter.Application.Queries.Client
{
    public class GetAllClientsQueryHandler : BaseService, IRequestHandler<GetAllClientsQuery, Result>
    {
        private readonly IClientRepository _clientRepository;
        
        public GetAllClientsQueryHandler(ILogger<GetAllClientsQueryHandler> logger, IMapper dataMapper, IClientRepository clientRepository)
            : base(logger, dataMapper)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
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
    }

    public class GetClientByDoctoQueryHandler: BaseService, IRequestHandler<GetClientByDoctoQuery, Result>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByDoctoQueryHandler(ILogger<GetClientByDoctoQueryHandler> logger, IMapper dataMapper, IClientRepository clientRepository)
            : base(logger, dataMapper)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result> Handle(GetClientByDoctoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(request.Docto, "docto");

                if (!DocumentValidator.IsCpfCnpjValid(request.Docto))
                    return Result.Failure("400", "Documento inválido");

                var client = await _clientRepository.GetByDocto(request.Docto);

                if (client is null)
                    return Result.Failure("404", "Cliente não encontrado"); //erro nao encontrado
                else
                {
                    var clientDto = _dataMapper.Map<ResponseClientDto>(client);

                    return Result<ResponseClientDto>.Success(clientDto);
                }

            }
            catch (ArgumentNullException exArg)
            {
                return Result.Failure("400", "Documento obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar um Cliente");
                return Result.Failure("666", "Erro ao consultar um Cliente");
            }
        }
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Porter.Application.Commands.Client;
using Porter.Application.Services;
using Porter.Common;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Queries.Client
{
    public class ClientCommandsHandlerService : BaseService, IRequestHandler<RegisterClientCommand, Result>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IValidator<RegisterClientCommand> _clientValidator;
        
        public ClientCommandsHandlerService(ILogger<ClientCommandsHandlerService> logger, IMapper dataMapper, IClientRepository clientRepository,
            IValidator<RegisterClientCommand> clientValidator)
            : base(logger, dataMapper)
        {
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
        }

        public async Task<Result> Handle(RegisterClientCommand clientRequest, CancellationToken cancellationToken)
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

                    Domain.Client client = new Domain.Client(clientRequest.Name, clientRequest.Docto);

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

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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private ILogger<RoomService> _logger;
        private readonly IValidator<RequestRegisterRoomDto> _roomValidator;
        private readonly IMapper _dataMapper;

        public RoomService(ILogger<RoomService> logger, IMapper dataMapper, IRoomRepository roomRepository,
            IValidator<RequestRegisterRoomDto> roomValidator)
        {
            _logger = logger;
            _roomRepository = roomRepository;
            _dataMapper = dataMapper;
            _roomValidator = roomValidator;
        }


        public async Task<Result> GetAll()
        {
            try
            {
                var roomList = await _roomRepository.GetAll();

                IList<ResponseRoomDto> listToReturn = roomList.Select(u => _dataMapper.Map<ResponseRoomDto>(u)).ToList();

                return Result<IList<ResponseRoomDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "Erro ao consultar salas");
            }
        }

        public async Task<Result> GetByName(string name)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrWhiteSpace(name, "name");

                if (!NameValidator.IsValidName(name))
                    return Result.Failure("400", "Nome inválido");

                var room = await _roomRepository.GetByName(name);

                if (room is null)
                    return Result.Failure("404", "Sala não encontrada"); //erro nao encontrado
                else
                {
                    var roomDto = _dataMapper.Map<ResponseRoomDto>(room);

                    return Result<ResponseRoomDto>.Success(roomDto);
                }

            }
            catch(ArgumentNullException exArg)
            {
                return Result.Failure("400", "Nome da sala  obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                return Result.Failure("666", "Erro ao consultar uma Sala");
            }


        }

        public async Task<Result> Register(RequestRegisterRoomDto roomRequest)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(roomRequest, "roomRequest");

                var validatorResult = _roomValidator.Validate(roomRequest);
                if (!validatorResult.IsValid)
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                if (await _roomRepository.GetCountByName(roomRequest.Name) == 0)
                {

                    Room room = new Room(roomRequest.Name);

                    int roomRegistered = await _roomRepository.Register(room);

                    if (roomRegistered > 0)
                    {
                        var response = _dataMapper.Map<ResponseRoomDto>(room);
                        return Result<ResponseRoomDto>.Success(response);
                    }
                    else
                        return Result.Failure("666", "Erro ao cadastrar uma sala");

                }
                else
                    return Result.Failure("400", "Ja existe sala com este nome");
            }
            catch (Exception e)
            {
                return Result.Failure("666", e.Message);
            }
        }
    }
}

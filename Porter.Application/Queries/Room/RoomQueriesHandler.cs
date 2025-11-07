using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Personal.Common;
using Porter.Application.Queries.Room;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Services
{
    public class GetAllRoomsQueryHandler : BaseService, IRequestHandler<GetAllRoomsQuery, Result>
    {
        private readonly IRoomRepository _roomRepository;
        
        public GetAllRoomsQueryHandler(ILogger<GetAllRoomsQueryHandler> logger, IMapper dataMapper, IRoomRepository roomRepository)
            : base(logger, dataMapper)
        {
            _roomRepository = roomRepository;
        }
 
        public async Task<Result> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
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
    }

    public class GetRoomByNameQueryHandler : BaseService, IRequestHandler<GetRoomByNameQuery, Result>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByNameQueryHandler(ILogger<GetRoomByNameQueryHandler> logger, IMapper dataMapper, IRoomRepository roomRepository)
            : base(logger, dataMapper)
        {
            _roomRepository = roomRepository;
        }

 
        public async Task<Result> Handle(GetRoomByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request, "request");


                var room = await _roomRepository.GetByName(request.Name);

                if (room is null)
                    return Result.Failure("404", "Sala não encontrada"); //erro nao encontrado
                else
                {
                    var roomDto = _dataMapper.Map<ResponseRoomDto>(room);

                    return Result<ResponseRoomDto>.Success(roomDto);
                }

            }
            catch (ArgumentNullException exArg)
            {
                return Result.Failure("400", "Nome da sala  obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma sala");
                return Result.Failure("666", "Erro ao consultar uma Sala");
            }
        }
    }
}

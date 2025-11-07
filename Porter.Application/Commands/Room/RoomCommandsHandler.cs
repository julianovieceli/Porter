using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Personal.Common;
using Porter.Application.Commands.Booking;
using Porter.Application.Services;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Queries.Client
{
    public class RegisterRoomCommandHandler : BaseService, IRequestHandler<RegisterRoomCommand, Result>
    {

        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<RegisterRoomCommand> _roomValidator;

        public RegisterRoomCommandHandler(ILogger<RegisterRoomCommandHandler> logger, IMapper dataMapper, IRoomRepository roomRepository,
            IValidator<RegisterRoomCommand> roomValidator) : base(logger, dataMapper)
        {
            _roomRepository = roomRepository;
            _roomValidator = roomValidator;
        }

        public async Task<Result> Handle(RegisterRoomCommand roomRequest, CancellationToken cancellationToken)
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

                    Domain.Room room = new Domain.Room(roomRequest.Name);

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

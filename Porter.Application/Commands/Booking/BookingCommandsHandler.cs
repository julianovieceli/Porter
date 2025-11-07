using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Personal.Common.Domain;
using Personal.Common.Domain.ExtensionMethods;
using Personal.Common.Domain.Interfaces.Services;
using Personal.Common.Utils;
using Porter.Application.Commands.Booking;
using Porter.Domain;
using Porter.Domain.Interfaces;
using Porter.Dto;
using System.Reflection;

namespace Porter.Application.Services
{
    public class RegisterBookingCommandHandler : BaseService, IRequestHandler<RegisterBookingCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClientRepository _clientRepository;

        private readonly IValidator<RegisterBookingCommand> _bookingValidator;

        public RegisterBookingCommandHandler(ILogger<RegisterBookingCommandHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            IRoomRepository roomRepository, IClientRepository clientRepository, ILogService logService,
            IValidator<RegisterBookingCommand> bookingValidator)
            :base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _clientRepository = clientRepository;
            _bookingValidator = bookingValidator;
        }

        public async Task<Result> Handle(RegisterBookingCommand requestRegisterBookingDto, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(requestRegisterBookingDto, "requestRegisterBookingDto");

                var validatorResult = _bookingValidator.Validate(requestRegisterBookingDto);
                if (!validatorResult.IsValid)
                {
                    _logger.LogInformation(DateTime.Now.ToBrazilDatetime().ToString("yyyy-MM-dd HH:mm:ss") + " - " + 
                        requestRegisterBookingDto.StartDate.ToString("yyyy-MM-dd HH:mm:ss") + " - " + requestRegisterBookingDto.EndDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                Room room = await _roomRepository.GetByName(requestRegisterBookingDto.RoomName);
                if (room is null)
                    return Result.Failure("400", "Sala não encontrada");

                Client client = await _clientRepository.GetByDocto(requestRegisterBookingDto.DoctoReservedBy);
                if (client is null)
                    return Result.Failure("400", "Cliente não encontrado");


                if (await _bookingRepository.GetBookingCountByRoomAndPeriod(room.Id, null, requestRegisterBookingDto.StartDate,
                    requestRegisterBookingDto.EndDate) == 0)
                {

                    Domain.Booking booking = new Domain.Booking(room, client, requestRegisterBookingDto.StartDate, requestRegisterBookingDto.EndDate, requestRegisterBookingDto.Obs);

                    int bookingRegistered = await _bookingRepository.Register(booking);



                    if (bookingRegistered > 0)
                    {
                        await base._logService.LogInsert(booking, this.GetType().Name + "_" + MethodBase.GetCurrentMethod().DeclaringType.Name);

                        var response = _dataMapper.Map<ResponseBookingDto>(booking);
                        return Result<ResponseBookingDto>.Success(response);
                    }
                    else
                        return Result.Failure("666", "Erro ao cadastrar uma reserva");

                }
                else
                    return Result.Failure("400", "Ja existe reserva para esta sala neste período");
            }
            catch (Exception e)
            {
                return Result.Failure("666", e.Message);
            }
        }

        



    }


    public class UpdateBookingCommandHandler : BaseService, IRequestHandler< UpdateBookingCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClientRepository _clientRepository;

        private readonly IValidator<UpdateBookingCommand> _bookingUpdateValidator;

        public UpdateBookingCommandHandler(ILogger<UpdateBookingCommandHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            ILogService logService,
            IValidator<UpdateBookingCommand> bookingUpdateValidator)
            : base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
            _bookingUpdateValidator = bookingUpdateValidator;
        }

        public async Task<Result> Handle(UpdateBookingCommand requestUpdateBookingDto, CancellationToken cancellationToken)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(requestUpdateBookingDto, "requestUpdateBookingDto");

                var validatorResult = _bookingUpdateValidator.Validate(requestUpdateBookingDto);
                if (!validatorResult.IsValid)
                {
                    return Result.Failure("400", validatorResult.Errors.FirstOrDefault().ErrorMessage);//Erro q usuario ja existe com este documento.
                }

                var booking = await _bookingRepository.GetById(requestUpdateBookingDto.Id);
                if (booking is null)
                    return Result.Failure("400", "Reerva não encontrada");

                Domain.Booking bookingToLog = JsonUtils.DeepClone(booking);

                // Verifica se ja existe outra reserva para a mesma sala e periodo que NAO seja a mesma reserva...
                if (await _bookingRepository.GetBookingCountByRoomAndPeriod(booking.Room.Id, booking.Id, requestUpdateBookingDto.StartDate,
                    requestUpdateBookingDto.EndDate) == 0)
                {
                    booking.Update(requestUpdateBookingDto.StartDate, requestUpdateBookingDto.EndDate,
                        requestUpdateBookingDto.Obs);

                    int bookingRegistered = await _bookingRepository.Update(booking);

                    if (bookingRegistered > 0)
                    {
                        await base._logService.LogUpdate(bookingToLog, this.GetType().Name + "_" + MethodBase.GetCurrentMethod().DeclaringType.Name);

                        booking = await _bookingRepository.GetById(requestUpdateBookingDto.Id);

                        var response = _dataMapper.Map<ResponseBookingDto>(booking);
                        return Result<ResponseBookingDto>.Success(response);
                    }
                    else
                        return Result.Failure("666", "Erro ao atualizar uma reserva");

                }
                else
                    return Result.Failure("400", "Ja existe reserva para esta sala neste período");
            }
            catch (Exception e)
            {
                return Result.Failure("666", e.Message);
            }
        }

  

    }


    public class DeleteBookingCommandHandler : BaseService, IRequestHandler<DeleteBookingByIdCommand, Result>
    {
        private readonly IBookingRepository _bookingRepository;


        public DeleteBookingCommandHandler(ILogger<DeleteBookingCommandHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            ILogService logService)
            : base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
        }


        public async Task<Result> Handle(DeleteBookingByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                    return Result.Failure("400", "Id inválido");

                if (await _bookingRepository.Delete(request.Id) > 0)
                {
                    await base._logService.LogDelete(new Domain.Booking() { Id = request.Id }, this.GetType().Name + "_" + MethodBase.GetCurrentMethod().DeclaringType.Name);
                    return Result.Success;
                }
                else
                {
                    return Result.Failure("400", "Reserva não encontrada");
                }
            }
            catch (Exception e)
            {
                return Result.Failure("666", e.Message);
            }
        }
    }
}

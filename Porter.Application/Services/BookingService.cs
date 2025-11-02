using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Common.Utils;
using Porter.Domain;
using Porter.Domain.Interfaces;
using Porter.Dto;
using System.Reflection;

namespace Porter.Application.Services
{
    public class BookingService : BaseService, IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClientRepository _clientRepository;

        private readonly IValidator<RequestRegisterBookingDto> _bookingValidator;
        private readonly IValidator<RequestUpdateBookingDto> _bookingUpdateValidator;

        public BookingService(ILogger<BookingService> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            IRoomRepository roomRepository, IClientRepository clientRepository, ILogRepository logRepository,
            IValidator<RequestRegisterBookingDto> bookingValidator, IValidator<RequestUpdateBookingDto> bookingUpdateValidator)
            :base(logger, dataMapper, logRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _clientRepository = clientRepository;
            _bookingValidator = bookingValidator;
            _bookingUpdateValidator = bookingUpdateValidator;
        }


        public async Task<Result> GetAll()
        {
            try
            {
                var bookingList = await _bookingRepository.GetAll();

                await base.LogList(new Booking(), MethodBase.GetCurrentMethod().DeclaringType.Name);

                IList<ResponseBookingDto> listToReturn = bookingList.Select(u => _dataMapper.Map<ResponseBookingDto>(u)).ToList();

                return Result<IList<ResponseBookingDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "Erro ao consultar reservas");
            }
        }

        public async Task<Result> GetById(int Id)
        {
            try
            {

                if (Id <= 0)
                    return Result.Failure("400", "Id inválido");

                var booking = await _bookingRepository.GetById(Id);

                await base.LogView(booking, MethodBase.GetCurrentMethod().DeclaringType.Name);

                if (booking is null)
                    return Result.Failure("404", "Reserva não encontrada"); //erro nao encontrado
                else
                {
                    var bookingDto = _dataMapper.Map<ResponseBookingDto>(booking);

                    return Result<ResponseBookingDto>.Success(bookingDto);
                }

            }
            catch(ArgumentNullException exArg)
            {
                return Result.Failure("400", "Id obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma reserva");
                return Result.Failure("666", "Erro ao consultar uma reserva");
            }


        }

        public async Task<Result> Register(RequestRegisterBookingDto requestRegisterBookingDto)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(requestRegisterBookingDto, "requestRegisterBookingDto");

                var validatorResult = _bookingValidator.Validate(requestRegisterBookingDto);
                if (!validatorResult.IsValid)
                {
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

                    Booking booking = new Booking(room, client, requestRegisterBookingDto.StartDate,requestRegisterBookingDto.EndDate, requestRegisterBookingDto.Obs);

                    int bookingRegistered = await _bookingRepository.Register(booking);



                    if (bookingRegistered > 0)
                    {
                        await base.LogInsert(booking, MethodBase.GetCurrentMethod().DeclaringType.Name);

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


        public async Task<Result> Delete(int Id)
        {
            try
            {
                if (Id <= 0)
                    return Result.Failure("400", "Id inválido");

                if (await _bookingRepository.Delete(Id) > 0)
                {
                    await base.LogDelete(new Booking() { Id = Id}, MethodBase.GetCurrentMethod().DeclaringType.Name);
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

        public async Task<Result> GetBookingListByRoomAndPeriod(int roomId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var bookingList = await _bookingRepository.GetBookingListByRoomAndPeriod(roomId, startDate, endDate);

                IList<ResponseBookingDto> listToReturn = bookingList.Select(u => _dataMapper.Map<ResponseBookingDto>(u)).ToList();

                await base.LogList(listToReturn, MethodBase.GetCurrentMethod().DeclaringType.Name);

                return Result<IList<ResponseBookingDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "Erro ao consultar reservas");
            }
        }

        public async Task<Result> Update(RequestUpdateBookingDto requestUpdateBookingDto)
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

                Booking bookingToLog = JsonUtils.DeepClone(booking);

                // Verifica se ja existe outra reserva para a mesma sala e periodo que NAO seja a mesma reserva...
                if (await _bookingRepository.GetBookingCountByRoomAndPeriod(booking.Room.Id, booking.Id, requestUpdateBookingDto.StartDate,
                    requestUpdateBookingDto.EndDate) == 0 )
                {
                    booking.Update(requestUpdateBookingDto.StartDate, requestUpdateBookingDto.EndDate,
                        requestUpdateBookingDto.Obs);

                    int bookingRegistered = await _bookingRepository.Update(booking);

                    if (bookingRegistered > 0)
                    {
                        await base.LogUpdate(bookingToLog, MethodBase.GetCurrentMethod().DeclaringType.Name);

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
}

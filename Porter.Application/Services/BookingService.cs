using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Domain;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IClientRepository _clientRepository;
        private ILogger<BookingService> _logger;
        private readonly IValidator<RequestRegisterBookingDto> _bookingValidator;
        private readonly IMapper _dataMapper;

        public BookingService(ILogger<BookingService> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            IRoomRepository roomRepository, IClientRepository clientRepository,
            IValidator<RequestRegisterBookingDto> bookingValidator)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _clientRepository = clientRepository;
            _dataMapper = dataMapper;
            _bookingValidator = bookingValidator;
        }


        public async Task<Result> GetAll()
        {
            try
            {
                var bookingList = await _bookingRepository.GetAll();

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


                if (await _bookingRepository.GetBookingCountByRoomAndPeriod(room.Id, requestRegisterBookingDto.StartDate,
                    requestRegisterBookingDto.EndDate) == 0)
                {

                    Booking booking = new Booking(room, client, requestRegisterBookingDto.StartDate,requestRegisterBookingDto.EndDate, requestRegisterBookingDto.Obs);

                    int bookingRegistered = await _bookingRepository.Register(booking);

                    if (bookingRegistered > 0)
                    {
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
}

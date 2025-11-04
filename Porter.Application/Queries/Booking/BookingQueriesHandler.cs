using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Porter.Application.Queries.Booking;
using Porter.Common;
using Porter.Common.Services;
using Porter.Domain.Interfaces;
using Porter.Dto;
using System.Reflection;

namespace Porter.Application.Services.Booking
{
    public class GetAllBookingsQueryHandler : BaseService, IRequestHandler<GetAllBookingsQuery, Result>
    {
        private readonly IBookingRepository _bookingRepository;


        public GetAllBookingsQueryHandler(ILogger<GetAllBookingsQueryHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            ILogService logService)
            : base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
        }


        public async Task<Result> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingList = await _bookingRepository.GetAll();

                await base._logService.LogList(new Domain.Booking(), MethodBase.GetCurrentMethod().DeclaringType.Name);

                IList<ResponseBookingDto> listToReturn = bookingList.Select(u => _dataMapper.Map<ResponseBookingDto>(u)).ToList();

                return Result<IList<ResponseBookingDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "Erro ao consultar reservas");
            }
        }
    }


    public class GetBookingByIdQueryHandler : BaseService, IRequestHandler<GetBookingByIdQuery, Result>
    {
        private readonly IBookingRepository _bookingRepository;


        public GetBookingByIdQueryHandler(ILogger<GetBookingByIdQueryHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            ILogService logService)
            : base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
        }

        public async  Task<Result> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.Id <= 0)
                    return Result.Failure("400", "Id inválido");

                var booking = await _bookingRepository.GetById(request.Id);

                await base._logService.LogView(booking, MethodBase.GetCurrentMethod().DeclaringType.Name);

                if (booking is null)
                    return Result.Failure("404", "Reserva não encontrada"); //erro nao encontrado
                else
                {
                    var bookingDto = _dataMapper.Map<ResponseBookingDto>(booking);

                    return Result<ResponseBookingDto>.Success(bookingDto);
                }

            }
            catch (ArgumentNullException exArg)
            {
                return Result.Failure("400", "Id obrigatório");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao consultar uma reserva");
                return Result.Failure("666", "Erro ao consultar uma reserva");
            }
        }
    }

    public class GetBookingListByRoomAndPeriodQueryHandler : BaseService, IRequestHandler<GetBookingListByRoomAndPeriodQuery, Result>
    {
        private readonly IBookingRepository _bookingRepository;


        public GetBookingListByRoomAndPeriodQueryHandler(ILogger<GetBookingListByRoomAndPeriodQueryHandler> logger, IMapper dataMapper, IBookingRepository bookingRepository,
            ILogService logService)
            : base(logger, dataMapper, logService)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result> Handle(GetBookingListByRoomAndPeriodQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bookingList = await _bookingRepository.GetBookingListByRoomAndPeriod(request.RoomId, request.StartDate, request.EndDate);

                IList<ResponseBookingDto> listToReturn = bookingList.Select(u => _dataMapper.Map<ResponseBookingDto>(u)).ToList();

                await base._logService.LogList(listToReturn, MethodBase.GetCurrentMethod().DeclaringType.Name);

                return Result<IList<ResponseBookingDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "Erro ao consultar reservas");
            }
        }
    }
}

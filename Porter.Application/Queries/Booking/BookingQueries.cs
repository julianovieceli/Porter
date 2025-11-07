using MediatR;
using Personal.Common;

namespace Porter.Application.Queries.Booking
{
    public class GetAllBookingsQuery : IRequest<Result> { }

    public class GetBookingByIdQuery : IRequest<Result>
    { 
        public int Id { get; set; }
    }

    public class GetBookingListByRoomAndPeriodQuery : IRequest<Result>
    {
        public int RoomId;
        public DateTime StartDate;
        public DateTime EndDate;
     }
}

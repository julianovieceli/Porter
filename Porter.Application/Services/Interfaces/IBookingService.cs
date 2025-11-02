using Porter.Common;
using Porter.Dto;

namespace Porter.Application.Services.Interfaces
{
    public interface IBookingService
    {
        Task<Result> GetAll();

        Task<Result> GetById(int Id);

        Task<Result> Register(RequestRegisterBookingDto bookingRequest);

        Task<Result> Delete(int Id);

        Task<Result> Update(RequestUpdateBookingDto requestUpdateBookingDto);

        Task<Result> GetBookingListByRoomAndPeriod(int roomId, DateTime startDate, DateTime endDate);
    }
}

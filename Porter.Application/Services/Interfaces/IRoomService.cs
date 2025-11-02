using Porter.Common;
using Porter.Dto;

namespace Porter.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<Result> GetAll();

        Task<Result> GetByName(string name);

        Task<Result> Register(RequestRegisterRoomDto roomRequest);
    }
}

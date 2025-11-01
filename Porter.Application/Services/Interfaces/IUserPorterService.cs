using Porter.Common;

namespace Porter.Application.Services.Interfaces
{
    public interface IUserPorterService
    {
        Task<Result> GetAll();
    }
}

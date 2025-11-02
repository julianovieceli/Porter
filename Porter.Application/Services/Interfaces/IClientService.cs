using Porter.Common;

namespace Porter.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> GetAll();
    }
}

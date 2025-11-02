using Porter.Common;
using Porter.Dto;

namespace Porter.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<Result> GetAll();

        Task<Result> GetByDocto(string docto);

        Task<Result> Register(RequestRegisterClientDto clientRequest);
    }
}

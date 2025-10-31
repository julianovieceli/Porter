using Porter.Domain;
using Porter.Dto;

namespace Porter.Application.Services.Interfaces
{
    public interface IUserPorterService
    {
        Task<IList<ResponseUserPorterDto>> GetAll();
    }
}

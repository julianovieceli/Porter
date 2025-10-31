using AutoMapper;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Domain.Interfaces;
using Porter.Dto;

namespace Porter.Application.Services
{
    public class UserPorterService: IUserPorterService
    {
        private readonly IUserPorterRepository _userPorterRepository;
        private ILogger<UserPorterService> _logger;
        private readonly IMapper _dataMapper;

        public UserPorterService(ILogger<UserPorterService> logger, IMapper dataMapper, IUserPorterRepository userPorterRepository)
        {
            _logger = logger;
            _userPorterRepository = userPorterRepository;
            _dataMapper = dataMapper;
        }


        public async Task<IList<ResponseUserPorterDto>> GetAll()
        {
            var userList = await _userPorterRepository.GetAll();

            return userList.Select(u => _dataMapper.Map<ResponseUserPorterDto>(u)).ToList();
        }
    }
}

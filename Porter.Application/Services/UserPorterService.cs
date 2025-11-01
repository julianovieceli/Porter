using AutoMapper;
using Microsoft.Extensions.Logging;
using Porter.Application.Services.Interfaces;
using Porter.Common;
using Porter.Domain.Interfaces;
using Porter.Dto;
using System.Collections.Generic;

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


        public async Task<Result> GetAll()
        {
            try
            {
                var userList = await _userPorterRepository.GetAll();

                IList<ResponseUserPorterDto> listToReturn = userList.Select(u => _dataMapper.Map<ResponseUserPorterDto>(u)).ToList();

                return Result<IList<ResponseUserPorterDto>>.Success(listToReturn);
            }
            catch (Exception ex)
            {
                return Result.Failure("666", "An error occurred while fetching users");
            }
        }
    }
}

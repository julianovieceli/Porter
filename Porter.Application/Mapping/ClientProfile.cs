using AutoMapper;
using Porter.Domain;
using Porter.Dto;

namespace Porter.Application.Mapping
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            
            CreateMap<Client, ResponseClientDto>().ReverseMap();
        }
    }
}

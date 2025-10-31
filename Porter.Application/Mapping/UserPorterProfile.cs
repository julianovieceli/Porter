using AutoMapper;
using Porter.Domain;
using Porter.Dto;

namespace Porter.Application.Mapping
{
    public class UserPorterProfile : Profile
    {
        public UserPorterProfile()
        {
            //CreateMap<UserPorter, ResponseUserPorterDto>()
            //.ForMember(to => to.Id, opt => opt.MapFrom(from => from.Id))
            //.ForMember(to => to.Login, opt => opt.MapFrom(from => from.Login))
            //ReverseMap();

            CreateMap<UserPorter, ResponseUserPorterDto>().ReverseMap();

        }
    }
}

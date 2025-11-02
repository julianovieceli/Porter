using AutoMapper;
using Porter.Domain;
using Porter.Dto;

namespace Porter.Application.Mapping
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            
            CreateMap<Room, ResponseRoomDto>().ReverseMap();
            CreateMap<Room, RequestRegisterRoomDto>().ReverseMap();

        }
    }
}

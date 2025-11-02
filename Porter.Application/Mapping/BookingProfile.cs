using AutoMapper;
using Porter.Domain;
using Porter.Dto;

namespace Porter.Application.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            
            CreateMap<Booking, RequestRegisterBookingDto>().ReverseMap();

            CreateMap<Booking, ResponseBookingDto>()
                .ForMember(to => to.Room, opt => opt.MapFrom(from => from.Room.Name))
                .ForMember(to => to.RoomId, opt => opt.MapFrom(from => from.Room.Id))
                .ForMember(to => to.ReservedBy, opt => opt.MapFrom(from => from.ReservedBy.Name))
                .ForMember(to => to.DoctoReservedBy, opt => opt.MapFrom(from => from.ReservedBy.Docto))
                ;
            

        }
    }
}

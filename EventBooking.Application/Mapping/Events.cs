using AutoMapper;
using EventBooking.Application.DTOs.Event;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Mapping
{
    public class Events : Profile
    {
        public Events()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<CreateEventDTO, Event>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateEventDTO, Event>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<EventDTO, Event>().ReverseMap();

        }
    }
}

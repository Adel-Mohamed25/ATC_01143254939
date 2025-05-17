using AutoMapper;
using EventBooking.Application.DTOs.Booking;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Mapping
{
    public class Bookings : Profile
    {
        public Bookings()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<CreateBookingDTO, Booking>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.BookingDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateBookingDTO, Booking>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<BookingDTO, Booking>().ReverseMap();

        }
    }
}

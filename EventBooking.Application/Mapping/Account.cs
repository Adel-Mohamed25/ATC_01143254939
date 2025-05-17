using AutoMapper;
using EventBooking.Application.DTOs.Account;
using EventBooking.Domain.Entities.IdentityEntities;

namespace EventBooking.Application.Mapping
{
    public class Account : Profile
    {
        public Account()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<RegisterDTO, User>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Email));

        }
    }
}

using EventBooking.Domain.Entities;
using EventBooking.Domain.IRepositories;
using EventBooking.Persistence.Context;

namespace EventBooking.Infrastructure.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(IEventBookingDbContext context)
            : base(context) { }
    }
}

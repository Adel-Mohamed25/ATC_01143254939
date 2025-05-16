using EventBooking.Domain.Entities;
using EventBooking.Domain.IRepositories;
using EventBooking.Persistence.Context;

namespace EventBooking.Infrastructure.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(IEventBookingDbContext context)
            : base(context) { }
    }
}

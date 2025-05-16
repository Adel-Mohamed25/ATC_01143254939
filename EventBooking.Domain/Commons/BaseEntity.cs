namespace EventBooking.Domain.Commons
{
    public class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
    }
}

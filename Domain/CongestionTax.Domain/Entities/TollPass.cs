namespace CongestionTax.Domain.Entities
{
    public class TollPass
    {
        public TollPass()
        {
            
        }
        public TollPass(DateTime dateTime, Vehicle vehicle)
        {
            Timestamp = dateTime;
            Vehicle = vehicle;
        }
        public int Id { get; set; }
        public DateTime Timestamp { get; private set; }
        public Vehicle Vehicle { get; private set; }
    }
}

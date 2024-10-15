namespace CongestionTax.Domain.ValueObjects
{
    public class TaxRule
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal Amount { get; set; }
    }
}

using CongestionTax.Domain.Entities;

namespace CongestionTax.Domain.Strategies
{
    public interface ITaxStrategy
    {
        decimal CalculateTaxPerDay(Vehicle vehicle, DateTime date, List<TollPass> tollPasses);
        bool IsTollFreeVehicle(Vehicle vehicle);
        bool IsTollFreeDate(DateTime date);
    }
}


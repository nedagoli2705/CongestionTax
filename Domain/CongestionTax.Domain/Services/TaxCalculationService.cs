using CongestionTax.Domain.Entities;
using CongestionTax.Domain.Strategies;

namespace CongestionTax.Domain.Services
{
    public class TaxCalculationService
    {
        private readonly ITaxStrategy _taxStrategy;

        public TaxCalculationService(ITaxStrategy taxStrategy)
        {
            _taxStrategy = taxStrategy;
        }

        public decimal CalculateTaxPerDay(Vehicle vehicle, DateTime date, List<TollPass> tollPasses)
        {
            return _taxStrategy.CalculateTaxPerDay(vehicle, date, tollPasses);
        }
    }
}



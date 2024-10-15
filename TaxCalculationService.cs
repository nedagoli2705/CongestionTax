using System;

public class TaxCalculationService
{
    private readonly ITaxStrategy _taxStrategy;

    public TaxCalculationService(ITaxStrategy taxStrategy)
    {
        _taxStrategy = taxStrategy;
    }

    public decimal CalculateTax(Vehicle vehicle, DateTime[] dates)
    {
        return _taxStrategy.CalculateTax(vehicle, dates);
    }
}

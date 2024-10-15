using System;

public class TaxAppService
{
    private readonly ITaxRepository _taxRepository;
    private readonly TaxCalculationService _taxCalculationService;

    public TaxAppService(ITaxRepository taxRepository, TaxCalculationService taxCalculationService)
    {
        _taxRepository = taxRepository;
        _taxCalculationService = taxCalculationService;
    }

    public decimal GetTaxForVehicle(Vehicle vehicle, DateTime[] dates)
    {
        return _taxCalculationService.CalculateTax(vehicle, dates);
    }

    // Additional methods to interact with the domain layer
}
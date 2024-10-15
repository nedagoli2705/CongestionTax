using System;

public class TaxRepository : ITaxRepository
{
    public IEnumerable<DateTime> GetTollFreeDates()
    {
        // Normally, you'd fetch this from a database or external service
        return new List<DateTime> { new DateTime(2023, 12, 25) }; // Example of a holiday
    }

    public decimal GetMaxTaxPerDay()
    {
        return 60m;
    }
}

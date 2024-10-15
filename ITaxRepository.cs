using System;

public interface ITaxRepository
{
    IEnumerable<DateTime> GetTollFreeDates();
    decimal GetMaxTaxPerDay();
    // Add more methods for fetching tax data or city configurations
}

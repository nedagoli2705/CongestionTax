using System;

public class GothenburgTaxStrategy : ITaxStrategy
{
    public decimal CalculateTax(Vehicle vehicle, DateTime[] dates)
    {
        decimal totalTax = 0;
        DateTime intervalStart = dates[0];

        foreach (var date in dates)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
                continue;

            var tempTax = GetTollFee(date);
            var diffInMinutes = (date - intervalStart).TotalMinutes;

            if (diffInMinutes <= 60)
                continue;

            totalTax += tempTax;
            intervalStart = date;

            if (totalTax > 60) totalTax = 60; // Max daily limit
        }

        return totalTax;
    }

    public bool IsTollFreeVehicle(Vehicle vehicle)
    {
        return vehicle.GetVehicleType() == "Motorbike"; // Add more toll-free vehicles
    }

    public bool IsTollFreeDate(DateTime date)
    {
        return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        // Add public holidays as well
    }

    private decimal GetTollFee(DateTime date)
    {
        var hour = date.Hour;

        if (hour >= 6 && hour < 7) return 8;
        if (hour >= 7 && hour < 8) return 13;
        if (hour >= 8 && hour < 15) return 18;
        if (hour >= 15 && hour < 17) return 13;
        if (hour >= 17 && hour < 18) return 8;
        return 0;
    }
}

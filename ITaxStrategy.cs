using System;

public interface ITaxStrategy
{
    decimal CalculateTax(Vehicle vehicle, DateTime[] dates);
    bool IsTollFreeVehicle(Vehicle vehicle);
    bool IsTollFreeDate(DateTime date);
}

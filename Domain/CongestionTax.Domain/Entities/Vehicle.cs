namespace CongestionTax.Domain.Entities
{
    public class Vehicle
    {
        public Vehicle(int id, string vehicleType, bool isExempt)
        {
            Id = id;
            VehicleType = vehicleType;
            IsExempt = isExempt;
        }

        public int Id { get; private set; }
        public string VehicleType { get; private set; }
        public bool IsExempt { get; private set; }

    }
}



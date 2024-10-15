using CongestionTax.Domain.Entities;

namespace CongestionTax.Application.Interfaces
{
    public interface ITaxRepository
    {
        //IEnumerable<DateTime> GetTollFreeDates();
        Task<Vehicle> GetVehicleById(int vehicleId);
        Task AddTollPass(TollPass tollPass);

        Task<List<TollPass>> GetTollPassesPerDayByVehicleId(DateTime date, int vehicleId);

    }
}



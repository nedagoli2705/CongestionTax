using CongestionTax.Application.Interfaces;
using CongestionTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CongestionTax.Infrastructure.Persistence
{
    public class TaxRepository : ITaxRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public TaxRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        //public IEnumerable<DateTime> GetTollFreeDates()
        //{
        //    // Normally, you'd fetch this from a database or external service
        //    return new List<DateTime> { new DateTime(2023, 12, 25) }; // Example of a holiday
        //}
        public async Task AddTollPass(TollPass tollPass)
        {
            await _dbContext.TollPasses.AddAsync(tollPass);
            _dbContext.SaveChanges();
        }

        public Task<List<TollPass>> GetTollPassesPerDayByVehicleId(DateTime date, int vehicleId)
        {
            var tollPasses = _dbContext.TollPasses
                .Where(a => a.Timestamp.Date == date.Date && a.Vehicle.Id == vehicleId)
                .OrderBy(a => a.Timestamp)
                .ToListAsync();

            return tollPasses;
        }

        public async Task<Vehicle> GetVehicleById(int vehicleId)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(t => t.Id == vehicleId);
        }
    }
}



using CongestionTax.Application.Interfaces;
using CongestionTax.Application.DTOs;
using CongestionTax.Domain.Services;
using CongestionTax.Domain.Entities;

namespace CongestionTax.Application.Services
{
    public class TaxAppService
    {
        private readonly ITaxRepository _taxRepository;
        private readonly TaxCalculationService _taxCalculationService;

        public TaxAppService(ITaxRepository taxRepository, TaxCalculationService taxCalculationService)
        {
            _taxRepository = taxRepository;
            _taxCalculationService = taxCalculationService;
        }
        public decimal? CalculateTaxPerDay(int vehicleId, DateTime date)
        {
            var vehicle = _taxRepository.GetVehicleById(vehicleId);
            var tollPasses = _taxRepository.GetTollPassesPerDayByVehicleId(date, vehicle.Result.Id);
         
            return _taxCalculationService.CalculateTaxPerDay(vehicle.Result, date, tollPasses.Result);
        }

        public decimal? CalculateTaxInPeriod(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var vehicle = _taxRepository.GetVehicleById(vehicleId);
            List<DateTime> datesInPeriod = new List<DateTime>();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                datesInPeriod.Add(date);
            }

            decimal totalTax = 0;

            foreach (var date in datesInPeriod)
            {
                var tollPasses = _taxRepository.GetTollPassesPerDayByVehicleId(date, vehicle.Result.Id);

                totalTax += _taxCalculationService.CalculateTaxPerDay(vehicle.Result, date, tollPasses.Result);
            }
            return totalTax;
        }

        public void AddTollPass(TollPassDto tollPassDto)
        {
            var vehicle = _taxRepository.GetVehicleById(tollPassDto.VehicleId);

            var tollPass = new TollPass(tollPassDto.PassDateTime, vehicle.Result);

            _taxRepository.AddTollPass(tollPass);
        }

    }
}


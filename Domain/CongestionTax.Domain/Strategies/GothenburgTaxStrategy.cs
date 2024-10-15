using CongestionTax.Domain.Entities;
using CongestionTax.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Json;

namespace CongestionTax.Domain.Strategies
{
    public class GothenburgTaxStrategy : ITaxStrategy
    {
        private readonly CityTaxOptions _cityTaxOptions;
        private readonly HolidayService _holidayService;

        public GothenburgTaxStrategy(IOptions<List<CityTaxOptions>> cityTaxOptions, HolidayService holidayService)
        {
            _cityTaxOptions = cityTaxOptions.Value.Single(c => c.CityName == "Gothenburg");
            _holidayService = holidayService;
        }

        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            return vehicle.IsExempt; 
        }

        public bool IsTollFreeDate(DateTime date)
        {
            if (IsHoliday(date).Result)
            {
                return true;
            }

            return false;
        }

        private decimal GetTollFee(DateTime date)
        {
            var rules = _cityTaxOptions.TaxRules;

            foreach (var rule in rules)
            {
                if (date.TimeOfDay >= rule.StartTime.ToTimeSpan() && date.TimeOfDay <= rule.EndTime.ToTimeSpan())
                {
                    return rule.Amount;
                }
            }
            return 0;
        }

        public decimal CalculateTaxPerDay(Vehicle vehicle, DateTime date, List<TollPass> tollPasses)
        {
            if (IsTollFreeVehicle(vehicle) || IsTollFreeDate(date))
                return 0;

            DateTime? currentWindowStart = null;
            decimal maxTaxInWindow = 0;
            decimal totalTax = 0;
            decimal dailyMaxTax = _cityTaxOptions.MaxTaxPerDay;

            foreach (var tollPass in tollPasses)
            {
                if (currentWindowStart == null)
                {
                    currentWindowStart = tollPass.Timestamp;
                    maxTaxInWindow = GetTollFee(currentWindowStart.Value);
                }
                else
                {
                    var timeDifference = tollPass.Timestamp - currentWindowStart.Value;
                    var currentTollPassAmount = GetTollFee(tollPass.Timestamp);
                    if (timeDifference.TotalMinutes <= 60)
                    {
                        if (currentTollPassAmount > maxTaxInWindow)
                        {
                            maxTaxInWindow = currentTollPassAmount;
                        }
                    }
                    else
                    {
                        totalTax += maxTaxInWindow;

                        currentWindowStart = tollPass.Timestamp;
                        maxTaxInWindow = currentTollPassAmount;

                        if (totalTax >= dailyMaxTax)
                        {
                            return dailyMaxTax;
                        }
                    }
                }
            }

            totalTax += maxTaxInWindow;

            return Math.Min(totalTax, dailyMaxTax);
        }

        private async Task<bool> IsHoliday(DateTime date)
        {
            int year = date.Year;
            string countryCode = "SE"; // Country code for Sweden

            var holidays = await _holidayService.GetHolidaysAsync(year, countryCode);

            return holidays.Any(h => DateTime.Parse(h.Date) == date);
        }
    }
}



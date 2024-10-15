using CongestionTax.Domain.Entities;
using CongestionTax.Domain.Strategies;
using CongestionTax.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using Moq;

namespace CongestionTax.Tests
{
    public class GothenburgTaxStrategyTests
    {
        private readonly GothenburgTaxStrategy _taxStrategy;
        private readonly Mock<HolidayService> _mockHolidayService;
        private readonly IOptions<List<CityTaxOptions>> _cityTaxOptions;

        public GothenburgTaxStrategyTests()
        {
            // Create city tax options with mock data
            var cityTaxOptions = new List<CityTaxOptions>
        {
            new CityTaxOptions
            {
                CityName = "Gothenburg",
                MaxTaxPerDay = 60m,
                TaxRules = new List<TaxRule>
                {
                    new TaxRule { StartTime = TimeOnly.Parse("06:00"), EndTime = TimeOnly.Parse("06:29"), Amount = 8m },
                    new TaxRule { StartTime = TimeOnly.Parse("06:30"), EndTime = TimeOnly.Parse("06:59"), Amount = 13m },
                    new TaxRule { StartTime = TimeOnly.Parse("07:00"), EndTime = TimeOnly.Parse("07:59"), Amount = 18m },
                    new TaxRule { StartTime = TimeOnly.Parse("08:30"), EndTime = TimeOnly.Parse("08:29"), Amount = 13m },
                    new TaxRule { StartTime = TimeOnly.Parse("08:30"), EndTime = TimeOnly.Parse("14:59"), Amount = 18m }
                    
                }
            }
        };

            _cityTaxOptions = Options.Create(cityTaxOptions);

            var mockHttpClient = new Mock<HttpClient>();
            _mockHolidayService = new Mock<HolidayService>(mockHttpClient.Object);

            _taxStrategy = new GothenburgTaxStrategy(_cityTaxOptions, _mockHolidayService.Object);
        }

        [Fact]
        public void IsTollFreeVehicle_ShouldReturnTrue_ForExemptVehicle()
        {
            // Arrange
            var exemptVehicle = new Vehicle (1, "car", true);

            // Act
            var result = _taxStrategy.IsTollFreeVehicle(exemptVehicle);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsTollFreeVehicle_ShouldReturnFalse_ForNonExemptVehicle()
        {
            var nonExemptVehicle = new Vehicle (1, "car", false);

            var result = _taxStrategy.IsTollFreeVehicle(nonExemptVehicle);

            Assert.False(result);
        }

        [Fact]
        public async Task IsTollFreeDate_ShouldReturnTrue_OnHoliday()
        {
            var holidayDate = new DateTime(2023, 12, 25); // Christmas
            _mockHolidayService
                .Setup(s => s.GetHolidaysAsync(2023, "SE"))
                .ReturnsAsync(new List<Holiday> { new Holiday { Date = "2023-12-25" } });

            var result = _taxStrategy.IsTollFreeDate(holidayDate);

            Assert.True(result);
        }

        [Fact]
        public async Task IsTollFreeDate_ShouldReturnFalse_OnNonHoliday()
        {
            // Arrange
            var nonHolidayDate = new DateTime(2023, 12, 26);
            _mockHolidayService
                .Setup(s => s.GetHolidaysAsync(2023, "SE"))
                .ReturnsAsync(new List<Holiday>());

            // Act
            var result = _taxStrategy.IsTollFreeDate(nonHolidayDate);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateTaxPerDay_ShouldReturnZero_ForExemptVehicle()
        {
            // Arrange
            var exemptVehicle = new Vehicle(1, "bus", true);
            var tollPasses = new List<TollPass>
        {
            new TollPass (new DateTime(2023, 8, 10, 6, 15, 0), exemptVehicle)
        };

            // Act
            var result = _taxStrategy.CalculateTaxPerDay(exemptVehicle, new DateTime(2023, 8, 10), tollPasses);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateTaxPerDay_ShouldRespectDailyMaxTax()
        {
            // Arrange
            var vehicle = new Vehicle(1, "car", false);

            var nonHolidayDate = new DateTime(2023, 12, 26);
            _mockHolidayService
                .Setup(s => s.GetHolidaysAsync(2023, "SE"))
                .ReturnsAsync(new List<Holiday>());

            var tollPasses = new List<TollPass>
        {
            new TollPass (new DateTime(2023, 8, 10, 6, 15, 0), vehicle),
            new TollPass (new DateTime(2023, 8, 10, 7, 30, 0), vehicle),
            new TollPass (new DateTime(2023, 8, 10, 9, 00, 0), vehicle),
            new TollPass (new DateTime(2023, 8, 10, 14, 00, 0), vehicle)
        };

            // Act
            var result = _taxStrategy.CalculateTaxPerDay(vehicle, new DateTime(2023, 8, 10), tollPasses);

            // Assert
            Assert.Equal(60, result); // Assuming daily max tax is 60
        }

        [Fact]
        public void CalculateTaxPerDay_TwoTollPassesIn60Minutes_ShouldApplyOneTaxWhichIsMax()
        {
            // Arrange
            var vehicle = new Vehicle(1, "car", false);

            var nonHolidayDate = new DateTime(2023, 12, 26);
            _mockHolidayService
                .Setup(s => s.GetHolidaysAsync(2023, "SE"))
                .ReturnsAsync(new List<Holiday>());

            var tollPasses = new List<TollPass>
        {
            new TollPass (new DateTime(2023, 8, 10, 6, 15, 0), vehicle),
            new TollPass (new DateTime(2023, 8, 10, 6, 32, 0), vehicle)
        };

            // Act
            var result = _taxStrategy.CalculateTaxPerDay(vehicle, new DateTime(2023, 8, 10), tollPasses);

            // Assert
            Assert.Equal(13, result); // Assuming daily max tax is 60
        }
    }
}

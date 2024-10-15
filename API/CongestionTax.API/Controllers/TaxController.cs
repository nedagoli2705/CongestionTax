using CongestionTax.API.DTOs;
using CongestionTax.Application.DTOs;
using CongestionTax.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CongestionTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly TaxAppService _taxAppService;

        public TaxController(TaxAppService taxAppService)
        {
            _taxAppService = taxAppService;
        }

        // POST: api/tax/calculate
        [HttpPost("calculate")]
        public IActionResult CalculateTaxPerDay([FromBody] TaxPerDayDto taxRequest)
        {
            var taxAmount = _taxAppService.CalculateTaxPerDay(taxRequest.VehicleId, taxRequest.Date);

            if (taxAmount == null)
            {
                return BadRequest("Invalid vehicle type or data.");
            }

            return Ok(new TaxResponseDto { Amount = taxAmount.Value });
        }

        [HttpPost("periodcalculate")]
        public IActionResult CalculateTaxInPeriod([FromBody] TaxInPeriod taxRequest)
        {
            var taxAmount = _taxAppService.CalculateTaxInPeriod(taxRequest.VehicleId, taxRequest.StartDate, taxRequest.EndDate);

            if (taxAmount == null)
            {
                return BadRequest("Invalid vehicle type or data.");
            }

            return Ok(new TaxResponseDto { Amount = taxAmount.Value });
        }

        [HttpPost("tollPass")]
        public IActionResult AddTollPass(TollPassDto passReq)
        {
            _taxAppService.AddTollPass(passReq);

            return Ok();
        }
    }
}



    //[Route("api/[controller]")]
    //[ApiController]
    //public class TaxController : ControllerBase
    //{
    //    private readonly TaxAppService _taxAppService;

    //    public TaxController(TaxAppService taxAppService)
    //    {
    //        _taxAppService = taxAppService;
    //    }

    //    // POST: api/tax/calculate
    //    [HttpPost("calculate")]
    //    public IActionResult CalculateTax([FromBody] TaxRequestDto taxRequest)
    //    {
    //        Vehicle vehicle = GetVehicleType(taxRequest.VehicleType);

    //        if (vehicle == null)
    //        {
    //            return BadRequest("Invalid vehicle type.");
    //        }

    //        decimal taxAmount = _taxAppService.GetTaxForVehicle(vehicle, taxRequest.Dates);

    //        return Ok(new { TaxAmount = taxAmount });
    //    }

    //    private Vehicle GetVehicleType(string vehicleType)
    //    {
    //        return vehicleType switch
    //        {
    //            "Car" => new Car(),
    //            "Motorbike" => new Motorbike(),
    //            _ => null
    //        };
    //    }
    //}


using Microsoft.AspNetCore.Mvc;
using TaxTool.Model.DTO;
using TaxTool.Model.Entity;
using TaxTool.Model.Exceptions;
using TaxTool.Service;

namespace TaxTool.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TaxRecordController(ITaxService taxService) : ControllerBase
    {
        [HttpGet("{municipalityName}")]
        public async Task<IActionResult> GetMunicipality(
            String municipalityName, DateTimeOffset date)
        {
            TaxRecordDto result;
            try
            {
                result = await taxService.GetTaxRecordAsync(new TaxRecordDto
                {
                    municipality = municipalityName,
                    from = date,
                    rate = 0.2,
                    to = date
                });
            }
            catch (MissingMunicipalityException ex)
            {
                return BadRequest(new { message = "Municipality does not exist" });
            }
            catch (RangeNotFoundException ex)
            {
                return BadRequest(new { message = "No tax record for given date" });
            }

            return Ok(result);
        }

        [HttpPut("{municipalityName}")]
        public async Task<IActionResult> PutMunicipality(
            String municipalityName,
            DateTimeOffset fromCurrent,
            DateTimeOffset fromUpdated,
            DateTimeOffset toCurrent,
            DateTimeOffset toUpdated,
            double rateCurrent,
            double rateUpdated
        )
        {
            try
            {
                await taxService.UpdateTaxRecordAsync(
                    new TaxRecordDto
                    {
                        municipality = municipalityName,
                        from = fromCurrent,
                        to = toCurrent,
                        rate = rateCurrent
                    },
                    new TaxRecordDto
                    {
                        municipality = municipalityName,
                        from = fromUpdated,
                        to = toUpdated,
                        rate = rateUpdated
                    }
                );
            }
            catch (MissingMunicipalityException e)
            {
                return BadRequest(new { message = "Municipality does not exist" });
            }
            catch (NoTaxRecordFoundException e)
            {
                return BadRequest(new { message = "No tax record found for given parameters" });
            }
            catch (RangeNotSupportedException ex)
            {
                return BadRequest(new { message = "Provide a valid range of dates" });
            }

            return Ok(new { message = "OK" });
        }

        [HttpPost("{municipalityName}")]
        public async Task<ActionResult<Municipality>> PostMunicipality(
            string municipalityName, double rate,
            DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            try
            {
                await taxService.AddNewTaxRecordAsync(new TaxRecordDto
                {
                    rate = rate,
                    municipality = municipalityName,
                    from = fromDate,
                    to = toDate
                });
            }
            catch (MissingMunicipalityException ex)
            {
                return BadRequest(new { message = "Municipality does not exist" });
            }
            catch (RangeNotSupportedException ex)
            {
                return BadRequest(new { message = "Provide a valid range of dates" });
            }

            return Ok(new { message = "OK" });
        }
    }
}
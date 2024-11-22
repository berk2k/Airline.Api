using Airline.Api.Models.DTO;
using Airline.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost("CreateFlight")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightRequest request)
        {
            var result = await _flightService.InsertFlightAsync(request);
            if (result)
            {
                return Ok("Flight created successfully.");
            }
            return BadRequest("Failed to create flight.");
        }

        [HttpGet("destination/{destination}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFlightsByDestination(string destination,int pageNumber,int pageSize = 10)
        {
            try
            {
                
                var flights = await _flightService.GetAllFlightsWithDestination(destination,pageNumber,pageSize);

                
                if (flights == null || !flights.Any())
                {
                    return NotFound(new { message = "No flights found for the given destination." });
                }

                
                return Ok(flights);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while retrieving flights.",
                    error = ex.Message
                });
            }
        }
    }
}

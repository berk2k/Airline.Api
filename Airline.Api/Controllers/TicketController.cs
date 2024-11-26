using Airline.Api.Models.DTO;
using Airline.Api.Services;
using Airline.Api.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airline.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;

        }

        [HttpPost("buy-ticket")]
        public async Task<IActionResult> BuyTicket([FromBody] BuyTicketDTO request)
        {
            var result = await _ticketService.BuyTicket(request.FlightId, request.userId);

            if (result.Contains("successfully"))
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpGet("my-tickets/{userId}")]
        public async Task<IActionResult> GetMyTickets(int userId)
        {
            var tickets = await _ticketService.GetMyTickets(userId);

            if (tickets == null || tickets.Count == 0)
            {
                return NotFound("No tickets found for the user.");
            }

            return Ok(tickets);
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data provided.");
            }

            try
            {
                // Call the CheckIn method from the service
                var result = await _ticketService.CheckInAsync(request.FlightId, request.UserId);

                // If successful, return a success message
                if (result == "Check-in successful.")
                {
                    return Ok(new { message = result });
                }

                // If there's any error or message from the service, return it as BadRequest
                return BadRequest(new { message = result });
            }
            catch (Exception ex)
            {
                // Catch unexpected errors
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}

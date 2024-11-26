using Airline.Api.Models.DTO;

namespace Airline.Api.Services.IServices
{
    public interface ITicketService
    {
        public Task<string> BuyTicket(int flightId, int userId);
        public Task<List<TicketDTO>> GetMyTickets(int userId);

        Task<string> CheckInAsync(int flightId, int userId);
    }
}

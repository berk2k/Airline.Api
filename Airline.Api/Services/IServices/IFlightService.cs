using Airline.Api.Models;
using Airline.Api.Models.DTO;

namespace Airline.Api.Services.IServices
{
    public interface IFlightService
    {
        Task<bool> InsertFlightAsync(CreateFlightRequest request);

        Task<List<FlightReportDTO>> GetAllFlightsWithDestination(string destination, int pageNumber, int pageSize);

        Task<List<QueryFlightDTO>> QueryFlights(DateTime date,string from,string to ,int pageNumber, int pageSize);
    }
}

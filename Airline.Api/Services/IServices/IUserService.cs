using Airline.Api.Models;
using Airline.Api.Models.DTO;

namespace Airline.Api.Services.IServices
{
    public interface IUserService
    {
        public bool IsUserUnique(string userName);
        public LoginResponseDTO Login(LoginReguestDTO loginReguestDTO);

        public User Register(RegisterRequestDTO reqisterationRequestDTO);
    }
}

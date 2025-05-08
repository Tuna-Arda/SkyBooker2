using System.Text;
using System.Text.Json;

namespace BookingService.Services
{
    public interface IHttpFlightService
    {
        Task<bool> ValidateFlightAsync(string flightId, int ticketCount);
        Task<bool> UpdateFlightSeatsAsync(string flightId, int ticketCount);
    }

    public class HttpFlightService : IHttpFlightService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HttpFlightService> _logger;

        public HttpFlightService(HttpClient httpClient, IConfiguration configuration, ILogger<HttpFlightService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> ValidateFlightAsync(string flightId, int ticketCount)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_configuration["Services:FlightService"]}/api/flight/{flightId}");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var flight = JsonSerializer.Deserialize<dynamic>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    
                    if (flight != null && flight.GetProperty("availableSeats").GetInt32() >= ticketCount)
                    {
                        return true;
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating flight");
                return false;
            }
        }

        public async Task<bool> UpdateFlightSeatsAsync(string flightId, int ticketCount)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(new { TicketCount = ticketCount }), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_configuration["Services:FlightService"]}/api/flight/{flightId}/seats", content);
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating flight seats");
                return false;
            }
        }
    }
}

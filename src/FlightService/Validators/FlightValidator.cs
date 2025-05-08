using FlightService.Models;
using FluentValidation;

namespace FlightService.Validators
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(f => f.FlightId).NotEmpty().WithMessage("Flight ID is required");
            RuleFor(f => f.AirlineName).NotEmpty().WithMessage("Airline name is required");
            RuleFor(f => f.Source).NotEmpty().WithMessage("Source is required");
            RuleFor(f => f.Destination).NotEmpty().WithMessage("Destination is required");
            RuleFor(f => f.DepartureTime).NotEmpty().WithMessage("Departure time is required");
            RuleFor(f => f.ArrivalTime).NotEmpty().WithMessage("Arrival time is required");
            RuleFor(f => f.ArrivalTime).GreaterThan(f => f.DepartureTime)
                .WithMessage("Arrival time must be after departure time");
            RuleFor(f => f.AvailableSeats).GreaterThan(0).WithMessage("Available seats must be greater than 0");
        }
    }
}

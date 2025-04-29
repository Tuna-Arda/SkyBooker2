using FluentValidation;

namespace FlightService.Application
{
    public class FlightValidator : AbstractValidator<FlightDto>
    {
        public FlightValidator()
        {
            RuleFor(x => x.FlightId).NotEmpty();
            RuleFor(x => x.AirlineName).NotEmpty();
            RuleFor(x => x.Source).NotEmpty();
            RuleFor(x => x.Destination).NotEmpty();
            RuleFor(x => x.DepartureTime).LessThan(x => x.ArrivalTime);
            RuleFor(x => x.AvailableSeats).GreaterThanOrEqualTo(0);
        }
    }
}

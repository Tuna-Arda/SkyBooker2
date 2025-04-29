using FluentValidation;

namespace BookingService.Application
{
    public class BookingValidator : AbstractValidator<BookingDto>
    {
        public BookingValidator()
        {
            RuleFor(x => x.FlightId).NotEmpty();
            RuleFor(x => x.TicketCount).GreaterThan(0);
        }
    }
}

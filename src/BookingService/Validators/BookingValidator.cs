using BookingService.Models;
using FluentValidation;

namespace BookingService.Validators
{
    public class BookingValidator : AbstractValidator<Booking>
    {
        public BookingValidator()
        {
            RuleFor(b => b.FlightId)
                .NotEmpty().WithMessage("Flight ID is required");

            RuleFor(b => b.PassengerId)
                .NotEmpty().WithMessage("Passenger ID is required");

            RuleFor(b => b.PassengerFirstname)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("First name must not exceed 100 characters");

            RuleFor(b => b.PassengerLastname)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Last name must not exceed 100 characters");

            RuleFor(b => b.TicketCount)
                .GreaterThan(0).WithMessage("Ticket count must be greater than 0");
        }
    }
}

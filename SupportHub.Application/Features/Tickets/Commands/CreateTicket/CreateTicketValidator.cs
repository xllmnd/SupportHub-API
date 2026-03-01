using FluentValidation;

namespace SupportHub.Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketValidator()
    {
        RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Enter valid a customer Id.");
        RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(150);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(10);
        RuleFor(x => x.Priority).IsInEnum(); 
        RuleFor(x => x.Category).IsInEnum();
    }
}
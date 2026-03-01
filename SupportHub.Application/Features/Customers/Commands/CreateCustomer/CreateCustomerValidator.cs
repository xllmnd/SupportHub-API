using FluentValidation;

namespace SupportHub.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Customer name cannot be empty.")
            .MaximumLength(100).WithMessage("Customer name cannot be more than 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Enter a valid email adress.");
    }
}
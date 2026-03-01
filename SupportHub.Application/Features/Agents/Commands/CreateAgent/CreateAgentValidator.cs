using FluentValidation;

namespace SupportHub.Application.Features.Agents.Commands.CreateAgent;

public class CreateAgentValidator : AbstractValidator<CreateAgentCommand>
{
    public CreateAgentValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Agent name cannot be empty.")
            .MinimumLength(3).WithMessage("Agent name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("Agent name cannot be more than 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email field is required.")
            .EmailAddress().WithMessage("Please enter a valid email address.");
    }
}
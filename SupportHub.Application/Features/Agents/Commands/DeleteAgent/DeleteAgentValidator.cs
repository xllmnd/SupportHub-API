using FluentValidation;
using SupportHub.Application.Features.Agents.Commands.DeleteAgent;

public class DeleteAgentValidator : AbstractValidator<DeleteAgentCommand>
{
    public DeleteAgentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Deleted agent id cannot be empty.")
            .GreaterThan(0).WithMessage("Enter a valid agent id");
    }
}
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusValidator : AbstractValidator<UpdateTicketStatusCommand>
    {
        public UpdateTicketStatusValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.NewStatus).IsInEnum().WithMessage("Invalid ticket status.");
        }
    }
}

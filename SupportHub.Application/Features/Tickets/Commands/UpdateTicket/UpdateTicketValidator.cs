using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MinimumLength(5).MaximumLength(150);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10);
        }
    }
}

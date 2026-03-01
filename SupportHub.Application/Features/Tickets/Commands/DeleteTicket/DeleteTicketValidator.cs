using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketValidator : AbstractValidator<DeleteTicketCommand>
    {
        public DeleteTicketValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("The ID of ticket to be deleted is invalid.");
        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.TicketId).GreaterThan(0);
            RuleFor(x => x.Body).NotEmpty().MinimumLength(1).MaximumLength(2000);

            RuleFor(x => x).Must(x => (x.AgentId.HasValue && !x.CustomerId.HasValue) ||
                                      (!x.AgentId.HasValue && x.CustomerId.HasValue))
                .WithMessage("A person that comment must be an agent or a customer.");
        }
    }
}

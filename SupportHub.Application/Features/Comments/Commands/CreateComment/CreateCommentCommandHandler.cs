using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportHub.Application.Features.Comments.Commands.CreateComment
{
    public record CreateCommentCommand(int TicketId, string Body, int? AgentId, int? CustomerId) : IRequest<int>;

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
    {
        private readonly IRepository<Comment> _commentRepo;

        public CreateCommentCommandHandler(IRepository<Comment> commentRepo) => _commentRepo = commentRepo;

        public async Task<int> Handle(CreateCommentCommand request, CancellationToken ct)
        {
            var comment = new Comment
            {
                TicketId = request.TicketId,
                Body = request.Body,
                AgentId = request.AgentId,
                CustomerId = request.CustomerId
            };
            await _commentRepo.AddAsync(comment);
            return comment.Id;
        }
    }
}

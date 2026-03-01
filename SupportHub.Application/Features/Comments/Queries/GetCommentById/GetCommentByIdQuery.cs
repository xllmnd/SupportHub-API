using AutoMapper;
using MediatR;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Application.Models;
using SupportHub.Domain.Entities;

public record GetCommentByIdQuery(int Id) : IRequest<CommentDto>;

public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
    private readonly IRepository<Comment> _repo;
    private readonly IMapper _mapper;

    public GetCommentByIdQueryHandler(IRepository<Comment> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken ct)
    {
        var comment = await _repo.GetEntityWithSpec(c => c.Id == request.Id, c => c.Agent, c => c.Customer);

        if (comment == null) throw new KeyNotFoundException("Yorum bulunamadı.");

        return _mapper.Map<CommentDto>(comment);
    }
}
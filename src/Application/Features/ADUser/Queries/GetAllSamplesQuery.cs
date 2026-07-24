using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.ADUser.Queries;

public record GetAllSamplesQuery : IRequest<IReadOnlyList<Domain.Entities.ADUser>>;

public class GetAllSamplesHandler(IRepository<Domain.Entities.ADUser> repository)
    : IRequestHandler<GetAllSamplesQuery, IReadOnlyList<Domain.Entities.ADUser>>
{
    public async Task<IReadOnlyList<Domain.Entities.ADUser>> Handle(
        GetAllSamplesQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(cancellationToken);
    }
}
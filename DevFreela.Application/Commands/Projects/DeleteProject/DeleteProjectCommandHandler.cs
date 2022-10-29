using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, Unit>
    {
        private readonly IProjectRepository _repository;

        public DeleteProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}

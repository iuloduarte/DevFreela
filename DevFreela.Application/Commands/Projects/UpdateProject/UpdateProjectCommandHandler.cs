using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Unit>
    {
        private readonly IProjectRepository _repository;

        public UpdateProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetProjectByIdAsync(request.Id);

            project.Update(request.Title, request.Description, request.TotalCost);

            await _repository.SaveChangesAsync();

            return Unit.Value;
        }

    }
}

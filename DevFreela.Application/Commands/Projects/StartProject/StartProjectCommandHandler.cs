using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.Projects
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand>
    {
        private readonly IProjectRepository _repository;

        public StartProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(StartProjectCommand command, CancellationToken cancellationToken)
        {
            var project = await _repository.GetProjectByIdAsync(command.Id);
            project.Start();

            await _repository.StartAsync(project);

            return Unit.Value;
        }
    }
}

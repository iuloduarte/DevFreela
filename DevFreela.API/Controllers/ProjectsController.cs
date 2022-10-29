using DevFreela.API.Models;
using DevFreela.Application.Commands.Projects;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [Authorize]
    public class ProjectsController : DevFreelaControllerBase
    {
        private readonly IMediator _mediator;
        private readonly OpeningTimeOption _option;

        public ProjectsController(IMediator mediator, IOptions<OpeningTimeOption> option)
        {
            _mediator = mediator;
            _option = option.Value;
        }

        // api/projects?query=asp.net core
        [HttpGet]
        //[Authorize(Roles = "freelancer, client")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(GetAllProjectsQuery getAllProjectsQuery)
        {
            var projects = await _mediator.Send(getAllProjectsQuery);

            return Ok(projects);
        }

        // api/projects/3 GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(query);

            if (project == null)
                return NotFound();

            return Ok(project);
        }

        // api/projects POST
        [HttpPost]
        //[Authorize(Roles = "client")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            if (command.Title.Length > 50)
            {
                return BadRequest("Título deve ter tamanho máximo de 50 caracteres");
            }

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command); // 201
        }

        // api/projects/3 PUT
        [HttpPut("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProjectCommand command)
        {
            if (command.Description.Length > 200)
                return BadRequest();

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpDelete("{id}")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        [Authorize(Roles = "freelancer, client")]
        public async Task<IActionResult> Comments([FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent(); // 204
        }

        // api/projects/3/start PUT
        [HttpPut("{id}/start")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // api/projects/3/finish PUT
        [HttpPut("{id}/finish")]
        [Authorize(Roles = "client")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand finishProjectCommand)
        {
            finishProjectCommand.Id = id;

            var result = await _mediator.Send(finishProjectCommand);

            if (!result)
                return BadRequest("O pagamento não pôde ser processado");


            return Accepted();
        }
    }
}

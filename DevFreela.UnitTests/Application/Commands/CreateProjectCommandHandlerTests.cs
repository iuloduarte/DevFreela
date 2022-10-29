using DevFreela.Application.Commands.Projects;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {
        [Fact]
        public async Task InputDataIsOk_Executed_ReturnProjectId()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var projectsRepositoryMock = new Mock<IProjectRepository>();
            var skillRepositoryMock = new Mock<ISkillRepository>();

            unitOfWorkMock.SetupGet(q => q.Projects).Returns(projectsRepositoryMock.Object);
            unitOfWorkMock.SetupGet(q => q.Skills).Returns(skillRepositoryMock.Object);

            var createProjectCommmand = new CreateProjectCommand()
            {
                Title = "Meu projeto",
                Description = "Minha descrição",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 10000
            };

            var createProjectCommandHandler = new CreateProjectCommandHandler(unitOfWorkMock.Object);

            // Act
            var id = await createProjectCommandHandler.Handle(createProjectCommmand, CancellationToken.None);

            // Assert
            Assert.True(id >= 0);
            projectsRepositoryMock.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}

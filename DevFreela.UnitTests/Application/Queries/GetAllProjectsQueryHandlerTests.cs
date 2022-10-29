using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Core.Entities;
using DevFreela.Core.Persistence.Models;
using DevFreela.Core.Repositories;
using Moq;

namespace DevFreela.UnitTests.Application.Queries
{
    public class GetAllProjectsQueryHandlerTests
    {
        [Fact]
        public async void ThreeProjectsExists_Executed_ReturnThreeProjectViewModels()
        {
            var projects = new PaginationResult<Project>();

            projects.Data = new List<Project>() {
                new Project("Nome do Teste 1", "Descrição do teste 1", 1 ,2, 10000),
                new Project("Nome do Teste 2", "Descrição do teste 2", 1, 2, 20000),
                new Project("Nome do Teste 3", "Descrição do teste 3", 1, 2, 30000)
            };

            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock.Setup(q => q.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()).Result).Returns(projects);

            var getAllProjectsQuery = new GetAllProjectsQuery();
            var getAllProjectsQueryHandler = new GetAllProjectsQueryHandler(projectRepositoryMock.Object);

            // Act
            var paginationProjectViewModelsList = await getAllProjectsQueryHandler.Handle(getAllProjectsQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(paginationProjectViewModelsList);
            Assert.NotEmpty(paginationProjectViewModelsList.Data);
            Assert.Equal(projects.Data.Count, paginationProjectViewModelsList.Data.Count);

            projectRepositoryMock.Verify(q => q.GetAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
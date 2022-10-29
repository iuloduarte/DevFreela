using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext;

        private readonly string _connectionString;

        public ProjectService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaConnStr");
        }

        #region Moved to command and queries (Reference)

        //public ProjectDetailsViewModel GetById(int id)
        //{
        //    var project = _dbContext.Projects
        //        .Include(q => q.Client)
        //        .Include(q => q.Freelancer)
        //        .SingleOrDefault(p => p.Id == id);

        //    if (project == null) return null;

        //    var projectDetailsViewModel = new ProjectDetailsViewModel(
        //        project.Id,
        //        project.Title,
        //        project.Description,
        //        project.TotalCost,
        //        project.StartedAt,
        //        project.FinishedAt,
        //        project.Client.FullName,
        //        project.Freelancer.FullName);

        //    return projectDetailsViewModel;
        //}

        //public List<ProjectViewModel> GetAll(string query)
        //{
        //    var projects = _dbContext.Projects;

        //    var projectsViewModel = projects
        //        .Select(q => new ProjectViewModel(q.Id, q.Title, q.CreatedAt))
        //        .ToList();

        //    return projectsViewModel;       
        //}

        //public int Create(NewProjectInputModel inputModel)
        //{
        //    var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
        //    _dbContext.Projects.Add(project);
        //    _dbContext.SaveChanges();
        //    return project.Id;
        //}

        //public void CreateComment(CreateCommentInputModel inputModel)
        //{
        //    var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
        //    _dbContext.ProjectComments.Add(comment);
        //    _dbContext.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        //    project.Cancel();
        //    _dbContext.SaveChanges();
        //}   

        //public void Update(UpdateProjectInputModel inputModel)
        //{
        //    var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);
        //    project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        //    _dbContext.SaveChanges();
        //}

        //public void Finish(int id)
        //{
        //    var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        //    project.Cancel();
        //    _dbContext.SaveChanges();
        //}

        //public void Start(int id)
        //{
        //    // Dapper
        //    using (var sqlconnection = new SqlConnection(_connectionString))
        //    {
        //        sqlconnection.Open();
        //        var script = "UPDATE Projects set Status = @status, StartedAt = @startedat WHERE Id = @id";
        //        sqlconnection.Execute(script, new { status = ProjectStatusEnum.InProgress, startedat = DateTime.Now, id = id });
        //    }

        //    #region Entity Framework

        //    //var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);
        //    //project.Start();
        //    //_dbContext.SaveChanges(); 

        //    #endregion
        //}

        #endregion
    }
}

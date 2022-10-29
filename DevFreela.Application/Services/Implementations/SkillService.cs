using Dapper;
using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext;
        private readonly string _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration)
        {
            dbContext = _dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaConnStr");
        }

        public List<SkillViewModel> GetAll(string query)
        {
            // Dapper
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                return sqlConnection.Query<SkillViewModel>(script).ToList();
            }

            var projects = _dbContext.Skills;

            var skillViewModel = projects
                .Select(q => new SkillViewModel(q.Id, q.Description))
                .ToList();

            return skillViewModel;

            #region Entity Framework

            //var projects = _dbContext.Skills;

            //var skillViewModel = projects
            //    .Select(q => new SkillViewModel(q.Id, q.Description))
            //    .ToList();

            //return skillViewModel;

            #endregion
        }
    }
}

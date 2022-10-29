using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DevFreelaDbContext _dbContext;
        public IProjectRepository Projects { get; }
        public IUserRepository Users { get; }
        public ISkillRepository Skills { get; }

        private IDbContextTransaction _transaction;

        public UnitOfWork(
            DevFreelaDbContext dbContext, 
            IProjectRepository projects, 
            IUserRepository user, 
            ISkillRepository skills)
        {
            _dbContext = dbContext;
            Projects = projects;
            Users = user;
            Skills = skills;
        }        

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();

                throw ex;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}

using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Payments
{
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            dbContext = _dbContext;
        }

        public int Create(CreateUserInputModel inputModel)
        {
            var user = new User(inputModel.FullName, inputModel.Email, inputModel.BirthDate/*, inputModel.Username, inputModel.Password*/);
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }   

        public UserViewModel GetUser(int id)
        {
            var user = _dbContext.Users.SingleOrDefault(p => p.Id == id);
            
            var projectDetailsViewModel = new UserViewModel(
                user.FullName,
                user.Email);

            return projectDetailsViewModel;
        }

    }
}

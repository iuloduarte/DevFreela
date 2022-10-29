using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext:DbContext
    {
        public DevFreelaDbContext(DbContextOptions<DevFreelaDbContext> options): base(options)
        {
            #region Populate 

            //Projects = new List<Project>()
            //{
            //    new Project("Meu projeto ASP.NET Core 1","Minha descrição de projeto 1", 1, 1, 10000),
            //    new Project("Meu projeto ASP.NET Core 2","Minha descrição de projeto 2", 1, 1, 20000),
            //    new Project("Meu projeto ASP.NET Core 3","Minha doscrição de projeto 3", 1, 1, 30000)
            //};

            //Users = new List<User>() {
            //    new User("Paulo Souto", "paulo@souto.com.br", new DateTime(1992, 1, 1)),
            //    new User("Maria Tereza", "maria@tereza.com.br", new DateTime(1981, 1, 1)),
            //    new User("Cristina Sé", "cristina@se.com.br", new DateTime(1966, 1, 1))
            //};

            //Skills = new List<Skill>() { 
            //    new Skill(".NET Core"),
            //    new Skill("C#"),
            //    new Skill("JavaScript"),
            //    new Skill("SQL Server")
            //};

            #endregion
        }

        #region DbSets

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<ProjectComment> ProjectComments { get; set; }

        #endregion
    
        #region Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Steps

            // Define primary keys
            // Call HasOne() followed by WithMany() or HasMany() followed by WithOne()
            // Define foreign keys
            // Define OnDelete

            // Info: WithOne() and WithMany() can be used without parameters, in this case there will be no Navigation Property

            #endregion

            #region Reference

            #region Project

            //modelBuilder.Entity<Project>()
            //    .HasKey(q => q.Id);

            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.Freelancer)
            //    .WithMany(f => f.FreelanceProjects)
            //    .HasForeignKey(p => p.IdFreelancer)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Project>()
            //    .HasOne(p => p.Client)
            //    .WithMany(c => c.OwnedProjects)
            //    .HasForeignKey(p => p.IdClient)
            //    .OnDelete(DeleteBehavior.Restrict);            

            #endregion

            #region ProjectComment

            //modelBuilder.Entity<ProjectComment>()
            //    .HasKey(q => q.Id);

            //modelBuilder.Entity<ProjectComment>()
            //    .HasOne(c => c.Project)
            //    .WithMany(p => p.Comments)
            //    .HasForeignKey(c => c.IdProject)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ProjectComment>()
            //    .HasOne(c => c.User)
            //    .WithMany(u => u.Comments)
            //    .HasForeignKey(c => c.IdUser)
            //    .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Skill

            //modelBuilder.Entity<Skill>()
            //    .HasKey(q => q.Id);

            #endregion

            #region User

            //modelBuilder.Entity<User>()
            //    .HasKey(q => q.Id);

            //modelBuilder.Entity<User>()
            //    .HasMany(s => s.Skills)
            //    .WithOne(u => u.User)
            //    .HasForeignKey(u => u.IdSkill)
            //    .OnDelete(DeleteBehavior.Restrict);      

            #endregion

            #region UserSkill

            //modelBuilder.Entity<UserSkill>()
            //    .HasKey(q => q.Id);            

            #endregion

            #endregion

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}

using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserSkillConfigurations: IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder
                .HasKey(q => q.Id);

            #region Optional?

            builder
                .HasOne(u => u.User)
                .WithMany(u => u.Skills)
                .HasForeignKey(u => u.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(u => u.Skill)
                .WithMany(u => u.UserSkill)
                .HasForeignKey(u => u.IdSkill)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

        }
    }
}

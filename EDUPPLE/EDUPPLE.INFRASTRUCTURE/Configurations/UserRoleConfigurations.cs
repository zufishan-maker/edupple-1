using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{   
    public class UserRoleConfigurations : BaseConfiguration<UserRole>
    {
        public UserRoleConfigurations(ModelBuilder modelBuilder) : base(modelBuilder)
        {
            _builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();
            _builder.HasKey(ur => new { ur.UserId, ur.RoleId });           
            _builder.Property(p => p.CreatedOn)
              .HasDefaultValue(DateTimeOffset.UtcNow);
            _builder.Property(p => p.CreatedBy);
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);
            _builder.Property(p => p.UpdatedBy);
            _builder.ToTable("tbl_user_roles", "dbo");

            _builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId)
            .HasConstraintName("fk_UserRoles_user_userid");

            _builder.HasOne(x => x.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("fk_UserRoles_role_roleid");
        }
    }
}

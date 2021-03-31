using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{
    public class UserPermissionConfiguration : BaseConfiguration<UserPermission, int>
    {
        public UserPermissionConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }
        public override void Configure()
        {
            base.Configure();       
            _builder.Property(p => p.CreatedOn)
               .HasDefaultValue(DateTimeOffset.UtcNow);
            _builder.Property(p => p.CreatedBy)
                .HasColumnType("text");
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);
            _builder.Property(p => p.UpdatedBy)
                .HasColumnType("text");
            _builder.ToTable("tbl_user_permission", "dbo");

            _builder.HasOne(x => x.Permission)
                   .WithMany(x => x.UserPermissions)
                   .HasForeignKey(x => x.PermissionId)
                   .HasConstraintName("fk_user_permissions_permission_permissionid");

            _builder.HasOne(x => x.User)
                 .WithMany(x => x.UserPermissions)
                 .HasForeignKey(x => x.UserId)
                 .HasConstraintName("fk_user_permissions_user_userid");
        }
    }
}

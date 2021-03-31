using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{
 
    public class RefreshTokenConfiguration : BaseConfiguration<RefreshToken, long>
    {
        public RefreshTokenConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {

        }

        public override void Configure()
        {
            base.Configure();
            _builder.HasKey(p => p.Id);
            _builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();
            _builder.Property(p => p.CreatedOn)
             .HasDefaultValue(DateTimeOffset.UtcNow);           
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);          
            _builder.ToTable("tbl_refresh_token", "dbo");

            _builder.HasOne(x => x.User)
             .WithMany(x => x.Tokens)
             .HasForeignKey(x => x.UserId)
             .HasConstraintName("fk_tokens_user_userid");
        }
    }
}


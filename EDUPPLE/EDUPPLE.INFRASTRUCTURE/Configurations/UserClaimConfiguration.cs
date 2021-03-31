using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{
    
    public class UserClaimConfiguration : BaseConfiguration<UserClaim>
    {
        public UserClaimConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
        {
            _builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();
            
            _builder.Property(p => p.CreatedOn)
              .HasDefaultValue(DateTimeOffset.UtcNow);
            _builder.Property(p => p.CreatedBy);
            _builder.Property(p => p.UpdatedOn)
                .IsRequired(false);
            _builder.Property(p => p.UpdatedBy);
            _builder.ToTable("tbl_user_claim", "dbo");

           }
    }
}

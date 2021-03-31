using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{
    public class EmailTemplateConfiguration : BaseConfiguration<EmailTemplate, int>
    {
        public EmailTemplateConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
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
            _builder.ToTable("tbl_email_template", "dbo");
        }
    }
}

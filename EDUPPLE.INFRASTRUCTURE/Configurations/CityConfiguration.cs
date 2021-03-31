using EDUPPLE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EDUPPLE.INFRASTRUCTURE.Configurations
{
    public class CityConfiguration: BaseConfiguration<City, int>
    {
        public CityConfiguration(ModelBuilder modelBuilder) : base(modelBuilder)
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
            _builder.ToTable("tbl_city", "dbo");

            _builder.HasOne(x => x.Country)
                    .WithMany(x => x.Cities)
                    .HasForeignKey(x => x.CountryId)
                    .HasConstraintName("fk_cities_country_countryid");


        }
    }
}

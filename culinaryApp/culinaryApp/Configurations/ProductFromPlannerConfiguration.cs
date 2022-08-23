using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class ProductFromPlannerConfiguration : IEntityTypeConfiguration<ProductFromPlanner>
    {
        public void Configure(EntityTypeBuilder<ProductFromPlanner> builder)
        {
            builder.Property(x => x.Unit)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Ingredient)
                .WithMany();

            builder.HasOne(x => x.Planner)
                .WithMany();
        }
    }
}

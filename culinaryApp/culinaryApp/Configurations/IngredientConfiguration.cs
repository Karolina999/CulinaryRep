using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.IngredientCategory)
                .HasColumnType("nvarchar(40)")
                .IsRequired();
        }
        
    }
}

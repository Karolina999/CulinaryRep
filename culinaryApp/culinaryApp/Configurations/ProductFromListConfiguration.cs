using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class ProductFromListConfiguration : IEntityTypeConfiguration<ProductFromList>
    {
        public void Configure(EntityTypeBuilder<ProductFromList> builder)
        {
            builder.Property(x => x.Unit)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Ingredient)
                .WithMany();

            builder.HasOne(x => x.ShoppingList)
                .WithMany();
        }
    }
}

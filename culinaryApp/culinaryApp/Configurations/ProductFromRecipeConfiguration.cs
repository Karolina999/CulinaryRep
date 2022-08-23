﻿using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class ProductFromRecipeConfiguration : IEntityTypeConfiguration<ProductFromRecipe>
    {
        public void Configure(EntityTypeBuilder<ProductFromRecipe> builder)
        {
            builder.Property(x => x.Unit)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Ingredient)
                .WithMany();

            builder.HasOne(x => x.Recipe)
                .WithMany();
        }
    {
    }
}

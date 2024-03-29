﻿using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class ProductFromPlannerConfiguration : IEntityTypeConfiguration<ProductFromPlanner>
    {
        public void Configure(EntityTypeBuilder<ProductFromPlanner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Unit)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.MealType)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Ingredient)
                .WithMany();

            builder.HasOne(x => x.Planner)
                .WithMany();
        }
    }
}

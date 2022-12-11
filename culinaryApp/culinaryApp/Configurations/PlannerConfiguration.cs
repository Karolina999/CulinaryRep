using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class PlannerConfiguration : IEntityTypeConfiguration<Planner>
    {
        public void Configure(EntityTypeBuilder<Planner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Date)
                .HasColumnType("datetime")
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany();
/*
            builder
            .HasMany(u => u.Recipes)
            .WithMany(r => r.Planners)
            .UsingEntity<PlannerRecipe>(
                x =>
                    x.HasOne(y => y.Recipe)
                        .WithMany(y => y.PlannerRecipe)
                        .HasForeignKey(y => y.RecipeId)
                        .OnDelete(DeleteBehavior.NoAction),
                x =>
                    x.HasOne(y => y.Planner)
                        .WithMany(y => y.PlannerRecipes)
                        .HasForeignKey(y => y.PlannerId)
                        .OnDelete(DeleteBehavior.NoAction)
                );*/

            /*
             Products
             PlannerRecipes - PlannerRecipe Conf
             */
        }
    }
}
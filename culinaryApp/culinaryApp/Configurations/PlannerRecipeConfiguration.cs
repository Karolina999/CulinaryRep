using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace culinaryApp.Configurations
{
    public class PlannerRecipeConfiguration : IEntityTypeConfiguration<PlannerRecipe>
    {
        public void Configure(EntityTypeBuilder<PlannerRecipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.MealType)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Recipe)
                .WithMany(x => x.PlannerRecipe)
                .HasForeignKey(x => x.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
;

            builder.HasOne(x => x.Planner)
                .WithMany(x => x.PlannerRecipes)
                .HasForeignKey(x => x.PlannerId)
                .OnDelete(DeleteBehavior.NoAction);
            ;
        }
    }
}

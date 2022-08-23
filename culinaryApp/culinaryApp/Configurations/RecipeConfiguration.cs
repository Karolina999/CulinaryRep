using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace culinaryApp.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Level)
                .HasColumnType("nvarchar(30)")
                .IsRequired();

            builder.Property(x => x.Time)
                .HasConversion(new TimeSpanToStringConverter())
                .IsRequired();

            builder.Property(x => x.People)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.Photo)
               .HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.RecipeType)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasOne(x => x.Owner)
                .WithMany();


            /*builder.HasMany(x => x.Steps)
                .WithOne(x => x.Recipe)
                .IsRequired();*/
            /*
             * Products
             * WatchedRecipes - watchedrecipes Conf
             * PlannerRecipe
             * UserComments
             */
        }
    }
}
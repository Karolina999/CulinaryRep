using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace culinaryApp.Configurations
{
    public class RecipeConfiguration
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

             
        }
    }
}

/*
        public int People { get; set; }

        public ICollection<ProductFromRecipe>? Products { get; set; }
        public ICollection<Step>? Steps { get; set; }
        public User? Owner { get; set; }
        public ICollection<User>? WatchedByUsers { get; set; }
        public ICollection<Planner>? Planners { get; set; }
        public ICollection<UserComment>? UserComments { get; set; }
*/
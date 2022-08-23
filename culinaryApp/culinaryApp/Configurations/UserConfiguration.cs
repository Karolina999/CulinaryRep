using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.ImageUrl)
                .HasColumnType("nvarchar(MAX)");

            builder
            .HasMany(u => u.Recipes)
            .WithMany(r => r.Watchers)
            .UsingEntity<WatchedRecipes>(
                x =>
                    x.HasOne(y => y.Recipe)
                        .WithMany(y => y.WatchedRecipes)
                        .HasForeignKey(y => y.RecipeId)
                        .OnDelete(DeleteBehavior.NoAction),
                x =>
                    x.HasOne(y => y.User)
                        .WithMany(y => y.WatchedRecipes)
                        .HasForeignKey(y => y.UserId)
                        .OnDelete(DeleteBehavior.NoAction)
                );


            /*builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);*
            /*
             * Planners
             OwnRecipes -> conf in recipeConf
            WatchedRecipes
            ShoppingLists
            Comments
             */
        }
    }
}
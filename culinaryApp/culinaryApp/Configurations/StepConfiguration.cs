using culinaryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace culinaryApp.Configurations
{
    public class StepConfiguration : IEntityTypeConfiguration<Step>
    {
        public void Configure(EntityTypeBuilder<Step> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Id)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Photo)
                .HasColumnType("nvarchar(MAX)");

            builder.HasOne(x => x.Recipe)
                .WithMany()
                .IsRequired();
        }
    }
}

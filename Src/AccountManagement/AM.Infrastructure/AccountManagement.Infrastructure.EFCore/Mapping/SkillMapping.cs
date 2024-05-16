using AccountManagement.Domain.SkillAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class SkillMapping : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Value).HasMaxLength(4).IsRequired();

            builder.HasOne(v => v.Page)
                .WithMany(v => v.Skills)
                .HasForeignKey(v => v.PageId);
        }
    }
}

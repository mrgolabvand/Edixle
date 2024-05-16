using AccountManagement.Domain.ProjectOfferAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class ProjectOfferMapping : IEntityTypeConfiguration<ProjectOffer>
    {
        public void Configure(EntityTypeBuilder<ProjectOffer> builder)
        {
            builder.ToTable("ProjectOffers");
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title).HasMaxLength(200).IsRequired();
            builder.Property(v => v.Price).HasMaxLength(100).IsRequired();
            builder.Property(v => v.Description).HasMaxLength(500);

            builder.HasOne(v => v.PersonalPage).WithMany(v => v.ProjectOffers).HasForeignKey(v => v.EditorPageId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(v => v.Project).WithMany(v => v.ProjectOffers).HasForeignKey(v => v.ProjectId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

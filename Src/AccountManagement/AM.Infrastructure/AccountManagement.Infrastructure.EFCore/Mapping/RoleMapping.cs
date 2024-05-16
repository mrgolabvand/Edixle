using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Name).HasMaxLength(100).IsRequired();

            builder.HasMany(v => v.Accounts).WithOne(v => v.Role).HasForeignKey(v => v.RoleId);

            builder.OwnsMany(v => v.Permissions, builder =>
             {
                 builder.ToTable("RolePermissions");
                 builder.HasKey(v => v.Id);
                 builder.Ignore(v => v.Name);

                 builder.WithOwner(v => v.Role).HasForeignKey(v => v.RoleId);
             });
        }
    }
}

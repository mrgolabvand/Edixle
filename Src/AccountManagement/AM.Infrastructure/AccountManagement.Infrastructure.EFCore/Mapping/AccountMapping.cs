using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.PersonalPageAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.UserName).HasMaxLength(255).IsRequired();
            builder.Property(v => v.Email).HasMaxLength(355).IsRequired();
            builder.Property(v => v.Password).HasMaxLength(155).IsRequired();
            builder.Property(v => v.PhoneNumber).HasMaxLength(20).IsRequired();

            builder.HasOne(v => v.Page)
                .WithOne(v => v.Account)
                .HasForeignKey<PersonalPage>(v => v.AccountId);

            builder.HasOne(v => v.EmployerPage)
                .WithOne(v => v.Account)
                .HasForeignKey<EmployerPage>(v => v.AccountId);
        }
    }
}

using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    internal class VerificationCodeMapping : IEntityTypeConfiguration<VerificationCode>
    {
        public void Configure(EntityTypeBuilder<VerificationCode> builder)
        {
            builder.ToTable("VerificationCodes");
            
            builder.HasKey(x => x.Id);

            builder.Property(v => v.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(v => v.Code).HasMaxLength(10).IsRequired();
        }
    }
}

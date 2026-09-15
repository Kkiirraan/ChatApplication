
using ChatApplication.AuthenticationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatApplication.AuthenticationService.Infrastructure.Persistence.configurations;

public class UserCredentialConfiguration : IEntityTypeConfiguration<UserCredential>
{
    public void Configure(EntityTypeBuilder<UserCredential> builder)
    {
        builder.ToTable("UserCredentials");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Provider).HasConversion<string>().HasMaxLength(50);
        builder.Property(c => c.Identifier).HasMaxLength(256).IsRequired();
        builder.Property(c => c.SecretHash).HasMaxLength(500);

        builder.HasIndex(c => new { c.UserId, c.Provider }).IsUnique();
    }
}

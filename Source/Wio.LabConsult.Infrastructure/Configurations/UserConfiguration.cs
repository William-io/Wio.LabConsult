using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Id).HasMaxLength(36);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(90);
    }
}

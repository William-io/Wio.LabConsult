using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Consults;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class ConsultConfiguration : IEntityTypeConfiguration<Consult>
{
    public void Configure(EntityTypeBuilder<Consult> builder)
    {
        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Consult)
            .HasForeignKey(x => x.ConsultId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Images)
            .WithOne(x => x.Consult)
            .HasForeignKey(x => x.ConsultId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

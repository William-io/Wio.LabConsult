using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class RequestItemConfiguration : IEntityTypeConfiguration<RequestItem>
{
    public void Configure(EntityTypeBuilder<RequestItem> builder)
    {
        builder.Property(req => req.Price).HasColumnType("decimal(10,2)");
    }
}

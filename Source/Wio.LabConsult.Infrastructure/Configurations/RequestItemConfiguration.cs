using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Orders;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class RequestItemConfiguration : IEntityTypeConfiguration<RequestItem>
{
    public void Configure(EntityTypeBuilder<RequestItem> builder)
    {
        builder.Property(req => req.Price).HasColumnType("decimal(10,2)");

        builder.HasOne(ri => ri.Request)
            .WithMany(r => r.RequestItems)
            .HasForeignKey(ri => ri.RequestId);
    }
}

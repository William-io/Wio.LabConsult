using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Requests;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class RequestConfiguration : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.OwnsOne(req => req.RequestConfirmation, req =>
        {
            req.WithOwner();
        });

        builder.HasMany(req => req.RequestItems)
            .WithOne()
            .HasForeignKey(ri => ri.RequestId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(req => req.Status).HasConversion(
            r => r.ToString(),
            r => (RequestStatus)Enum.Parse(typeof(RequestStatus), r));
    }
}

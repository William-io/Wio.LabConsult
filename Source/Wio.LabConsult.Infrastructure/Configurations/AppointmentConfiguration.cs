using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wio.LabConsult.Domain.Appointments;

namespace Wio.LabConsult.Infrastructure.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasMany(x => x.AppointmentItems)
            .WithOne(x => x.Appointment)
            .HasForeignKey(x => x.AppointmentCartId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}

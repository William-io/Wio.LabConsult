using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wio.LabConsult.Domain.Abstractions;
using Wio.LabConsult.Domain.Appointments;
using Wio.LabConsult.Domain.Categories;
using Wio.LabConsult.Domain.Consults;
using Wio.LabConsult.Domain.Orders;
using Wio.LabConsult.Domain.Requests;
using Wio.LabConsult.Domain.Reviews;
using Wio.LabConsult.Domain.Shared;
using Wio.LabConsult.Domain.Users;

namespace Wio.LabConsult.Infrastructure;

public class LabConsultDbContext : IdentityDbContext<User>
{
    public LabConsultDbContext(DbContextOptions<LabConsultDbContext> options) : base(options) { }

    public DbSet<Consult> Consults { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<Image> Images { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Address> Addresses { get; set; }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<AppointmentItem> AppointmentsItems { get; set; }

    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestItem> RequestsItems { get; set; }
    public DbSet<RequestConfirmation> RequestConfirmations { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userName = "system";

        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = userName;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LabConsultDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

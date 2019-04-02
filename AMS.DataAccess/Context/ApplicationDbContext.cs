using AMS.DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMS.DataAccess.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentStatus> ApartmentStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<RentInfo> RentInfos { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }

        public new DbSet<Role> Roles { get; set; }
        public new DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

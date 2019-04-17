using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AMS.Core.Models
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<RentInfo> RentInfos { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<ApartmentStatus> ApartmentStatuses { get; set; }
    }
}

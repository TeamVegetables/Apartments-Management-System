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

        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<RentInfo> RentInfos { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
    }
}

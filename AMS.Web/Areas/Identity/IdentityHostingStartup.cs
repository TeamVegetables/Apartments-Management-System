using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AMS.Web.Areas.Identity.IdentityHostingStartup))]
namespace AMS.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
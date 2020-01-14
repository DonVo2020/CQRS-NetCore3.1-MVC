using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(DonVo.CQRS.NetCore31.WebApp.Areas.Identity.IdentityHostingStartup))]
namespace DonVo.CQRS.NetCore31.WebApp.Areas.Identity
{
	public class IdentityHostingStartup : IHostingStartup
	{
		public void Configure(IWebHostBuilder builder)
		{
			builder.ConfigureServices((context, services) => { });
		}
	}
}
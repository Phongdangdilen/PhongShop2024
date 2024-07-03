
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopping.Models;
using Shopping.Repository;

namespace Shopping
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
            });	

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
                option.Cookie.IsEssential = true;
            });
			builder.Services.AddIdentity<AppUserModel,IdentityRole>()
	             .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
			builder.Services.AddRazorPages();

			builder.Services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;

		
				options.User.RequireUniqueEmail = true;
			});





			var app = builder.Build();

            app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode=0");
            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "category",
                pattern: "/category/{Slug?}",
                defaults: new {controller="Category",action="Index"});

            app.MapControllerRoute(
               name: "brand",
               pattern: "/brand/{Slug?}",
               defaults: new { controller = "Brand", action = "Index" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

           //SEEDING DATA
           var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            SeedData.SeedingData(context);
            app.Run();
        }
    }
}

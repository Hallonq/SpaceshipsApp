using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spaceships.Application.Spaceships;
using Spaceships.Application.Users;
using Spaceships.Infrastructure;
using Spaceships.Infrastructure.Persistance;
using Spaceships.Infrastructure.Persistance.Repositories;
using Spaceships.Infrastructure.Services;

namespace SpaceshipsApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddTransient<IIdentityUserService, IdentityUserService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<ISpaceshipsService, SpaceshipsService>();
            builder.Services.AddScoped<ISpaceshipRepository, SpaceshipRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Database connection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(connectionString));

            // Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(configure => configure.LoginPath = "/login");

            var app = builder.Build();

            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}

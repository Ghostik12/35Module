using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNet.Models;
using SocialNet.Repository;
using SocialNet.Extentions;

namespace SocialNet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connection = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentException("Bd connection");

            builder.Services.AddDbContext <ApplicationDbContext>(options => options.UseSqlServer(connection))
                .AddUnitOfWork()
                .AddCustomRepository<Message, MessageRepository>()
                .AddCustomRepository<Friend, FriendsRepository>()
                .AddIdentity<User, IdentityRole>(opts =>
                {
                    opts.Password.RequiredLength = 5;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireDigit = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();
                

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new Mapper());
            });

            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

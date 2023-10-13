using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.IRepository;
using Restaurant.IRepository.Repository;
using Restaurant.Models;
//using Microsoft.Identity.Web;
//using Microsoft.Identity.Web.Resource;

namespace Restaurant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            // .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            // Add swagger
            builder.Services.AddSwaggerGen();



            // Add Database
            var connectionString = builder.Configuration.GetConnectionString("RestaurantConnection");
            builder.Services.AddDbContext<RestaurantContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });


            // Add Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<RestaurantContext>();



            builder.Services.AddCors(corseOptions =>
            {
                corseOptions.AddPolicy("AppCorse", corsePolicy =>
                {
                    corsePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                 // corsePolicy.WithOrigins("www.google.com", "www.facebook.com").WithMethods("Get");
                });
            });



            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IMealRepository, MealRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            //app.UseStaticFiles();
            //app.UseCors("AppCorse");


            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
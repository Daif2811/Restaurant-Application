using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Hubs;
using Restaurant.IRepository;
using Restaurant.IRepository.Repository;
using Restaurant.Mapper;
using Restaurant.Models;
using System.Configuration;
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

            //builder.Services.AddLogging();

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


            // Add Auto Mapper
            builder.Services.AddAutoMapper(typeof(RestaurantMapper));

            // Add Email SMTP
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            // Add this at the end of ConfigureServices method
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            // Configure SignalR
            builder.Services.AddSignalR();

            // Add Policy 
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

            // Use policy
            app.UseCors("AppCorse");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpLogging();


            app.MapControllers();


            // Use signalR
            app.MapHub<RestaurantHub>("RestaurantHub");

            app.Run();
        }
    }
}
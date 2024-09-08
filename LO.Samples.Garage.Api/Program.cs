
using AutoMapper;
using LO.Samples.Garage.Api.Configurations;
using LO.Samples.Garage.Providers.Context;
using LO.Samples.Garage.Providers.Providers;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.UnitOfWork;
using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using LO.Samples.Garage.Services.Configuration;
using LO.Samples.Garage.Services.Services;
using LO.Samples.Garage.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LO.Samples.Garage.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GarageDbContext>(opt =>
                opt.UseSqlServer("{enter your connection string here}"));


            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(config =>
                config.AddProfiles(new List<Profile> { new ApiMapperProfile(), new ServicesMapperProfile() }));

            AddDependencyInjection(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin());

            app.UseAuthorization();

            app.MapControllers();


            //TODO we should by default not apply database migration because someone might run their code against a live server accidentally
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<GarageDbContext>();
                var migrations = context.Database.GetPendingMigrations();
                if (migrations.Any())
                {
                    context.Database.Migrate();
                }
            }

            app.Run();
        }

        private static void AddDependencyInjection(IServiceCollection services)
        {
            #region misc

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion
            #region Services

            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IVehicleService, VehicleService>();

            #endregion

            #region Providers

            services.AddScoped<IBranchProvider, BranchProvider>();
            services.AddScoped<ICustomerProvider, CustomerProvider>();
            services.AddScoped<IVehicleProvider, VehicleProvider>();

            #endregion
        }
    }
}

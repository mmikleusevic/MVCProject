using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Service.DbContext.Seed
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.VehicleMake.Any())
            {
                context.VehicleMake.AddRange(
                new VehicleMake
                {
                    Name = "Bayerische Motoren Werke GmbH",
                    Abrv = "BMW"
                },
                new VehicleMake
                {
                    Name = "Ferrari",
                    Abrv = "Ferrari"
                },
                new VehicleMake
                {
                    Name = "Aston Martin",
                    Abrv = "AMR",
                });
                context.SaveChanges();
            }
            if (!context.VehicleModel.Any())
            {
                context.VehicleModel.AddRange(
                new VehicleModel
                {
                    Name = "BMW 1 Series",
                    Abrv = "BMW 1",
                    MakeId = 1
                },
                new VehicleModel
                {
                    Name = "Ferrari Maranello",
                    Abrv = "Maranello",
                    MakeId = 2
                },
                new VehicleModel
                {
                    Name = "Aston Martin DB5",
                    Abrv = "DB5",
                    MakeId = 3
                });
                context.SaveChanges();
            }
        }
    }
}

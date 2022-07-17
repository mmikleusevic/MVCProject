namespace Service.DbContext
{
    using Microsoft.EntityFrameworkCore;
    using Service.Models;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
            public DbSet<VehicleMake> VehicleMake => Set<VehicleMake>();
            public DbSet<VehicleModel> VehicleModel => Set<VehicleModel>();
    }
}

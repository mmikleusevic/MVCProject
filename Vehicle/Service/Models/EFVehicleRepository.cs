using Service.DbContext;
using Service.Interfaces;

namespace Service.Models
{
    public class EFVehicleRepository : IVehicleRepository
    {
        private ApplicationDbContext _context;
        public EFVehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<VehicleMake> VehicleMakes => _context.VehicleMake;
        public IQueryable<VehicleModel> VehicleModels => _context.VehicleModel;
    }
}

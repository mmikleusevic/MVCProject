using Service.Models;

namespace Service.Interfaces
{
    public interface IVehicleRepository
    {
        IQueryable<VehicleMake> VehicleMakes { get; }
        IQueryable<VehicleModel> VehicleModels { get; }
    }
}

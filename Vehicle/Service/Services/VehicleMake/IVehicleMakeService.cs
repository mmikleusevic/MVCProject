using Service.Models.Dto;

namespace Service.Services.VehicleMake
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<VehicleMakeDto>> GetVehicleMakes(string sortOrder, string searchString, int? pageNumber, string currentFilter);
        Task<VehicleMakeDto> GetVehicleMake(int? id);
        Task<VehicleMakeDto> AddVehicleMake(VehicleMakeDto model);
        Task<VehicleMakeDto> UpdateVehicleMake(VehicleMakeDto model);
        Task DeleteVehicleMake(int id);
    }
}

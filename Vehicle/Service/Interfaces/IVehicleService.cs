using Service.Models.Dto;

namespace Service.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleMakeDto>> GetVehicleMakes(string sortOrder, string searchString, int? pageNumber, string currentFilter);
        Task<IEnumerable<VehicleModelDto>> GetVehicleModels(string sortOrder, string searchString, int? pageNumber, string currentFilter);

        Task<VehicleMakeDto> GetVehicleMake(int? id);
        Task<VehicleModelDto> GetVehicleModel(int? id);

        Task<VehicleMakeDto> AddVehicleMake(VehicleMakeDto model);
        Task<VehicleModelDto> AddVehicleModel(VehicleModelDto model);

        Task DeleteVehicleMake(int id);
        Task DeleteVehicleModel(int id);

        Task<VehicleModelDto> UpdateVehicleModel(VehicleModelDto model);
        Task<VehicleMakeDto> UpdateVehicleMake(VehicleMakeDto model);

    }
}

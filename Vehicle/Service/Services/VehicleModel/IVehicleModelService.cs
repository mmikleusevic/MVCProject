using Service.Models.Dto;

namespace Service.Services.VehicleModel
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModelDto>> GetVehicleModels(string sortOrder, string searchString, int? pageNumber, string currentFilter);
        Task<VehicleModelDto> GetVehicleModel(int? id);
        Task<VehicleModelDto> AddVehicleModel(VehicleModelDto model);
        Task<VehicleModelDto> UpdateVehicleModel(VehicleModelDto model);
        Task DeleteVehicleModel(int id);

    }
}

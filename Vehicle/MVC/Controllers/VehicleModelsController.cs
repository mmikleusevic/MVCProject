using Microsoft.AspNetCore.Mvc;
using Service.Models.Dto;

namespace Service.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly HttpClient _httpClient;

        public VehicleModelsController(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetVehicleModels(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            string requestEndpoint = $"api/VehicleModels?sortOrder={sortOrder}desc&searchString={searchString}&pageNumber={pageNumber}&currentFilter={currentFilter}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<List<VehicleModelDto>>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetVehicleModel(int? id)
        {
            string requestEndpoint = $"api/VehicleModels/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleModelDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddVehicleModel(VehicleModelDto vehicleModel)
        {
            string requestEndpoint = "api/VehicleModels";

            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(requestEndpoint, vehicleModel);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleModelDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelDto vehicleModel)
        {
            string requestEndpoint = $"api/VehicleModels/{id}";

            HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(requestEndpoint, vehicleModel);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleModelDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            string requestEndpoint = $"api/VehicleModels/{id}";

            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleModelDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }
    }
}

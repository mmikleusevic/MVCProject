using Microsoft.AspNetCore.Mvc;
using Service.Models.Dto;

namespace Service.Controllers
{
    public class VehicleMakesController : Controller
    {
        private readonly HttpClient _httpClient;

        public VehicleMakesController(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> GetVehicleMakes(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            string requestEndpoint = $"api/VehicleMakes?sortOrder={sortOrder}desc&searchString={searchString}&pageNumber={pageNumber}&currentFilter={currentFilter}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<List<VehicleMakeDto>>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return View(result);
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> GetVehicleMake(int? id)
        {
            string requestEndpoint = $"api/VehicleMakes/{id}";

            HttpResponseMessage httpResponse = await _httpClient.GetAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleMakeDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return View(result);
            }
            return RedirectToAction("Index");                
        }
        public async Task<IActionResult> AddVehicleMake(VehicleMakeDto vehicleMake)
        {
            string requestEndpoint = "api/VehicleMakes";

            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(requestEndpoint, vehicleMake);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleMakeDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        public async Task<IActionResult> UpdateVehicleMake(int id,VehicleMakeDto vehicleMake)
        {
            string requestEndpoint = $"api/VehicleMakes/{id}";

            HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(requestEndpoint, vehicleMake);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleMakeDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(result);
        }

        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            string requestEndpoint = $"api/VehicleMakes/{id}";

            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(requestEndpoint);
            var result = await httpResponse.Content.ReadFromJsonAsync<VehicleMakeDto>();
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.IsSuccessStatusCode)
            {               
                return RedirectToAction("Index");
            }
            return View(result);
        }
    }
}

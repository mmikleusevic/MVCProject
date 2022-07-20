using Microsoft.AspNetCore.Mvc;
using Service.Models.Dto;
using Service.Services.VehicleModel;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleModelService _vehicleService;
        private readonly ILogger<VehicleMakesController> _logger;

        public VehicleModelsController(
            IVehicleModelService vehicleService, 
            ILogger<VehicleMakesController> logger)
        {
            _vehicleService = vehicleService;
            _logger = logger;
        }
        // GET: VehicleModels
        [HttpGet]
        public async Task<IActionResult> GetVehicleModels(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            try
            {
                var result = await _vehicleService.GetVehicleModels(sortOrder, searchString, pageNumber, currentFilter);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("Didn't find any Vehicle models");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }


        // GET: VehicleModel/Get/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVehicleModel(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound($"Vehicle model with Id = {id} not found");
                }

                var result = await _vehicleService.GetVehicleModel(id);
                if (result == null)
                {
                    return NotFound("Vehicle model cannot be null");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        // POST: VehicleModel/Create
        [HttpPost]
        public async Task<IActionResult> AddVehicleModel(VehicleModelDto vehicleModel)
        {
            try
            {
                if (vehicleModel == null)
                {
                    return BadRequest();
                }

                var existing = await _vehicleService.GetVehicleModel(vehicleModel.Id);
                if (existing != null)
                {
                    ModelState.AddModelError("Id", "Vehicle model already exists");
                    return BadRequest(ModelState);
                }

                var result = await _vehicleService.AddVehicleModel(vehicleModel);

                return Ok(View(result));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating new vehicle model record");
            }


        }

        // POST: VehicleMakes/Update/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVehicleModel(int id, VehicleModelDto vehicleModel)
        {
            try
            {
                if (id != vehicleModel.Id)
                {
                    return BadRequest("Vehicle model ID mismatch");
                }

                if (vehicleModel == null)
                {
                    return NotFound($"Vehicle model with Id = {id} not found");
                }
                await _vehicleService.UpdateVehicleModel(vehicleModel);

                return Ok($"Vehicle model with Id = {id} updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vehicle model record");
            }
        }

        // POST: VehicleModel/Delete/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            try
            {
                var getVehicleMake = await _vehicleService.GetVehicleModel(id);

                if (getVehicleMake == null)
                {
                    return NotFound($"Vehicle model with Id = {id} not found");
                }

                await _vehicleService.DeleteVehicleModel(id);

                return Ok($"Vehicle model with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vehicle model record");
            }
        }
    }
}

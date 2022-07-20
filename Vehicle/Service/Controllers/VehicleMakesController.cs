using Microsoft.AspNetCore.Mvc;
using Service.DbContext;
using Service.Models.Dto;
using Service.Services.VehicleMake;

namespace Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehicleMakeService _vehicleService;
        private readonly ILogger<VehicleMakesController> _logger;

        public VehicleMakesController(
            ApplicationDbContext context, 
            IVehicleMakeService vehicleService, 
            ILogger<VehicleMakesController> logger)
        {
            _context = context;
            _vehicleService = vehicleService;
            _logger = logger;
        }

        // GET: VehicleModels
        [HttpGet]
        public async Task<IActionResult> GetVehicleMakes(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            try
            {
                var result = await _vehicleService.GetVehicleMakes(sortOrder, searchString, pageNumber, currentFilter);
                if (result.Any())
                {
                    return Ok(result);
                }
                return NotFound("Didn't find any Vehicle makes");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data from the database");
            }           
        }


        // GET: VehicleModel/get/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetVehicleMake(int? id)
        {
            try
            {
                if (id == null || _context.VehicleMake == null)
                {
                    return NotFound($"Vehicle make with Id = {id} not found");
                }

                var result = await _vehicleService.GetVehicleMake(id);
                if (result == null)
                {
                    return NotFound("Vehicle make cannot be null");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,"Error retrieving data from the database");
            }           
        }

        // POST: VehicleModel/Create.
        [HttpPost]
        public async Task<IActionResult> AddVehicleMake(VehicleMakeDto vehicleModel)
        {
            try
            {
                if(vehicleModel == null)
                {
                    return BadRequest();
                }

                var existing = await _vehicleService.GetVehicleMake(vehicleModel.Id);
                if(existing != null)
                {
                    ModelState.AddModelError("Id", "Vehicle make already exists");
                    return BadRequest(ModelState);
                }

                var result = await _vehicleService.AddVehicleMake(vehicleModel);

                return Ok(View(result));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,"Error creating new vehicle make record");
            }
            
            
        }

        // POST: VehicleMake/Update/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateVehicleMake(int id,VehicleMakeDto vehicleMake)
        {
            try
            {
                if (id != vehicleMake.Id)
                {
                    return BadRequest("Vehicle make ID mismatch");
                }

                if(vehicleMake == null)
                {
                    return NotFound($"Vehicle make with Id = {id} not found");
                }
                await _vehicleService.UpdateVehicleMake(vehicleMake);

                return Ok($"Vehicle make with Id = {id} updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vehicle make record");
            }
        }

        // POST: VehicleMake/Delete/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVehicleMake(int id)
        {
            try
            {
                var getVehicleMake = await _vehicleService.GetVehicleMake(id);

                if (getVehicleMake == null)
                {
                    return NotFound($"Vehicle make with Id = {id} not found");
                }

                await _vehicleService.DeleteVehicleMake(id);

                return Ok($"Vehicle make with Id = {id} deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating vehicle make record");
            }
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.DbContext;
using Service.Models;
using Service.Models.Dto;
using System.Web.Mvc;

namespace Service.Services.VehicleModel
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public VehicleModelService(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleModelDto>> GetVehicleModels(
            string sortOrder,
            string searchString,
            int? pageNumber,
            string currentFilter
            )
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var vehicleMakes = from Vma in _context.VehicleModel select Vma;

            vehicleMakes = Search.SearchVehicleModel(vehicleMakes, searchString).Result;

            vehicleMakes = Sort.SortByModel(vehicleMakes, sortOrder).Result;

            int pageSize = 3;
            var result = await PaginatedList<Models.VehicleModel>.CreateAsync(vehicleMakes.AsNoTracking(), pageNumber ?? 1, pageSize);

            return _mapper.Map<IEnumerable<VehicleModelDto>>(result);
        }

        [HttpGet]
        public async Task<VehicleModelDto> GetVehicleModel(int? id)
        {
            var result = await _context.VehicleModel.Include(a => a.Make).FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<VehicleModelDto>(result);
        }
        [HttpPost]
        public async Task<VehicleModelDto> AddVehicleModel(VehicleModelDto model)
        {
            var mappedModel = _mapper.Map<Models.VehicleModel>(model);

            var savedResult = await _context.VehicleModel.AddAsync(mappedModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleModelDto>(savedResult.Entity);
        }
        [HttpPut]
        public async Task<VehicleModelDto> UpdateVehicleModel(VehicleModelDto model)
        {
            var mappedModel = _mapper.Map<Models.VehicleModel>(model);

            var result = _context.VehicleModel.Update(mappedModel);
            if (result != null)
            {
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<VehicleModelDto>(result.Entity);
        }
        [HttpDelete]
        public async Task DeleteVehicleModel(int id)
        {
            var result = await _context.VehicleModel.FirstOrDefaultAsync(a => a.Id == id);
            if (result != null)
            {
                _context.VehicleModel.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}

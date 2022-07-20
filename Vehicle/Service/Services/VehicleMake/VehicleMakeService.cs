using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.DbContext;
using Service.Models;
using Service.Models.Dto;
using System.Web.Mvc;

namespace Service.Services.VehicleMake
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public VehicleMakeService(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<VehicleMakeDto>> GetVehicleMakes(
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

            var vehicleMakes = from Vma in _context.VehicleMake select Vma;

            vehicleMakes = Search.SearchVehicleMake(vehicleMakes, searchString).Result;

            vehicleMakes = Sort.SortByMake(vehicleMakes, sortOrder).Result;

            int pageSize = 3;
            var result = await PaginatedList<Models.VehicleMake>.CreateAsync(vehicleMakes.AsNoTracking(), pageNumber ?? 1, pageSize);

            return _mapper.Map<IEnumerable<VehicleMakeDto>>(vehicleMakes);
        }

        [HttpGet]
        public async Task<VehicleMakeDto> GetVehicleMake(int? id)
        {
            var result = await _context.VehicleMake.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<VehicleMakeDto>(result);
        }

        [HttpPost]
        public async Task<VehicleMakeDto> AddVehicleMake(VehicleMakeDto model)
        {
            var mappedModel = _mapper.Map<Models.VehicleMake>(model);

            var savedResult = await _context.VehicleMake.AddAsync(mappedModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleMakeDto>(savedResult.Entity);
        }

        [HttpPut]
        public async Task<VehicleMakeDto> UpdateVehicleMake(VehicleMakeDto model)
        {
            var mappedModel = _mapper.Map<Models.VehicleMake>(model);

            var result = _context.VehicleMake.Update(mappedModel);
            if (result != null)
            {
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<VehicleMakeDto>(result.Entity);
        }

        [HttpDelete]
        public async Task DeleteVehicleMake(int id)
        {
            var result = await _context.VehicleMake.FirstOrDefaultAsync(a => a.Id == id);
            if (result != null)
            {
                _context.VehicleMake.Remove(result);
                await _context.SaveChangesAsync();
            }
        }
    }
}

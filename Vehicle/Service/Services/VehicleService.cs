using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DbContext;
using Service.Interfaces;
using Service.Models;
using Service.Models.Dto;

namespace Service.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(
            IMapper mapper,
            ApplicationDbContext context,
            IVehicleRepository vehicleRepository)
        {
            _mapper = mapper;
            _context = context;
            _vehicleRepository = vehicleRepository;
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

            var vehicleMakes = _vehicleRepository.VehicleMakes;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    vehicleMakes = vehicleMakes.OrderBy(x => x.Id);
                    break;
                case "id_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(x => x.Id);
                    break;
                case "name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Name);
                    break;
                case "abrv":
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Abrv);
                    break;
                case "abrv_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            var result = await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes.AsNoTracking(), pageNumber ?? 1, pageSize);

            return _mapper.Map<IEnumerable<VehicleMakeDto>>(result);
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

            var vehicleModel = _vehicleRepository.VehicleModels;

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModel = vehicleModel.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    vehicleModel = vehicleModel.OrderBy(x => x.Id);
                    break;
                case "id_desc":
                    vehicleModel = vehicleModel.OrderByDescending(x => x.Id);
                    break;
                case "name_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Name);
                    break;
                case "abrv":
                    vehicleModel = vehicleModel.OrderBy(s => s.Abrv);
                    break;
                case "abrv_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Abrv);
                    break;
                case "make":
                    vehicleModel = vehicleModel.OrderBy(s => s.Make);
                    break;
                case "make_desc":
                    vehicleModel = vehicleModel.OrderByDescending(s => s.Make);
                    break;
                default:
                    vehicleModel = vehicleModel.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            var result = await PaginatedList<VehicleModel>.CreateAsync(vehicleModel.AsNoTracking(), pageNumber ?? 1, pageSize);

            return _mapper.Map<IEnumerable<VehicleModelDto>>(result);
        }

        [HttpGet]
        public async Task<VehicleMakeDto> GetVehicleMake(int? id)
        {
            var result = await _context.VehicleMake.FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<VehicleMakeDto>(result);
        }

        [HttpGet]
        public async Task<VehicleModelDto> GetVehicleModel(int? id)
        {
            var result = await _context.VehicleModel.Include(a => a.Make).FirstOrDefaultAsync(a => a.Id == id);
            return _mapper.Map<VehicleModelDto>(result);
        }

        [HttpPost]
        public async Task<VehicleMakeDto> AddVehicleMake(VehicleMakeDto model)
        {
            var mappedModel = _mapper.Map<VehicleMake>(model);

            var savedResult = await _context.VehicleMake.AddAsync(mappedModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleMakeDto>(savedResult.Entity);
        }

        [HttpPost]
        public async Task<VehicleModelDto> AddVehicleModel(VehicleModelDto model)
        {
            var mappedModel = _mapper.Map<VehicleModel>(model);

            var savedResult = await _context.VehicleModel.AddAsync(mappedModel);
            await _context.SaveChangesAsync();

            return _mapper.Map<VehicleModelDto>(savedResult.Entity);
        }

        

        [HttpPut]
        public async Task<VehicleModelDto> UpdateVehicleModel(VehicleModelDto model)
        {
            var mappedModel = _mapper.Map<VehicleModel>(model);

            var result = _context.VehicleModel.Update(mappedModel);
            if (result != null)
            {
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<VehicleModelDto>(result);
        }

        [HttpPut]
        public async Task<VehicleMakeDto> UpdateVehicleMake(VehicleMakeDto model)
        {
            var mappedModel = _mapper.Map<VehicleMake>(model);

            var result = _context.VehicleMake.Update(mappedModel);
            if (result != null)
            {
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<VehicleMakeDto>(result);
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

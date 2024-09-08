using Microsoft.AspNetCore.JsonPatch;
using LO.Samples.Garage.Providers.Context;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using Microsoft.EntityFrameworkCore;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Providers.Providers
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly GarageDbContext _context;

        public VehicleProvider(GarageDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleTable?> Get(Guid id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<ResultList<VehicleTable>> Get(VehicleTableQuery dtoQuery)
        {
            var vehicles = await dtoQuery.GetQuery(_context.Vehicles.AsQueryable()).ToListAsync();
            var totalCount = await dtoQuery.GetCountQuery(_context.Vehicles.AsQueryable()).CountAsync();
            return new ResultList<VehicleTable>(dtoQuery, totalCount, vehicles);
        }

        public async Task<VehicleTable> Add(VehicleTable dto)
        {
            dto.CreatedAtUTC = DateTime.UtcNow;
            dto.LastModifiedAtUTC = DateTime.UtcNow;
            var newVehicle = await _context.Vehicles.AddAsync(dto);
            await _context.SaveChangesAsync();
            return newVehicle.Entity;
        }

        public async Task<VehicleTable> Update(VehicleTable vehicle, JsonPatchDocument patchDocument)
        {
            patchDocument.ApplyTo(vehicle);
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }

        public async Task Delete(Guid branchId, Guid vehicleId)
        {
            var vehicle = await _context.Vehicles.FindAsync(vehicleId);
            if (vehicle != null && vehicle.BranchId == branchId)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}

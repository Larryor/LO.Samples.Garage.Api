using Microsoft.AspNetCore.JsonPatch;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Providers.Providers.Interfaces
{
    public interface IVehicleProvider
    {
        Task<VehicleTable?> Get(Guid id);
        Task<ResultList<VehicleTable>> Get(VehicleTableQuery dtoQuery);

        Task<VehicleTable> Add(VehicleTable dto);

        Task<VehicleTable> Update(VehicleTable vehicle, JsonPatchDocument patchDocument);

        Task Delete(Guid branchId, Guid vehicleId);
    }
}

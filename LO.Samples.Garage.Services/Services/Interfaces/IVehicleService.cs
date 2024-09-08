using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<Result<ResultList<Vehicle>>> Get(VehicleQuery query);

        Task<Result<Vehicle>> Add(Vehicle vehicle);

        Task<Result<bool>> Delete(Guid branchId, Guid vehicleId);
    }
}

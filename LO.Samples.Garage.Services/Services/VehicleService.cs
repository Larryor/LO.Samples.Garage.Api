using AutoMapper;
using LO.Samples.Garage.Providers.Providers;
using LO.Samples.Garage.Providers.Providers.Interfaces;
using LO.Samples.Garage.Providers.Queries;
using LO.Samples.Garage.Providers.Tables;
using LO.Samples.Garage.Providers.UnitOfWork.Interfaces;
using LO.Samples.Garage.Services.Services.Interfaces;
using LO.Samples.Garage.Shared.Internal.BusinessErrors;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services
{
    public class VehicleService : ServiceBase, IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IVehicleProvider _vehicleProvider;
        private readonly IBranchProvider _branchProvider;

        public VehicleService(IMapper mapper,
            IVehicleProvider vehicleProvider,
            IUnitOfWork unitOfWork,
            IBranchProvider branchProvider) 
            : base(unitOfWork)
        {
            _mapper = mapper;
            _vehicleProvider = vehicleProvider;
            _branchProvider = branchProvider;
        }

        public async Task<Result<ResultList<Vehicle>>> Get(VehicleQuery query)
        {
            var dtoQuery = _mapper.Map<VehicleQuery, VehicleTableQuery>(query);

            var vehicleDtos = await _vehicleProvider.Get(dtoQuery);
            var vehicles = _mapper.Map<ResultList<Vehicle>>(vehicleDtos);
            return ResultWithoutCommit(vehicles);
        }

        public async Task<Result<Vehicle>> Add(Vehicle vehicle)
        {
            var branch = await _branchProvider.Get(vehicle.BranchId);
            if (branch == null) return await BusinessErrorWithRollBack<Vehicle>(new NotFoundBusinessError<Vehicle>());
            var dto = _mapper.Map<Vehicle, VehicleTable>(vehicle);

            var vehiclesWithDuplicateNames = await _vehicleProvider.Get(new VehicleTableQuery() { Name = vehicle.Name });
            if (vehiclesWithDuplicateNames.Items.Any()) return await BusinessErrorWithRollBack<Vehicle>(new ValidationBusinessError<Vehicle>($"vehicle with the name: {vehicle.Name} already exists"));

            dto = await _vehicleProvider.Add(dto);
            vehicle = _mapper.Map<VehicleTable, Vehicle>(dto);
            return await ResultWithCommit(vehicle);
        }

        public async Task<Result<bool>> Delete(Guid branchId, Guid vehicleId)
        {
            await _vehicleProvider.Delete(branchId, vehicleId);
            return await ResultWithCommit(true);
        }
    }
}

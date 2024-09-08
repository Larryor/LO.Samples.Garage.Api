using AutoMapper;
using LO.Samples.Garage.Services.Services.Interfaces;
using LO.Samples.Garage.Shared.External.Queries;
using LO.Samples.Garage.Shared.External.Requests.POST;
using LO.Samples.Garage.Shared.External.Responses;
using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;
using Microsoft.AspNetCore.Mvc;

namespace LO.Samples.Garage.Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : LOControllerBase
    {
        private readonly IBranchService _branchService;
        private readonly IVehicleService _vehicleService;

        public BranchesController(IMapper mapper,
            IBranchService branchService,
            IVehicleService vehicleService) : base(mapper)
        {
            _branchService = branchService;
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// This will intentionally only return 1 default result as the customer did not request to have multiple branches but if the business is successful they may likely expand and would like to manage multiple branches of their business with the application
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetAllBranches([FromRoute] BranchRequestQuery request)
        {
            var query = Mapper.Map<BranchRequestQuery, BranchQuery>(request);
            var result = await _branchService.Get(query);
            return ResultOkOrFailure<ResultList<Branch>, ResultList<BranchResponse>>(result);
        }

        [HttpGet("{branchId:guid}/Vehicles")]
        public async Task<IActionResult> GetAllVehicles([FromRoute] Guid branchId, [FromRoute] VehicleRequestQuery request)
        {
            var query = Mapper.Map<VehicleRequestQuery, VehicleQuery>(request);
            var result = await _vehicleService.Get(query);
            return ResultOkOrFailure<ResultList<Vehicle>, ResultList<VehicleResponse>>(result);
        }

        /// <summary>
        /// intentionally allow only 1 vehicle add at a time
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{branchId:guid}/Vehicles")]
        public async Task<IActionResult> AddVehicle([FromRoute] Guid branchId, [FromBody] VehiclePostRequest request)
        {
            var vehicle = Mapper.Map<VehiclePostRequest, Vehicle>(request);
            vehicle.BranchId = branchId;
            var result = await _vehicleService.Add(vehicle);
            return ResultCreatedOrFailure<Vehicle, VehicleResponse>(result);
        }

        /// <summary>
        /// Intentionally we will return a successful response if the branch or vehicle does not exist
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        [HttpDelete("{branchId:guid}/Vehicles/{VehicleId:guid}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] Guid branchId, [FromRoute] Guid vehicleId)
        {
            var result = await _vehicleService.Delete(branchId, vehicleId);
            return ResultNoContentOrFailure(result);
        }
    }
}

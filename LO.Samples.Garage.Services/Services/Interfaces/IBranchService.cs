using LO.Samples.Garage.Shared.Internal.Entities;
using LO.Samples.Garage.Shared.Internal.Queries;
using LO.Samples.Garage.Shared.Internal.Results;

namespace LO.Samples.Garage.Services.Services.Interfaces
{
    public interface IBranchService
    {
        Task<Result<ResultList<Branch>>> Get(BranchQuery query);
    }
}
